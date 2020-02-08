using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot1.Modules
{

    public class Commands : ModuleBase<SocketCommandContext>
    {

        string jokeoftd = "https://www.reddit.com/r/Jokes/comments/erhii3/when_i_was_younger_the_local_priest_told_me_that/";

        [Command("Hi")]
        public async Task Hi()
        {
            await ReplyAsync("Hallo. wenn du Hilf bruchsch gibsch ih: corina help. Aber jetzt herzlich willkomme uf dem Server");
        }
        [Command("help")]
        public async Task help()
        {
            await ReplyAsync("Du gisch eifach ih corina und denn was du wotsch mache. Zum Bispil: \ncorina wer isch de robin\ncorina explode. Für alli Commands gib corina [commands] i.");
        }
        [Command("wer isch de robin")]
        public async Task robin()
        {
            await ReplyAsync("@TryFresco Er isch mis ein und alles...");
        }
        [Command("explode")]
        public async Task explode()
        {
            await ReplyAsync("https://gph.is/1uEIQsP");
        }
        [Command("commands")]
        public async Task commands()
        {
            await ReplyAsync("En übersicht was es für Commands git:\ncorina help\ncorina explode\ncorina Hi\ncorina robin\ncorina commands\ncorina warmode\ncorina mikedrop\ncorina wer bin ich\ncorina wele tag isch hüt\ncorina gay");
        }

        [Command("mikedrop")]
        public async Task mikedrop()
        {
            await ReplyAsync("https://gph.is/g/E06DJ3y");

        }
        [Command("joke")]
        public async Task joke()
        {
            await ReplyAsync("Das isch de hütig witz:\n" + jokeoftd + "");

        }
        [Command("wer bin ich")]
        public async Task werbinich()
        {
            string author = Context.Message.Author.Username;
            await ReplyAsync(author);
        }
        [Command("wele tag isch hüt")]
        public async Task tag()
        {
            string currentDate = DateTime.Now.ToString("dd.MM.yyyy");
            await ReplyAsync(currentDate);
        }
        [Command("figg mich")]
        public async Task figgmich()
        {
            await ReplyAsync("Hm okei aber de robin isch zerscht");
        }
        [Command("wie gay bin ich")]
        public async Task gay()
        {
            string user = Context.Message.Author.Username;

            if (user == "TryFresco")
            {
                Random rnd = new Random();
                int prz = rnd.Next(75, 100);
                string gay = Convert.ToString(prz);
                await ReplyAsync("Du bisch " + prz + "% Gayyyy");
            }
            else if (user == "anusfister42069")
            {
                await ReplyAsync("Ich bruch das jetzt als reminder das du mit de Leticia an Kantiball hesch welle gah");
            }
            else
            {
                Random rnd = new Random();
                int prz = rnd.Next(1, 20);
                string gay = Convert.ToString(prz);
                await ReplyAsync("Du bisch " + prz + "% Gayyyy");
            }
        }
        [Command("spam")]
        public async Task spam()
        {
            string antwort;

            Console.WriteLine("Söll ich spame?");
            antwort = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Und wievil nachrichte?");
            int eing = Convert.ToInt32(Console.ReadLine());
            int nchr = 0;
            switch (antwort)
            {
                case "ja":
                    await ReplyAsync("Spam activated");
                    Console.WriteLine("Oke, spam activated");
                    while (nchr < eing)
                    {
                        await ReplyAsync("Moin");
                        nchr++;
                    }
                    break;

                case "nei":
                    Console.WriteLine("Oke, programm gstoppt");
                    break;
            }
        }
        [Command("content")]
        public async Task content()
        {

            string antwort = Convert.ToString(Context.Message.Content);
            await ReplyAsync(antwort);

        }
        [Command("i welem channel bin ich")]
        public async Task channel()
        {
            string channel = Convert.ToString(Context.Message.Channel);
            await ReplyAsync(Context.Message.Author.Mention + " Du bisch im " + channel + " Channel");
        }
        [Command("säg")]
        public async Task säg(string antwort)
        {
            await Task.Delay(1000);
            await ReplyAsync(antwort);
        }

        [Command("delete")]
        public async Task lka()
        {
            await Task.Delay(1500);
            await Context.Message.DeleteAsync();
        }
        [Command("nudes")]
        public async Task nudes()
        {

            await ReplyAsync("https://pornhub.com");
        }
        [Command("addier")]
        public async Task add(double zahl1, double zahl2)
        {
            try
            {
                double antw = zahl1 + zahl2;

                string antwort = Convert.ToString(antw);

                await ReplyAsync(antwort);
            }
            catch
            {
                await ReplyAsync("Die Zahle sind z gross oder z komisch");
            }

        }
        [Command("subtrahier")]
        public async Task minsu(double zahl1, double zahl2)
        {
            double antw = zahl1 - zahl2;

            string antwort = Convert.ToString(antw);

            await ReplyAsync(antwort);

        }
        [Command("dividier")]
        public async Task dividier(double zahl1, double zahl2)
        {
            double antw = zahl1 / zahl2;

            string antwort = Convert.ToString(antw);

            await ReplyAsync(antwort);

        }
        [Command("multiplizier")]
        public async Task mal(double zahl1, double zahl2)
        {
            double antw = zahl1 * zahl2;

            string antwort = Convert.ToString(antw);

            await ReplyAsync(antwort);

        }
        [Command("pass")]
        public async Task add(string antw)
        {

            {
                string user = Context.Message.Author.Username;
                string masterno;

                await ReplyAsync("Hi, " + user + " wotsch du es Gheimniss wüsse?");
                System.Threading.Thread.Sleep(2000);

                await Task.Delay(500);

                Console.WriteLine("De " + user + " will s gheminiss wüsse. ja oder nei?" + antw);

                masterno = Convert.ToString(Console.ReadLine());

                switch (antw)
                {
                    case "ja":
                        await ReplyAsync("De Admind het dini Request übercho");
                        break;
                    case "nei":
                        await ReplyAsync("Ok denn halt ned");
                        break;
                    default:
                        break;
                }

                if (masterno == "ja")
                {
                    await ReplyAsync("Youre Request got accepted");
                    System.Threading.Thread.Sleep(1000);

                    await ReplyAsync("Youre pass phrase is ROBINISCHGAY delete that message now or someone else will get it");
                    await ReplyAsync("here the link for you https://onetimesecret.com/secret/bo806saph9gdgeygkchs3zh17m6vbjr");

                }
                else if (masterno == "nei")
                {
                    await ReplyAsync("Request got denied");
                }
                else
                {
                    await ReplyAsync("Öppis het ned funktioniert");
                }


            }
        }
    }
}
