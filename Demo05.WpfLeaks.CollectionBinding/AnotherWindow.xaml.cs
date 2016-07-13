using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using Demo05.WpfLeaks.CollectionBinding.Annotations;

namespace Demo05.WpfLeaks.CollectionBinding
{
    /// <summary>
    /// Interaction logic for AnotherWindow.xaml
    /// </summary>
    public partial class AnotherWindow
    {
        public AnotherWindow()
        {
            InitializeComponent();

            var fatZombie = new Zombie(new Kg(50), new Kg(250));

            DataContext = new ZombieContainer
                          {
                                  Zombies = new List<Zombie>
                                            {
                                                    new Zombie(new Kg(28), new Kg(70)),
                                                    new Zombie(new Kg(2), new Kg(60)),
                                                    new Zombie(new Kg(8), new Kg(50)),
                                                    fatZombie,
                                            }
                          };
        }
    }

    public class ZombieContainer : INotifyPropertyChanged
    {
        public List<Zombie> Zombies { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
