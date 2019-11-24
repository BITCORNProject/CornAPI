using System;
using System.Collections.Generic;

namespace CornAPI.Models
{
    public partial class UserIdentity
    {
        public int UserId { get; set; }
        public string TwitchUsername { get; set; }
        public string Auth0Nickname { get; set; }
        public string Auth0Id { get; set; }
        public string Twitchid { get; set; }
        public string Discordid { get; set; }
        public string Twitterid { get; set; }
        public string Redditid { get; set; }

        public virtual User User { get; set; }
    }
}
