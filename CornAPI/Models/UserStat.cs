using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CornAPI.Models
{
    public class UserStat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public int UserId { get; set; }
        public int? Rains { get; set; }
        public int? Rainedon { get; set; }
        public int? Tips { get; set; }
        public decimal? Tiptotal { get; set; }
        public decimal? Raintotal { get; set; }
        public decimal? Rainontotal { get; set; }
    }
}
