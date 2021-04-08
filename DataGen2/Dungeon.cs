using System;

namespace DataGen2
{
    class Dungeon
    {
        private static readonly Random ran = new Random();
        public string Name {get; private set;}
        public int NumBosses { get; private set; }
        public int Min_Time { get; private set; }
        public int Max_Time { get; private set; }
        public int Avg_Time { get; private set; }

        public Dungeon()
        {
            Name = Gen_Name();
            NumBosses = Gen_NB();
            Min_Time = Gen_Min_Time();
            Max_Time = Gen_Max_Time();
            Avg_Time = Gen_Avg_Time();
        }

        public Dungeon(string name)
        {
            // should really check the name here but w/e
            Name = name;
            NumBosses = Gen_NB();
            Min_Time = Gen_Min_Time();
            Max_Time = Gen_Max_Time();
            Avg_Time = Gen_Avg_Time();
        }

        private string Gen_Name()
        {
            int num = ran.Next(1, 6);

            switch (num)
            {
                case 1:
                    return "Imperical Mines";
                case 2:
                    return "City of Gold";
                case 3:
                    return "Pharoah's Tomb";
                case 4:
                    return "Halls of the Righteous";
                case 5:
                    return "Monestary of the Chosen";
            }
            return "";
        }

        private int Gen_NB()
        {
            switch (Name)
            {
                case "Imperical Mines":
                    return 5;
                case "City of Gold":
                    return 4;
                case "Pharoah's Tomb":
                    return 6;
                case "Halls of the Righteous":
                    return 5;
                case "Monestary of the Chosen":
                    return 7;
            }
            return 0;
        }

        // maximum time a group took to kill the last boss 30-40mins?
        private int Gen_Max_Time()
        {
            switch (Name)
            {
                case "Imperical Mines":
                    return ran.Next(50, 80);
                case "City of Gold":
                    return ran.Next(40, 70);
                case "Pharoah's Tomb":
                    return ran.Next(60, 100);
                case "Halls of the Righteous":
                    return ran.Next(50, 80);
                case "Monestary of the Chosen":
                    return ran.Next(65, 110);
            }
            return ran.Next(50, 90);
        }

        // fastest time a group took to kill the last boss
        private int Gen_Min_Time()
        {
            switch (Name)
            {
                case "Imperical Mines":
                    return ran.Next(25, 35);
                case "City of Gold":
                    return ran.Next(20, 30);
                case "Pharoah's Tomb":
                    return ran.Next(27, 40);
                case "Halls of the Righteous":
                    return ran.Next(25,35);
                case "Monestary of the Chosen":
                    return ran.Next(35, 50);
            }
            return ran.Next(20, 30);
        }

        // average time groups took to kill the last boss
        private int Gen_Avg_Time()
        {
            switch (Name)
            {
                case "Imperical Mines":
                    return ran.Next(35, 50);
                case "City of Gold":
                    return ran.Next(30, 40);
                case "Pharoah's Tomb":
                    return ran.Next(40, 60);
                case "Halls of the Righteous":
                    return ran.Next(35, 50);
                case "Monestary of the Chosen":
                    return ran.Next(50, 65);
            }
            return ran.Next(30, 40);
        }
    }
}