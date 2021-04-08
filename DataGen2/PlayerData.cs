using System;
using System.Collections.Generic;
using System.Text;

namespace DataGen2
{
    class PlayerData
    {
        private static readonly Random ran = new Random();
        private static int curID = 0000;

        private string Player_ID { get; set; }
        private int Avg_Days_Logged_Per_Week { get; set; }
        private int Avg_Daily_Playtime { get; set; }
        private bool Played_Solo { get; set; }
        private int Avg_Time_Solo { get; set; }
        private bool Played_Pvp { get; set; }
        private int Avg_Time_Pvp { get; set; }
        private int Avg_BG_Day { get; set; }
        private bool Played_Group { get; set; }
        private int Avg_Time_Group { get; set; }
        private int Avg_Dung_Day { get; set; }

        private int[] Avg_Time_Ingame { get; set; }

        public PlayerData()
        {
            Player_ID = $"P{curID:0000}";
            curID++;

            Avg_Days_Logged_Per_Week = Gen_Avg_Days();
            Avg_Daily_Playtime = Gen_Avg_Playtime();
            Played_Solo = Gen_Solo();
            Played_Pvp = Gen_PVP();
            Played_Group = Gen_Group();

            Avg_Time_Ingame = Gen_Time_Ingame();
            Avg_Time_Solo = Avg_Time_Ingame[0];
            Avg_Time_Pvp = Avg_Time_Ingame[1];
            Avg_Time_Group = Avg_Time_Ingame[2];

            Avg_BG_Day = Gen_BG_Day();
            Avg_Dung_Day = Gen_Dung_Day();
        }

        private int Gen_Avg_Days()
        {
            int val = ran.Next(1, 100);
            if (val >= 95)
                return 7;
            else if (val >= 85)
                return 6;
            else if (val >= 70)
                return 5;
            else if (val >= 50)
                return 4;
            else if (val >= 35)
                return 3;
            else if (val >= 15)
                return 2;
            else
                return 1;
        }

        private int Gen_Avg_Playtime()
        {
            // We want the avg of all players to be around 2 hours per day - 120 mins
            int val = ran.Next(1, 100);
            if (val >= 95)
                return ran.Next(200, 300);
            else if (val >= 85)
                return ran.Next(150, 250);
            else if (val >= 70)
                return ran.Next(125, 200);
            else if (val >= 50)
                return ran.Next(75, 150);
            else if (val >= 35)
                return ran.Next(35, 90);
            else if (val >= 15)
                return ran.Next(20, 50);
            else
                return ran.Next(10, 45);
        }

        private bool Gen_Solo()
        {
            if (ran.Next(100) <= 95)
                return true;
            return false;
        }

        private bool Gen_PVP()
        {
            if (ran.Next(100) <= 75)
                return true;
            return false;
        }

        private bool Gen_Group()
        {
            if (!Played_Pvp && !Played_Solo)
                return true;
            else if (ran.Next(100) <= 80)
                return true;
            else
                return false;
        }

        private int[] Gen_Time_Ingame()
        {              // { Played_SOLO, Played_PVP, Played_GROUP }
            int[] value = { 0, 0, 0 };
            // get avg playtime and distribute between the areas spent in game
            float playTime = Avg_Daily_Playtime;

            int areas = 3;
            if (!Played_Group) areas--;
            if (!Played_Pvp) areas--;
            if (!Played_Solo) areas--;

            // account for variation in playtimes -10% to 10%
            int temp = ran.Next(-10, 10);
            while (temp == 0)
                temp = ran.Next(-10, 10);
            playTime += playTime * ((float)temp / 100);

            // account for some random afk time
            playTime -= playTime * (ran.Next(1, 15) / 100);

            // 3 areas to split the play time
            if (Played_Group)
            {
                int time = (int)(playTime * ran.Next(20, 40) / 100);
                value[2] = time;
                playTime -= time;
            }

            if (areas >= 2 && Played_Pvp)
            {
                int time = (int)(playTime * ran.Next(15, 40) / 100);
                value[1] = time;
                playTime -= time;
            }
            if (Played_Solo)
                value[0] = (int)playTime;
            else if (Played_Pvp)
                value[1] = (int)playTime;
            else if (Played_Group)
                value[2] = (int)playTime;

            return value;
        }

        private int Gen_BG_Day()
        {
            if (!Played_Pvp)
                return 0;
            // take the average time in pvp and divide it by average 10mins per bg
            return Avg_Time_Pvp / 10;
        }

        private int Gen_Dung_Day()
        {
            if (!Played_Group)
                return 0;
            // take the average time in group and divide it average by 20mins per dun
            return Avg_Time_Group / 20;
        }

        public override string ToString()
        {
            return $"{Player_ID}, {Avg_Days_Logged_Per_Week:00}, {Avg_Daily_Playtime:000}, {Played_Solo}, {Avg_Time_Solo:000}"+
                $", {Played_Pvp}, {Avg_Time_Pvp:000}, {Avg_BG_Day:00}, {Played_Group}, {Avg_Time_Group:000}, {Avg_Dung_Day:00}";
        }
    }
}
