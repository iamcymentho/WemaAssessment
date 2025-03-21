using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Domain.Entities
{
    public class StateLgaMapping
    {
        public string State { get; set; } = string.Empty;
        public List<string> LGAs { get; set; } = new();
    }

}
