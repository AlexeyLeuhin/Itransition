using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Fanfic.Data
{
    public class User : IdentityUser
    {
       
        public string Name { get; set; }
        public UInt16 Age { get; set; }
        public List<Tale> Tales { get; set; } = new List<Tale>();
        public string AvatarPath { get; set; }
        public List<Rating> Ratings { get; set; } = new List<Rating>();
        public List<Like> Likes { get; set; } = new List<Like>();
    }
}
