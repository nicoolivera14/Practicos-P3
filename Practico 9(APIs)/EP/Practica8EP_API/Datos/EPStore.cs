using Practica8EP_API.Modelos.Dto;

namespace Practica8EP_API.Datos

{
    public class EPStore
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public static List<EPDto> EPList = new List<EPDto>
        {
            new EPDto {ID = 1, Nombre = "Vista a la Piscina", Ocupantes= 3, MetrosCuadrados=50 }

            new EPDto {ID = 2, Nombre = "Vista a la Playa", Ocupantes=2, MetrosCuadrados=100}
        }
    }
}
