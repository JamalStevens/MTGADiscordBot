using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity;
using Newtonsoft.Json;
using System.Threading;
using DSharpPlus.Entities;

namespace MTGADiscordBot.Commands
{
    public class CardCommands : BaseCommandModule
    {
        #region GeneratePool Command
        [Command("generatepool")]
        [Description("takes a 3 character set type to generate your uncommon and common cards (24 common, 12 common) i.e !generatepool m21")]
        public async Task GeneratePool(CommandContext ctx, string setAbbr)
        {
            List<string> cards = new List<string>();

            #region Common Cards

            await ctx.Channel
                .SendMessageAsync((":black_circle: Common Cards:").ToString())
                .ConfigureAwait(false);
            for (int x = 0; x < 24; x++)
            {
                var URI = "https://api.scryfall.com/cards/random?q=s%3A" + setAbbr + "+r%3Acommon+-t%3Abasic";
                WebClient client = new WebClient();
                string reply = client.DownloadString(URI);
                var card = JsonConvert.DeserializeObject<JsonCard>(reply);

                //add common card to array
                cards.Add("1 " + card.Cardname.ToString());

                //print card
                await ctx.Channel
                    .SendMessageAsync("1 " + card.Cardname.ToString() + " (" + card.Setabbr.ToString() + ")")
                    .ConfigureAwait(false);

                Thread.Sleep(500);

            }
            #endregion

            #region UncommonCards
            await ctx.Channel
                .SendMessageAsync((":blue_circle: Uncommon Cards:").ToString())
                .ConfigureAwait(false);

            for (int x = 0; x < 12; x++)
            {
                var URI = "https://api.scryfall.com/cards/random?q=s%3A" + setAbbr + "+r%3Auncommon+-t%3Abasic";
                WebClient client = new WebClient();
                string reply = client.DownloadString(URI);
                var card = JsonConvert.DeserializeObject<JsonCard>(reply);
                //add uncommon card to array
                cards.Add("1 " + card.Cardname.ToString());

                //print card
                await ctx.Channel
                    .SendMessageAsync("1 " + card.Cardname.ToString() + " (" + card.Setabbr.ToString() + ")")
                    .ConfigureAwait(false);

                Thread.Sleep(500);
            }
            #endregion

            #region Output
            File.WriteAllLines(ctx.User.Username.ToString() + "-cardlist.txt", cards);
            await ctx.Channel.SendFileAsync(ctx.User.Username.ToString() + "-cardlist.txt", "Card List").ConfigureAwait(false);
            File.Delete(ctx.User.Username.ToString() + "-cardlist.txt");
            #endregion
        }
        #endregion
        #region Random Rare
        public async Task RandomRare(CommandContext ctx, string setAbbr)
        {
            var URI = "https://api.scryfall.com/cards/random?q=s%3A" + setAbbr + "+r%3Acommon+-t%3Abasic";
            WebClient client = new WebClient();
            string reply = client.DownloadString(URI);
            var card = JsonConvert.DeserializeObject<JsonCard>(reply);

            await ctx.Channel
                .SendMessageAsync("1 " + card.Cardname.ToString() + " (" + card.Setabbr.ToString() + ")")
                .ConfigureAwait(false);

            Thread.Sleep(500);
        }
        #endregion
    }

}
