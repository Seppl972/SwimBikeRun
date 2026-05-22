using Microsoft.EntityFrameworkCore;
using SwimBikeRun.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwimBikeRun.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Trainingseinheit> Trainingseinheiten { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
