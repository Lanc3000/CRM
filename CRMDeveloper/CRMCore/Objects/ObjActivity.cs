using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Objects
{
    /// <summary>
    /// Активити содержащий разрешения 
    /// </summary>
    public class ObjActivity
    {
        public ObjActivity(string title, List<ObjActivityPermision> objActivityPermisions)
        {
            Title = title;
            Permisioins = objActivityPermisions;
        }

        public string Title { get; }

        public List<ObjActivityPermision> Permisioins { get; }
    }
}
