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

        }
    }
}