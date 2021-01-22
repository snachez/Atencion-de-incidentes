namespace Atencion_de_incidentes.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModeloUnedMS : DbContext
    {
        public ModeloUnedMS()
            : base("name=ModeloUnedMS")
        {
        }

        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<CustomPermission> CustomPermission { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<TBMCALICalificacion> TBMCALICalificacion { get; set; }
        public virtual DbSet<TBMSCATCategorias> TBMSCATCategorias { get; set; }
        public virtual DbSet<TBMSCATEstadosTickets> TBMSCATEstadosTickets { get; set; }
        public virtual DbSet<TBMSMAEBaseConoc> TBMSMAEBaseConoc { get; set; }
        public virtual DbSet<TBMSMAEEspecialidades> TBMSMAEEspecialidades { get; set; }
        public virtual DbSet<TBMSMAEPrioridades> TBMSMAEPrioridades { get; set; }
        public virtual DbSet<TBMSMAETickets> TBMSMAETickets { get; set; }
        public virtual DbSet<MenuTemp> MenuTemp { get; set; }
        public virtual DbSet<Mensajes> Mensajes { get; set; }
        public virtual DbSet<TBMSESPECIALIDADXCATEGORIA> TBMSESPECIALIDADXCATEGORIAs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TBMSCATCategorias>()
              .HasMany(e => e.TBMSESPECIALIDADXCATEGORIAs)
              .WithRequired(e => e.TBMSCATCategorias)
              .HasForeignKey(e => e.IdCategoria);

            modelBuilder.Entity<TBMSMAEEspecialidades>()
              .HasMany(e => e.TBMSESPECIALIDADXCATEGORIAs)
              .WithRequired(e => e.TBMSMAEEspecialidades)
              .HasForeignKey(e => e.IdEspecialidad);

            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.Permission)
                .WithRequired(e => e.AspNetRoles)
                .HasForeignKey(e => e.RoleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.CustomPermission)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.TBMSMAETickets)
                .WithOptional(e => e.AspNetUsers)
                .HasForeignKey(e => e.IdUsuarioTec);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.TBMSMAETickets1)
                .WithOptional(e => e.AspNetUsers1)
                .HasForeignKey(e => e.idUsuario);

modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Mensajes)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.idUsuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Menu>()
                .HasMany(e => e.CustomPermission)
                .WithRequired(e => e.Menu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Menu>()
                .HasMany(e => e.Permission)
                .WithRequired(e => e.Menu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TBMCALICalificacion>()
                .Property(e => e.comentario)
                .IsUnicode(false);

            modelBuilder.Entity<TBMSCATCategorias>()
                .Property(e => e.tipoCategoria)
                .IsUnicode(false);

            modelBuilder.Entity<TBMSCATEstadosTickets>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<TBMSMAEBaseConoc>()
                .Property(e => e.BaseDescripcion)
                .IsUnicode(false);

            modelBuilder.Entity<TBMSMAEEspecialidades>()
                .Property(e => e.tipoEspecialidad)
                .IsUnicode(false);

            modelBuilder.Entity<TBMSMAEPrioridades>()
                .Property(e => e.nombrePrioridad)
                .IsUnicode(false);

            modelBuilder.Entity<TBMSMAEPrioridades>()
                .HasMany(e => e.TBMSMAETickets)
                .WithRequired(e => e.TBMSMAEPrioridades)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TBMSMAETickets>()
                .Property(e => e.titulo)
                .IsUnicode(false);

            modelBuilder.Entity<TBMSMAETickets>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<TBMSMAETickets>()
                .Property(e => e.adjuntoTicket)
                .IsUnicode(false);

            modelBuilder.Entity<Mensajes>()
               .Property(e => e.texto)
               .IsUnicode(false);

 modelBuilder.Entity<TBMSMAETickets>()
                .HasMany(e => e.Mensajes)
                .WithRequired(e => e.TBMSMAETickets)
                .WillCascadeOnDelete(false);

        }
    }
}
