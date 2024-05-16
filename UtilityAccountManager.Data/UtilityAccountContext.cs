using Microsoft.EntityFrameworkCore;
using UtilityAccountManager.Data.Models;

namespace UtilityAccountManager.Data;

public class UtilityAccountContext : DbContext
{
    public UtilityAccountContext(DbContextOptions<UtilityAccountContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ResidentUtilityAccountModel>()
            .HasKey(resUtilAcc => new { resUtilAcc.ResidentId, resUtilAcc.UtilityAccountNumber });

        modelBuilder.Entity<ResidentUtilityAccountModel>()
            .HasOne(resUtilAcc => resUtilAcc.Resident)
            .WithMany(resident => resident.ResidentUtilityAccounts)
            .HasForeignKey(resUtilAcc => resUtilAcc.ResidentId);

        modelBuilder.Entity<ResidentUtilityAccountModel>()
            .HasOne(resUtilAcc => resUtilAcc.UtilityAccount)
            .WithMany(account => account.ResidentUtilityAccounts)
            .HasForeignKey(resUtilAcc => resUtilAcc.UtilityAccountNumber);

        modelBuilder.Entity<UtilityAccountModel>()
            .HasOne(account => account.Address)
            .WithMany()
            .HasForeignKey(account => account.AddressId);
    }

    public DbSet<UtilityAccountModel> UtilityAccounts { get; set; }
    public DbSet<ResidentModel> Residents { get; set; }
    public DbSet<ResidentUtilityAccountModel> ResidentUtilityAccounts { get; set; }
    public DbSet<AddressModel> Addresses { get; set; }
}
