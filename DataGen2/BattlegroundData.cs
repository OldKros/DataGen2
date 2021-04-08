using System;
using System.Collections.Generic;
using System.Text;

namespace DataGen2
{
    class BattlegroundData
    {
        private static readonly Random ran = new Random();

        // start at 1st june 2020.
        private static DateTime dateTime = new DateTime(2020, 6, 1);

        private Dictionary<Battleground, int> bgs;
        private DateTime Date;
        private int Total_Bgs;
        private int Players_Per_BG;

        public BattlegroundData()
        {
            Date = dateTime;
            dateTime = dateTime.AddDays(1);
            bgs = new Dictionary<Battleground, int>();
            Total_Bgs = Gen_Total_BGs();
            Players_Per_BG = 12;
            Gen_BGs();
        }

        // Generate the total amount of BGs for that day
        private int Gen_Total_BGs()
        {
            bool isWkend = Date.DayOfWeek == DayOfWeek.Saturday || Date.DayOfWeek == DayOfWeek.Sunday;
            // Gradually reduce or increase the amount of entries over a set time
            DateTime firstDateToReduce = new DateTime(2020, 6, 24);
            DateTime secDateToReduce = new DateTime(2020, 7, 5);
            DateTime thirdDateToReduce = new DateTime(2020, 7, 16);
            if (Date.Date > thirdDateToReduce)
            {
                if (isWkend)
                    return ran.Next(600, 900);
                return ran.Next(400, 750);

            }
            else if (Date.Date > secDateToReduce)
            {
                if (isWkend)
                    return ran.Next(800, 1300);
                return ran.Next(500, 900);
            }
            else if (Date.Date > firstDateToReduce)
            {
                if (isWkend)
                    return ran.Next(1000, 1300);
                return ran.Next(700, 1300);
            }

            if (isWkend)
                return ran.Next(1200, 1500);
            return ran.Next(800, 1300);
        }


        // split the total Bgs for the day between each bgs
        // offset list to give each BG a random offset
        private void Gen_BGs()
        {
            List<int> offset = new List<int>(4) {-2, -1 ,1 ,2 };
            int total = Total_Bgs;
            int pos = offset[ran.Next(offset.Count)];
            offset.Remove(pos);

            int num = Total_Bgs / 5 + (int)(Total_Bgs * (pos / 100.0f));
            total -= num;
            bgs.Add(new Battleground("Temporal Mines"), num);

            pos = offset[ran.Next(offset.Count)];
            offset.Remove(pos);
            num = Total_Bgs / 5 + (int)(Total_Bgs * (pos / 100.0f));
            total -= num;
            bgs.Add(new Battleground("Battlecry Valley"), num);

            pos = offset[ran.Next(offset.Count)];
            offset.Remove(pos);
            num = Total_Bgs / 5 + (int)(Total_Bgs * (pos / 100.0f));
            total -= num;
            bgs.Add(new Battleground("Isle of Death"), num);

            pos = offset[ran.Next(offset.Count)];
            offset.Remove(pos);
            num = Total_Bgs / 5 + (int)(Total_Bgs * (pos / 100.0f));
            total -= num;
            bgs.Add(new Battleground("Temple of the Cursed"), num);
            bgs.Add(new Battleground("Shore of Ash"), total);
        }

        // Generate the amount of players that were unique for that BG
        private int Gen_Unique(int numOfBgs)
        {
            // total bgs per day * ppbg * % of players that would be recurring
            return (int)(numOfBgs * Players_Per_BG * (ran.Next(50, 75) / 100.0f));
        }

        public override string ToString()
        {
            string output = "";
            foreach (var pair in bgs)
            {
                output += $"{Date}, {pair.Key.Name}, {pair.Value}, {Players_Per_BG:00}, {pair.Value * Players_Per_BG}, {Gen_Unique(pair.Value)}\n";
            }
            return output;
        }
    }
}
