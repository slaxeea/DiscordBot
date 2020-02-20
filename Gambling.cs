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

/*
* Datei:                TestCorina02VS/Economy.cs
* Author:               Simon Kappeler
* Datum:                18.2.2020
* Beschreibung:         Die Commands auf die der Bot antworten kann, die etwas mit der Economy.mdf Database zu tun haben.
*/

public class Gambling : ModuleBase<SocketCommandContext>
{
    [Command("zahlerate")]
    public async Task zahlerate(int zahl)
    {


        string connetionString;
        SqlConnection cnn;
        connetionString = @"Data Source";
        cnn = new SqlConnection(connetionString);
        string author = Context.Message.Author.Username;

        Random rnd = new Random();
        int zpc = rnd.Next(1, 11);
        int coins = rnd.Next(5, 30);
        cnn.Open();
        Guid newGUID = Guid.NewGuid();
        if (zahl == zpc)
        {
            await ReplyAsync("Du hesch richtig grate");
            string insertQuery = "Update EconomyCoins Set Coins = Coins + " + coins + "; ";
            SqlCommand com = new SqlCommand(insertQuery, cnn);
            com.ExecuteNonQuery();
        }
        else
        {
            await ReplyAsync("Du hesch verlore, d Zahl wär " + zpc + " gsi");
            string insertQuery = "Update EconomyCoins Set Coins = Coins - " + coins + "; ";
            SqlCommand com = new SqlCommand(insertQuery, cnn);
            com.ExecuteNonQuery();
        }

        Console.WriteLine(zahl);

        cnn.Close();
    }
    [Command("gamble")]
    public async Task gamble(int amt)
    {
        string connetionString;
        SqlConnection cnn;
        connetionString = @"Data Source"
        cnn = new SqlConnection(connetionString);
        string author = Context.Message.Author.Username;

        Random rnd = new Random();
        int eins = amt * -1;
        int cnst = 1;
        int coins2;
        int zpc = rnd.Next(eins, amt + 1);
        double coins = cnst * zpc;

        cnn.Open();
        if (coins < 0)
        {
            coins2 = Convert.ToInt32(coins * -1);
            await ReplyAsync("Du hesch " + amt + " gwettet und " + coins2 + " Coins verlore");
            string insertQuery = "Update EconomyCoins Set Coins = Coins + " + coins + "; ";
            SqlCommand com = new SqlCommand(insertQuery, cnn);
            com.ExecuteNonQuery();
        }
        else
        {
            await ReplyAsync("Du hesch " + amt + " gwettet und " + coins + " Coins gunne");
            string insertQuery = "Update EconomyCoins Set Coins = Coins - " + coins + "; ";
            SqlCommand com = new SqlCommand(insertQuery, cnn);
            com.ExecuteNonQuery();
        }
        cnn.Close();
    }
    [Command("RR")]
    public async Task rr()
    {
        await ReplyAsync("Du wetsch also Russisches Roulet spile. Wenn du günsch, chunsch vil coins über und en boost zu dine coins. Wenn du verlürsch, verlürsch alli coins und Items.");
        Random rnd = new Random();
        int eis = 1;
        int a = rnd.Next(1, 6); //De Ort wo d Chugle isch
        int b = rnd.Next(1, 6); //De Ort wo du duesch abdrucke
        string connetionString;
        SqlConnection cnn;
        connetionString = @"Data Source";
        cnn = new SqlConnection(connetionString);
        cnn.Open();

        if (a == b)
        {
            await ReplyAsync("Du bisch tot und hesch alles verlore");
            Guid newGUID = Guid.NewGuid();
            string insertQuery = "Update EconomyCoins Set Coins = 0; ";
            SqlCommand com = new SqlCommand(insertQuery, cnn);
            com.ExecuteNonQuery();

            insertQuery = "Update Items Set Phone = 0 ;Update ItemsSet Pc = 0; Update ItemsSet Laptop = 0; Update ItemsSet Dildo = 0; ";
            SqlCommand com2 = new SqlCommand(insertQuery, cnn);
            com.ExecuteNonQuery();
        }
        if (a + eis == b || a - eis == b)
        {
            await ReplyAsync("Das isch sehr knapp gsi. Du chunsch es paar coins und en boos übercho");
            string insertQuery = "Update EconomyCoins Set Coins = Coins + " + 100 * (a + b) + "; ";
            SqlCommand com = new SqlCommand(insertQuery, cnn);
            com.ExecuteNonQuery();

            insertQuery = "Update EconomyCoins Set Plus = Plus + " + 5 * (a + b) + "; ";
            SqlCommand com2 = new SqlCommand(insertQuery, cnn);
            com.ExecuteNonQuery();
        }
        else
        {
            await ReplyAsync("Du hesch gunne und es paar coins übercho");
            string insertQuery = "Update EconomyCoins Set Coins = " + 25 * (a + b) + "; ";
            SqlCommand com = new SqlCommand(insertQuery, cnn);
            com.ExecuteNonQuery();

            insertQuery = "Update EconomyCoins Set Plus = Plus + " + 3 * (a + b) + "; ";
            SqlCommand com2 = new SqlCommand(insertQuery, cnn);
            com.ExecuteNonQuery();
        }
        cnn.Close();
    }


}


/*
static void Main(string[] args)
    {
        string connetionString;
        SqlConnection cnn;
        connetionString = @"Data Source";
        cnn = new SqlConnection(connetionString);
        cnn.Open();
        Console.WriteLine("Connection Open!");

        Guid newGUID = Guid.NewGuid();


        string insertQuery = "Update EconomyCoins Set Coins = Coins + 10; ";
        SqlCommand com = new SqlCommand(insertQuery, cnn);
       


        com.ExecuteNonQuery();


        cnn.Close();

    } 
     
*/
