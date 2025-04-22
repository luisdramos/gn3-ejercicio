using api.Models;
using Microsoft.EntityFrameworkCore;

public class DevDbContext : DbContext
{
    public DevDbContext(DbContextOptions<DevDbContext> options) : base(options) { }

    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Sueldo> Sueldos { get; set; }
    public DbSet<Departamento> Departamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.ClaveEmpleado);

            entity.Property(e => e.NombreEmpleado)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.FechaIngreso)
                  .IsRequired();

            entity.Property(e => e.FechaNacimiento)
                  .IsRequired();

            entity.HasOne(e => e.Departamento)
                  .WithMany(d => d.Empleados)
                  .HasForeignKey(e => e.ClaveDepartamento);
        });

        // Sueldos
        _ = modelBuilder.Entity<Sueldo>(entity =>
        {
            entity.HasKey(s => s.ClaveEmpleado);

            entity.Property(s => s.SueldoMensual)
                  .HasColumnType("decimal(10,2)")
                  .IsRequired();

            entity.Property(s => s.FormaPago)
                  .HasMaxLength(20);

            entity.HasCheckConstraint("CK_Sueldos_FormaPago", "[FormaPago] IN ('Efectivo', 'Transferencia')");

            entity.HasOne(s => s.Empleado)
                  .WithOne(e => e.Sueldo)
                  .HasForeignKey<Sueldo>(s => s.ClaveEmpleado);
        });

        // Departamentos
        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(d => d.ClaveDepartamento);

            entity.Property(d => d.Descripcion)
                  .IsRequired()
                  .HasMaxLength(100);
        });


    }
}
