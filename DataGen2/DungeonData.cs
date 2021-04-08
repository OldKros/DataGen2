using System;
using System.Collections.Generic;
using System.Text;

namespace DataGen2
{
    class DungeonData
    {
        private static readonly Random ran = new Random();

        // start at 1st june 2020.
        private static DateTime dateTime = new DateTime(2020, 6, 1);

        private Dictionary<Dungeon, int> dgs ;
        private DateTime Date;
        private int Total_Dungs;

        public DungeonData()
        {
            Date = dateTime;
            dateTime = dateTime.AddDays(1);
            dgs = new Dictionary<Dungeon, int>();
            Total_Dungs = Gen_Dungeons_Entered();
            Gen_Duns();
        }

        private int Gen_Dungeons_Entered()
        {
            bool isWkend = Date.DayOfWeek == DayOfWeek.Saturday || Date.DayOfWeek == DayOfWeek.Sunday;
            // Gradually increase the amount of entries over a set time
            DateTime firstDateToReduce = new DateTime(2020, 6, 24);
            DateTime secDateToReduce = new DateTime(2020, 7, 5);
            DateTime thirdDateToReduce = new DateTime(2020, 7, 16);
            if (Date.Date > thirdDateToReduce)
            {
                if (isWkend)
                    return ran.Next(2500, 3000);
                return ran.Next(1500, 2500);
            }
            else if (Date.Date > secDateToReduce)
            {
                if (isWkend)
                    return ran.Next(1500, 2100);
                return ran.Next(1400, 2000);
            }
            else if (Date.Date > firstDateToReduce)
            {
                if (isWkend)
                    return ran.Next(1300, 1600);
                return ran.Next(1200, 1500);
            }

            if (isWkend)
                return ran.Next(900, 1400);
            return ran.Next(750, 1250);
        }

        private void Gen_Duns()
        {
            List<int> temp = new List<int>(4) { -2, -1, 1, 2 };
            // split the total dungs for the day between each dung
            int total = Total_Dungs;
            int pos = temp[ran.Next(temp.Count)];
            temp.Remove(pos);

            int num = Total_Dungs / 5 + (int)(Total_Dungs * (pos / 100.0f));
            total -= num;
            dgs.Add(new Dungeon("Imperical Mines"), num);

            pos = temp[ran.Next(temp.Count)];
            temp.Remove(pos);
            num = Total_Dungs / 5 + (int)(Total_Dungs * (pos / 100.0f));
            total -= num;
            dgs.Add(new Dungeon("City of Gold"), num);

            pos = temp[ran.Next(temp.Count)];
            temp.Remove(pos);
            num = Total_Dungs / 5 + (int)(Total_Dungs * (pos / 100.0f));
            total -= num;
            dgs.Add(new Dungeon("Pharoah's Tomb"), num);

            pos = temp[ran.Next(temp.Count)];
            temp.Remove(pos);
            num = Total_Dungs / 5 + (int)(Total_Dungs * (pos / 100.0f));
            total -= num;
            dgs.Add(new Dungeon("Halls of the Righteous"), num);
            dgs.Add(new Dungeon("Monestary of the Chosen"), total);
        }

        private int Gen_Final_Boss_Kills(int dun)
        {
            return (int)(dun * (ran.Next(80, 95) / 100.0f));
        }

        public override string ToString()
        {
            string output = "";
            foreach (var pair in dgs)
            {
                output += $"{Date}, {pair.Key.Name}, {pair.Value}, {Gen_Final_Boss_Kills(pair.Value)}, {pair.Key.Max_Time}, {pair.Key.Min_Time}, {pair.Key.Avg_Time}\n";
            }
            return output;
        }
    }
}
