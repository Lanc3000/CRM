using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore
{
    public static class CoreConfiguration
    {
        //ToDo формировать путь всегда уникальный
        public static string PathRoot  { get; set;}
        public static string PathStorage { get { return PathRoot + @"\Storage"; } }
        public static string PathAvatar { get { return PathRoot + @"\Avatar"; } }
    }
}
