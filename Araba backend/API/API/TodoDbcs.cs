using Microsoft.EntityFrameworkCore;

namespace API
{
    public class TodoDbcs: DbContext
    {
        public TodoDbcs(DbContextOptions<TodoDbcs> options)
           : base(options)
        { }

        public DbSet<Todo> Todos => Set<Todo>();
    }
}
