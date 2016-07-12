using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Demo03.WellKnownLeaks.StaticCollectionGrow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            uiZombiesCount.Text = CollectionHost.Zombies.Count.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var leakyForm = new LeakyForm();
            leakyForm.ShowDialog(this);
        }
    }

    public static class CollectionHost
    {
        public static List<Zombie> Zombies = new List<Zombie>();
    }

    public class Zombie
    {
        internal Zombie(Kg bones, Kg flesh)
        {
            Bones = bones;
            Flesh = flesh;
        }

        internal Kg Flesh { get; }

        internal Kg Bones { get; }
    }

    internal class Kg
    {
        private readonly int[] _gram;

        internal Kg(int kg)
        {
            _gram = new int[kg * 1000];
        }

        public static Kg operator + (Kg left, Kg right)
        {
            return new Kg(left._gram.Length + right._gram.Length);
        }

        public static implicit operator long (Kg kg)
        {
            return kg._gram.LongLength / 1000;
        }

        public override string ToString()
        {
            return $"{_gram.Length / 1000} kg";
        }
    }

}
