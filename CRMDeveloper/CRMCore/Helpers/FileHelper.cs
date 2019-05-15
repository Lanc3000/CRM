using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CRMCore.Helpers
{
  public  class FileHelper
    {
        public void SaveFile(IFormFile file, string path)
        {
            using (FileStream fs = File.Create(path))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
        }
    }
}
