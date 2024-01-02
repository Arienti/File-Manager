using File_Manager.Bussines;
using File_Manager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace File_Manager
{
    /// <summary>
    /// Interaction logic for DialogWindowDirectories.xaml
    /// </summary>
    public partial class DialogWindowDirectories : Window
    {
        public bool AddDirectory = false;
        public bool RenameItem = false;
        public bool AddFile = false;
        public DialogWindowDirectories()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            Directories directory = new Directories();
            DirectoriesBussines directoriesB = new DirectoriesBussines();
            FilesBussines filesB = new FilesBussines();
            Files files = new Files();
            try
            {
                directory.Directoryname = Location.location + "\\" + StringName.Text;
                files.filesname = Location.location + "\\" + StringName.Text;
            }
            catch (Exception exception)
            {
                ErrorMessage.Text = exception.Message;
                return;
            }
            Result result = new Result();
            if (AddDirectory)
            {
                if (StringName.Text == "")
                {
                    result.Error = true;
                    result.Message = "Type name of Directory";
                    ErrorMessage.Text = result.Message;
                }
                else
                {
                    result = directoriesB.CreateDirectory(directory);
                }
            }

            if (AddFile)
            {
                if (StringName.Text == "")
                {
                    result.Error = true;
                    result.Message = "Type name of File";
                    ErrorMessage.Text = result.Message;
                }
                else
                {
                    result = filesB.CreateFile(files);
                }
            }
            
            if ((RenameItem) && (MainWindow.isFolder))
            {
                if (StringName.Text == "")
                {
                    result.Error = true;
                    result.Message = "Type name of Directory";
                    ErrorMessage.Text = result.Message;
                }
                else
                {
                    directory.Directoryname = Location.location + "\\" + MainWindow.selected;
                    string name = Location.location + "\\" + StringName.Text;
                    result = directoriesB.RenameDirectories(directory, name);
                }
            }
            if ((RenameItem) && (!MainWindow.isFolder))
            {
                if (StringName.Text == "")
                {
                    result.Error = true;
                    result.Message = "Type name of File";
                    ErrorMessage.Text = result.Message;
                }
                else
                {
                    files.filesname = Location.location + "\\" + MainWindow.selected;
                    string Filename = Location.location + "\\" + StringName.Text + ".txt";
                    result = filesB.RenameFile(files, Filename);
                }
            }
            if (result.Error)
            {
                ErrorMessage.Text = result.Message;
            }
            else
            {
                this.DialogResult = true;
                Close();
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }
    }
}
