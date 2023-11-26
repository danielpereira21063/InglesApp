﻿using InglesApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace InglesApp.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Vocabulario> Vocabularios { get; set; }
    }
}