using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PartyGamesWebApp.Models
{
    public class User
    {
        public int UserID { get; set; }
        [Required]
        //[MaxLength(100, )]
        public string LoginName { get; set; }
        public string Password { get; set; }
    }
}
