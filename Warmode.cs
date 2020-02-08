using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;
using System.Data;

namespace DiscordBot1.Warmode
{

    namespace TestCorina02VS.Commands
    {

        public class Commands : ModuleBase<SocketCommandContext>
        {


            [Command("warmode")]
            public async Task warmode()
            {
                string coiins = "";
                int upgpoints = 0;
                string[] item = new string[5];

                string[] stat = new string[5];
                stat[0] = "HPMain";
                stat[1] = "StrenghShip";
                stat[2] = "StrenghTank";
                stat[3] = "UpgradePoints";
                stat[4] = "Mode";

                int i = 0;
                string pc2 = "";
                int times = 0;
                string author = Context.Message.Author.Username;

                string connetionString;
                SqlConnection cnn;
                connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kappe\Desktop\Schuel\IMS\Lernatelier\DiscordBot\TestCorina02VS\TestCorina02VS\Economy.mdf;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=true";
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                Console.WriteLine("Connection Opend");
                try
                {
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand("Select UpgradePoints from Warmode Where Username = '" + author + "'; ", cnn);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        //Console.WriteLine(myReader["Coins"].ToString());
                        coiins = myReader["UpgradePoints"].ToString();
                        upgpoints = Convert.ToInt32(coiins);

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                if (upgpoints > 0)
                {
                    try
                    {
                        while (times < 6)
                        {
                            SqlDataReader myReader = null;
                            SqlCommand myCommand = new SqlCommand("Select " + stat[i] + " from Warmode Where Username = '" + author + "'; ", cnn);
                            myReader = myCommand.ExecuteReader();
                            while (myReader.Read())
                            {
                                //Console.WriteLine(myReader["Coins"].ToString());
                                pc2 = myReader[stat[i]].ToString();
                                item[i] = Convert.ToString(pc2);

                            }
                            times++;
                            i++;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }

                    cnn.Close();
                    Console.WriteLine("Connection Closed");
                    await ReplyAsync(Context.Message.Author.Mention + "\n" + stat[0] + ": " + item[0] + "\n" + stat[1] + ": " + item[1] + "\n" + stat[2] + ": " + item[2] + "\n" + stat[3] + ": " + item[3] + "\n" + stat[4] + ": " + item[4]);
                }
            }


            [Command("mode")]
            public async Task mode(string mode)
            {
                string author = Context.Message.Author.Username;
                string insertQuery = "";
                string connetionString;
                SqlConnection cnn;
                connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kappe\Desktop\Schuel\IMS\Lernatelier\DiscordBot\TestCorina02VS\TestCorina02VS\Economy.mdf;Integrated Security=True;Connect Timeout=30";
                cnn = new SqlConnection(connetionString);


                cnn.Open();
                Console.WriteLine("Connection Opend");

                Guid newGUID = Guid.NewGuid();

                switch (mode)
                {
                    case "attack":
                        insertQuery = "Update Warmode Set Mode = 'Attack'where Username = '" + author + "'; Update Warmode Set HPMain = HPMAIN - 20 where Username ='" + author + "';";
                        break;
                    case "defense":
                        insertQuery = "Update Warmode Set Mode = 'Defense'where Username = '" + author + "'; Update Warmode Set HPMain = HPMAIN + 20 where Username ='" + author + "';";
                        break;
                    case "neutral":
                        insertQuery = "Update Warmode Set Mode = 'Neutral'where Username = '" + author + "'; Update Warmode Set HPMain = HPMAIN + 10 where Username ='" + author + "';";
                        break;
                }

                SqlCommand com = new SqlCommand(insertQuery, cnn);
                com.ExecuteNonQuery();



                cnn.Close();
                Console.WriteLine("Connection Closed");

                await ReplyAsync(Context.Message.Author.Mention + "Du bisch jetzt im " + mode + " Modus");

            }

            [Command("attack")]
            public async Task attack(string user)
            {
                string author = Context.Message.Author.Username;

                string connetionString;
                SqlConnection cnn;
                connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kappe\Desktop\Schuel\IMS\Lernatelier\DiscordBot\TestCorina02VS\TestCorina02VS\Economy.mdf;Integrated Security=True;Connect Timeout=30";
                cnn = new SqlConnection(connetionString);

                cnn.Open();
                Console.WriteLine("Connection Opend");

                Guid newGUID = Guid.NewGuid();

                string insertQuery = "Update Warmode Set Mode = 'attack'where Username = '" + author + "'; Update Warmode Set HPMain = HPMAIN - 20 where Username ='" + author + "';";
                SqlCommand com = new SqlCommand(insertQuery, cnn);
                com.ExecuteNonQuery();

                insertQuery = "Select Coins From EconomyCoins where Username = '" + author + "';";

                cnn.Close();
                Console.WriteLine("Connection Closed");



            }
            [Command("upgrade")]
            public async Task up(string upg)
            {
                string insertQuery = "";
                int upgpoints = 0;
                string coiins;
                string author = Convert.ToString(Context.Message.Author);



                string connetionString;
                SqlConnection cnn;
                connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kappe\Desktop\Schuel\IMS\Lernatelier\DiscordBot\TestCorina02VS\TestCorina02VS\Economy.mdf;Integrated Security=True;Connect Timeout=30";
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                Console.WriteLine("Connection Opend");
                try
                {
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand("Select UpgradePoints from Warmode Where Username = '" + author + "'; ", cnn);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        //Console.WriteLine(myReader["Coins"].ToString());
                        coiins = myReader["UpgradePoints"].ToString();
                        upgpoints = Convert.ToInt32(coiins);

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                if (upgpoints >= 1)
                {
                    insertQuery = "Update Warmode Set UpgradePoints = UpgradePoints - 1 where Username = '" + author + "'; Update Warmode Set " + upg + " = " + upg + " + 20 where Username ='" + author + "';Update Warmode Set HPMain = HPMain - 10 where Username ='" + author + "';";
                    Guid newGUID = Guid.NewGuid();
                    SqlCommand com = new SqlCommand(insertQuery, cnn);
                    com.ExecuteNonQuery();

                    await ReplyAsync(Context.Message.Author.Mention + "Du hesch " + upg + " upgraded");
                }
                if (upg == "")
                {
                    await ReplyAsync("Du hesch nüt usgwählt");
                }
                else
                {
                    await ReplyAsync(Context.Message.Author.Mention + "Du hesch kei Upgradepünkt meh");
                }
                cnn.Close();
                Console.WriteLine("Connection Closed");
            }

        }
    }
}
