﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace WebApplication2
{
    public class TaxFreeContext : DbContext
    {
        public DbSet<TaxFree> TaxFrees { get; set; }

        public TaxFreeContext(DbContextOptions<TaxFreeContext> options) : base() {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;User Id=postgres;Password=1111;Database=TaxFree");

    }
}