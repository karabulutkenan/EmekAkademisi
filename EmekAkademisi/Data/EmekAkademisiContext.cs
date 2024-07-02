using EmekAkademisi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace EmekAkademisi.Data
{
    public class EmekAkademisiContext : DbContext
    {
        public EmekAkademisiContext(DbContextOptions<EmekAkademisiContext> options)
            : base(options)
        {
        }
        public DbSet<Question>? Questions { get; set; }
        public DbSet<Regulation>? Regulations { get; set; }



        // Burada veritabanı tablolarınız için DbSet'ler ekleyebilirsiniz.
        // Örneğin:
        // public DbSet<User> Users { get; set; }
    }
}
