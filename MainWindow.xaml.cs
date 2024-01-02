using File_Manager.Bussines;
using File_Manager.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        
        public static bool isFolder;
      
        public static string? selected;
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
        public void refreshList()
        {
            Directories directories = new Directories();
            Files files = new Files();
            directories.directories = new DirectoryInfo(Location.location).GetDirectories();
            files.files = new DirectoryInfo(Location.location).GetFiles();
            MyDataGrid.Items.Clear();
            foreach (var d in directories.directories)
            {
                directories.Directoryname = Path.GetFileName(d.Name);
                MyDataGrid.Items.Add(directories.Directoryname);
            }
            foreach (var f in files.files)
            {
                files.filesname = Path.GetFileName(f.Name);
                MyDataGrid.Items.Add(files.filesname);
            }
        }
        public void LoadFoldersAndFiles()
        {
            string actuallocation = "";
            string exceptionlocation = "";
            if(MyDataGrid.SelectedItem ==null)
            {
                return;
            }
            try 
            {
            if (MyDataGrid.SelectedItem != null)
            {
                    actuallocation = Location.location + "\\" + (string)MyDataGrid.SelectedItem;
                    exceptionlocation = Location.location;
            }
            else
            {
                actuallocation = Location.location;
            }
                if (isFolder)
                {
                    Location.location = actuallocation;
                    driveB.GetDirectories(directories);
                    driveB.GetFiles(files);
                    MyDataGrid.Items.Clear();
                    for (int i = 0; i < directories.directories?.Length; i++)
                    {
                        directories.Directoryname = directories.directories[i].Name;
                        string name = Path.GetFileName(directories.Directoryname);
                        MyDataGrid.Items.Add(name);
                    }
                    for (int i = 0; i < files.files?.Length; i++)
                    {
                        files.filesname = files.files[i].Name;
                        string name = Path.GetFileName(files.filesname);
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
                Location.location = exceptionlocation;
            }
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
                    string name = Path.GetFileName(directories.Directoryname);
                    MyDataGrid.Items.Add(name);
                }
                for (int i = 0; i < files.files?.Length; i++)
                {
                    files.filesname = files.files[i].Name;
                    string name = Path.GetFileName(files.filesname);
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
            LoadFoldersAndFiles();
        }
        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((MyDataGrid.SelectedItem != null) && (isFolder))
                {
                    directories.Directoryname = Location.location + "\\" + (string)MyDataGrid.SelectedItem;
                    directoriesB.RemoveDirectory(directories);
                    MyDataGrid.Items.Remove(MyDataGrid.SelectedItem);
                }
                if ((MyDataGrid.SelectedItem != null) && (!isFolder))
                {
                    files.filesname = Location.location + "\\" + (string)MyDataGrid.SelectedItem;
                    filesB.RemoveFiles(files);
                    MyDataGrid.Items.Remove(MyDataGrid.SelectedItem);
                }
            }
            catch (FileNotFoundException)
            {

            }
            catch (DirectoryNotFoundException) 
            {

            } 
        }
       
        private void NewText_Click(object sender, RoutedEventArgs e)
        {
            DialogWindowDirectories dialogWindow = new DialogWindowDirectories();
            dialogWindow.AddFile = true;
            dialogWindow.RenameItem = false;
            dialogWindow.AddDirectory = false;
            dialogWindow.Owner = this;
            dialogWindow.Title = "Create File";
            dialogWindow.Text.Text = "Type name of file";
            if (dialogWindow.ShowDialog() == true)
            {
                if (dialogWindow.DialogResult == true)
                {
                    refreshList();
                }
            }
        }
        private void CreateDirectory_Click(object sender, RoutedEventArgs e)
        {
            DialogWindowDirectories dialogWindow = new DialogWindowDirectories();
            dialogWindow.AddFile = false;
            dialogWindow.RenameItem = false;
            dialogWindow.AddDirectory = true;
            dialogWindow.Title = "Create Directory";
            dialogWindow.Owner = this;
            if (dialogWindow.ShowDialog() == true)
            {
                if (dialogWindow.DialogResult == true)
                {
                    refreshList();
                }
            }
        }
        private void RenameItem_Click(object sender, RoutedEventArgs e)
        {
            DialogWindowDirectories dialogWindow = new DialogWindowDirectories();
            dialogWindow.AddFile = false;
            dialogWindow.AddDirectory = false;
            dialogWindow.RenameItem = true;
            dialogWindow.Owner = this;
            try
            {
                if (isFolder)
                {
                    selected = (string)MyDataGrid.SelectedItem;
                    dialogWindow.Title = "Rename Directory";
                    dialogWindow.Text.Text = "Type name of Directory";
                    dialogWindow.StringName.Text = selected;
                }
                else
                {
                    selected = (string)MyDataGrid.SelectedItem;
                    dialogWindow.Title = "Rename File";
                    dialogWindow.Text.Text = "Type name of File";
                    string fileN = selected;
                    fileN = fileN.Substring(0, fileN.LastIndexOf("."));
                    dialogWindow.StringName.Text = fileN;
                }
                if (dialogWindow.ShowDialog() == true)
                {
                    if (dialogWindow.DialogResult == true)
                    {
                        refreshList();
                    }
                }
            }
            catch(UnauthorizedAccessException)
            {

            }
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
                    string dirname = Path.GetFileName(d);
                    MyDataGrid.Items.Add(dirname);
                }
                foreach (var d in file)
                {
                    string filename = Path.GetFileName(d);
                    MyDataGrid.Items.Add(filename);
                }
            }
            catch 
            { 
            } 
        } 
    }
}
 