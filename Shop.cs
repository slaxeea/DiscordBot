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

namespace DiscordBot1.Shop
{

    namespace TestCorina02VS.Shop
    {

        public class Commands : ModuleBase<SocketCommandContext>
        {
            [Command("shop")]
            public async Task shop()
            {
                int[] item = new int[6];

                string[] itemname = new string[6];
                itemname[0] = "Laptop";
                itemname[1] = "Phone";
                itemname[2] = "Pc";
                itemname[3] = "Plane";
                itemname[4] = "Dildo";
                itemname[5] = "Playstation";

                int i = 0;
                string cost = "";
                int times = 0;
                string coins = " Coins";
                string author = Context.Message.Author.Username;

                string connetionString;
                SqlConnection cnn;
                connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kappe\Desktop\Schuel\IMS\Lernatelier\DiscordBot\TestCorina02VS\TestCorina02VS\Economy.mdf;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=true";
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                Console.WriteLine("Connection Opend");

                try
                {
                    while (times < 6)
                    {
                        SqlDataReader myReader = null;
                        SqlCommand myCommand = new SqlCommand("Select Cost from ItemCost Where Item = '" + itemname[i] + "'; ", cnn);
                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            //Console.WriteLine(myReader["Coins"].ToString());
                            cost = myReader["Cost"].ToString();
                            item[i] = Convert.ToInt32(cost);
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
                await ReplyAsync(Context.Message.Author.Mention + "D Items kostet: \n"+ itemname[0] + ":   " + item[0] +"    "+coins+ "   \n" + itemname[1] + ":    " + item[1] +"     "+coins+"  \n" + itemname[2] + ":           " + item[2] +"     "+coins+ " \n" + itemname[3] + ":     " + item[3] +" "+ coins+"  \n" + itemname[4] + ":     " + item[4]+"   "+coins+"\n"+itemname[5]+":  "+item[5]+"  "+coins);
            }
            [Command("buy")]
            public async Task buy(string item, int anz)
            {
                string author = Context.User.Username;
                string cost2 = "";
                int cost = 0;
                int zahle = 2;
                string coiins;
                int coins = 0;
                string connetionString;
                SqlConnection cnn;
                connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kappe\Desktop\Schuel\IMS\Lernatelier\DiscordBot\TestCorina02VS\TestCorina02VS\Economy.mdf;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=true";
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                Console.WriteLine("Connection Opend");
                try
                {
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand("Select Coins from EconomyCoins Where Username = '" + author + "'; ", cnn);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        //Console.WriteLine(myReader["Coins"].ToString());
                        coiins = myReader["Coins"].ToString();
                        coins = Convert.ToInt32(coiins);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                try
                {
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand("Select Cost from ItemCost Where Item ='" + item + "'", cnn);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        //Console.WriteLine(myReader["Coins"].ToString());
                        cost2 = myReader["Cost"].ToString();
                        cost = Convert.ToInt32(cost2);

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                zahle = Convert.ToInt32(cost) * Convert.ToInt32(anz);
                if (coins < zahle)
                {
                    await ReplyAsync(Context.Message.Author.Mention + "Du hesch ned gnueg Gäld");
                }
                else
                {
                    string insertQuery = "Update Items Set " + item + " = " + item + " + " + anz + " where Username = '" + author + "';";

                    SqlCommand com = new SqlCommand(insertQuery, cnn);
                    com.ExecuteNonQuery();

                    insertQuery = "Update EconomyCoins Set Coins = Coins - " + zahle + " where Username = '" + author + "'; ";

                    SqlCommand com2 = new SqlCommand(insertQuery, cnn);
                    com2.ExecuteNonQuery();

                    cnn.Close();
                    Console.WriteLine("Connection Closed\n");
                    await ReplyAsync(Context.Message.Author.Mention + "Du hesch " + anz + " " + item + " für " + zahle + " kauft");

                    //Console.WriteLine("Zahlt: " + zahle + "\nKostet: " + cost + "\nItem: " + item + "\nQuery: " + insertQuery + "\n");
                }
            }
            [Command("inventory")]
            public async Task inventar()
            {
                int[] item = new int[5];

                string[] itemname = new string[5];
                itemname[0] = "Laptop";
                itemname[1] = "Phone";
                itemname[2] = "Pc";
                itemname[3] = "Plane";
                itemname[4] = "Dildo";

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
                    while (times < 6)
                    {
                        SqlDataReader myReader = null;
                        SqlCommand myCommand = new SqlCommand("Select " + itemname[i] + " from Items Where Username = '" + author + "'; ", cnn);
                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            //Console.WriteLine(myReader["Coins"].ToString());
                            pc2 = myReader[itemname[i]].ToString();
                            item[i] = Convert.ToInt32(pc2);
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
                await ReplyAsync(Context.Message.Author.Mention + " Inventar vo " + author + "\n" + itemname[0] + ": " + item[0] + "\n" + itemname[1] + ": " + item[1] + "\n" + itemname[2] + ": " + item[2] + "\n" + itemname[3] + ": " + item[3] + "\n" + itemname[4] + ": " + item[4]);
            }
            [Command("admin inventory")]
            public async Task adinventory(string user)
            {
                string author = Context.Message.Author.Username;

                if (author == "slaxeea")
                {

                    int[] item = new int[5];

                    string[] itemname = new string[5];
                    itemname[0] = "Laptop";
                    itemname[1] = "Phone";
                    itemname[2] = "Pc";
                    itemname[3] = "Plane";
                    itemname[4] = "Dildo";

                    int i = 0;
                    string pc2 = "";
                    int times = 0;


                    string connetionString;
                    SqlConnection cnn;
                    connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kappe\Desktop\Schuel\IMS\Lernatelier\DiscordBot\TestCorina02VS\TestCorina02VS\Economy.mdf;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=true";
                    cnn = new SqlConnection(connetionString);
                    cnn.Open();
                    Console.WriteLine("Connection Opend");

                    try
                    {
                        while (times < 6)
                        {
                            SqlDataReader myReader = null;
                            SqlCommand myCommand = new SqlCommand("Select " + itemname[i] + " from Items Where Username = '" + user + "'; ", cnn);
                            myReader = myCommand.ExecuteReader();
                            while (myReader.Read())
                            {
                                //Console.WriteLine(myReader["Coins"].ToString());
                                pc2 = myReader[itemname[i]].ToString();
                                item[i] = Convert.ToInt32(pc2);
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
                    await ReplyAsync(Context.Message.Author.Mention + " Inventar vo " + user + "\n" + itemname[0] + ": " + item[0] + "\n" + itemname[1] + ": " + item[1] + "\n" + itemname[2] + ": " + item[2] + "\n" + itemname[3] + ": " + item[3] + "\n" + itemname[4] + ": " + item[4]);
                }

                else
                {
                    await ReplyAsync("Du hesch kei Admin berechtigung");
                }
            }
            [Command("schenk")]
            public async Task share(string item, string rcver, int amnt)
            {
                string author = Context.User.Username;
                string connetionString;
                SqlConnection cnn;
                connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kappe\Desktop\Schuel\IMS\Lernatelier\DiscordBot\TestCorina02VS\TestCorina02VS\Economy.mdf;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=true";
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                Console.WriteLine("Connection Open");

                Guid newGUID = Guid.NewGuid();


                string insertQuery = "Update Items Set " + item + " = " + item + " + " + amnt + " Where Username ='" + rcver + "';Update Items Set " + item + " = " + item + " - " + amnt + " where Username = '" + author + "';";
                SqlCommand com = new SqlCommand(insertQuery, cnn);

                com.ExecuteNonQuery();
                cnn.Close();
                Console.WriteLine("Connection Closed");
                await ReplyAsync("Du hesch " + amnt + " " + item + " am " + rcver + " gschenkt");

            }
            [Command("admin inventory")]
            public async Task inventar(string user)
            {
                string author = Context.Message.Author.Username;
                if (author == "slaxeea")
                {
                    int[] item = new int[5];

                    string[] itemname = new string[5];
                    itemname[0] = "Laptop";
                    itemname[1] = "Phone";
                    itemname[2] = "Pc";
                    itemname[3] = "Plane";
                    itemname[4] = "Dildo";

                    int i = 0;
                    string pc2 = "";
                    int times = 0;


                    string connetionString;
                    SqlConnection cnn;
                    connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kappe\Desktop\Schuel\IMS\Lernatelier\DiscordBot\TestCorina02VS\TestCorina02VS\Economy.mdf;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=true";
                    cnn = new SqlConnection(connetionString);
                    cnn.Open();
                    Console.WriteLine("Connection Opend");

                    try
                    {
                        while (times < 6)
                        {
                            SqlDataReader myReader = null;
                            SqlCommand myCommand = new SqlCommand("Select " + itemname[i] + " from Items Where Username = '" + user + "'; ", cnn);
                            myReader = myCommand.ExecuteReader();
                            while (myReader.Read())
                            {
                                //Console.WriteLine(myReader["Coins"].ToString());
                                pc2 = myReader[itemname[i]].ToString();
                                item[i] = Convert.ToInt32(pc2);
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
                    await ReplyAsync(Context.Message.Author.Mention + " Inventar vo " + user + "\n" + itemname[0] + ": " + item[0] + "\n" + itemname[1] + ": " + item[1] + "\n" + itemname[2] + ": " + item[2] + "\n" + itemname[3] + ": " + item[3] + "\n" + itemname[4] + ": " + item[4]);
                }
                else
                {
                    await ReplyAsync("Du hesch kei Admin berechtigung");
                }
            }


            [Command("sell")]
            public async Task sell(string item, int anz)
            {
                string author = Context.User.Username;
                int monback = 1000;
                int zahle = 2;
                int coins = 0;
                string connetionString;

                switch (item)
                {
                    case "Pc":
                        monback = 2000;
                        break;
                    case "Laptop":
                        monback = 1200;
                        break;
                    case "Phone":
                        monback = 750;
                        break;
                    case "Plane":
                        monback = 5500;
                        break;
                    case "House":
                        monback = 8500;
                        break;
                    case "Dildo":
                        monback = 0;
                        break;
                }
                if (item == "Dildo")
                {
                    await ReplyAsync("Das chasch du doch ned verchaufe! Als ob das öpper no wott. Schmeiss das wegg du grüsel");
                    SqlConnection cnn;
                    connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kappe\Desktop\Schuel\IMS\Lernatelier\DiscordBot\TestCorina02VS\TestCorina02VS\Economy.mdf;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=true";
                    cnn = new SqlConnection(connetionString);
                    cnn.Open();
                    Console.WriteLine("Connection Opend");


                    string insertQuery = "Update Items Set " + item + " = " + item + " - " + anz + " where Username = '" + author + "';";

                    //Console.WriteLine("Query: " + insertQuery);
                    SqlCommand com = new SqlCommand(insertQuery, cnn);
                    com.ExecuteNonQuery();

                    cnn.Close();
                    Console.WriteLine("Connection Closed\n");
                }
                else
                {
                    SqlConnection cnn;
                    connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kappe\Desktop\Schuel\IMS\Lernatelier\DiscordBot\TestCorina02VS\TestCorina02VS\Economy.mdf;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=true";
                    cnn = new SqlConnection(connetionString);
                    cnn.Open();
                    Console.WriteLine("Connection Opend");

                    zahle = Convert.ToInt32(monback) * Convert.ToInt32(anz);

                    string insertQuery = "Update Items Set " + item + " = " + item + " - " + anz + " where Username = '" + author + "';";

                    //Console.WriteLine("Query: " + insertQuery);
                    SqlCommand com = new SqlCommand(insertQuery, cnn);
                    com.ExecuteNonQuery();

                    insertQuery = "Update EconomyCoins Set Coins = Coins - " + zahle + " where Username = '" + author + "'; ";

                    SqlCommand com2 = new SqlCommand(insertQuery, cnn);
                    com2.ExecuteNonQuery();

                    cnn.Close();
                    Console.WriteLine("Connection Closed\n");
                    await ReplyAsync(Context.Message.Author.Mention + "Du hesch " + anz + " " + item + " für " + zahle + " verchauft");
                }
            }
        }
    }
}
