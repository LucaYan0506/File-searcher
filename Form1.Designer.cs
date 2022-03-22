
namespace File_searcher
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.directoryLbl = new System.Windows.Forms.Label();
            this.directoryCombobox = new System.Windows.Forms.ComboBox();
            this.open_folder_browserBtn = new System.Windows.Forms.Button();
            this.file_nameLbl = new System.Windows.Forms.Label();
            this.file_nameTxt = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.searchBtn = new System.Windows.Forms.Button();
            this.searching_progressLbl = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.files_ListBox = new System.Windows.Forms.ListBox();
            this.Press_esc_timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // directoryLbl
            // 
            this.directoryLbl.AutoSize = true;
            this.directoryLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.directoryLbl.Location = new System.Drawing.Point(12, 9);
            this.directoryLbl.Name = "directoryLbl";
            this.directoryLbl.Size = new System.Drawing.Size(260, 29);
            this.directoryLbl.TabIndex = 0;
            this.directoryLbl.Text = "Directory/Folder path";
            // 
            // directoryCombobox
            // 
            this.directoryCombobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.directoryCombobox.FormattingEnabled = true;
            this.directoryCombobox.Location = new System.Drawing.Point(12, 41);
            this.directoryCombobox.Name = "directoryCombobox";
            this.directoryCombobox.Size = new System.Drawing.Size(345, 33);
            this.directoryCombobox.TabIndex = 1;
            this.directoryCombobox.SelectedIndexChanged += new System.EventHandler(this.directoryCombobox_SelectedIndexChanged);
            // 
            // open_folder_browserBtn
            // 
            this.open_folder_browserBtn.BackColor = System.Drawing.SystemColors.Control;
            this.open_folder_browserBtn.BackgroundImage = global::File_searcher.Properties.Resources._3_dots;
            this.open_folder_browserBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.open_folder_browserBtn.Location = new System.Drawing.Point(364, 41);
            this.open_folder_browserBtn.Name = "open_folder_browserBtn";
            this.open_folder_browserBtn.Size = new System.Drawing.Size(53, 33);
            this.open_folder_browserBtn.TabIndex = 2;
            this.open_folder_browserBtn.UseVisualStyleBackColor = false;
            this.open_folder_browserBtn.Click += new System.EventHandler(this.open_folder_browserBtn_Click);
            // 
            // file_nameLbl
            // 
            this.file_nameLbl.AutoSize = true;
            this.file_nameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.file_nameLbl.Location = new System.Drawing.Point(7, 80);
            this.file_nameLbl.Name = "file_nameLbl";
            this.file_nameLbl.Size = new System.Drawing.Size(129, 29);
            this.file_nameLbl.TabIndex = 0;
            this.file_nameLbl.Text = "File name";
            // 
            // file_nameTxt
            // 
            this.file_nameTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.file_nameTxt.Location = new System.Drawing.Point(12, 112);
            this.file_nameTxt.Name = "file_nameTxt";
            this.file_nameTxt.Size = new System.Drawing.Size(324, 31);
            this.file_nameTxt.TabIndex = 3;
            this.file_nameTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.file_nameTxt_KeyDown);
            // 
            // searchBtn
            // 
            this.searchBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBtn.Location = new System.Drawing.Point(342, 90);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(75, 53);
            this.searchBtn.TabIndex = 4;
            this.searchBtn.Text = "Search";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // searching_progressLbl
            // 
            this.searching_progressLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searching_progressLbl.ForeColor = System.Drawing.SystemColors.Highlight;
            this.searching_progressLbl.Location = new System.Drawing.Point(12, 159);
            this.searching_progressLbl.Name = "searching_progressLbl";
            this.searching_progressLbl.Size = new System.Drawing.Size(405, 35);
            this.searching_progressLbl.TabIndex = 5;
            this.searching_progressLbl.Text = "Progress";
            this.searching_progressLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 187);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(405, 32);
            this.progressBar1.TabIndex = 6;
            // 
            // files_ListBox
            // 
            this.files_ListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.files_ListBox.FormattingEnabled = true;
            this.files_ListBox.HorizontalScrollbar = true;
            this.files_ListBox.ItemHeight = 20;
            this.files_ListBox.Location = new System.Drawing.Point(17, 234);
            this.files_ListBox.Name = "files_ListBox";
            this.files_ListBox.Size = new System.Drawing.Size(400, 264);
            this.files_ListBox.TabIndex = 7;
            this.files_ListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.files_ListBox_MouseDoubleClick);
            // 
            // Press_esc_timer
            // 
            this.Press_esc_timer.Interval = 1000;
            this.Press_esc_timer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 509);
            this.Controls.Add(this.files_ListBox);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.searching_progressLbl);
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.file_nameTxt);
            this.Controls.Add(this.open_folder_browserBtn);
            this.Controls.Add(this.directoryCombobox);
            this.Controls.Add(this.file_nameLbl);
            this.Controls.Add(this.directoryLbl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label directoryLbl;
        private System.Windows.Forms.ComboBox directoryCombobox;
        private System.Windows.Forms.Button open_folder_browserBtn;
        private System.Windows.Forms.Label file_nameLbl;
        private System.Windows.Forms.TextBox file_nameTxt;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.Label searching_progressLbl;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ListBox files_ListBox;
        private System.Windows.Forms.Timer Press_esc_timer;
    }
}

