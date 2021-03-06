﻿using System;
using System.Collections.Generic;
using System.Text;

using CRMCore.DB.Identity;
using CRMCore.Enums;

namespace CRMCore.DB
{
    public class Project : IEntity
    {
        public int Id { get; set; }
        public int RootId { get; set; }
        public RootTypes RootType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Status Status { get; set; }
        public int StatusId { get; set; }

        public byte CompeteProcent { get; set; }
        public decimal Cost { get; set; }
        public CurrencyType Currency { get; set; }
        public decimal Residue { get; set; }
        public DateTime DeadLine { get; set; }
        public DateTime DateStartProject { get; set; }

        public ProjectType ProjectType { get; set; }
        public int ProjectTypeId { get; set; }

        public int? UserId { get; set; }
        public User User{ get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }
    }
}
