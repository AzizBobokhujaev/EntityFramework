using HW_16_09_21.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_16_09_21.Context
{
    public class UsersDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Data source = WIN-HFC12JL6G7P\SQLEXPRESS; Initial catalog = Users; Integrated security = true");
        }
        public DbSet<Users> Users { get; set; }
    }
}
