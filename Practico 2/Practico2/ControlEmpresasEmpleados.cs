using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practico2Ej2
{
    internal class ControlEmpresasEmpleados
    {
        public List<Empresa> listaEmpresas;
        public List<Empleado> listaEmpleados;

        public ControlEmpresasEmpleados()
        {
            listaEmpresas = new List<Empresa>();
            listaEmpleados = new List<Empleado>();

            listaEmpresas.Add(new Empresa { Id = 1, Nombre = "IAlpha" });
            listaEmpresas.Add(new Empresa { Id = 2, Nombre = "Udelar" });
            listaEmpresas.Add(new Empresa { Id = 3, Nombre = "Spacez"});

            listaEmpleados.Add(new Empleado { Id = 1, Nombre = "Gonzalo", Cargo = "CEO", EmpresaId = 1, Salario = 3000 });
            listaEmpleados.Add(new Empleado { Id = 2, Nombre = "JuanC", Cargo = "Desarrollador", EmpresaId =1 , Salario = 2900 });
            listaEmpleados.Add(new Empleado { Id = 3, Nombre = "JuanR", Cargo = "Desarrollador", EmpresaId = 1, Salario = 2000 });
            listaEmpleados.Add(new Empleado { Id = 4, Nombre = "Daniel", Cargo = "Desarrollador", EmpresaId = 1, Salario = 2000 });
            listaEmpleados.Add(new Empleado { Id = 5, Nombre = "GonzaloT", Cargo = "CEO", EmpresaId = 2, Salario = 2200 });
            listaEmpleados.Add(new Empleado { Id = 6, Nombre = "Leonardo", Cargo = "CEO", EmpresaId = 1, Salario = 3000 });
            listaEmpleados.Add(new Empleado { Id = 1, Nombre = "Gonzalo", Cargo = "CEO", EmpresaId = 3, Salario = 3000 });
            listaEmpleados.Add(new Empleado { Id = 6, Nombre = "Leonardo", Cargo = "CEO", EmpresaId = 3, Salario = 3000 });
        }
        //___________Getters___________
        public void getSeo(string _Cargo)
        {
            IEnumerable<Empleado> empleados = from empleado in listaEmpleados
                                              where empleado.Cargo == _Cargo
                                              select empleado;
            foreach(Empleado elemento in empleados)
            {
                elemento.getDatosEmpleado();
            }
        }
        public void getEmpleadosOrdenados()
        {
            IEnumerable<Empleado> empleados = from empleado in listaEmpleados
                                              orderby empleado.Nombre
                                              select empleado;
            foreach(Empleado elemento in empleados)
            {
                elemento.getDatosEmpleado();
            }
        }
        public void getEmpleadosOrdenadosSegun()
        {
            IEnumerable<Empleado> empleados = from empleado in listaEmpleados
                                              orderby empleado.Salario
                                              select empleado;
            foreach(Empleado elemento in empleados)
            {
                elemento.getDatosEmpleado();
            }
        }
        public void getEmpleadosEmpresa(int _Empresa)
        {
            IEnumerable<Empleado> empleados = from empleado in listaEmpleados
                                              join empresa in listaEmpresas on empleado.EmpresaId
                                              equals empresa.Id
                                              where empresa.Id == _Empresa
                                              select empleado;
            foreach(Empleado elemento in empleados)
            {
                elemento.getDatosEmpleado();
            }
        }

        //_____________Metodos Particulares____________
        public void promedioSalario()
        {
            var consulta = from e in listaEmpleados
                           group e by e.EmpresaId into g
                           select new { empresa = g.Key, PromedioSalario = g.Average(e => e.Salario) };

            foreach(var resultado in consulta)
            {
                switch(resultado.empresa)
                {
                    case 1: Console.WriteLine($"Empresa IAlpha - Promedio de salario: {resultado.PromedioSalario}");
                    break;

                    case 2: Console.WriteLine($"Empresa Udelar - Promedio de salario: {resultado.PromedioSalario}");
                    break;

                    case 3: Console.WriteLine($"Empresa SpaceZ - Promedio de salario: {resultado.PromedioSalario}");
                    break;
                }
            }
        }
    }   
}
