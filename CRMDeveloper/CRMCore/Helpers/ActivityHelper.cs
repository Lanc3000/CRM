using System;
using System.Collections.Generic;
using System.Text;

using CRMCore.Objects;

namespace CRMCore.Helpers
{
    public static class ActivityHelper
    {
        
        public static readonly List<ObjActivity> _activities;

        static ActivityHelper()
        {
            _activities = GetActivity();
        }

        public static List<ObjActivity> GetActivities()
        {
            return _activities;
        }
        static List<ObjActivity> GetActivity()
        {
            var adminActivities = new List<ObjActivityPermision>()
            {
                new ObjActivityPermision(ObjActivities.Admin, "Просмотр"),
                new ObjActivityPermision(ObjActivities.AdminEdit, "Редактирование"),
            };

            var usersAtivities = new List<ObjActivityPermision>()
            {
                new ObjActivityPermision(ObjActivities.User, "Просмотр"),
                new ObjActivityPermision(ObjActivities.UserEdit, "Редактирование"),
            };

            var potentialClientActivities = new List<ObjActivityPermision>()
            {
                new ObjActivityPermision(ObjActivities.PotentialClient, "Просмотр"),
                new ObjActivityPermision(ObjActivities.PotentialClientEdit, "Редактирование"),
            };

            var ClientActivities = new List<ObjActivityPermision>()
            {
                new ObjActivityPermision(ObjActivities.Client, "Просмотр"),
                new ObjActivityPermision(ObjActivities.ClientEdit, "Редактирование"),
            };

            var ProjectActivities = new List<ObjActivityPermision>()
            {
                new ObjActivityPermision(ObjActivities.Project, "Просмотр"),
                new ObjActivityPermision(ObjActivities.ProjectEdit, "Редактирование"),
            };

            var FinanceActivities = new List<ObjActivityPermision>()
            {
                new ObjActivityPermision(ObjActivities.Finance, "Просмотр"),
                new ObjActivityPermision(ObjActivities.FinanceEdit, "Редактирование"),
            };

           
            var ContactActivities = new List<ObjActivityPermision>()
            {
                new ObjActivityPermision(ObjActivities.Contact, "Просмотр"),
                new ObjActivityPermision(ObjActivities.ContactEdit, "Редактирование"),
            };

            var FileActivities = new List<ObjActivityPermision>()
            {
                new ObjActivityPermision(ObjActivities.File, "Просмотр"),
                new ObjActivityPermision(ObjActivities.FileEdit, "Редактирование"),
            };

            var ParticipantActivities = new List<ObjActivityPermision>()
            {
                new ObjActivityPermision(ObjActivities.Participant, "Просмотр"),
                new ObjActivityPermision(ObjActivities.ParticipantEdit, "Редактирование"),
            };

            var RoleActivities = new List<ObjActivityPermision>
            {
                new ObjActivityPermision(ObjActivities.Role, "Просмотр"),
                new ObjActivityPermision(ObjActivities.RoleEdit, "Редактирование"),
            };

            var StatusActivities = new List<ObjActivityPermision>()
            {
                new ObjActivityPermision(ObjActivities.Status, "Просмотр"),
                new ObjActivityPermision(ObjActivities.StatusEdit, "Редактирование"),
            };

            var TaskActivities = new List<ObjActivityPermision>()
            {
                new ObjActivityPermision(ObjActivities.Task, "Просмотр"),
                new ObjActivityPermision(ObjActivities.TaskEdit, "Редактирование"),
            };

            var FinanceSubTypeActivities = new List<ObjActivityPermision>()
            {
                new ObjActivityPermision(ObjActivities.FinanceSubType, "Просмотр"),
                new ObjActivityPermision(ObjActivities.FinanceSubTypeEdit, "Редактирование"),
            };

            var ProjectTypeActivities = new List<ObjActivityPermision>()
            {
                new ObjActivityPermision(ObjActivities.ProjectType, "Просмотр"),
                new ObjActivityPermision(ObjActivities.ProjectTypeEdit, "Редактирование"),
            };

            var SourceActivities = new List<ObjActivityPermision>()
            {
                new ObjActivityPermision(ObjActivities.Source, "Просмотр"),
                new ObjActivityPermision(ObjActivities.SourceEdit, "Редактирование"),
            };

            var  activities = new List<ObjActivity>()
            {
                new ObjActivity("Управление администраторами", adminActivities),
                new ObjActivity("Управление пользователями", usersAtivities),
                new ObjActivity("Управление потенциальными клиентами", potentialClientActivities),
                new ObjActivity("Управление клиентами", ClientActivities),
                new ObjActivity("Управление проектами", ProjectActivities),
                new ObjActivity("Управление финансами", FinanceActivities),
                new ObjActivity("Управление контактами", ContactActivities),
                new ObjActivity("Управление файлами", FileActivities),
                new ObjActivity("Управление участниками", ParticipantActivities),
                new ObjActivity("Управление ролями", RoleActivities),
                new ObjActivity("Управление статусами", StatusActivities),
                new ObjActivity("Управление задачами", TaskActivities),
                new ObjActivity("Управление подтипами финансов", FinanceSubTypeActivities),
                new ObjActivity("Управление типами проектов", ProjectTypeActivities),
                new ObjActivity("Управление источниками клиентов", SourceActivities),
            };

            return activities;
        }
    }
}
