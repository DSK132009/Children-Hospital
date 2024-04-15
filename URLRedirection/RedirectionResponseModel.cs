using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLRedirection
{
    public class RedirectionResponseModel
    {
        public string FinalUrl { get; set; }
        public bool PermanentRedirect { get; set; }
    }
}
