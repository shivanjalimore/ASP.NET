using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CodeFirstFinal.Models
{
    public class TeacherDBContext:DbContext
    {
        public TeacherDBContext(DbContextOptions option) : base(option) //call parent call constructor with parameter -> option
        {
            
        }
        public DbSet<Teacher> Teachers { get; set; }

        /*DbSet<Teacher>: Manages Teacher entities, maps to Teachers table.
        Teachers: Property name for the entity collection.
        Purpose: Enables database operations on Teacher entities through EF Core.*/
    }
}
