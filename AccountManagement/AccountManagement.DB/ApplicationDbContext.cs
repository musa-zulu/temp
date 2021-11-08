using AccountManagement.DB.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AccountManagement.DB
{
    public interface IApplicationDbContext
    {
        DbSet<AccountStatus> AccountStatus { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<Person> Persons { get; set; }
        DbSet<Transaction> Transactions { get; set; }

        Task<int> SaveChangesAsync();
    }

    public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AccountStatus> AccountStatus { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AccountStatus>().ToTable("AccountStatus");
            builder.Entity<AccountStatus>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.AccountCode).HasColumnName("account_code");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("status")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountStatus)
                    .HasForeignKey(d => d.AccountCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountStatus_Accounts");
            });

            builder.Entity<Account>().ToTable("Accounts");
            builder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.HasIndex(e => e.AccountNumber, "IX_Account_num")
                    .IsUnique();

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("account_number");

                entity.Property(e => e.OutstandingBalance)
                    .HasColumnType("money")
                    .HasColumnName("outstanding_balance");

                entity.Property(e => e.PersonCode).HasColumnName("person_code");

                entity.HasOne(d => d.PersonCodeNavigation)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.PersonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Person");
            });

            builder.Entity<Person>().ToTable("Persons");
            builder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.HasIndex(e => e.IdNumber, "IX_Person_id")
                    .IsUnique();

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.IdNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("id_number");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("surname");
            });

            builder.Entity<Transaction>().ToTable("Transactions");
            builder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.AccountCode).HasColumnName("account_code");

                entity.Property(e => e.Amount)
                    .HasColumnType("money")
                    .HasColumnName("amount");

                entity.Property(e => e.CaptureDate)
                    .HasColumnType("datetime")
                    .HasColumnName("capture_date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("transaction_date");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.AccountCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_Account");
            });
        }
    }
}