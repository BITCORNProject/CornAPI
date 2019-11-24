using System;
using System.Collections.Generic;

namespace CornAPI.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Level { get; set; }
        public string Username { get; set; }
        public string Avatar { get; set; }

        public virtual UserIdentity UserIdentity { get; set; }
        public virtual UserStat UserStat { get; set; }
        public virtual UserWallet UserWallet { get; set; }
    }
}
