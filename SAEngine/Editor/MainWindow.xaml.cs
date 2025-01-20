using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateProjectGrid_ToggleVisibility(false);
            PathTxtBox.IsReadOnly = true;
        }

        private void CreateProjectBtn_onClick(object sender, RoutedEventArgs e)
        {
            CreateProjectGrid_ToggleVisibility(true);
        }

        private void CreateProjectGrid_ToggleVisibility(bool on)
        {
            CreateProjectGrid.Visibility = on ? Visibility.Visible : Visibility.Hidden;
        }

        private void CreateProjectGrid_CancelBtn_onClick(object sender, RoutedEventArgs e)
        {
            CreateProjectGrid_ToggleVisibility(false);
        }

        private void BrowseBtn_Click(object sender, RoutedEventArgs e)
        {
            string? res = GetFolderFromDialog();
            if (res == null)
            {
                Console.WriteLine("Error: Parent Folder");
                return;
            }
            parentFolder = res;

            PathTxtBox.Text = parentFolder;
        }

        private string? GetFolderFromDialog()
        {
            var folderDialog = new OpenFolderDialog
            {
                // Set options here
            };

            if (folderDialog.ShowDialog() == true)
            {
                return parentFolder = folderDialog.FolderName;
            }

            return null;
        }

        /// <summary>
        /// To create a folder with this name inside the <see cref="parentFolder"/>
        /// </summary>
        string projectName = "";

        string parentFolder = "";

        private void NameTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            if (textBox != null)
            {
                projectName = textBox.Text;
            }
        }

        private void PathTxtBox_TextChanged(object sender, TextChangedEventArgs e) { }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            // Commbine 
            string projectPath = System.IO.Path.Combine(parentFolder, projectName);

            // Create (check?)
            DirectoryInfo directoryInfo = Directory.CreateDirectory(projectPath);

            // All done?
        }
    }
}