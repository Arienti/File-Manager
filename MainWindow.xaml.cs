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
        string[] drives = Directory.GetLogicalDrives();
        List<string> directoriesList = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            ShowDrives();
            MyCanvas.Focus();
        }
        private void ShowDrives()
        {
            foreach (var drive in drives)
            {
                MyList.Items.Add(drive);
            }
        }
        private void Drives_MouseClick(object sender, MouseButtonEventArgs e)
        {
            MyDataGrid.Items.Clear();
            string selected = MyList.SelectedItems[0].ToString();
            string[] directories = Directory.GetDirectories(selected);
            string[] files = Directory.GetFiles(selected);
            foreach (string directory in directories)
            {
                if (MyDataGrid.SelectedItem == null)
                {
                    StringFile.Content = selected;
                }
                else
                {
                    StringFile.Content = directory;
                }
                string name = System.IO.Path.GetFileName(directory);
                directoriesList.Add(directory);
                MyDataGrid.Items.Add(name);
            }
            foreach (string file in files)
            {
                string filename = System.IO.Path.GetFileName(file);
                MyDataGrid.Items.Add(filename);
            }
        }
        
        private void Directories_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MyDataGrid.Items.Clear();
        }
    }
}
 