using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public partial class DbTextingGameContext : DbContext
{
    public DbTextingGameContext()
    {
    }

    public DbTextingGameContext(DbContextOptions<DbTextingGameContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblUserDetail> TblUserDetails { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseSqlServer("Server = 65.0.181.176;Database=db_TextingGame;User Id = admin;Password = Asdf1234*;TrustServerCertificate=True;Connection Timeout=300;command timeout=300");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblUserDetail>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__tbl_User__1788CC4C676CC82F");

            entity.ToTable("tbl_UserDetails");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
