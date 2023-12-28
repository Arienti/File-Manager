using File_Manager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace File_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string currentSelected = string.Empty;
        string dskselected = string.Empty;
        static string location;
        bool isFolder;
        bool isFile;
        public MainWindow()
        {
            InitializeComponent();
            ShowDrives();
            MyCanvas.Focus();
            

        }
        private void ShowDrives()
        {
            Drives drives = new Drives();
            foreach (var drive in drives.drives) //Get local Drives
            {
                if (drive.IsReady) // check if drive is in use
                {
                    drives.name = drive.Name;
                    MyList.Items.Add(drives.name);
                }
            }
        }
        public void LoadFilesAndDirecories()
        {
            MyDataGrid.Items.Clear();
            Directories directories = new Directories();
            DirectoryInfo files;
            Files file = new Files();
            dskselected = MyList.SelectedItem.ToString();
            try
            {
                if (dskselected != null)
                {
                    location = dskselected;
                    files = new DirectoryInfo(dskselected);
                    var directory = Directory.GetDirectories(dskselected); //Get All Directories
                    file.files = files.GetFiles(); //Get All files

                    foreach (var dirs in directory)
                    {
                        directories.Directoryname = dirs.ToString();
                        string name = System.IO.Path.GetFileName(directories.Directoryname);
                        MyDataGrid.Items.Add(name);
                    }
                    foreach (var fls in file.files)
                    {
                        file.name = fls.Name;
                        MyDataGrid.Items.Add(file.name);
                    }
                }
            }
            catch (Exception e)
            {
                
            }
        }
        public void LoadFoldersAndFiles()
        {
            MyDataGrid.Items.Clear();
            currentSelected = StringFile.Text.ToString();
            Directories directories1 = new Directories();
            Files filesname = new Files();
            try
            {
                if (currentSelected != null)
                {
                    var directories = Directory.GetDirectories(currentSelected); //Get all subdirectories(folders) 
                    var files = Directory.GetFiles(currentSelected); //Get all files inside subdirectories(folders)
                    foreach (var dirs in directories)
                    {
                        directories1.Directoryname = dirs.ToString();
                        string name = System.IO.Path.GetFileName(directories1.Directoryname);
                        MyDataGrid.Items.Add(name);
                    }
                    foreach (var file in files)
                    {
                        filesname.name = file.ToString();
                        string filename = System.IO.Path.GetFileName(filesname.name);
                        MyDataGrid.Items.Add(filename);
                    }
                    location = ($"{currentSelected}");
                }
            }
            catch (Exception e)
            {
                
            }
        }
            private void Directories_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            string files = StringFile.Text;
            FileAttributes fileAttributes = System.IO.File.GetAttributes(files);
            try
            {
                if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    isFolder = true;
                    isFile = false;
                    LoadFoldersAndFiles();
                }
                else
                {
                    isFolder = false;
                    isFile = true;
                    Process.Start(files);
                }
            }
            catch 
            { 
            }
        }

        private void MyList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentSelected = e.AddedItems[0].ToString();
            if (currentSelected != null) 
            {
                StringFile.Text = currentSelected.ToString();
            }
            LoadFilesAndDirecories();
        }
        private void MyDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selcted = e.AddedItems;

            foreach (var d in selcted)
            {
                if (d != null)
                {
                    StringFile.Text = location + "\\" + d.ToString();
                }
            }
            string files = StringFile.Text;
            FileAttributes fileAttributes = System.IO.File.GetAttributes(files);
            try
            {
                if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    isFolder = true;
                    isFile = false;
                }
                else
                {
                    isFolder = false;
                    isFile = true;
                }
            }
            catch
            {

            }
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isFolder)
                {
                    LoadFoldersAndFiles();
                }
                else
                {
                    Process.Start(StringFile.Text);
                }
            }
            catch
            {

            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            var selected = MyDataGrid.SelectedItems[0];
            if ((selected != null) && (isFolder))
            {
                Directory.Delete(StringFile.Text);
                
            }
            else
            {
                System.IO.File.Delete(StringFile.Text);
            }
            StringFile.Text = location;
            MyDataGrid.Items.Remove(selected);
        }

        private void NewText_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateDirectory_Click(object sender, RoutedEventArgs e)
        {
            Directories directories = new Directories();
            directories.Directoryname = location + "\\" + "New Folder";
            if (!Directory.Exists(directories.Directoryname))
            {
                var dir = Directory.CreateDirectory(directories.Directoryname);
                MyDataGrid.Items.Add(dir.Name);
            }
        }
        private void RenameItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
 