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
    [Migration("20181207110519_FileUrl")]
    partial class FileUrl
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("CRMCore.DB.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BIK");

                    b.Property<string>("BankName");

                    b.Property<string>("CompanyName");

                    b.Property<string>("CorrAccount");

                    b.Property<DateTime>("Created");

                    b.Property<decimal>("Credit");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("PaymentAccount");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Clients");
                });

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

            modelBuilder.Entity("CRMCore.DB.FileData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<int?>("CreatedId");

                    b.Property<bool>("Deleted");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("OriginalName");

                    b.Property<string>("Path");

                    b.Property<int>("RootId");

                    b.Property<int>("RootType");

                    b.HasKey("Id");

                    b.ToTable("FileData");
                });

            modelBuilder.Entity("CRMCore.DB.Finance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Cost");

                    b.Property<DateTime>("Created");

                    b.Property<int>("CreatedId");

                    b.Property<int>("Currency");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("Deleted");

                    b.Property<string>("DocumentName");

                    b.Property<int>("FinanceType");

                    b.Property<DateTime>("Modified");

                    b.Property<int>("Place");

                    b.Property<int>("ProjectId");

                    b.Property<int?>("SubTypeId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SubTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Finances");
                });

            modelBuilder.Entity("CRMCore.DB.FinanceSubType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Name");

                    b.Property<int>("Position");

                    b.HasKey("Id");

                    b.ToTable("FinanceSubTypes");
                });

            modelBuilder.Entity("CRMCore.DB.Participant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<int>("CreatedId");

                    b.Property<int>("Currency");

                    b.Property<DateTime>("DateStartWork");

                    b.Property<bool>("Deleted");

                    b.Property<DateTime>("Modified");

                    b.Property<decimal>("Residue");

                    b.Property<int>("RootId");

                    b.Property<int>("RootType");

                    b.Property<string>("Task");

                    b.Property<int>("UserId");

                    b.Property<DateTime>("WorkPeriod");

                    b.Property<decimal>("WorkSum");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("CRMCore.DB.PotentialClient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyName");

                    b.Property<decimal>("Cost");

                    b.Property<DateTime>("Created");

                    b.Property<int>("Currency");

                    b.Property<byte>("DateCount");

                    b.Property<int>("DateType");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Name");

                    b.Property<byte>("Probability");

                    b.Property<int>("ProjectTypeId");

                    b.Property<int?>("SourceId");

                    b.Property<int>("StatusId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectTypeId");

                    b.HasIndex("SourceId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("PotentialClients");
                });

            modelBuilder.Entity("CRMCore.DB.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte>("CompeteProcent");

                    b.Property<decimal>("Cost");

                    b.Property<DateTime>("Created");

                    b.Property<int>("Currency");

                    b.Property<DateTime>("DateStartProject");

                    b.Property<DateTime>("DeadLine");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<DateTime>("Modified");

                    b.Property<int>("ProjectTypeId");

                    b.Property<decimal>("Residue");

                    b.Property<int>("RootId");

                    b.Property<int>("RootType");

                    b.Property<int>("StatusId");

                    b.Property<string>("Title");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectTypeId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("CRMCore.DB.ProjectType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Name");

                    b.Property<int>("Posititon");

                    b.HasKey("Id");

                    b.ToTable("ProjectTypes");
                });

            modelBuilder.Entity("CRMCore.DB.PTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Name");

                    b.Property<int>("Position");

                    b.HasKey("Id");

                    b.ToTable("PTasks");
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

            modelBuilder.Entity("CRMCore.DB.Source", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Name");

                    b.Property<int>("Position");

                    b.HasKey("Id");

                    b.ToTable("Source");
                });

            modelBuilder.Entity("CRMCore.DB.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color");

                    b.Property<DateTime>("Created");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<bool>("IsRoot");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Name");

                    b.Property<int>("Position");

                    b.Property<int>("rootType");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
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

                    b.Property<bool>("IsManager");

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

            modelBuilder.Entity("CRMCore.DB.Client", b =>
                {
                    b.HasOne("CRMCore.DB.User", "Manager")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CRMCore.DB.Finance", b =>
                {
                    b.HasOne("CRMCore.DB.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CRMCore.DB.FinanceSubType", "SubType")
                        .WithMany()
                        .HasForeignKey("SubTypeId");

                    b.HasOne("CRMCore.DB.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CRMCore.DB.Participant", b =>
                {
                    b.HasOne("CRMCore.DB.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CRMCore.DB.PotentialClient", b =>
                {
                    b.HasOne("CRMCore.DB.ProjectType", "ProjectType")
                        .WithMany()
                        .HasForeignKey("ProjectTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CRMCore.DB.Source", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId");

                    b.HasOne("CRMCore.DB.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CRMCore.DB.User", "Manager")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CRMCore.DB.Project", b =>
                {
                    b.HasOne("CRMCore.DB.ProjectType", "ProjectType")
                        .WithMany()
                        .HasForeignKey("ProjectTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CRMCore.DB.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CRMCore.DB.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
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
