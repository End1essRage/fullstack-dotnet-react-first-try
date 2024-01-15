using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TodosBackend.Data
{
    public class TodosDbContext : DbContext
    {
        public TodosDbContext(DbContextOptions<TodosDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Todo> Todos { get; set; }
    }

}