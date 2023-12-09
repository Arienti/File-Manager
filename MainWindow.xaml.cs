using File_Manager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace File_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string currentSelected = "";
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
                    MyList.Items.Add(drive);
                }
            }
        }
        public void LoadFilesAndDirecories()
        {
            MyDataGrid.Items.Clear();
            Directories directories = new Directories();
            DirectoryInfo files;
            Files file = new Files();
            var selected = MyList.SelectedItem.ToString();
            try
            {
                if (selected != null)
                {
                    files = new DirectoryInfo(selected);
                    Directories.directories = files.GetDirectories(); //Get All Directories
                    file.files = files.GetFiles(); //Get All files

                    foreach (var dirs in Directories.directories)
                    {
                        directories.name = dirs.Name;
                        MyDataGrid.Items.Add(directories.name);
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
        }
        private void Directories_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            LoadFoldersAndFiles();


        }

        private void MyList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentSelected = e.AddedItems[0].ToString();
            if (currentSelected != null) 
            {
                StringFile.Content = currentSelected.ToString();
            }
            LoadFilesAndDirecories();
        }
    }
}
 