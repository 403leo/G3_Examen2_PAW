using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PAWMarte.Models;

namespace PAWMartes.Models
{
    public class PAWContext : DbContext
    {

        public PAWContext(DbContextOptions<PAWContext> options) : base(options)
        {
        }
        // Tablas o las entidades
        public DbSet<Evento> Evento { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Asistente> Asistente { get; set; }
        public DbSet<Categoria> Categoria { get; set; }

        // Sobre escribir el evento para modificar la creacion de la instancia y sus propiedades
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Llamar al metodo de la clase base
            modelBuilder.Entity<Evento>(Evento =>
            {
                // Evento.HasKey(e => new {e.Id, e.Nombre});
                Evento.HasKey(e => e.Id);
                Evento.Property(n => n.Titulo).IsRequired().HasMaxLength(200).IsUnicode(true);
                Evento.Property(f => f.Fecha).IsRequired().HasColumnName("FechaEvento");
                Evento.Property(h => h.Hora).IsRequired();
            });

            // Se configura la tabla de Usuario
            modelBuilder.Entity<Usuario>(Usuario =>
            {
                Usuario.HasKey(e => e.Id);
                Usuario.Property(n => n.Nombre).HasMaxLength(250).IsRequired();
                Usuario.Property(c => c.Correo).IsRequired().HasMaxLength(300).IsUnicode(true);
                Usuario.Property(c => c.Contraseña).HasMaxLength(150);
            });

            modelBuilder.Entity<Categoria>(Reserva =>
            {
                Reserva.HasKey(e => e.Id);
                Reserva.Property(n => n.Nombre).HasMaxLength(250).IsRequired();
            });

            modelBuilder.Entity<Categoria>(Reserva =>
            {
                Reserva.HasKey(e => e.Id);
                Reserva.Property(n => n.Nombre).HasMaxLength(250).IsRequired();
            });

            // Relaciones ASISTENTE 
            modelBuilder.Entity<Asistente>().HasOne(x => x.Evento).WithMany(e => e.Asistentes).HasForeignKey(r => r.EventoId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Asistente>().HasOne(x => x.Usuario).WithMany(e => e.Asistentes).HasForeignKey(r => r.UsuarioId).OnDelete(DeleteBehavior.NoAction);

            // Relaciones CATEGORIA
            modelBuilder.Entity<Categoria>().HasOne(x => x.Usuario).WithMany(e => e.Categorias).HasForeignKey(r => r.UsuarioId).OnDelete(DeleteBehavior.NoAction);

            // Relaciones EVENTO
            modelBuilder.Entity<Evento>().HasOne(x => x.Categoria).WithMany(e => e.Eventos).HasForeignKey(r => r.CategoriaId).OnDelete(DeleteBehavior.NoAction);

        }

        public async Task<bool> LoginUsuario(string Usuario, string contraseña)
        {
            var Exitos = new SqlParameter() { 
                ParameterName = "@Exitos",
                SqlDbType = System.Data.SqlDbType.Bit,
                Direction = System.Data.ParameterDirection.Output
            };
            int f = await Database.ExecuteSqlRawAsync("sp_login @User, @Pass, @Exitos OUTPUT",
                new SqlParameter("@User", Usuario),
                new SqlParameter("@Pass", contraseña),
                Exitos);
            return (bool)Exitos.Value;
        }
        // Me puede salir un usuario nulo y por ende es imporante el ? para que no me de error
        public async Task<Usuario?> ObtenerUsuario(string Usu, string Contraseña)
        {
            // Lo primero es recibir lo que el metodo en el sql me regresa
            var LUsuario = await Usuario.FromSqlRaw("sp_ObtenerUsuario @User, @Contraseña",
                new SqlParameter("@User", Usu),
                new SqlParameter("@Contraseña", Contraseña)).ToListAsync();

            // Me devuelve un usuario o un usuario nulo
            return LUsuario.FirstOrDefault();


        }
    }
}
