using System;
using System.Linq;
using System.Windows.Forms;

namespace Demo03.WellKnownLeaks.StaticCollectionGrow
{
    public partial class LeakyForm : Form
    {
        public LeakyForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < uiZombiesToPrepare.Value; i++)
            {
                var zombie = new Zombie(new Kg(30), new Kg(60));

                CollectionHost.Zombies.Add(zombie);
            }

            var counter = new ZombieBonesCounter();

            MessageBox.Show($"{CollectionHost.Zombies.Count} zombies have {counter.Total} of bones");
        }
    }

    internal class ZombieBonesCounter
    {
        public Kg Total
        {
            get { return new Kg((int) CollectionHost.Zombies.Sum(z => z.Bones)); }
        }
    }
}
