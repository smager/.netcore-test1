using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netzwelt_gmfuentes.Models
{

        public partial class Territories
        {
        public List<Territory> Data { get; set; }
        }

        public partial class Territory
        {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Parent { get; set; }
        public List<Territory> Children { get; set; }

    }

}
