using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace artf_MVC.Models;

public partial class BaseartfContext : DbContext
{
    public BaseartfContext()
    {
    }

    public BaseartfContext(DbContextOptions<BaseartfContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Canrf> Canrves { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Equiuni> Equiunis { get; set; }

    public virtual DbSet<Fabricante> Fabricantes { get; set; }

    public virtual DbSet<Insrf> Insrves { get; set; }

    public virtual DbSet<Modelo> Modelos { get; set; }

    public virtual DbSet<Modrf> Modrves { get; set; }

    public virtual DbSet<Rectrf> Rectrves { get; set; }

    public virtual DbSet<Solrf> Solrves { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //       => optionsBuilder.UseMySQL("Server=localhost;Database=baseartf;User=root;Password=;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Canrf>(entity =>
        {
            entity.HasKey(e => e.Idcan).HasName("PRIMARY");

            entity.ToTable("canrf");

            entity.HasIndex(e => e.Idmodcan, "idmodcan");

            entity.HasIndex(e => e.Idusercan, "idusercan");

            entity.Property(e => e.Idcan).HasColumnName("idcan");
            entity.Property(e => e.Clavecan)
                .HasMaxLength(100)
                .HasColumnName("clavecan");
            entity.Property(e => e.Fechacan)
                .HasColumnType("date")
                .HasColumnName("fechacan");
            entity.Property(e => e.Fechaofcan)
                .HasColumnType("date")
                .HasColumnName("fechaofcan");
            entity.Property(e => e.Fichacan)
                .HasColumnType("blob")
                .HasColumnName("fichacan");
            entity.Property(e => e.Idmodcan).HasColumnName("idmodcan");
            entity.Property(e => e.Idusercan).HasColumnName("idusercan");
            entity.Property(e => e.Juscan)
                .HasColumnType("text")
                .HasColumnName("juscan");
            entity.Property(e => e.Numacuofcan)
                .HasMaxLength(30)
                .HasColumnName("numacuofcan");
            entity.Property(e => e.Obscan)
                .HasColumnType("text")
                .HasColumnName("obscan");

            entity.HasOne(d => d.IdmodcanNavigation).WithMany(p => p.Canrves)
                .HasForeignKey(d => d.Idmodcan)
                .HasConstraintName("canrf_ibfk_1");

            entity.HasOne(d => d.IdusercanNavigation).WithMany(p => p.Canrves)
                .HasForeignKey(d => d.Idusercan)
                .HasConstraintName("canrf_ibfk_2");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.Idempre).HasName("PRIMARY");

            entity.ToTable("empresa");

            entity.Property(e => e.Idempre).HasColumnName("idempre");
            entity.Property(e => e.Dirempre)
                .HasMaxLength(355)
                .HasColumnName("dirempre");
            entity.Property(e => e.Rsempre)
                .HasMaxLength(255)
                .HasColumnName("rsempre");
            entity.Property(e => e.Tipoempre)
                .HasColumnType("enum('asignatario','particular','concesionario','permisionario')")
                .HasColumnName("tipoempre");
        });

        modelBuilder.Entity<Equiuni>(entity =>
        {
            entity.HasKey(e => e.Idequi).HasName("PRIMARY");

            entity.ToTable("equiuni");

            entity.HasIndex(e => e.Idcanequi, "idcanequi");

            entity.HasIndex(e => e.Idempreequi, "idempreequi");

            entity.HasIndex(e => e.Idfabequi, "idfabequi");

            entity.HasIndex(e => e.Idinsequi, "idinsequi");

            entity.HasIndex(e => e.Idmodeequi, "idmodeequi");

            entity.HasIndex(e => e.Idmodequi, "idmodequi");

            entity.HasIndex(e => e.Idrectequi, "idrectequi");

            entity.HasIndex(e => e.Idsolequi, "idsolequi");

            entity.Property(e => e.Idequi).HasColumnName("idequi");
            entity.Property(e => e.Combuequi)
                .HasColumnType("enum('Diesel','Gasolina','Electrico')")
                .HasColumnName("combuequi");
            entity.Property(e => e.Fcons)
                .HasColumnType("year")
                .HasColumnName("fcons");
            entity.Property(e => e.Fcontra)
                .HasColumnType("date")
                .HasColumnName("fcontra");
            entity.Property(e => e.Fecharequi)
                .HasColumnType("date")
                .HasColumnName("fecharequi");
            entity.Property(e => e.Fichaequi)
                .HasColumnType("blob")
                .HasColumnName("fichaequi");
            entity.Property(e => e.Graequi)
                .HasColumnType("enum('Si','No')")
                .HasColumnName("graequi");
            entity.Property(e => e.Idcanequi).HasColumnName("idcanequi");
            entity.Property(e => e.Idempreequi).HasColumnName("idempreequi");
            entity.Property(e => e.Idfabequi).HasColumnName("idfabequi");
            entity.Property(e => e.Idinsequi).HasColumnName("idinsequi");
            entity.Property(e => e.Idmodeequi).HasColumnName("idmodeequi");
            entity.Property(e => e.Idmodequi).HasColumnName("idmodequi");
            entity.Property(e => e.Idrectequi).HasColumnName("idrectequi");
            entity.Property(e => e.Idsolequi).HasColumnName("idsolequi");
            entity.Property(e => e.Modaequi)
                .HasColumnType("enum('Arrastre','Tractivo','Trabajo')")
                .HasColumnName("modaequi");
            entity.Property(e => e.Monrent)
                .HasColumnType("enum('MXN','USD')")
                .HasColumnName("monrent");
            entity.Property(e => e.Mrent).HasColumnName("mrent");
            entity.Property(e => e.Nofact)
                .HasMaxLength(50)
                .HasColumnName("nofact");
            entity.Property(e => e.Nserie)
                .HasMaxLength(50)
                .HasColumnName("nserie");
            entity.Property(e => e.Obsarre)
                .HasColumnType("text")
                .HasColumnName("obsarre");
            entity.Property(e => e.Obsequi)
                .HasColumnType("text")
                .HasColumnName("obsequi");
            entity.Property(e => e.Obsgra)
                .HasColumnType("text")
                .HasColumnName("obsgra");
            entity.Property(e => e.Pequi).HasColumnName("pequi");
            entity.Property(e => e.Regiequi)
                .HasColumnType("enum('Arrendado','Propio')")
                .HasColumnName("regiequi");
            entity.Property(e => e.Tcontra)
                .HasMaxLength(50)
                .HasColumnName("tcontra");
            entity.Property(e => e.Tipequi)
                .HasColumnType("enum('Coche','Coche domo/comedor','Coche terraza','Coche bar','Furgon','Gondola','Plataforma','S169','Tolva','Locomotora','Locomotora-AC','Locomotora-DC','Locomotora-EVO')")
                .HasColumnName("tipequi");
            entity.Property(e => e.Usoequi)
                .HasColumnType("enum('Carga','Pasajeros','Patio','Mixto')")
                .HasColumnName("usoequi");
            entity.Property(e => e.Vcontra)
                .HasMaxLength(30)
                .HasColumnName("vcontra");

            entity.HasOne(d => d.IdcanequiNavigation).WithMany(p => p.Equiunis)
                .HasForeignKey(d => d.Idcanequi)
                .HasConstraintName("equiuni_ibfk_8");

            entity.HasOne(d => d.IdempreequiNavigation).WithMany(p => p.Equiunis)
                .HasForeignKey(d => d.Idempreequi)
                .HasConstraintName("equiuni_ibfk_1");

            entity.HasOne(d => d.IdfabequiNavigation).WithMany(p => p.Equiunis)
                .HasForeignKey(d => d.Idfabequi)
                .HasConstraintName("equiuni_ibfk_2");

            entity.HasOne(d => d.IdinsequiNavigation).WithMany(p => p.Equiunis)
                .HasForeignKey(d => d.Idinsequi)
                .HasConstraintName("equiuni_ibfk_5");

            entity.HasOne(d => d.IdmodeequiNavigation).WithMany(p => p.Equiunis)
                .HasForeignKey(d => d.Idmodeequi)
                .HasConstraintName("equiuni_ibfk_3");

            entity.HasOne(d => d.IdmodequiNavigation).WithMany(p => p.Equiunis)
                .HasForeignKey(d => d.Idmodequi)
                .HasConstraintName("equiuni_ibfk_7");

            entity.HasOne(d => d.IdrectequiNavigation).WithMany(p => p.Equiunis)
                .HasForeignKey(d => d.Idrectequi)
                .HasConstraintName("equiuni_ibfk_6");

            entity.HasOne(d => d.IdsolequiNavigation).WithMany(p => p.Equiunis)
                .HasForeignKey(d => d.Idsolequi)
                .HasConstraintName("equiuni_ibfk_4");
        });

        modelBuilder.Entity<Fabricante>(entity =>
        {
            entity.HasKey(e => e.Idfab).HasName("PRIMARY");

            entity.ToTable("fabricante");

            entity.Property(e => e.Idfab).HasColumnName("idfab");
            entity.Property(e => e.Marfab)
                .HasMaxLength(100)
                .HasColumnName("marfab");
            entity.Property(e => e.Rsfab)
                .HasMaxLength(100)
                .HasColumnName("rsfab");
        });

        modelBuilder.Entity<Insrf>(entity =>
        {
            entity.HasKey(e => e.Idins).HasName("PRIMARY");

            entity.ToTable("insrf");

            entity.HasIndex(e => e.Idempins, "idempins");

            entity.HasIndex(e => e.Idsolins, "idsolins");

            entity.HasIndex(e => e.Iduserins, "iduserins");

            entity.Property(e => e.Idins).HasColumnName("idins");
            entity.Property(e => e.Docins)
                .HasColumnType("blob")
                .HasColumnName("docins");
            entity.Property(e => e.Fecapins)
                .HasColumnType("date")
                .HasColumnName("fecapins");
            entity.Property(e => e.Idempins).HasColumnName("idempins");
            entity.Property(e => e.Idsolins).HasColumnName("idsolins");
            entity.Property(e => e.Iduserins).HasColumnName("iduserins");
            entity.Property(e => e.Numacuofins)
                .HasMaxLength(30)
                .HasColumnName("numacuofins");
            entity.Property(e => e.Obsins)
                .HasMaxLength(200)
                .HasColumnName("obsins");

            entity.HasOne(d => d.IdempinsNavigation).WithMany(p => p.Insrves)
                .HasForeignKey(d => d.Idempins)
                .HasConstraintName("insrf_ibfk_1");

            entity.HasOne(d => d.IdsolinsNavigation).WithMany(p => p.Insrves)
                .HasForeignKey(d => d.Idsolins)
                .HasConstraintName("insrf_ibfk_2");

            entity.HasOne(d => d.IduserinsNavigation).WithMany(p => p.Insrves)
                .HasForeignKey(d => d.Iduserins)
                .HasConstraintName("insrf_ibfk_3");
        });

        modelBuilder.Entity<Modelo>(entity =>
        {
            entity.HasKey(e => e.Idmod).HasName("PRIMARY");

            entity.ToTable("modelo");

            entity.Property(e => e.Idmod).HasColumnName("idmod");
            entity.Property(e => e.Modequi)
                .HasMaxLength(50)
                .HasColumnName("modequi");
        });

        modelBuilder.Entity<Modrf>(entity =>
        {
            entity.HasKey(e => e.Idmod).HasName("PRIMARY");

            entity.ToTable("modrf");

            entity.HasIndex(e => e.Idrectmod, "idrectmod");

            entity.HasIndex(e => e.Idusermod, "idusermod");

            entity.Property(e => e.Idmod).HasColumnName("idmod");
            entity.Property(e => e.Acumod)
                .HasColumnType("blob")
                .HasColumnName("acumod");
            entity.Property(e => e.Clavemod)
                .HasMaxLength(100)
                .HasColumnName("clavemod");
            entity.Property(e => e.Desmod)
                .HasColumnType("text")
                .HasColumnName("desmod");
            entity.Property(e => e.Fechamod)
                .HasColumnType("date")
                .HasColumnName("fechamod");
            entity.Property(e => e.Fichatecmod)
                .HasColumnType("blob")
                .HasColumnName("fichatecmod");
            entity.Property(e => e.Idrectmod).HasColumnName("idrectmod");
            entity.Property(e => e.Idusermod).HasColumnName("idusermod");
            entity.Property(e => e.Numacuofmod)
                .HasMaxLength(30)
                .HasColumnName("numacuofmod");
            entity.Property(e => e.Obsmod)
                .HasColumnType("text")
                .HasColumnName("obsmod");

            entity.HasOne(d => d.IdrectmodNavigation).WithMany(p => p.Modrves)
                .HasForeignKey(d => d.Idrectmod)
                .HasConstraintName("modrf_ibfk_1");

            entity.HasOne(d => d.IdusermodNavigation).WithMany(p => p.Modrves)
                .HasForeignKey(d => d.Idusermod)
                .HasConstraintName("modrf_ibfk_2");
        });

        modelBuilder.Entity<Rectrf>(entity =>
        {
            entity.HasKey(e => e.Idrect).HasName("PRIMARY");

            entity.ToTable("rectrf");

            entity.HasIndex(e => e.Idinsrect, "idinsrect");

            entity.HasIndex(e => e.Iduserrect, "iduserrect");

            entity.Property(e => e.Idrect).HasColumnName("idrect");
            entity.Property(e => e.Acurect)
                .HasColumnType("blob")
                .HasColumnName("acurect");
            entity.Property(e => e.Claverect)
                .HasMaxLength(100)
                .HasColumnName("claverect");
            entity.Property(e => e.Desrect)
                .HasColumnType("text")
                .HasColumnName("desrect");
            entity.Property(e => e.Fechadocsol)
                .HasColumnType("date")
                .HasColumnName("fechadocsol");
            entity.Property(e => e.Fechamodrect)
                .HasColumnType("date")
                .HasColumnName("fechamodrect");
            entity.Property(e => e.Fecharect)
                .HasColumnType("date")
                .HasColumnName("fecharect");
            entity.Property(e => e.Fichatecrect)
                .HasColumnType("blob")
                .HasColumnName("fichatecrect");
            entity.Property(e => e.Idinsrect).HasColumnName("idinsrect");
            entity.Property(e => e.Iduserrect).HasColumnName("iduserrect");
            entity.Property(e => e.Numacuofrect)
                .HasMaxLength(30)
                .HasColumnName("numacuofrect");
            entity.Property(e => e.Numdocemp)
                .HasMaxLength(30)
                .HasColumnName("numdocemp");
            entity.Property(e => e.Obsrect)
                .HasColumnType("text")
                .HasColumnName("obsrect");

            entity.HasOne(d => d.IdinsrectNavigation).WithMany(p => p.Rectrves)
                .HasForeignKey(d => d.Idinsrect)
                .HasConstraintName("rectrf_ibfk_1");

            entity.HasOne(d => d.IduserrectNavigation).WithMany(p => p.Rectrves)
                .HasForeignKey(d => d.Iduserrect)
                .HasConstraintName("rectrf_ibfk_2");
        });

        modelBuilder.Entity<Solrf>(entity =>
        {
            entity.HasKey(e => e.Idsol).HasName("PRIMARY");

            entity.ToTable("solrf");

            entity.HasIndex(e => e.Idempsol, "idempsol");

            entity.HasIndex(e => e.Idusersol, "idusersol");

            entity.Property(e => e.Idsol).HasColumnName("idsol");
            entity.Property(e => e.Docsol)
                .HasColumnType("longblob")
                .HasColumnName("docsol");
            entity.Property(e => e.Fecapsol)
                .HasColumnType("date")
                .HasColumnName("fecapsol");
            entity.Property(e => e.Idempsol).HasColumnName("idempsol");
            entity.Property(e => e.Idusersol).HasColumnName("idusersol");
            entity.Property(e => e.Numacuofsol)
                .HasMaxLength(30)
                .HasColumnName("numacuofsol");
            entity.Property(e => e.Obssol)
                .HasMaxLength(200)
                .HasColumnName("obssol");

            entity.HasOne(d => d.IdempsolNavigation).WithMany(p => p.Solrves)
                .HasForeignKey(d => d.Idempsol)
                .HasConstraintName("solrf_ibfk_1");

            entity.HasOne(d => d.IdusersolNavigation).WithMany(p => p.Solrves)
                .HasForeignKey(d => d.Idusersol)
                .HasConstraintName("solrf_ibfk_2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.Iduser).HasColumnName("iduser");
            entity.Property(e => e.Mailuser)
                .HasColumnType("tinytext")
                .HasColumnName("mailuser");
            entity.Property(e => e.Nomuser)
                .HasMaxLength(25)
                .HasColumnName("nomuser");
            entity.Property(e => e.Passuser).HasColumnName("passuser");
            entity.Property(e => e.Pauser)
                .HasMaxLength(25)
                .HasColumnName("pauser");
            entity.Property(e => e.Sauser)
                .HasMaxLength(25)
                .HasColumnName("sauser");
            entity.Property(e => e.Tipouser)
                .HasColumnType("enum('admin','second')")
                .HasColumnName("tipouser");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
