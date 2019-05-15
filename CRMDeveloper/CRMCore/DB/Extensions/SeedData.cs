using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRMCore.Enums;

namespace CRMCore.DB.Extensions
{
    public static class SeedData
    {
        public static void Initialize(DBContext context)
        {
            context.Database.EnsureCreated();
            if (!context.Roles.Any())
            {
                #region DefaultAdmin
                context.Roles.AddRange(
                    new Role { Id = 1, Title = "Администратор", Name = "admin" });



                context.Users.Add(new User()
                {
                    Email = "seven-group@mail.ru",
                    PassHash = "827ccb0eea8a706c4c34a16891f84e7b",
                    RoleId = 1,
                    Grade = 1,
                    Fio = "SevenGroup"
                });
                #endregion

                context.Statuses.AddRange(
                    new Status { Name = "В плане", rootType = RootTypes.Project, Color = "#808080", Position = 0, IsRoot = true, },
                    new Status { Name = "В работе", rootType = RootTypes.Project, Color = "#808080", Position = 1, IsRoot = false },
                    new Status { Name = "Завершен удачно", rootType = RootTypes.Project, Color = "#808080", Position = 100, IsRoot = true },
                    new Status { Name = "Завершен провалом", rootType = RootTypes.Project, Color = "#808080", Position = 101, IsRoot = true },
                    new Status { Name = "Новый", rootType = RootTypes.PotentialCLient, Color = "#808080", Position = 0, IsRoot = true },
                    new Status { Name = "Заключен договор", rootType = RootTypes.PotentialCLient, Color = "#808080", Position = 100, IsRoot = true },
                    new Status { Name = "Отказался", rootType = RootTypes.PotentialCLient, Color = "#808080", Position = 101, IsRoot = true }
                    );

                context.SaveChanges();
            }

            if (!context.RoleActivities.Any())
            {
                context.RoleActivities.AddRange(
                   new RoleActivity { Activity = "User", RoleId = 1 },
                   new RoleActivity { Activity = "UserEdit", RoleId = 1 },

                   new RoleActivity { Activity = "Admin", RoleId = 1 },
                   new RoleActivity { Activity = "AdminEdit", RoleId = 1 },

                   new RoleActivity { Activity = "PotentialClient", RoleId = 1 },
                   new RoleActivity { Activity = "PotentialClientEdit", RoleId = 1 },

                   new RoleActivity { Activity = "Client", RoleId = 1 },
                   new RoleActivity { Activity = "ClientEdit", RoleId = 1 },

                   new RoleActivity { Activity = "Project", RoleId = 1 },
                   new RoleActivity { Activity = "ProjectEdit", RoleId = 1 },

                   new RoleActivity { Activity = "Finance", RoleId = 1 },
                   new RoleActivity { Activity = "FinanceEdit", RoleId = 1 },

                   new RoleActivity { Activity = "Contact", RoleId = 1 },
                   new RoleActivity { Activity = "ContactEdit", RoleId = 1 },

                   new RoleActivity { Activity = "File", RoleId = 1 },
                   new RoleActivity { Activity = "FileEdit", RoleId = 1 },

                   new RoleActivity { Activity = "Participant", RoleId = 1 },
                   new RoleActivity { Activity = "ParticipantEdit", RoleId = 1 },

                   new RoleActivity { Activity = "Role", RoleId = 1 },
                   new RoleActivity { Activity = "RoleEdit", RoleId = 1 },

                   new RoleActivity { Activity = "Status", RoleId = 1 },
                   new RoleActivity { Activity = "StatusEdit", RoleId = 1 },

                   new RoleActivity { Activity = "Task", RoleId = 1 },
                   new RoleActivity { Activity = "TaskEdit", RoleId = 1 },

                   new RoleActivity { Activity = "FinanceSubType", RoleId = 1 },
                   new RoleActivity { Activity = "FinanceSubTypeEdit", RoleId = 1 },

                   new RoleActivity { Activity = "ProjectType", RoleId = 1 },
                   new RoleActivity { Activity = "ProjectTypeEdit", RoleId = 1 },

                   new RoleActivity { Activity = "TaskType", RoleId = 1 },
                   new RoleActivity { Activity = "TaskTypeEdit", RoleId = 1 },

                   new RoleActivity { Activity = "Source", RoleId = 1 },
                   new RoleActivity { Activity = "SourceEdit", RoleId = 1 }
                   );
                context.SaveChanges();
            }
        }
    }
}
