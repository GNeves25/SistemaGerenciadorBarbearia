using System;
using BarbershopManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BarbershopManager.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(BarbershopContext))]
    partial class BarbershopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BarbershopManager.Domain.Entities.Appointment", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<Guid>("BarberId")
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("CustomerName")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                b.Property<TimeSpan>("Duration")
                    .HasColumnType("time");

                b.Property<string>("Notes")
                    .HasMaxLength(500)
                    .HasColumnType("nvarchar(500)");

                b.Property<DateTime>("ScheduledAt")
                    .HasColumnType("datetime2");

                b.Property<Guid>("ServiceOfferingId")
                    .HasColumnType("uniqueidentifier");

                b.HasKey("Id");

                b.HasIndex("BarberId");

                b.HasIndex("ServiceOfferingId");

                b.ToTable("Appointments");
            });

            modelBuilder.Entity("BarbershopManager.Domain.Entities.Barber", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnType("nvarchar(150)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnType("nvarchar(150)");

                b.Property<string>("Phone")
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.Property<string>("Specialty")
                    .HasMaxLength(150)
                    .HasColumnType("nvarchar(150)");

                b.HasKey("Id");

                b.ToTable("Barbers");
            });

            modelBuilder.Entity("BarbershopManager.Domain.Entities.ServiceOffering", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Description")
                    .HasMaxLength(500)
                    .HasColumnType("nvarchar(500)");

                b.Property<TimeSpan>("Duration")
                    .HasColumnType("time");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnType("nvarchar(150)");

                b.Property<decimal>("Price")
                    .HasColumnType("decimal(10,2)");

                b.HasKey("Id");

                b.ToTable("Services");
            });

            modelBuilder.Entity("BarbershopManager.Domain.Entities.User", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("PasswordHash")
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnType("nvarchar(128)");

                b.Property<string>("Role")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<string>("Username")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.HasKey("Id");

                b.HasIndex("Username")
                    .IsUnique();

                b.ToTable("Users");
            });

            modelBuilder.Entity("BarbershopManager.Domain.Entities.Appointment", b =>
            {
                b.HasOne("BarbershopManager.Domain.Entities.Barber", null)
                    .WithMany()
                    .HasForeignKey("BarberId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("BarbershopManager.Domain.Entities.ServiceOffering", null)
                    .WithMany()
                    .HasForeignKey("ServiceOfferingId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });
#pragma warning restore 612, 618
        }
    }
}
