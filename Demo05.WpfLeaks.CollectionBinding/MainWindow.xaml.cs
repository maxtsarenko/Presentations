using System.Windows;

namespace Demo05.WpfLeaks.CollectionBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var anotherWindow = new AnotherWindow();
            anotherWindow.ShowDialog();
        }
    }
}
