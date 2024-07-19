namespace CarRental.DataAccess
{
    public class CarRentalContext : DbContext
    {
        public CarRentalContext(DbContextOptions<CarRentalContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Uncategorized> Uncategorized => Set<Uncategorized>();
        public DbSet<SmallCar> SmallCars => Set<SmallCar>();
        public DbSet<StationWagon> StationWagons => Set<StationWagon>();
        public DbSet<Truck> Trucks => Set<Truck>();
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<ContactInfo> ContactInfos => Set<ContactInfo>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Finnish_Swedish_100_CI_AS");
            modelBuilder.Entity<Category>()
                .HasDiscriminator<string>(ColumnNames.Discriminator)
                .HasValue<Uncategorized>(nameof(CategoryNameAndDiscriminator.Uncategorized))
                .HasValue<SmallCar>(nameof(CategoryNameAndDiscriminator.SmallCar))
                .HasValue<StationWagon>(nameof(CategoryNameAndDiscriminator.StationWagon))
                .HasValue<Truck>(nameof(CategoryNameAndDiscriminator.Truck));

            modelBuilder.Entity<Category>()
                .Property(p => p.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<Category>()
                .Property(e => e.Name)
                .HasConversion<string>()
                .HasColumnName(ColumnNames.CategoryName);

            modelBuilder.Entity<Category>()
                .Property(p => p.BasePrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Category>()
                .HasMany<Vehicle>()
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<CategoryPriced>()
                .Property(e => e.BaseKmPrice).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<CategoryPriced>()
                .Property(p => p.AdditionalFee).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<CategoryFee>()
                .Property(p => p.Fee)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Vehicle>()
                .HasOne(navProp => navProp.Category)
                .WithMany(navProp => navProp.Vehicles)
                .HasForeignKey(navProp => navProp.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.TotalCost)
                    .HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Customer>()
                .HasOne(p => p.ContactInfo)
                .WithOne(p => p.Customer)
                .HasForeignKey<ContactInfo>(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Customer>(b =>
            {
                b.Property(p => p.FirstName).HasMaxLength(50).IsRequired();
                b.Property(p => p.LastName).HasMaxLength(50).IsRequired();
                b.Property(p => p.PersonalIdentityNumber).HasMaxLength(13);
                b.HasIndex(p => p.PersonalIdentityNumber).IsUnique();
            });

            modelBuilder.Entity<Vehicle>()
                .HasIndex(p => p.PlateNumber)
                .IsUnique();

            modelBuilder.Entity<ContactInfo>()
                .HasAlternateKey(p => p.EmailAddress);

            modelBuilder.Entity<Uncategorized>()
                .HasData(
                    new Uncategorized
                    {
                        Id = 1,
                        Name = CategoryNameAndDiscriminator.Uncategorized
                    });
            modelBuilder.Entity<SmallCar>()
                .HasData(
                    new SmallCar
                    {
                        Id = 2,
                        Name = CategoryNameAndDiscriminator.SmallCar
                    });

            modelBuilder.Entity<StationWagon>()
                .HasData(
                    new StationWagon
                    {
                        Id = 3,
                        Name = CategoryNameAndDiscriminator.StationWagon,
                        BasePrice = 100.00m,
                        AdditionalFee = 1.30m,
                        BaseKmPrice = 0.50m
                    });

            modelBuilder.Entity<Vehicle>()
                .HasData(
                    new Vehicle
                    {
                        Id = 1,
                        Make = "Skoda",
                        Model = "Octavia",
                        PlateNumber = "OUH283",
                        CategoryId = 3
                    },
                    new Vehicle()
                    {
                        Id = 2,
                        CategoryId = 1,
                        Make = "Renault",
                        Model = "Clio",
                        PlateNumber = "REN-323"
                    });

            modelBuilder.Entity<Customer>()
                .HasData(
                new Customer
                {
                    Id = 1,
                    FirstName = "Sinan",
                    LastName = "Altaii",
                    PersonalIdentityNumber = "19851223-8273"
                });

            modelBuilder.Entity<ContactInfo>()
                .HasData(new ContactInfo
                {
                    Id = 1,
                    CustomerId = 1,
                    StreetAddress = "Skäftingebacken 21 LGH 1303",
                    City = "Spånga",
                    PostalCode = "163 67",
                    PhoneNumber = "0730323252",
                    EmailAddress = "sinan.altaii@live.se",
                });
        }
    }
}
