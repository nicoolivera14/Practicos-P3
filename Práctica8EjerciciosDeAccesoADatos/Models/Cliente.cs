using System;
using System.Collections.Generic;

namespace Práctica8EjerciciosDeAccesoADatos.Models;

public partial class Cliente
{
    public long Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Direccion { get; set; }

    public string? DocumentoIdentidad { get; set; }

    public string? Correo { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Alquilere> Alquileres { get; set; } = new List<Alquilere>();
}
