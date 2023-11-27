using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Data.ViewModels
{
    public class CertificatesViewModel
    {
        public string CertificateName { get; set; }

        public string CertifiedFrom { get; set; }

        public string CertifiedDate { get; set; }
    }
}
