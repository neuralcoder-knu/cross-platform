using Lab6.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Cardholder> Cardholders { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<RefCardType> RefCardTypes { get; set; }
    public DbSet<RefCurrencyCode> RefCurrencyCodes { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<CardholderBank> CardholderBanks { get; set; }
    public DbSet<Merchant> Merchants { get; set; }
    public DbSet<MerchantBank> MerchantBanks { get; set; }
    public DbSet<ATMMachine> ATMMachines { get; set; }
    public DbSet<FinancialTransaction> FinancialTransactions { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CardholderBank>()
            .HasKey(cb => cb.CardholderBankId);

        modelBuilder.Entity<CardholderBank>()
            .HasOne(cb => cb.Cardholder)
            .WithMany(ch => ch.CardholderBanks)
            .HasForeignKey(cb => cb.CardholderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CardholderBank>()
            .HasOne(cb => cb.Bank)
            .WithMany(b => b.CardholderBanks)
            .HasForeignKey(cb => cb.BankId);

        modelBuilder.Entity<MerchantBank>()
            .HasKey(mb => mb.MerchantBankId);

        modelBuilder.Entity<MerchantBank>()
            .HasOne(mb => mb.Merchant)
            .WithMany(m => m.MerchantBanks)
            .HasForeignKey(mb => mb.MerchantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MerchantBank>()
            .HasOne(mb => mb.Bank)
            .WithMany(b => b.MerchantBanks)
            .HasForeignKey(mb => mb.BankId);

        modelBuilder.Entity<Card>()
            .HasOne(c => c.Cardholder)
            .WithMany(ch => ch.Cards)
            .HasForeignKey(c => c.CardholderId);
        modelBuilder.Entity<Card>()
            .HasOne(c => c.CardType)
            .WithMany(ct => ct.Cards)
            .HasForeignKey(c => c.CardTypeCode);

        modelBuilder.Entity<Card>()
            .HasOne(c => c.Currency)
            .WithMany(rc => rc.Cards)
            .HasForeignKey(c => c.CurrencyCode);

        modelBuilder.Entity<FinancialTransaction>()
            .HasOne(ft => ft.Card)
            .WithMany()
            .HasForeignKey(ft => ft.CardNumber);

        modelBuilder.Entity<FinancialTransaction>()
            .HasOne(ft => ft.ATM)
            .WithMany(a => a.FinancialTransactions)
            .HasForeignKey(ft => ft.ATMId);

        modelBuilder.Entity<FinancialTransaction>()
            .HasOne(ft => ft.Currency)
            .WithMany(rc => rc.FinancialTransactions)
            .HasForeignKey(ft => ft.CurrencyCode);

        modelBuilder.Entity<FinancialTransaction>()
            .HasOne(ft => ft.Merchant)
            .WithMany(m => m.FinancialTransactions)
            .HasForeignKey(ft => ft.MerchantId);

        modelBuilder.Entity<Card>()
            .HasIndex(c => c.CardNumber)
            .IsUnique();

        modelBuilder.Entity<Cardholder>()
            .HasIndex(ch => ch.AccountNumber)
            .IsUnique();

        modelBuilder.Entity<Cardholder>()
            .Property(ch => ch.CountryCode)
            .IsRequired();

        modelBuilder.Entity<Merchant>()
            .Property(m => m.CountryCode)
            .IsRequired();

        modelBuilder.Entity<RefCurrencyCode>().HasData(
            new RefCurrencyCode { CurrencyCode = 1, CurrencyName = "USD" },
            new RefCurrencyCode { CurrencyCode = 2, CurrencyName = "EUR" }
        );

        modelBuilder.Entity<RefCardType>().HasData(
            new RefCardType { CardTypeCode = 1, TypeName = "Credit" },
            new RefCardType { CardTypeCode = 2, TypeName = "Debit" }
        );
    }

    public void Seed()
    {
        var banks = Enumerable.Range(1, 5).Select(i => new Bank
        {
            BankId = i,
        }).ToList();

        this.Banks.AddRange(banks);

        var cardholders = Enumerable.Range(1, 5).Select(i => new Cardholder
        {
            CardholderId = i,
            AccountNumber = $"ACC_{i}",
            CountryCode = $"C{i:D3}"
        }).ToList();

        this.Cardholders.AddRange(cardholders);

        var cards = Enumerable.Range(1, 5).Select(i => new Card
        {
            CardNumber = 1000 + i,
            CardholderId = i,
            CardTypeCode = i % 2 + 1,
            CurrencyCode = i % 2 + 1
        }).ToList();

        this.Cards.AddRange(cards);
        
        var cardholderBanks = Enumerable.Range(1, 5).Select(i => new CardholderBank
        {
            CardholderBankId = i,
            CardholderId = i,
            BankId = i
        }).ToList();

        this.CardholderBanks.AddRange(cardholderBanks);

        var merchants = Enumerable.Range(1, 5).Select(i => new Merchant
        {
            MerchantId = i,
            CountryCode = $"C{i:D3}",
            MerchantCategoryCode = $"CAT_{i}"
        }).ToList();

        this.Merchants.AddRange(merchants);

        var merchantBanks = Enumerable.Range(1, 5).Select(i => new MerchantBank
        {
            MerchantBankId = i,
            MerchantId = i,
            BankId = i
        }).ToList();

        this.MerchantBanks.AddRange(merchantBanks);

        var atms = Enumerable.Range(1, 5).Select(i => new ATMMachine
        {
            ATMId = i,
            Location = $"Location_{i}"
        }).ToList();

        this.ATMMachines.AddRange(atms);

        var transactions = Enumerable.Range(1, 5).Select(i => new FinancialTransaction
        {
            TransactionId = i,
            TransactionDate = GenerateRandomTransactionDate(),
            CardNumber = 1000 + i,
            CardNumberTransferFrom = (i % 2 == 0) ? 1000 + (i - 1) : 0,
            CardNumberTransferTo = (i % 2 != 0) ? 1000 + (i + 1) : 0,
            ATMId = i,
            CurrencyCode = i % 2 + 1,
            MerchantId = i,
            TransactionTypeCode = i % 2 == 0 ? "TRANSFER" : "PURCHASE"
        }).ToList();

        this.FinancialTransactions.AddRange(transactions);
        this.SaveChanges();
    }
    
    private DateTime GenerateRandomTransactionDate()
    {
        var random = new Random();

        var start = new DateTime(2024, 1, 1);
        var range = (DateTime.Today - start).Days;

        var randomDate = start.AddDays(random.Next(range));

        randomDate = randomDate.AddHours(random.Next(24));
        randomDate = randomDate.AddMinutes(random.Next(60));
        randomDate = randomDate.AddSeconds(random.Next(60));

        var ukraineTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Kiev");
        var transactionDateInUkraine = TimeZoneInfo.ConvertTimeToUtc(randomDate, ukraineTimeZone);

        return transactionDateInUkraine;
    }
}