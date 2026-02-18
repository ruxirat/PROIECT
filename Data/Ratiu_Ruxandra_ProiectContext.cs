using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ratiu_Ruxandra_Proiect.Models;

namespace Ratiu_Ruxandra_Proiect.Data
{
    public class Ratiu_Ruxandra_ProiectContext : DbContext
    {
        public Ratiu_Ruxandra_ProiectContext (DbContextOptions<Ratiu_Ruxandra_ProiectContext> options)
            : base(options)
        {
        }

        public DbSet<Ratiu_Ruxandra_Proiect.Models.Appointment> Appointment { get; set; } = default!;
        public DbSet<Ratiu_Ruxandra_Proiect.Models.Doctor> Doctor { get; set; } = default!;
        public DbSet<Ratiu_Ruxandra_Proiect.Models.Patient> Patient { get; set; } = default!;
        public DbSet<Ratiu_Ruxandra_Proiect.Models.Service> Service { get; set; } = default!;
        public DbSet<Ratiu_Ruxandra_Proiect.Models.Specialization> Specialization { get; set; } = default!;
    }
}
