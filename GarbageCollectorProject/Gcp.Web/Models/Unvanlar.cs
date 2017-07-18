using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Gcp.Web.Models
{
    public class Unvanlar
    {
        public int UnvanID { get; set; }
        public string UnvanAd { get; set; }

        public IEnumerable<Personel> Personel { get; set; }
    }
}