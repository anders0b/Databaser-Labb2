using Microsoft.EntityFrameworkCore;

namespace Labb2_DbFirst_Template.Entities;

public partial class AndersBooksContext : DbContext
{
    public AndersBooksContext()
    {
    }

    public AndersBooksContext(DbContextOptions<AndersBooksContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Title> Titles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=Talos;Initial Catalog=AndersBooks;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Authors__3213E83FFF6C6B41");

            entity.ToTable("Authors", "Books");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateOfBirth).HasColumnName("Date of Birth");
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC072FA5897B");

            entity.ToTable("Customers", "Bookstore");

            entity.Property(e => e.EMail)
                .HasMaxLength(255)
                .HasColumnName("E-Mail");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("First Name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("Last Name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC0729E03371");

            entity.ToTable("Employees", "Bookstore");

            entity.Property(e => e.ContactEMail)
                .HasMaxLength(255)
                .HasColumnName("Contact E-Mail");
            entity.Property(e => e.EmployeeSince).HasColumnName("Employee Since");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("First Name");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(255)
                .HasColumnName("Job Title");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("Last Name");
            entity.Property(e => e.StoreId).HasColumnName("Store Id");

            entity.HasOne(d => d.Store).WithMany(p => p.Employees)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__Employees__Store__36B12243");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC07F26F0044");

            entity.ToTable("Orders", "Bookstore");

            entity.Property(e => e.CustomerId).HasColumnName("Customer Id");
            entity.Property(e => e.DeliveryAddress)
                .HasMaxLength(255)
                .HasColumnName("Delivery Address");
            entity.Property(e => e.DeliveryCity)
                .HasMaxLength(255)
                .HasColumnName("Delivery City");
            entity.Property(e => e.DeliveryPostalCode).HasColumnName("Delivery Postal Code");
            entity.Property(e => e.OrderDate).HasColumnName("Order Date");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Orders__Customer__3C69FB99");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.BookId }).HasName("PK__Order_De__097D1F5D9AFA52DA");

            entity.ToTable("Order_Details", "Bookstore");

            entity.Property(e => e.OrderId).HasColumnName("Order ID");
            entity.Property(e => e.BookId)
                .HasMaxLength(13)
                .HasColumnName("Book ID");

            entity.HasOne(d => d.Book).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order_Det__Book __403A8C7D");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order_Det__Order__3F466844");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => new { e.StoreId, e.Isbnid }).HasName("PK__Stock__483D85374173B7EE");

            entity.ToTable("Stock", "Bookstore");

            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.Isbnid)
                .HasMaxLength(13)
                .HasColumnName("ISBNID");
            entity.Property(e => e.NumberOfBooks).HasColumnName("Number of Books");

            entity.HasOne(d => d.Isbn).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.Isbnid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Stock__ISBNID__31EC6D26");

            entity.HasOne(d => d.Store).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Stock__StoreID__30F848ED");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Stores__3214EC076C2222DF");

            entity.ToTable("Stores", "Bookstore");

            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.PostalCode).HasColumnName("Postal Code");
            entity.Property(e => e.StreetAddress)
                .HasMaxLength(255)
                .HasColumnName("Street Address");
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.HasKey(e => e.Isbn).HasName("PK__Titles__447D36EB95BAD773");

            entity.ToTable("Titles", "Books");

            entity.Property(e => e.Isbn)
                .HasMaxLength(13)
                .HasColumnName("ISBN");
            entity.Property(e => e.Language).HasMaxLength(255);
            entity.Property(e => e.Title1)
                .HasMaxLength(255)
                .HasColumnName("Title");
            entity.Property(e => e.YearOfPublication).HasColumnName("Year of publication");

            entity.HasMany(d => d.Authors).WithMany(p => p.Isbns)
                .UsingEntity<Dictionary<string, object>>(
                    "AuthorMap",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__AuthorMap__Autho__2C3393D0"),
                    l => l.HasOne<Title>().WithMany()
                        .HasForeignKey("Isbn")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__AuthorMap__ISBN__2B3F6F97"),
                    j =>
                    {
                        j.HasKey("Isbn", "AuthorId").HasName("PK__AuthorMa__70127A74D591F827");
                        j.ToTable("AuthorMap", "Books");
                        j.IndexerProperty<string>("Isbn")
                            .HasMaxLength(13)
                            .HasColumnName("ISBN");
                        j.IndexerProperty<int>("AuthorId").HasColumnName("Author ID");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
