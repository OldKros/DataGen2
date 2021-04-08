using System;
using System.Collections.Generic;
using System.Text;

namespace DataGen2
{
    class BattlegroundPlayer
    {
        private static readonly Random ran = new Random();
        private static int curID = 000;

        private string BG_ID;
        private Battleground BG_Played;
        private List<BG_Player> Team1 = new List<BG_Player>();
        private List<BG_Player> Team2 = new List<BG_Player>();

        public BattlegroundPlayer()
        {
            BG_ID = $"BG{curID:0000}";
            curID++;
            BG_Played = new Battleground();

            Gen_Teams();
        }

        private void Gen_Teams()
        {
            bool requeued;
            bool t1won = false;

            // first we decide who won and who lost
            if (ran.Next(1, 100) < 50)
                t1won = true;

            // gen each player for each team
            // team 1
            for (int i = 0; i < 6; i++)
            {   // 65% chance they requeued if they won the match
                // 30% if they lost
                if (t1won)
                    requeued = ran.Next(1,100) <= 65;
                else
                    requeued = ran.Next(1, 100) <= 30;

                Team1.Add(new BG_Player(ran.Next(0, 15), ran.Next(0, 15), t1won, requeued));
            }
            // team 2
            for (int i = 0; i < 6; i++)
            {   // 65% chance they requeued if they won the match
                // 30% if they lost
                if (!t1won)
                    requeued = ran.Next(1,100) <= 65;
                else
                    requeued = ran.Next(1, 100) <= 30;

                Team2.Add(new BG_Player(ran.Next(0, 15), ran.Next(0, 15), !t1won, requeued));
            }
        }

        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < Team1.Count; i++)
            {
                output += $"{BG_ID}, {BG_Played.Name}, ";
                output += Team1[i].ToString();
                output += "\n";
            }

            for (int i = 0; i < Team2.Count; i++)
            {
                output += $"{BG_ID}, {BG_Played.Name}, ";
                output += Team2[i].ToString();
                output += "\n";
            }
            return output;
        }

        class BG_Player
        {
            private static int curID = 000;
            private string Player_ID;
            private int Kills;
            private int Deaths;
            private bool Win;
            private bool Lose;
            private bool Requeued;

            public BG_Player(int k, int d, bool w, bool r)
            {
                Player_ID = $"P{curID:0000}";
                curID++;
                Kills = k;
                Deaths = d;
                Win = w;
                Lose = !w;
                Requeued = r;
            }

            public override string ToString()
            {
                return $"{Player_ID}, {Kills}, {Deaths}, {Win}, {Lose}, {Requeued}";
            }
        }
    }
}
