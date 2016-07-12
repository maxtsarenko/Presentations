using System;
using System.Linq;
using System.Threading;

namespace Demo02.WellKnownLeaks.StaticEventLeak
{
    internal class Program
    {
        private const string MakeASnapshotHere = "Make a snapshot here then press Enter to continue";

        static void Main(string[] args)
        {
            // https://gist.github.com/maxtsarenko/ddd94eb948c00f41dba4b1420dfffdaa

            Console.WriteLine(MakeASnapshotHere);
            Console.ReadLine();

            Console.WriteLine("Creating citizen...");

            var citizen = new Citizen();

            Console.Out.WriteLine("Converting him to zombie...");

            ConvertToZombie(citizen);

            Console.Out.WriteLine(MakeASnapshotHere + " and see if citizen is infected");
            Console.ReadLine();

            Console.WriteLine($"Citizen is infected: {citizen.IsInfected}");

            Console.ReadKey();
        }

        private static void ConvertToZombie(Citizen citizen)
        {
            var zombie = new Zombie(bones: new Kg(40), flesh: new Kg(60));

            Citizen.Yelled += zombie.BiteAndInfect;

            DoActivity(citizen);

            Citizen.Yelled -= zombie.BiteAndInfect;
        }

        private static void DoActivity(Citizen citizen)
        {
            Console.Out.WriteLine("Doing some activity...");

            citizen.Freighten();

            Thread.Sleep(TimeSpan.FromSeconds(3));


            Console.Out.WriteLine("Activity is done.");
        }
    }


    internal class Citizen
    {
        internal static event EventHandler Yelled;

        internal bool IsInfected { get; set; }

        internal void Freighten()
        {
            Yelled?.Invoke(this, EventArgs.Empty);
        }
    }

    internal class Zombie
    {
        internal Zombie(Kg bones, Kg flesh)
        {
            Bones = bones;
            Flesh = flesh;
        }

        internal Kg Flesh { get; }

        internal Kg Bones { get; }

        internal void BiteAndInfect(object sender, EventArgs args)
        {
            var citizen = sender as Citizen;
            if (citizen != null)
            {
                citizen.IsInfected = true;

                Console.Out.WriteLine($"citizen {citizen} was infected by zombie with {Flesh} of flesh and {Bones} of bones");
            }
        }
    }

    internal class Kg
    {
        private readonly int[] _gram;

        public Kg(int kg)
        {
            _gram = Enumerable.Repeat(1, kg * 1000).ToArray();
        }

        public override string ToString()
        {
            return $"{_gram.Length / 1000} kg";
        }
    }
}
