﻿// <auto-generated />
using System;
using MegaDesk.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MegaDesk.Migrations
{
    [DbContext(typeof(MegaDeskContext))]
    [Migration("20201105195437_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MegaDesk.Models.DeliveryOption", b =>
                {
                    b.Property<int>("DeliveryOptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeliveryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("LargePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MediumPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SmallPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("DeliveryOptionId");

                    b.ToTable("DeliveryOption");
                });

            modelBuilder.Entity("MegaDesk.Models.Desk", b =>
                {
                    b.Property<int>("DeskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Depth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("DesktopMaterialId")
                        .HasColumnType("int");

                    b.Property<int>("Drawers")
                        .HasColumnType("int");

                    b.Property<decimal>("Width")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("DeskId");

                    b.HasIndex("DesktopMaterialId");

                    b.ToTable("Desk");
                });

            modelBuilder.Entity("MegaDesk.Models.DeskQuote", b =>
                {
                    b.Property<int>("DeskQuoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("DeliveryOptionId")
                        .HasColumnType("int");

                    b.Property<int>("DeskId")
                        .HasColumnType("int");

                    b.Property<DateTime>("QuoteDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("QuotePrice")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("DeskQuoteId");

                    b.HasIndex("DeliveryOptionId");

                    b.HasIndex("DeskId");

                    b.ToTable("DeskQuote");
                });

            modelBuilder.Entity("MegaDesk.Models.DesktopMaterial", b =>
                {
                    b.Property<int>("DesktopMaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaterialName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DesktopMaterialId");

                    b.ToTable("DesktopMaterial");
                });

            modelBuilder.Entity("MegaDesk.Models.Desk", b =>
                {
                    b.HasOne("MegaDesk.Models.DesktopMaterial", "DesktopMaterial")
                        .WithMany()
                        .HasForeignKey("DesktopMaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MegaDesk.Models.DeskQuote", b =>
                {
                    b.HasOne("MegaDesk.Models.DeliveryOption", "DeliveryOption")
                        .WithMany()
                        .HasForeignKey("DeliveryOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MegaDesk.Models.Desk", "Desk")
                        .WithMany()
                        .HasForeignKey("DeskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
