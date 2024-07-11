using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Práctica8EjerciciosDeAccesoADatos.Models
{
    public partial class Pelicula
    {
        public long Id { get; set; }

        public string? Titulo { get; set; }

        public int? Anio { get; set; }

        [Range(1, 10, ErrorMessage = "La Calificacion tiene que ser entre 1 y 10.")]
        public int? Calificacion { get; set; }

        public virtual ICollection<Copia> Copia { get; set; } = new List<Copia>();
    }
}
