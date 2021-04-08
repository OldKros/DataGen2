using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGen2
{
    class Program
    {
        static void Main(string[] args)
        {
            string output_loc = "C:\\Users\\krost\\Documents\\College\\Second_Year\\Unit14_Business_Intelligence\\Data\\";

            /** Possible Additions:
            * Solo Player data - quests an repeatable activities
            * Solo Data per day
            * add activity timestamps to PlayerData e.g.
            *   - online from 18:30 to 20:30
            *   - would need integration with server data
            * Make it so daily BG and DG data is generated from the server data numbers
            *
            **/

            /* Uncomment what data you want to generate and how much*/
            // Gen_Player_Data(output_loc, 400);
            // Gen_BG_Player_Data(output_loc, 201);
            // Gen_DG_Player_Data(output_loc, 201);
            // Gen_BG_Data(output_loc, 91);
            Gen_DG_Data(output_loc, 91);
            // Gen_Server_Data(output_loc, 61);

            Console.WriteLine("Finished generating data.");
            Console.WriteLine("Press Any key to exit");
            Console.Read();
        }

        private static void Gen_Server_Data(string output_loc, int num)
        {
            // Server Data per day
            using (StreamWriter sw = new StreamWriter(output_loc + "ServerData.csv", true, Encoding.ASCII))
            {
                sw.WriteLine("Date, Active Players");
                for (int i = 0; i < num; i++)
                {
                    var temp = new ServerData().ToString();
                    System.Console.WriteLine(temp);
                    sw.WriteLine(temp);
                }
            }
        }

        // Dungeon Data per day
        private static void Gen_DG_Data(string output_loc, int num)
        {
            using (StreamWriter sw = new StreamWriter(output_loc + "DungeonData.csv", true, Encoding.ASCII))
            {
                sw.WriteLine("Date, Dungeon Name, Amount of Runs, Final Boss Kills, Max Clear Time, Min Clear Time, Avg Clear Time");
                for (int i = 0; i < num; i++)
                {
                    sw.WriteLine(new DungeonData().ToString());
                }
            }
        }

        // Dungeon Player Data
        // Each output prints a new random dungeon with 5 players
        private static void Gen_DG_Player_Data(string output_loc, int num)
        {
            using (StreamWriter sw = new StreamWriter(output_loc + "DungeonPlayerData.csv", true, Encoding.ASCII))
            {
                sw.WriteLine("Dungeon ID, Dungeon Name, Dungeon Bosses, Player ID, Bosses Killed, Loot Rewards, Stayed Together");
                for (int i = 0; i < num; i++)
                {
                    sw.WriteLine(new DungeonPlayer().ToString());
                }
            }
        }

        // Battleground Data per day
        private static void Gen_BG_Data(string output_loc, int num)
        {
            using (StreamWriter sw = new StreamWriter(output_loc + "BattlegroundData.csv", true, Encoding.ASCII))
            {
                sw.WriteLine("Date, Battleground Name, Number of games, Players Per BG, Total Players, Unique Players");
                for (int i = 0; i < num; i++)
                {
                    sw.WriteLine(new BattlegroundData().ToString());
                }
            }
        }

        // Battleground Player Data
        // Each output prints a new random BG with 12 players
        private static void Gen_BG_Player_Data(string output_loc, int num)
        {
            using (StreamWriter sw = new StreamWriter(output_loc + "BattlegroundPlayerData.csv", true, Encoding.ASCII))
            {
                sw.WriteLine("Battleground ID, Battleground Name, Player ID, Kills, Deaths, Won, Lost, Requeued");
                for (int i = 0; i < num; i++)
                {
                    sw.WriteLine(new BattlegroundPlayer().ToString());
                }
            }
        }

        // Player Data
        private static void Gen_Player_Data(string output_loc, int num)
        {
            using (StreamWriter sw = new StreamWriter(output_loc + "PlayerData.csv", true, Encoding.ASCII))
            {
                sw.WriteLine("Player ID, Avg days per week logged in, Avg login time per day, " +
                 "Interact with Solo?, Avg time spent solo per day, " +
                 "Interact with pvp?, Avg time spent pvp per day, Avg Battlegrounds per day, " +
                 "Interact with group?, Avg time spent group per day, Avg dungeons per day");
                for (int i = 0; i < num; i++)
                {
                    sw.WriteLine(new PlayerData().ToString());
                }
            }
        }
    }
}
