﻿// <auto-generated />
using System;
using Cms.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cms.Api.Migrations
{
    [DbContext(typeof(CmsDbContext))]
    [Migration("20241122203924_InitialBuild")]
    partial class InitialBuild
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Cms.Entity.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 11, 22, 20, 39, 24, 319, DateTimeKind.Utc).AddTicks(5679),
                            UpdatedAt = new DateTime(2024, 11, 22, 20, 39, 24, 319, DateTimeKind.Utc).AddTicks(5685)
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2024, 11, 22, 20, 39, 24, 319, DateTimeKind.Utc).AddTicks(5687),
                            UpdatedAt = new DateTime(2024, 11, 22, 20, 39, 24, 319, DateTimeKind.Utc).AddTicks(5687)
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2024, 11, 22, 20, 39, 24, 319, DateTimeKind.Utc).AddTicks(5688),
                            UpdatedAt = new DateTime(2024, 11, 22, 20, 39, 24, 319, DateTimeKind.Utc).AddTicks(5688)
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2024, 11, 22, 20, 39, 24, 319, DateTimeKind.Utc).AddTicks(5689),
                            UpdatedAt = new DateTime(2024, 11, 22, 20, 39, 24, 319, DateTimeKind.Utc).AddTicks(5690)
                        });
                });

            modelBuilder.Entity("Cms.Entity.CategoryLanguage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<int>("LanguageId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("CategoryId", "LanguageId")
                        .IsUnique();

                    b.ToTable("CategoryLanguages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            LanguageId = 1,
                            Title = "Teknoloji"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            LanguageId = 2,
                            Title = "Technology"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 2,
                            LanguageId = 1,
                            Title = "Sağlık"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 2,
                            LanguageId = 2,
                            Title = "Health"
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 3,
                            LanguageId = 1,
                            Title = "Eğitim"
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 3,
                            LanguageId = 2,
                            Title = "Education"
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 4,
                            LanguageId = 1,
                            Title = "Ulaşım"
                        },
                        new
                        {
                            Id = 8,
                            CategoryId = 4,
                            LanguageId = 2,
                            Title = "Transportation"
                        });
                });

            modelBuilder.Entity("Cms.Entity.Content", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Contents");
                });

            modelBuilder.Entity("Cms.Entity.ContentLanguage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ContentId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LanguageId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("VariantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("ContentId", "LanguageId")
                        .IsUnique();

                    b.ToTable("ContentLanguages");
                });

            modelBuilder.Entity("Cms.Entity.SystemLanguage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("LanguageCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LanguageCode")
                        .IsUnique();

                    b.ToTable("SystemLanguages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            LanguageCode = "tr-TR"
                        },
                        new
                        {
                            Id = 2,
                            LanguageCode = "en-US"
                        });
                });

            modelBuilder.Entity("Cms.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Cms.Entity.CategoryLanguage", b =>
                {
                    b.HasOne("Cms.Entity.Category", "Category")
                        .WithMany("Languages")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cms.Entity.SystemLanguage", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("Cms.Entity.Content", b =>
                {
                    b.HasOne("Cms.Entity.Category", "Category")
                        .WithMany("Contents")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cms.Entity.User", "User")
                        .WithMany("Contens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cms.Entity.ContentLanguage", b =>
                {
                    b.HasOne("Cms.Entity.Content", "Content")
                        .WithMany("Languages")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cms.Entity.SystemLanguage", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("Cms.Entity.Category", b =>
                {
                    b.Navigation("Contents");

                    b.Navigation("Languages");
                });

            modelBuilder.Entity("Cms.Entity.Content", b =>
                {
                    b.Navigation("Languages");
                });

            modelBuilder.Entity("Cms.Entity.User", b =>
                {
                    b.Navigation("Contens");
                });
#pragma warning restore 612, 618
        }
    }
}
