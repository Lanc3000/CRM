﻿using CRMCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Objects
{
    public class ObjTaskSecond
    {
        public int Id { get; set; }
        public int RootId { get; set; }
        public RootTypes RootType { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string StatusName { get; set; }
        public string StatusColor { get; set; }
        public int StatusId { get; set; }

        public byte CompeteProcent { get; set; }

        public DateTime DeadLine { get; set; }

        public string TaskTypeName { get; set; }
        public int TaskTypeId { get; set; }

        public int? UserId { get; set; }

        public List<ObjParticipant> Participants { get; set; }
        public DateTime Created { get; set; }

        public string RootName { get; set; }
        public string RootUrl { get; set; }
        public string UserFio { get; set; }
    }
}
