
using CRMCore.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRMCore.Objects
{
    public class ObjProjectList
    {
       public List<ObjProject> list { get; set; }
    }

    public class ObjProject
    {
        public int Id { get; set; }
        public int RootId { get; set; }
        public RootTypes RootType { get; set; }
        [Required(ErrorMessage = "Введите название проекта")]
        public string Title { get; set; }
        public string Description { get; set; }

        public string StatusName { get; set; }
        public string StatusColor { get; set; }
        public int StatusId { get; set; }

        public byte CompeteProcent { get; set; }
        public decimal Cost { get; set; }
        public CurrencyType Currency { get; set; }

        public decimal Residue { get; set; }

        public DateTime DeadLine { get; set; }

        public string ProjectTypeName { get; set; }
        public int ProjectTypeId { get; set; }

        public int? UserId { get; set; }

        public List<ObjParticipant> Participants { get; set; }

        public string RootName { get; set; }
        public string RootUrl { get; set; }
    }
}
