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

public class Commands : ModuleBase<SocketCommandContext>
{


    [Command("coins")]
    public async Task Coins()
    {
        string author = Context.Message.Author.Username;
        int coinsübrcho = 0;
        string plus1 = "";
        int plus2 = 0;
        string connetionString;
        SqlConnection cnn;
        connetionString = @"Data Source";
        cnn = new SqlConnection(connetionString);
        cnn.Open();
        try
        {
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("Select Plus from EconomyCoins Where Username = '" + author + "'; ", cnn);
            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                plus1 = myReader["Plus"].ToString();
                plus2 = Convert.ToInt32(plus1);

            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        cnn.Close();
        Random rnd = new Random();
        int zahl = rnd.Next(1, 5);
        Random plus3 = new Random();
        int plus4 = rnd.Next(1, 5);

        coinsübrcho = 10 + zahl + plus2;
        //Console.WriteLine(coinsübrcho);
        cnn.Open();
        Console.WriteLine("\nConnection Opend");

        Guid newGUID = Guid.NewGuid();

        string insertQuery = "Update EconomyCoins Set Coins = Coins + " + coinsübrcho + " where Username = '" + author + "';";
        SqlCommand com = new SqlCommand(insertQuery, cnn);
        com.ExecuteNonQuery();

        insertQuery = "Update EconomyCoins Set Streak = Streak + 1 where Username = '" + author + "';";
        SqlCommand com2 = new SqlCommand(insertQuery, cnn);
        com2.ExecuteNonQuery();

        insertQuery = "Update EconomyCoins Set Plus = Plus +" + plus4 + " where Username = '" + author + "';";
        SqlCommand com3 = new SqlCommand(insertQuery, cnn);
        com3.ExecuteNonQuery();

        cnn.Close();
        Console.WriteLine("Connection Closed\n");
        //Console.WriteLine("Plus2: " + plus2 + "\nRND Zahl: " + zahl + "\nPlus4:"+plus4+"\n" );
        await Context.Channel.SendMessageAsync(Context.Message.Author.Mention + " Du hesch jetzt " + coinsübrcho + " Coins übercho");


    }
    [Command("leticia")]
    public async Task leticiadiehoe()
    {
        string author = Context.Message.Author.Username;
        string connetionString;
        SqlConnection cnn;
        connetionString = @"Data Source";
        cnn = new SqlConnection(connetionString);

        cnn.Open();
        Console.WriteLine("Connection Opend");

        Guid newGUID = Guid.NewGuid();

        string insertQuery = "Update EconomyCoins Set Coins = Coins - 1000 where Username = '" + author + "';";
        SqlCommand com = new SqlCommand(insertQuery, cnn);
        com.ExecuteNonQuery();

        cnn.Close();
        Console.WriteLine("Connection Closed\n");


        await ReplyAsync("Du hesch de name erwähnt und drum 1000 Coins verlore");
    }
    [Command("balance")]
    public async Task balance()
    {
        int coins = 0;
        string coiins = "";
        string author = Context.Message.Author.Username;

        string connetionString;
        SqlConnection cnn;
        connetionString = @"Data Source";
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

        cnn.Close();
        Console.WriteLine("Connection Closed\n");

        await ReplyAsync(Context.Message.Author.Mention + " Du hesch " + coins + " Coins");
    }
   
    [Command("rps")]
    public async Task rps(string choice)
    {
        Random rnd = new Random();
        int rps = rnd.Next(1, 4);
        string chos = "";
        string chospc = "";

        if (choice.Contains("paper"))
        {
            chos = "paper";
        }
        if (choice.Contains("rock"))
        {
            chos = "rock";
        }
        if (choice.Contains("scissor"))
        {
            chos = "scissor";
        }

        switch (rps)
        {
            case 1:
                chospc = "rock";
                break;
            case 2:
                chospc = "paper";
                break;
            case 3:
                chospc = "scissor";
                break;
        }


        if (chos == "rock")
        {
            if (chospc == "rock")
            {
                await ReplyAsync("Es isch unentschide");
            }
            if (chospc == "paper")
            {
                await ReplyAsync("Du hesch verlore");
                verlore();
            }
            if (chospc == "scissor")
            {
                await ReplyAsync("Du hesch gunne");
                gunne();
            }
        }
        if (chos == "paper")
        {
            if (chospc == "rock")
            {
                await ReplyAsync("Du hesch gunne");
                gunne();
            }
            if (chospc == "paper")
            {
                await ReplyAsync("Es isch unentschide");
            }
            if (chospc == "scissor")
            {
                await ReplyAsync("Du hesch verlore");
                verlore();
            }
        }
        if (chos == "scissor")
        {
            if (chospc == "rock")
            {
                await ReplyAsync("Du hesch verlore");
                verlore();
            }
            if (chospc == "paper")
            {
                await ReplyAsync("Es isch gunne");
                gunne();
            }
            if (chospc == "scissor")
            {
                await ReplyAsync("Es isch unentschide");
            }
        }
        await ReplyAsync("Du hesch " + chos + " gno und de pc het " + chospc + " gno");

        void gunne()
        {
            Random rnd = new Random();
            int cns = rnd.Next(1, 11);
            string author = Context.User.Username;
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            Console.WriteLine("Connection Open");

            Guid newGUID = Guid.NewGuid();

            string insertQuery = "Update EconomyCoins Set Coins = Coins + " + cns + "; ";
            SqlCommand com = new SqlCommand(insertQuery, cnn);
            com.ExecuteNonQuery();

            cnn.Close();


        }
        void verlore()
        {
            Random rnd = new Random();
            int cns = rnd.Next(1, 6);
            string author = Context.User.Username;
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            Console.WriteLine("Connection Open");

            Guid newGUID = Guid.NewGuid();

            string insertQuery = "Update EconomyCoins Set Coins = Coins - " + cns + "; ";
            SqlCommand com = new SqlCommand(insertQuery, cnn);
            com.ExecuteNonQuery();

            cnn.Close();

        }
    }
    [Command("share")]
    public async Task share(int amnt, string rcver)
    {
        await ReplyAsync("Du hesch " + amnt + " Coins am " + rcver + " gschenkt");
        string author = Context.User.Username;
        string connetionString;
        SqlConnection cnn;
        connetionString = @"Data Source";
        cnn = new SqlConnection(connetionString);
        cnn.Open();
        Console.WriteLine("Connection Open");

        Guid newGUID = Guid.NewGuid();


        string insertQuery = "Update EconomyCoins Set Coins = Coins + " + amnt + " Where Username ='" + rcver + "';Update EconomyCoins Set Coins = Coins -" + amnt + " where Username = '" + author + "';";
        SqlCommand com = new SqlCommand(insertQuery, cnn);



        com.ExecuteNonQuery();
        cnn.Close();
        Console.WriteLine("Connection Closed");
    }
    [Command("query")]
    public async Task query(string set, string table, string op, string ding, int amnt, string user)
    {

        string author = Context.User.Username;


        if (author == "slaxeea")
        {

            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            Console.WriteLine("Connection Open");

            Guid newGUID = Guid.NewGuid();


            string insertQuery = "Update " + table + " Set " + ding + " = " + ding + " " + op + " " + amnt + " Where Username ='" + user + "';";
            SqlCommand com = new SqlCommand(insertQuery, cnn);



            com.ExecuteNonQuery();
            cnn.Close();
            Console.WriteLine("Connection Closed\n");
            await ReplyAsync(insertQuery);



        }
        else
        {
            await ReplyAsync("Du hesch kei Admin berechtigung");
        }
    }
    [Command("read")]
    public async Task read(string table, string ding, string user)
    {
        string author = Context.User.Username;


        if (author == "slaxeea")
        {
            int coins; string coiins;
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            Console.WriteLine("Connection Opend");

            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("Select " + ding + " from " + table + " Where Username = '" + user + "'; ", cnn);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    //Console.WriteLine(myReader["Coins"].ToString());
                    coiins = myReader[ding].ToString();
                    coins = Convert.ToInt32(coiins);
                    await ReplyAsync(ding + " isch: " + coiins);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            cnn.Close();
            Console.WriteLine("Connection Closed\n");
        }
        else
        {
            await ReplyAsync("Du hesch kei Admin berechtigung");
        }
    }

    [Command("admin coins")]
    public async Task admin(string user, string ding, int amnt)
    {
        string author = Convert.ToString(Context.Message.Author);

        if (author == "slaxeea#3646")
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source";
            cnn = new SqlConnection(connetionString);
            string insertQuery = "";
            cnn.Open();

            switch (ding)
            {
                case "plus":
                    insertQuery = "Update EconomyCoins Set Coins = Coins +" + +amnt + " where Username = '" + user + "';";
                    break;
                case "minus":
                    insertQuery = "Update EconomyCoins Set Coins = Coins - " + +amnt + " where Username = '" + user + "';";
                    break;
                case "mal":
                    insertQuery = "Update EconomyCoins Set Coins = Coins * " + +amnt + " where Username = '" + user + "';";
                    break;
                case "gleich":
                    insertQuery = "Update EconomyCoins Set Coins = " + +amnt + " where Username = '" + user + "';";
                    break;
                default:
                    break;
            }

            Console.WriteLine("Connection Opend");

            Guid newGUID = Guid.NewGuid();

            SqlCommand com3 = new SqlCommand(insertQuery, cnn);
            com3.ExecuteNonQuery();

            cnn.Close();
            Console.WriteLine("Connection Closed\n");

            await ReplyAsync(user + " het " + ding + " " + amnt + " Coins");

        }
        else
        {
            await ReplyAsync("Du hech kei Admin berechtigung");
        }
    }
    [Command("admin balance")]
    public async Task balance(string user)
    {
        string author = Convert.ToString(Context.Message.Author);

        if (author == "slaxeea#3646")
        {

            int coins = 0;
            string coiins = "";

            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            Console.WriteLine("Connection Opend");

            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("Select Coins from EconomyCoins Where Username = '" + user + "'; ", cnn);
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

            cnn.Close();
            Console.WriteLine("Connection Closed\n");

            await ReplyAsync(user + " het " + coins + " Coins");
        }
        else
        {
            await ReplyAsync("Du hesch kei Admin berechtigung");
        }

    }
}
