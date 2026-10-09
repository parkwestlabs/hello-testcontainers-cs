
using Microsoft.EntityFrameworkCore;

namespace MyApp.App;

public class MyDbContext(DbContextOptions<MyDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => this.Set<User>();
}
