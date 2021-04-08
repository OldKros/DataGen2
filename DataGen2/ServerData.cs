using System;
using System.Collections.Generic;

namespace DataGen2
{
    class ServerData
    {
        private static readonly Random ran = new Random();
        private static int curID = 000;
        private static DateTime dateTime = new DateTime(2020, 6, 1);

        private int ServerID;
        private DateTime Date;
        private int UniquePlayers;
        private SortedDictionary<DateTime, int> HourlyData = new SortedDictionary<DateTime, int>();

        public ServerData()
        {
            Date = dateTime;
            // dateTime = dateTime.AddDays(1);

            ServerID = curID;
            curID++;

            UniquePlayers = Gen_TotalPlayers();
            Gen_HourlyData();
        }

        // Logic based on Date aswell as Hour
        // high numbers at peak times (5pm - 10pm)
        // low numbers outside of that - minimal at 2am-7am
        private void Gen_HourlyData()
        {
            bool isWknd = this.Date.DayOfWeek == DayOfWeek.Saturday ||
                          this.Date.DayOfWeek == DayOfWeek.Sunday;

            for (int i = 0; i < 24; i++)
            {
                // between 2am and 5am - low
                if (i >= 2 && i <= 5)
                {
                    if (isWknd)
                        HourlyData.Add(dateTime, ran.Next(UniquePlayers / 85, UniquePlayers / 60));
                    else
                        HourlyData.Add(dateTime, ran.Next(UniquePlayers / 100, UniquePlayers / 75));
                }
                // between 5am and 7am - slightly higher
                else if (i >= 5 && i <= 7)
                {
                    if (isWknd)
                        HourlyData.Add(dateTime, ran.Next(UniquePlayers / 60, UniquePlayers / 50));
                    else
                        HourlyData.Add(dateTime, ran.Next(UniquePlayers / 75, UniquePlayers / 60));
                }
                // between 7am and 5pm - ppl at work except for weekends
                else if (i >= 7 && i <= 17)
                {
                    if (isWknd)
                        HourlyData.Add(dateTime, ran.Next(UniquePlayers / 10, UniquePlayers / 5));
                    else
                        HourlyData.Add(dateTime, ran.Next(UniquePlayers / 60, UniquePlayers / 50));
                }
                // between 5pm and 11pm - peak
                else if (i >= 17 && i <= 23)
                {
                    if (isWknd)
                        HourlyData.Add(dateTime, ran.Next(UniquePlayers / 3, (int)(UniquePlayers / 1.4f)));
                    else
                        HourlyData.Add(dateTime, ran.Next(UniquePlayers / 4, UniquePlayers / 2));
                }
                // between 11pm and 2am - slightly lower than peak
                else
                {
                    if (isWknd)
                        HourlyData.Add(dateTime, ran.Next(UniquePlayers / 15, UniquePlayers / 5));
                    else
                        HourlyData.Add(dateTime, ran.Next(UniquePlayers / 20, UniquePlayers / 10));
                }
                dateTime = dateTime.AddHours(1);
            }
        }

        private int Gen_TotalPlayers()
        {
            bool isWkend = Date.DayOfWeek == DayOfWeek.Saturday ||
                           Date.DayOfWeek == DayOfWeek.Sunday;
            DateTime firstDateToReduce = new DateTime(2020, 6, 24);
            DateTime secDateToReduce = new DateTime(2020, 7, 5);
            DateTime thirdDateToReduce = new DateTime(2020, 7, 16);

            if (Date.Date > thirdDateToReduce)
            {
                if (isWkend)
                    return ran.Next(5000, 8000);
                return ran.Next(3000, 6000);
            }
            else if (Date.Date > secDateToReduce)
            {
                if (isWkend)
                    return ran.Next(6000, 8500);
                return ran.Next(3500, 6500);
            }
            else if (Date.Date > firstDateToReduce)
            {
                if (isWkend)
                    return ran.Next(7000, 10000);
                return ran.Next(3750, 7500);
            }

            if (isWkend)
                return ran.Next(7500, 12000);
            return ran.Next(4000, 8000);
        }

        public override string ToString()
        {
            string output = "";
            foreach (var keyval in HourlyData)
            {
                output += $"{keyval.Key}, {keyval.Value}\n";
            }
            return output;
        }
    }
}
