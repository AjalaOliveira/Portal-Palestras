using System;
using System.Collections.Generic;
using System.Text;

namespace Palestras.Tests.API.DTO
{
    public class UserJsonDTO
    {
        public bool success { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public string email { get; set; }
            public string password { get; set; }
            public bool rememberMe { get; set; }
        }

    }
}
