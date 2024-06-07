using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SnackisWebbAPI.Models
{
    public class SnackisUser : IdentityUser
    {
        public string ProfileImageUrl { get; set; }
    }
}