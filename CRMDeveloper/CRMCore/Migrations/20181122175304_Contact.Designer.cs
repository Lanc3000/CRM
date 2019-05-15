﻿// <auto-generated />
using System;
using CRMCore.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CRMCore.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20181122175304_Contact")]
    partial class Contact
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("CRMCore.DB.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CreateId");

                    b.Property<DateTime>("Created");

                    b.Property<bool>("Deleted");

                    b.Property<DateTime>("Modified");

                    b.Property<int>("RootId");

                    b.Property<int>("RootType");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("CRMCore.DB.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<int?>("CreatedId");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Email");

                    b.Property<string>("FIO");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Other");

                    b.Property<string>("Phone");

                    b.Property<string>("Position");

                    b.Property<int>("RootID");

                    b.Property<int>("RootType");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("CRMCore.DB.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<bool>("Deleted");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Name");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CRMCore.DB.RoleActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Activity");

                    b.Property<DateTime>("Created");

                    b.Property<bool>("Deleted");

                    b.Property<DateTime>("Modified");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleActivities");
                });

            modelBuilder.Entity("CRMCore.DB.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<DateTime>("Created");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Email");

                    b.Property<string>("Fio");

                    b.Property<int>("Grade");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Other");

                    b.Property<string>("PassHash");

                    b.Property<string>("Phone");

                    b.Property<string>("Position");

                    b.Property<int>("RoleId");

                    b.Property<string>("Skype");

                    b.Property<string>("Telegram");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CRMCore.DB.RoleActivity", b =>
                {
                    b.HasOne("CRMCore.DB.Role", "Role")
                        .WithMany("RoleActivitys")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CRMCore.DB.User", b =>
                {
                    b.HasOne("CRMCore.DB.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
