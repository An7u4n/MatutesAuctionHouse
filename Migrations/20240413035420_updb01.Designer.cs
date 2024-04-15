﻿// <auto-generated />
using System;
using MatutesAuctionHouse.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MatutesAuctionHouse.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240413035420_updb01")]
    partial class updb01
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MatutesAuctionHouse.Models.Auction", b =>
                {
                    b.Property<int>("auction_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("auction_id"));

                    b.Property<DateTime>("auction_start_date")
                        .HasColumnType("datetime2");

                    b.Property<int>("item_id")
                        .HasColumnType("int");

                    b.HasKey("auction_id");

                    b.HasIndex("item_id");

                    b.ToTable("Auction", (string)null);
                });

            modelBuilder.Entity("MatutesAuctionHouse.Models.Item", b =>
                {
                    b.Property<int>("item_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("item_id"));

                    b.Property<string>("item_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("item_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("item_id");

                    b.HasIndex("user_id");

                    b.ToTable("Item", (string)null);
                });

            modelBuilder.Entity("MatutesAuctionHouse.Models.User", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("user_id"));

                    b.Property<string>("user_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("user_id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("MatutesAuctionHouse.Models.Auction", b =>
                {
                    b.HasOne("MatutesAuctionHouse.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("item_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("MatutesAuctionHouse.Models.Item", b =>
                {
                    b.HasOne("MatutesAuctionHouse.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
