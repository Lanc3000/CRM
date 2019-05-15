using CRMCore.DB;
using CRMCore.Enums;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace CRMCore.Repositories.Impl
{
    public class ParticipantRepository : Repository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(DBContext context) : base(context)
        {
        }

        public List<Participant> AllFull()
        {
            return Queryable()
                .Include(x => x.User)
                .ToList();
        }

        public List<Participant> Get(int rootId, RootTypes rootType)
        {
            return Queryable<Participant>()
                .Where(p => p.RootId == rootId && p.RootType == rootType)
                .Include(x => x.User)
                .ToList();
        }

        public override Participant Get(int key)
        {
            return Queryable()
                .Include(x => x.User)
                .SingleOrDefault(x => x.Id == key);
        }

        public Participant Get(int userId, int rootID, RootTypes rootType)
        {
            var result = Queryable()
                .FirstOrDefault(x => x.UserId == userId
                && x.RootId == rootID
                && x.RootType == rootType);

            return result;
        }

        List<Participant> IParticipantRepository.GetList(int userId)
        {
            var result = Queryable()
                .Where(x => x.UserId == userId)
                .ToList();
                
                
            return result;
                
        }
    }
}