namespace MyLottoCheck.Models
{
    using System.Data.Entity;

    public class MyLottoCheckModels : DbContext
    {
        public MyLottoCheckModels() : base("name=DefaultConnection")
            
        {
            Database.CommandTimeout = 600;
        }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<CaliforniaMegaMillionUserPick> CaliforniaMegaMillionUserPicks { get; set; }
        public virtual DbSet<CaliforniaMegaMillionsAllWinningNumberAndPrize> CaliforniaMegaMillionsAllWinningNumbersAndPrizes { get; set; }
        public virtual DbSet<CaliforniaMegaMillionsAllWinningNumber> CaliforniaMegaMillionsAllWinningNumbers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.MegaMillionPicks)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
