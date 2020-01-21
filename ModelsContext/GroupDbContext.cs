using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tp3_MVC.Models;

namespace Tp3_MVC.ModelsContext
{
    public class GroupDbContext : DbContext
    {
        public GroupDbContext(DbContextOptions<GroupDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Student> Students { get; set; }
    }
}
