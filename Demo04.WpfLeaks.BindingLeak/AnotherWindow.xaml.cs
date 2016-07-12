using System;
using System.Globalization;
using System.Windows.Data;

namespace Demo04.WpfLeaks.BindingLeak
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
    }

    public class Zombie
    {
        public Zombie(Kg bones, Kg flesh)
        {
            Bones = bones;
            Flesh = flesh;
        }

        public Kg Flesh { get; set; }

        public Kg Bones { get; set; }
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

    public class KgConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Kg) value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Kg(int.Parse(value.ToString()));
        }
    }
}
