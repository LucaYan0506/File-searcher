using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace File_searcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /*                                   Global variables                            */

        //here we save the path of all files shows in the listbox
        List<string> file_list = new List<string>();


        //////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////
        /*                                 Functions/subroutines/struct                            */

        struct DataParameter{
            public string path;
            public string file_name;

        }

        private List<DirectoryInfo> get_all_directories(DirectoryInfo directory)
        {
            List<DirectoryInfo> all_directories = new List<DirectoryInfo>();

            //from directory get all sub directory
            foreach(DirectoryInfo sub_directory in directory.GetDirectories())
            {
                List<DirectoryInfo> folders_in_sub_directory = new List<DirectoryInfo>();

                //try to get all sub directory
                try
                {
                    DirectoryInfo[] subdirectories = directory.GetDirectories();

                    //check if current subdirectory has more subdirectories
                    folders_in_sub_directory = get_all_directories(sub_directory);
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return all_directories;
                }

                //add sub directories
                all_directories.Add(sub_directory);

                //if there are some sub directories inside current sub_directory then add them to all_directories
                if (folders_in_sub_directory.Count > 0)
                    all_directories.AddRange(folders_in_sub_directory);
            }

            return all_directories;
        }

        private void search_files(DirectoryInfo directory,string filename)
        {
            //here it is stored files that contains filename
            List<string> files = new List<string>();

            FileInfo[] all_files = directory.GetFiles();
            foreach (FileInfo file in all_files)
            {
                if (file.Name.Contains(filename))
                {
                    file_list.Add(file.FullName);
                }
            }

        }

        
        //////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////
        /*                                 EVENTS                                             */
        private void open_folder_browserBtn_Click(object sender, EventArgs e)
        {
            //open folder browser. It allows users to select the path of folder that they want to.
            folderBrowserDialog1.ShowDialog();
            directoryCombobox.Text = "";
            //update the path
            directoryCombobox.Text = folderBrowserDialog1.SelectedPath;
        }

        private void file_nameTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                searchBtn.PerformClick();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                //clear listbox
                files_ListBox.Items.Clear();

                //add path and file name to the parameters (so backgroundWorker1 can access these data)
                DataParameter parameters = new DataParameter();
                parameters.path = directoryCombobox.Text;
                parameters.file_name = file_nameTxt.Text;

                //run backgroundWorker1
                backgroundWorker1.RunWorkerAsync(parameters);
            }

            //add the directory path into the combobox as "history" only when this path in not in the "History"
            foreach (string item in directoryCombobox.Items)
                if (item == directoryCombobox.Text)
                    return;

            //lines of code below won't run if directory.text is already in the combobox.items

            if (directoryCombobox.Items.Count > 0)
            {
                //if last item is clear remove it for now 
                string last_item = directoryCombobox.Items[directoryCombobox.Items.Count - 1].ToString();
                if (last_item == "Clear")
                    directoryCombobox.Items.Remove(last_item);

                //add new item
                directoryCombobox.Items.Add(directoryCombobox.Text);
                //add back "clear"
                directoryCombobox.Items.Add(last_item);
            }
            else
            {
                //add new item
                directoryCombobox.Items.Add(directoryCombobox.Text);
            }
          
        }

        private void files_ListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = files_ListBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                System.Diagnostics.Process.Start(file_list[index]);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //save the "history" (items in the combobox in a file) so next time we open the app the history remain
            //it will cancel the previous file is exists
            using (StreamWriter file = File.CreateText("history.txt"))
            {
                for (int i = 0; i< directoryCombobox.Items.Count; i++)
                    file.WriteLine(directoryCombobox.Items[i]);
            }
         }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("history.txt"))
            {
                //read the file and add it to combobox items
                using (StreamReader readtext = new StreamReader("history.txt"))
                {
                    while (!readtext.EndOfStream)
                        directoryCombobox.Items.Add(readtext.ReadLine());

                    if (directoryCombobox.Items.Count > 0 && directoryCombobox.Items[directoryCombobox.Items.Count - 1].ToString() != "Clear")
                        directoryCombobox.Items.Add("Clear");
                }
            }
          
        }

        private void directoryCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected_item = directoryCombobox.GetItemText(directoryCombobox.SelectedItem);

            if (selected_item == "Clear")
            {
                DialogResult result = MessageBox.Show("Are you sure to delete the history?", "Alert", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    directoryCombobox.Items.Clear();
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            searching_progressLbl.Text = string.Format("Searching...{0}%", e.ProgressPercentage);
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //reset progress bar to 0
            backgroundWorker1.ReportProgress(0);

            //try to get the directory and subdirectory from the path given by the user
            string path = ((DataParameter)e.Argument).path;
            DirectoryInfo directory;
            List<DirectoryInfo> all_subDirectories = new List<DirectoryInfo>();
            try
            {
                //get the directory info
                directory = new DirectoryInfo(path);
                //get all sub directories
                all_subDirectories = get_all_directories(directory);

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //reset to the progress to default (0)
            backgroundWorker1.ReportProgress(0);

            //clear file_list
            file_list.Clear();

            //search files from directory (path given by the user)
            search_files(directory, ((DataParameter)e.Argument).file_name);

            //if there isn't any subdirectory set the progress to 100
            if (all_subDirectories.Count == 0)
            {
                backgroundWorker1.ReportProgress(100);
                return;
            }

            //Following code will run only if there are some subdirecotories;

            //Use those variables to show the percentage of searching done
            int directory_completed = 0;
            int progress_percentage = directory_completed / all_subDirectories.Count * 100;

            //search all directories found
            foreach (DirectoryInfo sub in all_subDirectories)
            {
                //search
                search_files(sub, ((DataParameter)e.Argument).file_name);

                //searching for sub done, so directory_completed + 1
                directory_completed++;

                //update the progress
                progress_percentage = directory_completed * 100 / all_subDirectories.Count;
                backgroundWorker1.ReportProgress(progress_percentage);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //searching complete
            searching_progressLbl.Text = "Done";

            foreach (string file in file_list){
                files_ListBox.Items.Add(file.Substring(file.LastIndexOf("\\") + 1));
            }

        }
    }
}
