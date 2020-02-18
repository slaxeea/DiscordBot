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


/*
 * Datei:                TestCorina02VS/Program.cs
 * Author:               Simon Kappeler
 * Datum:                18.2.2020
 * Beschreibung:         Ein Bot der auf Discord auf Nachrichten automatisch 
 *                       antwortet.
 */


namespace TestCorina02
{
    class Program
    {

        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            string token = "/*Hier Bot Token einfügen, den ich nicht auf Github veröffentlichen darf*/";
            _client.Log += _client_Log;
            _client.UserJoined += AnnounceJoindeUser; //Hook into the UserJoined event of the client.

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, token);

            await _client.StartAsync();

            await Task.Delay(-1);

        }

        private Task _client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            if (message.Author.IsBot) return;

            int argPos = 0;
            if (message.HasStringPrefix("corina ", ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
            }
        }

        public async Task AnnounceJoindeUser(SocketGuildUser user)
        {

            var guild = user.Guild;
            var channel = guild.DefaultChannel;
            Console.WriteLine("En neue user isch joint, er heisst " + user);
            await channel.SendMessageAsync($"Willkomme uf de Server, {user.Mention}");
            string username = user.Username;


            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            Console.WriteLine("User i Database hinzuefüege");

            Guid newGUID = Guid.NewGuid();


            string insertQuery = "Insert into EconomyCoins (Coins, Username, Streak, Plus) Values (0, '" + username + "',0,0);";
            SqlCommand com = new SqlCommand(insertQuery, cnn);
            com.ExecuteNonQuery();

            insertQuery = "Insert into Items (Username, Laptop, Phone, Pc, Plane, Dildo, Playstation) Values ('" + username + "',0,0,0,0,0,0);";
            SqlCommand com2 = new SqlCommand(insertQuery, cnn);
            com2.ExecuteNonQuery();

            insertQuery = "Insert into Warmode (Username, HPMain, StrenghTank, StrenghShip, Mode, Upgradepoints)Values('" + username + "',100,20,20,'neutral',20);";
            SqlCommand com3 = new SqlCommand(insertQuery, cnn);
            com2.ExecuteNonQuery();
            cnn.Close();
        }

    }
}



