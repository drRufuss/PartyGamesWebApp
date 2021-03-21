using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace PartyGamesWebApp.Models
{
    public class User
    {
        public int UserID { get; set; }
        [Required]
        [MaxLength(50)]
        public string LoginName { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
