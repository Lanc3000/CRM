using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CRMCore.Objects
{
    public class StreamWithName
    {
        public string Name { get; set; }

        public string ContentType { get; set; }

        public Stream ObjectStream { get; set; }

        public StreamWithName Assemble(string name, string contenttype, Stream objectstream)
        {
            return new StreamWithName { Name = name, ContentType = contenttype, ObjectStream = objectstream };
        }
    }
}
