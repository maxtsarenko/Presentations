using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Demo06.WpfLeaks.XNameLeak.Annotations;

namespace Demo06.WpfLeaks.XNameLeak
{
    /// <summary>
    /// Interaction logic for AnotherWindow.xaml
    /// </summary>
    public partial class AnotherWindow
    {
        public AnotherWindow()
        {
            InitializeComponent();

            DataContext = new Zombie(new Kg(28), new Kg(70));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ((Grid) uiBonesInfo.Parent).Children.Remove(uiBonesInfo);
            uiBonesInfo = null;

//          SOLUTION:
//            UnregisterName(nameof(uiBonesInfo));
        }
    }

    public class Zombie : INotifyPropertyChanged
    {
        public Zombie(Kg bones, Kg flesh)
        {
            Bones = bones;
            Flesh = flesh;
        }

        public Kg Flesh { get; set; }

        public Kg Bones { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Kg
    {
        private readonly int[] _gram;

        public Kg(int kg)
        {
            _gram = new int[kg * 1000];
        }

        public override string ToString()
        {
            return $"{_gram.Length / 1000} kg";
        }
    }

    public class KgConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Kg)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Kg(int.Parse(value.ToString()));
        }
    }
}
