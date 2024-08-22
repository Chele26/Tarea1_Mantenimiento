using MANTENIMIENTO_B3.Models;
using Microsoft.EntityFrameworkCore;


namespace MANTENIMIENTO_B3.Data
{
    public class AplicationDBContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<TypeEmployee> TypeEmployees { get; set; }

        public AplicationDBContext(DbContextOptions<AplicationDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la entidad Employee
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee"); // Nombre de la tabla en la base de datos
                entity.HasKey(e => e.Id);   // Clave primaria
            });

            // Configuración de la entidad TypeEmployee
            modelBuilder.Entity<TypeEmployee>(entity =>
            {
                entity.ToTable("TypeEmployee"); // Nombre de la tabla en la base de datos
                entity.HasKey(e => e.Id);   // Clave primaria
            });
        }
    }
}
