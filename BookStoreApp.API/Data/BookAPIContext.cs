using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace BookStoreApp.API.Data
{
    public partial class BookAPIContext : IdentityDbContext<ApiUser>
    {
        public BookAPIContext()
        {
        }

        public BookAPIContext(DbContextOptions<BookAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Bio).HasMaxLength(250);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Image)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Isbn)
                    .HasMaxLength(50)
                    .HasColumnName("ISBN");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Summary).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Books_ToTable");
                    
            });

            modelBuilder.Entity<IdentityRole>().HasData(
                 new IdentityRole
                 {
                     Name = "User",
                     NormalizedName = "USER",
                     Id = "d5ec26c2-b669-447b-9a8b-87adf91ec0b6"
                 },
                 new IdentityRole
                 {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    Id = "2d169a3f-ba5f-4336-81ce-e145df98a117"
                 }
                );


            var hasher = new PasswordHasher<ApiUser>();

            modelBuilder.Entity<ApiUser>().HasData(
                    new ApiUser
                    {
                        Id = "47e3e214-13b4-4a99-b777-1e707c4889b5",
                        Email = "admin@bookstore.com",
                        NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                        UserName = "admin@bookstore.com",
                        NormalizedUserName = "ADMIN@BOOKSTORE.COM",
                        FirstName = "System",
                        LastName = "Admin",
                        PasswordHash = hasher.HashPassword(null, "P@ssword1")
                    },
                    new ApiUser
                    {
                        Id = "89fa0c65-f78c-4b4e-a635-4b626fa007cc",
                        Email = "user@bookstore.com",
                        NormalizedEmail = "USER@BOOKSTORE.COM",
                        UserName = "user@bookstore.com",
                        NormalizedUserName = "USER@BOOKSTORE.COM",
                        FirstName = "User",
                        LastName = "User",
                        PasswordHash = hasher.HashPassword(null, "P@ssword1")
                    }
                );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(

                new IdentityUserRole<string>
                {
                    RoleId = "d5ec26c2-b669-447b-9a8b-87adf91ec0b6",
                    UserId = "89fa0c65-f78c-4b4e-a635-4b626fa007cc"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "2d169a3f-ba5f-4336-81ce-e145df98a117",
                    UserId = "47e3e214-13b4-4a99-b777-1e707c4889b5"
                }
                );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
