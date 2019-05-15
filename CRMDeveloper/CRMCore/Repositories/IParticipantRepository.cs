
using System;
using System.Collections.Generic;
using System.Text;

using CRMCore.DB;
using CRMCore.Enums;

namespace CRMCore.Repositories
{
    public interface IParticipantRepository : IRepository<Participant>
    {
        List<Participant> Get(int rootId, RootTypes rootType);

        /// <summary>
        /// Метод возвращающий id проектов для одного User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Project Ids</returns>
        List<Participant> GetList(int userId);
        Participant Get(int userId, int projectId, RootTypes rootType);
        List<Participant> AllFull();
    }
}
