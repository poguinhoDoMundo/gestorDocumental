namespace Proveedores
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BancoContext : DbContext
    {
        public BancoContext()
            : base("name=BancoContext")
        {
        }

        public virtual DbSet<ACTVIDAD_ECO> ACTVIDAD_ECO { get; set; }
        public virtual DbSet<DOCS_CARGADO> DOCS_CARGADO { get; set; }
        public virtual DbSet<DOCUMENTO> DOCUMENTO { get; set; }
        public virtual DbSet<EMPRESA> EMPRESA { get; set; }
        public virtual DbSet<EMPRESA_JUR> EMPRESA_JUR { get; set; }
        public virtual DbSet<EMPRESA_NAT> EMPRESA_NAT { get; set; }
        public virtual DbSet<ESTADO> ESTADO { get; set; }
        public virtual DbSet<PROFESION> PROFESION { get; set; }
        public virtual DbSet<REVISION> REVISION { get; set; }
        public virtual DbSet<TIPO_EMPRESA> TIPO_EMPRESA { get; set; }
        public virtual DbSet<USUARIO> USUARIO { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<ACTVIDAD_ECO>()
                .Property(e => e.ID_ACTIVIDAD)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ACTVIDAD_ECO>()
                .Property(e => e.NOM_ACTIVIDAD)
                .IsUnicode(false);

            modelBuilder.Entity<DOCS_CARGADO>()
                .Property(e => e.ID_CARGA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DOCS_CARGADO>()
                .Property(e => e.ID_DOCUMENTO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DOCS_CARGADO>()
                .Property(e => e.ID_EMPRESA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DOCS_CARGADO>()
                .Property(e => e.RUTA)
                .IsUnicode(false);

            modelBuilder.Entity<DOCS_CARGADO>()
                .Property(e => e.IP)
                .IsUnicode(false);

            modelBuilder.Entity<DOCS_CARGADO>()
                .Property(e => e.VERSION)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DOCS_CARGADO>()
                .Property(e => e.ID_ESTADO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DOCUMENTO>()
                .Property(e => e.ID_DOCUMENTO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DOCUMENTO>()
                .Property(e => e.NOM_DOCUMENTO)
                .IsUnicode(false);

            modelBuilder.Entity<DOCUMENTO>()
                .Property(e => e.ID_TIPO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<EMPRESA>()
                .Property(e => e.ID_EMPRESA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<EMPRESA>()
                .Property(e => e.NOM_EMPRESA)
                .IsUnicode(false);

            modelBuilder.Entity<EMPRESA>()
                .Property(e => e.NIT)
                .IsUnicode(false);

            modelBuilder.Entity<EMPRESA>()
                .Property(e => e.DIRECCION)
                .IsUnicode(false);

            modelBuilder.Entity<EMPRESA>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<EMPRESA>()
                .Property(e => e.ID_TIPO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<EMPRESA_JUR>()
                .Property(e => e.ID_JURIDICA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<EMPRESA_JUR>()
                .Property(e => e.ID_EMPRESA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<EMPRESA_JUR>()
                .Property(e => e.ID_ACTIVIDAD)
                .HasPrecision(38, 0);

            modelBuilder.Entity<EMPRESA_JUR>()
                .Property(e => e.NOMREPRESENTANTE)
                .IsUnicode(false);

            modelBuilder.Entity<EMPRESA_JUR>()
                .Property(e => e.CEDREPRESENTANE)
                .IsUnicode(false);

            modelBuilder.Entity<EMPRESA_JUR>()
                .Property(e => e.DIRECCIONPRINCI)
                .IsUnicode(false);

            modelBuilder.Entity<EMPRESA_NAT>()
                .Property(e => e.ID_NATURAL)
                .HasPrecision(38, 0);

            modelBuilder.Entity<EMPRESA_NAT>()
                .Property(e => e.ID_EMPRESA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<EMPRESA_NAT>()
                .Property(e => e.COPNIA)
                .IsUnicode(false);

            modelBuilder.Entity<EMPRESA_NAT>()
                .Property(e => e.LIBRETA)
                .IsUnicode(false);

            modelBuilder.Entity<EMPRESA_NAT>()
                .Property(e => e.ID_PROFESION)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ESTADO>()
                .Property(e => e.ID_ESTADO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ESTADO>()
                .Property(e => e.NOM_ESTADO)
                .IsUnicode(false);

            modelBuilder.Entity<PROFESION>()
                .Property(e => e.ID_PROFESION)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PROFESION>()
                .Property(e => e.NOM_PROFESION)
                .IsUnicode(false);

            modelBuilder.Entity<REVISION>()
                .Property(e => e.ID_REVISION)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REVISION>()
                .Property(e => e.ID_CARGA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REVISION>()
                .Property(e => e.USUARIO)
                .IsUnicode(false);

            modelBuilder.Entity<REVISION>()
                .Property(e => e.ID_ESTADO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TIPO_EMPRESA>()
                .Property(e => e.ID_TIPO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TIPO_EMPRESA>()
                .Property(e => e.NOM_TIPO)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.ID_EMPRESA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.USUARIO1)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.CLAVE)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.ID_USUARIO)
                .HasPrecision(38, 0); */
        }
    }
}
