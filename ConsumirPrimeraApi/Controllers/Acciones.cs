using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ConsumirPrimeraApi.Models;
using Newtonsoft.Json;




namespace ConsumirPrimeraApi.Controllers
{
    internal class Acciones
    {
        public readonly HttpClient client = new HttpClient();



        public async Task ObtenerAlumnosAsync()
        {
            client.BaseAddress = new Uri("https://localhost:7061/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("api/alumnos");
            if (response.IsSuccessStatusCode)
            {
                var alumnos = JsonConvert.DeserializeObject<List<Alumno>>(await response.Content.ReadAsStringAsync());
                foreach (var alumno in alumnos)
                {
                    Console.WriteLine($"ID: {alumno.Id}, Nombre: {alumno.Nombre}, Edad: {alumno.Edad}, Fecha Nacimiento: {alumno.FechaNacimiento.ToShortDateString()}");
                }
            }
            else
            {
                Console.WriteLine("Error al obtener los alumnos.");
            }
        }

        public  async Task ObtenerAlumnoPorIdAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"api/alumnos/{id}");
            if (response.IsSuccessStatusCode)
            {
                var alumno = JsonConvert.DeserializeObject<Alumno>(await response.Content.ReadAsStringAsync());
                Console.WriteLine($"ID: {alumno.Id}, Nombre: {alumno.Nombre}, Edad: {alumno.Edad}, Fecha Nacimiento: {alumno.FechaNacimiento.ToShortDateString()}");
            }
            else
            {
                Console.WriteLine("Alumno no encontrado.");
            }
        }

        public async Task CrearAlumnoAsync()
        {
            Console.Write("Ingrese el nombre del alumno: ");
            string nombre = Console.ReadLine();
            Console.Write("Ingrese la edad del alumno: ");
            int edad = int.Parse(Console.ReadLine());
            Console.Write("Ingrese la fecha de nacimiento del alumno (yyyy-mm-dd): ");
            DateTime fechaNacimiento = DateTime.Parse(Console.ReadLine());

            var alumno = new Alumno { Nombre = nombre, Edad = edad, FechaNacimiento = fechaNacimiento };
            var json = JsonConvert.SerializeObject(alumno);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/alumnos", content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Alumno creado con éxito.");
            }
            else
            {
                Console.WriteLine("Error al crear el alumno.");
            }
        }

        public async Task ActualizarAlumnoAsync(int id)
        {
            Console.Write("Ingrese el nuevo nombre del alumno: ");
            string nombre = Console.ReadLine();
            Console.Write("Ingrese la nueva edad del alumno: ");
            int edad = int.Parse(Console.ReadLine());
            Console.Write("Ingrese la nueva fecha de nacimiento del alumno (yyyy-mm-dd): ");
            DateTime fechaNacimiento = DateTime.Parse(Console.ReadLine());

            var alumno = new Alumno { Nombre = nombre, Edad = edad, FechaNacimiento = fechaNacimiento };
            var json = JsonConvert.SerializeObject(alumno);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync($"api/alumnos/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Alumno actualizado con éxito.");
            }
            else
            {
                Console.WriteLine("Error al actualizar el alumno.");
            }
        }

        public async Task EliminarAlumnoAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/alumnos/{id}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Alumno eliminado con éxito.");
            }
            else
            {
                Console.WriteLine("Error al eliminar el alumno.");
            }
        }
    }


}

