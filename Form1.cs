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
        /*                                 Functions/subroutines                            */
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
                    files_ListBox.Items.Add(file.Name);
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

            //try to get the directory and subdirectory from the path given by the user
            string path = directoryCombobox.Text;
            DirectoryInfo directory;
            List<DirectoryInfo> all_directories = new List<DirectoryInfo>();
            //reset progress bar to 0
            progressBar1.Value = 0;

            //show the progress (not accurate, it is used to let user know that the app is not frozen, it is searching all sub directories)
            searching_progressLbl.Text = "Searching folders";
            progressBar1.Value = 50;
            
            //close the next message autotically after 1s
            Press_esc_timer.Enabled = true;
            Press_esc_timer.Start();

            //pop up the message to tell the user that the searching is started
            MessageBox.Show("Started to searching, please wait", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            

            try
            {
                //get the directory info
                directory = new DirectoryInfo(path);
                //get all sub directories
                all_directories = get_all_directories(directory);

                //all sub directories found
                progressBar1.Value = 100;
            }
            catch (Exception error)
            {
                //reset progress bar to 0
                progressBar1.Value = 0;
                MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //reset to the progress to default (0)
            searching_progressLbl.Text = "Searching...0%";
            progressBar1.Value = 0;

            //clear listbox
            files_ListBox.Items.Clear();

            //clear file_list
            file_list.Clear();

            //Use those variables to show the percentage of searching done
            int directory_completed = 0;
            int progress_percentage = directory_completed / all_directories.Count * 100;

            //search files from directory (path given by the user)
            search_files(directory, file_nameTxt.Text);

            //search all directories found
            foreach (DirectoryInfo sub in all_directories)
            {
                //search
                search_files(sub, file_nameTxt.Text);

                //searching for sub done, so directory_completed + 1
                directory_completed++;

                //update the progress
                progress_percentage = directory_completed * 100 / all_directories.Count ;
                searching_progressLbl.Text = "Searching..." + progress_percentage + "%";
                progressBar1.Value = progress_percentage;
            }

            //searching complete
            searching_progressLbl.Text = "Done";

            //add the directory path into the combobox as "history"
            directoryCombobox.Items.Add(directoryCombobox.Text);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendKeys.SendWait("{ESC}");

            Press_esc_timer.Stop();
            Press_esc_timer.Enabled = false;
        }
    }
}
