﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoLink.Infra.ServiceToken.Models
{
    public class Token
    {
        public string Secret { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int Expiry { get; set; }

        public int RefreshExpiry { get; set; }
    }
}
