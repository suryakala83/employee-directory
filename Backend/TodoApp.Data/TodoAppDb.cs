using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data.Entities;

namespace TodoApp.Data
{
    public class TodoAppDb : IdentityDbContext
    {
        public TodoAppDb(DbContextOptions<TodoAppDb> options) : base(options) { }

        public DbSet<Item> Item { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
