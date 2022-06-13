﻿// <auto-generated />
using System;

using KafkaCDC.Traders.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KafkaCDC.Traders.Data.Migration
{
    [DbContext(typeof(TradersDbContext))]
    partial class TradersDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("KafkaCDC.Common.Entities.Outbox", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AggregateId")
                        .HasColumnType("uuid");

                    b.Property<string>("AggregateType")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOccurred")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Payload")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OutboxEvents");
                });

            modelBuilder.Entity("KafkaCDC.Traders.Data.DealSubscriptions", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DealId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TraderId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("DealSubscriptions");
                });

            modelBuilder.Entity("KafkaCDC.Traders.Data.Trader", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Traders");
                });
#pragma warning restore 612, 618
        }
    }
}
