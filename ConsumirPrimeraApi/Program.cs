using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ConsumirPrimeraApi.Models;
using Newtonsoft.Json;
using ConsumirPrimeraApi.Controllers;


namespace ConsumirPrimeraApi
{
    public enum Menu
    {
        ObtenerAlumnosAsync = 1, ObtenerAlumnoPorIdAsync, CrearAlumnoAsync, ActualizarAlumnoAsync, EliminarAlumnoAsync,  Salir
    }

    internal class Program
    {
        static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            Acciones acciones = new Acciones();


            bool exit = false;
            while (!exit)
            {
                switch (Seleccion())
                {
                    case Menu.ObtenerAlumnosAsync:
                        await acciones.ObtenerAlumnosAsync();
                        break;
                    case Menu.ObtenerAlumnoPorIdAsync:
                        Console.Write("Ingrese el ID del alumno: ");
                        int idObtener = int.Parse(Console.ReadLine());
                        await acciones.ObtenerAlumnoPorIdAsync(idObtener);
                        break;
                    case Menu.CrearAlumnoAsync:
                        await acciones.CrearAlumnoAsync();
                        break;
                    case Menu.ActualizarAlumnoAsync:
                        Console.Write("Ingrese el ID del alumno a actualizar: ");
                        int idActualizar = int.Parse(Console.ReadLine());
                        await acciones.ActualizarAlumnoAsync(idActualizar);
                        break;
                    case Menu.EliminarAlumnoAsync:
                        Console.Write("Ingrese el ID del alumno a eliminar: ");
                        int idEliminar = int.Parse(Console.ReadLine());
                        await acciones.EliminarAlumnoAsync(idEliminar, acciones.GetClient());
                        break;
                    case Menu.Salir:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            }
        }

        static Menu Seleccion()
        {
            Console.WriteLine("Seleccione la opción que desea realizar:");
            Console.WriteLine("1) Obtener registro de alumnos completo");
            Console.WriteLine("2) Obtener registro de alumno por ID");
            Console.WriteLine("3) Crear registro de alumno");
            Console.WriteLine("4) Actualizar registro de alumno");
            Console.WriteLine("5) Eliminar registro de alumno");
            Console.WriteLine("6) Salir");

            try
            {
                Menu opc = (Menu)Convert.ToInt32(Console.ReadLine());
                return opc;
            }
            catch (Exception)
            {
                Console.WriteLine("La opción seleccionada no es válida. Intente de nuevo.");
                return 0;
            }
        }
    }

        

   
}

