using System;
using System.Collections.Generic;
using System.Text;

namespace DataGen2
{
    class DungeonPlayer
    {
        private static readonly Random ran = new Random();
        private static int curId = 0000;

        private string Dungeon_ID;
        private Dungeon DungeonPlayed;
        private int Party_Size;
        private List<Dun_Player> Party = new List<Dun_Player>();
        private bool Finished_Dungeon;
        private int BossesKilled;
        private bool Stayed_Together;

        public DungeonPlayer()
        {
            Dungeon_ID = $"DG{curId:0000}";
            curId++;
            DungeonPlayed = new Dungeon();
            Party_Size = 5;
            Finished_Dungeon = Gen_Finished();
            Gen_Party();
            Stayed_Together = Gen_Stayed();
        }

        private bool Gen_Stayed()
        {
            // 75% chance they stayed together if finished
            if (Finished_Dungeon)
                return ran.Next(1, 100) <= 75;

            // if they didn't kill any or only 1 bosses they wouldnt stay together as a group.
            if (BossesKilled <= 2)
                return false;

            // 10% chance they stayed if they failed
            return ran.Next(1, 100) <= 10;
        }

        private bool Gen_Finished()
        {
            // 80-95 chance they finished the dungeon.
            return ran.Next(1, 100) <= ran.Next(80, 95);
        }

        private void Gen_Party()
        {
            BossesKilled = Finished_Dungeon ? DungeonPlayed.NumBosses : ran.Next(0,DungeonPlayed.NumBosses);
            // if they finished the dungeon then there should be atleast 1 loot per boss (2 from end boss)
            int totalLoot = Finished_Dungeon ? DungeonPlayed.NumBosses + 1 : BossesKilled;

            // distribute the loot between all players based on the amount of bosses killed
            int[] lootList = new int[5];
            int index = 0;
            while(totalLoot > 0)
            {
                if (ran.Next(0, 2) == 1)
                {
                    lootList[index%5]++;
                    totalLoot--;
                }
                index++;
            }

            for (int i = 0; i < Party_Size; i++)
            {
                Party.Add(new Dun_Player(BossesKilled, lootList[i]));
            }
        }

        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < Party.Count; i++)
            {
                output += $"{Dungeon_ID}, {DungeonPlayed.Name}, {DungeonPlayed.NumBosses}, ";
                output += Party[i].ToString();
                output += $", {Stayed_Together}\n";
            }
            return output;
        }

        class Dun_Player
        {
            private static int curId = 0000;
            private string Player_ID;
            private int Bosses_Killed;
            private int Loot_Rewards;

            public Dun_Player(int bossesKilled, int rewards)
            {
                Player_ID = $"P{curId:0000}";
                curId++;

                Bosses_Killed = bossesKilled;
                Loot_Rewards = rewards;
            }

            public override string ToString()
            {
                return $"{Player_ID}, {Bosses_Killed}, {Loot_Rewards}";
            }
        }
    }
}
