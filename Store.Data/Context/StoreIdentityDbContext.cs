﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.Data.Entities.IDentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Context
{
    public class StoreIdentityDbContext : IdentityDbContext
    {
        public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options) : base(options)
        {

        }
    }
}
