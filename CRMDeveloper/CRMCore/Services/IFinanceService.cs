using System;
using System.Collections.Generic;
using System.Text;
using CRMCore.Enums;
using CRMCore.Extensions;
using CRMCore.Objects;

namespace CRMCore.Services
{
    public interface IFinanceService
    {
        ServiceResult<List<ObjFinance>> GetList(int rootId, RootTypes rootType);
        ServiceResult Add(ObjFinance objFinance);
        ServiceResult Delete(int id);
        List<ObjFinance> All();

        List<ObjFinanceSubType> GetFSubTypes();
        void Add(ObjFinanceSubType obj);
        ObjFinanceSubType GetSubType(int id);
        void Edit(ObjFinanceSubType obj);
        void DeleteSubType(int id);
        
    }
}
