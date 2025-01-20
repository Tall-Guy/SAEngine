using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
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
            SetNameError(false);
            SetCreateError(false);
        }

        bool nameError;
        bool createError;

        private void CreateProjectBtn_onClick(object sender, RoutedEventArgs e)
        {
            CreateProjectGrid_ToggleVisibility(true);
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
            CheckIfProjectPathExists();
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
        // Commbine 
        string projectPath => System.IO.Path.Combine(parentFolder, projectName);

        private void NameTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            if (textBox != null)
            {
                projectName = textBox.Text;
            }

            bool needsError = !IsValidFolderName(projectName);
            SetNameError(needsError);
            CheckIfProjectPathExists();
        }

        private void CheckIfProjectPathExists()
        {
            bool showProjectError = Directory.Exists(projectPath);
            SetCreateError(showProjectError);
        }

        // 2024 says sorry for multiple functions doing the same thing ^_^
        private void SetCreateError(bool on)
        {
            createError = on;
            CreateErrorTxt.Visibility = on ? Visibility.Visible : Visibility.Hidden;

            ValidateCreateBtn();
        }

        private void SetNameError(bool on)
        {
            nameError = on;
            NameErrorTxt.Visibility = on ? Visibility.Visible : Visibility.Hidden;

            ValidateCreateBtn();
        }

        private void ValidateCreateBtn()
        {
            bool hasErrors = nameError || createError;
            bool hasPath = projectName != "" && parentFolder != "";

            CreateBtn.IsEnabled = !hasErrors && hasPath;
        }

        private void CreateProjectGrid_ToggleVisibility(bool on)
        {
            CreateProjectGrid.Visibility = on ? Visibility.Visible : Visibility.Hidden;
        }

        private bool IsValidFolderName(string projectName)
        {
            char[] invalidFileNameChars = System.IO.Path.GetInvalidFileNameChars();
            char[] invalidPathChars = System.IO.Path.GetInvalidPathChars();
            foreach (char c in projectName)
                if (invalidFileNameChars.Contains(c) || invalidPathChars.Contains(c))
                    return false;

            return true;
        }

        private void PathTxtBox_TextChanged(object sender, TextChangedEventArgs e) { }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            // Create (check?)
            DirectoryInfo directoryInfo = Directory.CreateDirectory(projectPath);

            // TODO: Create any necessary files

            // All done?
            throw new NotImplementedException("TODO: IMPLEMENT TRANSITION TO VIEWPORT!");
        }
    }
}