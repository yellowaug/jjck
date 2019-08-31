﻿// <auto-generated />
using System;
using DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.EntityFramework.Migrations
{
    [DbContext(typeof(BaseDbContext))]
    partial class BaseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccess.Entity.Dividend", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("DividendDate");

                    b.Property<string>("RateOfReturn");

                    b.Property<DateTime>("RegistrationTime");

                    b.Property<DateTime>("ReleaseTime");

                    b.Property<int?>("StockID");

                    b.HasKey("ID");

                    b.HasIndex("StockID");

                    b.ToTable("Dividends");
                });

            modelBuilder.Entity("DataAccess.Entity.New", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentUri");

                    b.Property<DateTime>("ReleaseTime");

                    b.Property<int?>("StockID");

                    b.Property<string>("Tite");

                    b.HasKey("ID");

                    b.HasIndex("StockID");

                    b.ToTable("News");
                });

            modelBuilder.Entity("DataAccess.Entity.Quote", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AMO");

                    b.Property<DateTime>("Date");

                    b.Property<int?>("DividendID");

                    b.Property<decimal>("EndPrice");

                    b.Property<decimal>("MaxPrice");

                    b.Property<decimal>("MinPrice");

                    b.Property<decimal>("StartPrice");

                    b.Property<string>("TurnoverRate");

                    b.Property<double>("Volume");

                    b.HasKey("ID");

                    b.HasIndex("DividendID");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("DataAccess.Entity.Stock", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("DataAccess.Entity.Dividend", b =>
                {
                    b.HasOne("DataAccess.Entity.Stock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockID");
                });

            modelBuilder.Entity("DataAccess.Entity.New", b =>
                {
                    b.HasOne("DataAccess.Entity.Stock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockID");
                });

            modelBuilder.Entity("DataAccess.Entity.Quote", b =>
                {
                    b.HasOne("DataAccess.Entity.Dividend")
                        .WithMany("Quotes")
                        .HasForeignKey("DividendID");
                });
#pragma warning restore 612, 618
        }
    }
}
