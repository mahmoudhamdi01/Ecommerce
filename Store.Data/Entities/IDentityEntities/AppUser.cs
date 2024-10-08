﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Entities.IDentityEntities
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address address { get; set; }
    }
}
