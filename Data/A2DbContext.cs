using Microsoft.EntityFrameworkCore;
using A2Template.Models;

namespace A2Template.Data
{
    public class A2DbContext : DbContext
    {
        public A2DbContext(DbContextOptions<A2DbContext> options) : base(options) { }
        
        public DbSet<Event> Events { get; set; }
        public DbSet<Staff> Staff  { get; set; }
        public DbSet<User> Users { get; set; }
    }
}