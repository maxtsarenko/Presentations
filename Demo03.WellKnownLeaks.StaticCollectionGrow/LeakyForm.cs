using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Demo03.WellKnownLeaks.StaticCollectionGrow
{
    public partial class LeakyForm : Form
    {
        private readonly List<Zombie> _zombies;

        public LeakyForm(List<Zombie> zombies)
        {
            _zombies = zombies;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < uiZombiesToPrepare.Value; i++)
            {
                var zombie = new Zombie(new Kg(30), new Kg(60));

                _zombies.Add(zombie);
            }

            var counter = new ZombieBonesCounter(_zombies);

            MessageBox.Show($"{_zombies.Count} zombies have {counter.Total} of bones");
        }
    }

    internal class ZombieBonesCounter
    {
        private readonly List<Zombie> _zombies;

        public ZombieBonesCounter(List<Zombie> zombies)
        {
            _zombies = zombies;
        }

        public Kg Total
        {
            get { return new Kg((int) _zombies.Sum(z => z.Bones)); }
        }
    }
}
