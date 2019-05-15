using CRMCore.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.DB
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
    : base(options)
        { }

        public DbSet<RoleActivity> RoleActivities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<PotentialClient> PotentialClients { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<FinanceSubType> FinanceSubTypes { get; set; }
        public DbSet<Finance> Finances { get; set; }
        public DbSet<PTask> PTasks { get; set; }
        public DbSet<FileData> FileData { get; set; }
        public DbSet<CustomOption> CustomOptions { get; set; }
    }
}
