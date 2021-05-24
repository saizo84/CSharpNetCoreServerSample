﻿// <auto-generated />
using System;
using DBHandle;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CSharpNetCoreServer.Migrations
{
    [DbContext(typeof(AuthContext))]
    [Migration("20210524095555_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("DBEntities.Device", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("DeviceId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("MCC")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("OsType")
                        .HasColumnType("int");

                    b.Property<string>("OsVersion")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("PushToken")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("devices");
                });

            modelBuilder.Entity("DBEntities.PlatformAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("PlatformAccountAuthorizationId")
                        .HasColumnType("bigint");

                    b.Property<string>("PlatformId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("PlatformType")
                        .HasColumnType("int");

                    b.Property<long?>("UserId")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PlatformAccountAuthorizationId");

                    b.HasIndex("UserId", "PlatformType", "PlatformId")
                        .IsUnique();

                    b.ToTable("platform_accounts");
                });

            modelBuilder.Entity("DBEntities.PlatformAccountAuthorization", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<long>("PlatformAccountId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("platform_account_authorizations");
                });

            modelBuilder.Entity("DBEntities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ServerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("DBEntities.Device", b =>
                {
                    b.HasOne("DBEntities.User", "User")
                        .WithMany("Devices")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DBEntities.PlatformAccount", b =>
                {
                    b.HasOne("DBEntities.PlatformAccountAuthorization", "PlatformAccountAuthorization")
                        .WithMany()
                        .HasForeignKey("PlatformAccountAuthorizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DBEntities.User", "User")
                        .WithMany("PlatformAccounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlatformAccountAuthorization");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DBEntities.User", b =>
                {
                    b.Navigation("Devices");

                    b.Navigation("PlatformAccounts");
                });
#pragma warning restore 612, 618
        }
    }
}
