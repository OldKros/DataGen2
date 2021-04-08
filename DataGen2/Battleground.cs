using System;

namespace DataGen2
{
    class Battleground
    {
        private static readonly Random ran = new Random();

        public string Name {get; private set;}

        public Battleground()
        {
            Name = Gen_Name();
        }

        public Battleground(string name)
        {
            // should really check what is being passed matches the name of a bg
            Name = name;
        }

        // Generate a random battleground to be used
        private string Gen_Name()
        {
            int id = ran.Next(1, 6);
            switch (id)
            {
                case 1:
                    return "Temporal Mines";
                case 2:
                    return "Battlecry Valley";
                case 3:
                    return "Isle of Death";
                case 4:
                    return "Temple of the Cursed";
                case 5:
                    return "Shore of Ash";
            }
            return "";
        }
    }
}