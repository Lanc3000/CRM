using System;
using System.Collections.Generic;
using System.Text;
using CRMCore.Enums;
using CRMCore.Repositories;

namespace CRMCore.Services.Impl
{
    public class RootTypesService : IRootTypesService
    {
        IClientRepository _clientRepository { get; }

        public RootTypesService(
            IClientRepository clientRepository
           )
        {
            _clientRepository = clientRepository;
        }

        public string GetRootName(RootTypes rootType, int rootId)
        {
            string result =string.Empty;
            switch (rootType)
            {
                case RootTypes.Client:
                    var client = _clientRepository.Get(rootId);
                    result = client.CompanyName;
                    break;
            }
            return result;
        }

        public string GetRootUrl(RootTypes rootType, int rootId)
        {
            string result = string.Empty;
            switch (rootType)
            {
                case RootTypes.Client:
                    result = "/Clients/Details/"+ rootId;
                    break;
            }
            return result;
        }
    }
}
