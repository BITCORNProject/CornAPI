using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CornAPI.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Auth0Id { get; set; }
        public string Auth0Nickname { get; set; }
        public string Twitchid { get; set; }
        public string Discordid { get; set; }
        public string Twitterid { get; set; }
        public string Redditid { get; set; }
        public string TwitchUsername { get; set; }
        public string Cornaddy { get; set; }
        public int Walletserver { get; set; }
        public decimal Balance { get; set; }
        public string Level { get; set; }
        public string Avatar { get; set; }
        public string Subtier { get; set; }
       
    }
}
