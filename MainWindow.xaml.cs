using File_Manager.Bussines;
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
        Directories directories = new Directories();

        Files files = new Files();

        DrivesBussines driveB = new DrivesBussines();

        DirectoriesBussines directoriesB = new DirectoriesBussines();

        FilesBussines filesB = new FilesBussines();
        static string location = "";
        bool isFolder;
      
        static int dirInt = 0;
        static int fileInt = 0;
        public MainWindow()
        {
            InitializeComponent();
            ShowDrives();
            MyCanvas.Focus();
        }
        private void ShowDrives()
        {
            DrivesBussines drivesBussines = new DrivesBussines();
            Drives drives = new Drives();
            drivesBussines.GetDrives(drives);
            for (int i = 0; i < drivesBussines.drivesList.Count; i++)
            {
                drives.drivername = drives.drives[i].Name;
                MyList.Items.Add(drives.drivername);
            }
        }
        public void LoadFoldersAndFiles()
        {
            string actuallocation = "";
            if (MyDataGrid.SelectedItem != null)
            {
                actuallocation = Location.location + "\\" + (string)MyDataGrid.SelectedItem;
            }
            else
            {
                actuallocation = Location.location;
            }
            try
            {
                if (isFolder)
                {
                    Location.location = actuallocation;
                    driveB.GetDirectories(directories);
                    driveB.GetFiles(files);
                    MyDataGrid.Items.Clear();
                    for (int i = 0; i < directories.directories?.Length; i++)
                    {
                        directories.Directoryname = directories.directories[i].Name;
                        string name = System.IO.Path.GetFileName(directories.Directoryname);
                        MyDataGrid.Items.Add(name);
                    }
                    for (int i = 0; i < files.files?.Length; i++)
                    {
                        files.filesname = files.files[i].Name;
                        string name = System.IO.Path.GetFileName(files.filesname);
                        MyDataGrid.Items.Add(name);
                    }
                    StringFile.Text = Location.location;
                }
                else
                {
                    ProcessStartInfo psi = new ProcessStartInfo(Location.location + "\\" + MyDataGrid.SelectedItem);
                    psi.Verb = "open";
                    psi.UseShellExecute = true;
                    Process.Start(psi);
                }
            }
            catch (UnauthorizedAccessException)
            {

            }
        }
        private void RenameItems()
        {

        }
            private void Directories_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            LoadFoldersAndFiles();
        }

        private void MyList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Drives drives = new Drives();
                  
            if (MyList.SelectedItem == null)
            {
                return;
            }
            MyList.SelectedItem = e.AddedItems[0];

            if (MyList.SelectedItem != null)
            {
                drives.drivername = (string)MyList.SelectedItem;
                Location.location = drives.drivername;
                driveB.GetDirectories(directories);
                driveB.GetFiles(files);
                MyDataGrid.Items.Clear();
                for (int i = 0; i < directories.directories?.Length; i++)
                {
                    directories.Directoryname = directories.directories[i].Name;
                    string name = System.IO.Path.GetFileName(directories.Directoryname);
                    MyDataGrid.Items.Add(name);
                }
                for (int i = 0; i < files.files?.Length; i++)
                {
                    files.filesname = files.files[i].Name;
                    string name = System.IO.Path.GetFileName(files.filesname);
                    MyDataGrid.Items.Add(name);
                }
                StringFile.Text = Location.location;
            }
        }
        private void MyDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MyDataGrid.SelectedItem == null)
            {
                return;
            }
            foreach (var d in MyDataGrid.SelectedItems)
            {
                if (d != null)
                {
                    string file = Location.location +"\\" + d;
                    
                    FileAttributes fileAttributes = System.IO.File.GetAttributes(file);
                    try
                    {
                        if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory)
                        {
                            isFolder = true;
                        }
                        else
                        {
                            isFolder = false;
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            string actuallocation = "";
            if (MyDataGrid.SelectedItem != null)
            {
                actuallocation = Location.location + "\\" + (string)MyDataGrid.SelectedItem;
            }
            else
            {
                actuallocation = Location.location;
            }
            try
            {
                if (isFolder)
                {
                    Location.location = actuallocation;
                    driveB.GetDirectories(directories);
                    driveB.GetFiles(files);
                    MyDataGrid.Items.Clear();
                    for (int i = 0; i < directories.directories?.Length; i++)
                    {
                        directories.Directoryname = directories.directories[i].Name;
                        string name = System.IO.Path.GetFileName(directories.Directoryname);
                        MyDataGrid.Items.Add(name);
                    }
                    for (int i = 0; i < files.files?.Length; i++)
                    {
                        files.filesname = files.files[i].Name;
                        string name = System.IO.Path.GetFileName(files.filesname);
                        MyDataGrid.Items.Add(name);
                    }
                    StringFile.Text = Location.location;
                }
                else
                {
                    ProcessStartInfo psi = new ProcessStartInfo(Location.location + "\\" + MyDataGrid.SelectedItem);
                    psi.Verb = "open";
                    psi.UseShellExecute = true;
                    Process.Start(psi);
                }
            }
            catch (UnauthorizedAccessException)
            {

            }
        }
        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((MyDataGrid.SelectedItem != null) && (isFolder))
                {
                    directories.Directoryname = Location.location + "\\" + (string)MyDataGrid.SelectedItem;
                    if (Directory.Exists(directories.Directoryname))
                    {
                        directoriesB.RemoveDirectory(directories);
                        MyDataGrid.Items.Remove(MyDataGrid.SelectedItem);
                    }

                    if ((MyDataGrid.SelectedItem != null) && (!isFolder))
                    {
                        files.filesname = Location.location + "\\" + (string)MyDataGrid.SelectedItem;
                        if (System.IO.File.Exists(files.filesname))
                        {
                            filesB.RemoveFiles(files);
                            MyDataGrid.Items.Remove(MyDataGrid.SelectedItem);
                        }
                    }
                }
            }
            catch (FileNotFoundException) 
            {

            }
            catch (DirectoryNotFoundException) { }
        }
        private void NewText_Click(object sender, RoutedEventArgs e)
        {
            Files files = new Files();
            files.filesname = location + "\\" + "New Text Document.txt " + fileInt;
            if (!System.IO.File.Exists(files.filesname))
            {
                var file = System.IO.File.Create(files.filesname);
                var filename = System.IO.Path.GetFileName(file.Name);
                if (fileInt <= 0)
                {
                    files.filesname = location + "\\" + "New Text Document.txt";
                    file = System.IO.File.Create(files.filesname);
                    filename = System.IO.Path.GetFileName(file.Name);
                }
                MyDataGrid.Items.Add(filename);
            }
            if (System.IO.File.Exists(files.filesname))
            {
                fileInt++;
            }
        }
        private void CreateDirectory_Click(object sender, RoutedEventArgs e)
        {
         /*   Directories directories = new Directories();
            directories.Directoryname = location + "\\" + "New Folder";
            if (!Directory.Exists(directories.Directoryname))
            {
                var dir = Directory.CreateDirectory(directories.Directoryname);
                MyDataGrid.Items.Add(dir.Name);
            }
            else
            {
                if (Directory.Exists(directories.Directoryname))
                {
                    dirInt++;
                    var dir = Directory.CreateDirectory(directories.Directoryname + " " + dirInt);
                    MyDataGrid.Items.Add(dir.Name);
                }
            } */
        }
        private void RenameItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MyDataGrid.Items.Clear();
            string path = StringFile.Text;
            Drives drives = new Drives();
            try
            {
                path = path.Substring(0, path.LastIndexOf("\\"));
                foreach (var drive in drives.drives)
                {
                    if ((path == drive.Name) || ( path.Length < drive.Name.Length))
                    {
                        path = drive.Name;
                    }
                }
                StringFile.Text = path;
                Location.location = StringFile.Text;
                var dir = Directory.GetDirectories(path);
                var file = Directory.GetFiles(path);
                foreach (var d in dir)
                {
                    string dirname = System.IO.Path.GetFileName(d);
                    MyDataGrid.Items.Add(dirname);
                }
                foreach (var d in file)
                {
                    string filename = System.IO.Path.GetFileName(d);
                    MyDataGrid.Items.Add(filename);
                }
            }
            catch 
            { 
            } 
        } 
    }
}
 