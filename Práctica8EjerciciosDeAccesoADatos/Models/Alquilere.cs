using System;
using System.Collections.Generic;

namespace Práctica8EjerciciosDeAccesoADatos.Models;

public partial class Alquilere
{
    public long Id { get; set; }

    public long? IdCopia { get; set; }

    public long? IdCliente { get; set; }

    public DateTime? FechaAlquiler { get; set; }

    public DateTime? FechaTope { get; set; }

    public DateTime? FechaEntregada { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Copia? IdCopiaNavigation { get; set; }
}
