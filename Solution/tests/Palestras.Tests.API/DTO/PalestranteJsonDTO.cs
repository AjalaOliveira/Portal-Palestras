using System;
using System.Collections.Generic;
using System.Text;

namespace Palestras.Tests.API.DTO
{
    public class PalestranteJsonDTO
    {
        public bool success { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public string id { get; set; }
            public string nome { get; set; }
            public string miniBio { get; set; }
            public string url { get; set; }
        }

    }
}
