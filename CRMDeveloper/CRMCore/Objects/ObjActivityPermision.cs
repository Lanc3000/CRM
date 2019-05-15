using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Objects
{
    /// <summary>
    /// Разрешение Активити
    /// </summary>
    public class ObjActivityPermision
    {
        public ObjActivityPermision(string key, string title)
        {
            Key = key;
            Title = title;
        }
        public string Title { get; }

        public string Key { get; }
    }
}
