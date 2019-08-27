using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RPSurvey3.Models;

namespace RPSurvey3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Section> Section { get; set; }

        public DbSet<SubSection> SubSection { get; set; }

        public DbSet<Question> Question { get; set; }
    }
}