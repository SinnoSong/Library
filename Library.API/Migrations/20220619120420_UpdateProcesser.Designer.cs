﻿// <auto-generated />
using System;
using Library.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Library.API.Migrations
{
    [DbContext(typeof(LibraryDbContext))]
    [Migration("20220619120420_UpdateProcesser")]
    partial class UpdateProcesser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Library.API.Entities.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("IsLend")
                        .HasColumnType("bit");

                    b.Property<string>("Isbn")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("ISBN");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Pages")
                        .HasColumnType("int");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Summary")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = new Guid("058ddac3-c6f5-42c2-a8ec-4a8000802990"),
                            Author = "马克思",
                            CategoryId = new Guid("00000000-0000-0000-0000-000000000000"),
                            IsLend = false,
                            Isbn = "12345667",
                            Location = "2FC1005",
                            Pages = 100,
                            Summary = "book1 Description",
                            Title = "book1 Title"
                        },
                        new
                        {
                            Id = new Guid("d64263c7-a382-482c-b773-ba74caa8f4a1"),
                            Author = "毛泽东",
                            CategoryId = new Guid("00000000-0000-0000-0000-000000000000"),
                            IsLend = false,
                            Isbn = "12384298",
                            Location = "2FC1004",
                            Pages = 100,
                            Summary = "book2 Description",
                            Title = "book2 Title"
                        },
                        new
                        {
                            Id = new Guid("7c75c7c4-1268-4d60-999b-686b08d3b269"),
                            Author = "Jon Asa",
                            CategoryId = new Guid("00000000-0000-0000-0000-000000000000"),
                            IsLend = false,
                            Location = "2FC1003",
                            Pages = 100,
                            Summary = "book3 Description",
                            Title = "book3 Title"
                        });
                });

            modelBuilder.Entity("Library.API.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Summary")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Library.API.Entities.LendConfig", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MaxLendDays")
                        .HasColumnType("int");

                    b.Property<int>("MaxLendNumber")
                        .HasColumnType("int");

                    b.Property<byte>("ReaderGrade")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.ToTable("LendConfigs");
                });

            modelBuilder.Entity("Library.API.Entities.LendRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Processer")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RealReturnTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("LendBookRecords");
                });

            modelBuilder.Entity("Library.API.Entities.Notice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Notices");
                });

            modelBuilder.Entity("Library.API.Entities.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "77967ECF-0A18-4993-A243-FDCC86F7EC1B",
                            ConcurrencyStamp = "c6eb3269-b4df-42ba-8f6b-47cc7a2aa00a",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "2093BF77-ED2E-453B-911F-325BC99C4504",
                            ConcurrencyStamp = "7dbc1442-3f01-49c6-a670-92ae973eb09d",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = "BCC4BFC0-6252-4A32-B5B8-9005E4560402",
                            ConcurrencyStamp = "c2d1be71-e893-4e9a-a17e-2b54dec0238a",
                            Name = "SuperAdministrator",
                            NormalizedName = "SUPERADMINISTRATOR"
                        });
                });

            modelBuilder.Entity("Library.API.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<byte?>("Grade")
                        .HasColumnType("tinyint");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "8e448afa-f008-446e-a52f-13c449803c2e",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "14c3106a-e54d-4f56-8fec-d5bbcb8c6b71",
                            Email = "admin@library.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@LIBRARY.COM",
                            NormalizedUserName = "ADMIN@LIBRARY.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ/fhejtzisij/f07y9AWMbpBZ62bop2ydZGjBtxfV1Dms4XLIm6zKgKE54qhsQ31Q==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "d6e37a61-05b5-4fe9-9de7-02557f319065",
                            TwoFactorEnabled = false,
                            UserName = "admin@library.com"
                        },
                        new
                        {
                            Id = "30a24107-d279-4e37-96fd-01af5b38cb27",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "f99f9077-b08b-4cf0-8b7f-c05a4268499e",
                            Email = "user@library.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "USER@LIBRARY.COM",
                            NormalizedUserName = "USER@LIBRARY.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAED8p+jUDA8/rymAjzQw4MRkjnUKd0HXLNogBXIb5KQs7nKCRbSHImPUWOcpKlcXw+g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "57730a98-ae55-4246-b579-dc969cd381da",
                            TwoFactorEnabled = false,
                            UserName = "user@library.com"
                        },
                        new
                        {
                            Id = "32A61BDD-DF3C-4B71-9444-F5730F8A619B",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "14cb59f9-058c-47c9-abd5-e2601fd6ad8c",
                            Email = "super@library.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "SUPER@LIBRARY.COM",
                            NormalizedUserName = "SUPER@LIBRARY.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEKqu83wrnTpiWkPRywrgnpID4TSaWvFotiXPNHtjMVFu/br7vcyLeSv0GGE9USeKoA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "9d87c70f-1fe3-4aea-986b-c8185e6b8fbf",
                            TwoFactorEnabled = false,
                            UserName = "super@library.com"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "30a24107-d279-4e37-96fd-01af5b38cb27",
                            RoleId = "77967ECF-0A18-4993-A243-FDCC86F7EC1B"
                        },
                        new
                        {
                            UserId = "8e448afa-f008-446e-a52f-13c449803c2e",
                            RoleId = "2093BF77-ED2E-453B-911F-325BC99C4504"
                        },
                        new
                        {
                            UserId = "32A61BDD-DF3C-4B71-9444-F5730F8A619B",
                            RoleId = "BCC4BFC0-6252-4A32-B5B8-9005E4560402"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Library.API.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Library.API.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Library.API.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Library.API.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.API.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Library.API.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}