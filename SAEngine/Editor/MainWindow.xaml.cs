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
            Create_SetNameError(false);
            Create_SetCreateError(false);
            Load_LoadBtn.IsEnabled = false;

            windows.Add(CreateProjectBtn, gridCreateProject);
            windows.Add(LoadProjectBtn, gridLoadProject);

            ToggleVisibility_AllOff();

            foreach (KeyValuePair<Button, Grid> kVP in windows)
                kVP.Key.Click += OnKeyClick;

            Create_PathTxtBox.IsReadOnly = true;
        }

        private void OnKeyClick(object sender, RoutedEventArgs e)
        {
            // Which button?
            Button button = (Button)sender;

            // Which grid?
            Grid grid = windows[button]; // TODO: FailsafeCheck

            // Visibilities
            ToggleVisibility_AllOff();
            ToggleVisibility(grid, true);
        }

        Dictionary<Button, Grid> windows = new Dictionary<Button, Grid>();

        bool nameError;
        bool createError;


        private void CancelBtn_onClick(object sender, RoutedEventArgs e) => ToggleVisibility_AllOff();

        private void ToggleVisibility_AllOff()
        {
            foreach (Grid g in windows.Values)
                ToggleVisibility(g, false);
        }

        private void ToggleVisibility(Grid grid, bool on)
        {
            grid.Visibility = on ? Visibility.Visible : Visibility.Hidden;
        }

        private void CreateBrowseBtn_Click(object sender, RoutedEventArgs e)
        {
            string? res = GetFolderFromDialog();
            if (res == null)
            {
                Console.WriteLine("Error: Parent Folder");
                return;
            }
            create_ParentFolder = res;

            Create_PathTxtBox.Text = create_ParentFolder;
            Create_CheckIfProjectPathExists();
        }

        private void LoadBrowseBtn_Click(object sender, RoutedEventArgs e)
        {
            string? res = GetFolderFromDialogue_SAE();

            if (res == null)
            {
                Console.WriteLine("Error: No Parent Folder To SAE File");
                return;
            }

            load_ProjectPath = res;
            Load_PathTxtBox.Text = load_ProjectPath;

            Load_LoadBtn.IsEnabled = true;
        }

        private string? GetFolderFromDialog()
        {
            var folderDialog = new OpenFolderDialog
            {
                // Set options here
            };

            if (folderDialog.ShowDialog() == true)
            {
                return create_ParentFolder = folderDialog.FolderName;
            }

            return null;
        }

        private string? GetFolderFromDialogue_SAE()
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "SAE Project | *.sae"  

                // Set options here
            };

            if (fileDialog.ShowDialog() == true)
            {
                string pathToFile = fileDialog.FileName;
                DirectoryInfo? parentFolderInfo = Directory.GetParent(pathToFile);
                if (parentFolderInfo == null)
                    throw new Exception("WTF");

                return parentFolderInfo.FullName;
            }

            return null;
        }

        /// <summary>
        /// To create a folder with this name inside the <see cref="create_ParentFolder"/>
        /// </summary>
        string projectName = "";
        string create_ParentFolder = "";
        string load_ProjectPath = "";
        // Commbine 
        string create_projectPath => System.IO.Path.Combine(create_ParentFolder, projectName);

        private void NameTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            if (textBox != null)
            {
                projectName = textBox.Text;
            }

            bool needsError = !IsValidFolderName(projectName);
            Create_SetNameError(needsError);
            Create_CheckIfProjectPathExists();
        }

        private void Create_CheckIfProjectPathExists()
        {
            bool showProjectError = Directory.Exists(create_projectPath);
            Create_SetCreateError(showProjectError);
        }

        // 2024 says sorry for multiple functions doing the same thing ^_^
        private void Create_SetCreateError(bool on)
        {
            createError = on;
            Create_CreateErrorTxt.Visibility = on ? Visibility.Visible : Visibility.Hidden;
            
            ValidateCreateBtn();
        }

        private void Create_SetNameError(bool on)
        {
            nameError = on;
            Create_NameErrorTxt.Visibility = on ? Visibility.Visible : Visibility.Hidden;

            ValidateCreateBtn();
        }

        private void ValidateCreateBtn()
        {
            bool hasErrors = nameError || createError;
            bool hasPath = projectName != "" && create_ParentFolder != "";

            Create_CreateBtn.IsEnabled = !hasErrors && hasPath;
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
            DirectoryInfo directoryInfo = Directory.CreateDirectory(create_projectPath);

            // TODO: Create any necessary files
            string fullPathToParentFolder = directoryInfo.FullName;
            string fileName = "";
            string fileExtension = "sae";
            string fullFileName = fileName + "." + fileExtension;
            string fullPathToFile = System.IO.Path.Combine(fullPathToParentFolder, fullFileName);

            FileStream fs = File.Create(fullPathToFile);

            // All done?
            TransitionToEditorViewPort(create_projectPath);
        }
        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            TransitionToEditorViewPort(load_ProjectPath);
        }

        private void TransitionToEditorViewPort(string projectFolder)
        {
            Console.WriteLine($"Transitioning To Viewport... for {projectFolder}");
            // HIDE PROJECT CREATE/SELECT
            // LOAD VIEWPORT
            throw new NotImplementedException("TODO: IMPLEMENT TRANSITION TO VIEWPORT!");
        }
    }
}