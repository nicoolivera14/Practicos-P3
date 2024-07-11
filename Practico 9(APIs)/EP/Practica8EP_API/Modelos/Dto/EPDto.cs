using System.ComponentModel.DataAnnotations;

namespace Practica8EP_API.Modelos.Dto
{
    public class EPDto
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(20)]
        public string Nombre { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }

    }
}
