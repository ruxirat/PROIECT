using System.Collections.Generic;
using System.Numerics;

namespace Ratiu_Ruxandra_Proiect.Models
{
    public class Specialization
    {
        public int SpecializationId { get; set; }  // PK
        public string Name { get; set; } = string.Empty;

        public ICollection<Doctor>? Doctors { get; set; }
    }
}
