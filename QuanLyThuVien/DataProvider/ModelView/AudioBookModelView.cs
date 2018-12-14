using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataProvider.ModelView
{
    public class AudioBookModelView
    {
        public int BookID { get; set; }
        public int AudioID { get; set; }
        public string AudioName { get; set; }
        public Nullable<int> FileAudioSize { get; set; }
        public string FileAudioPath { get; set; }
        public string Alias { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}
