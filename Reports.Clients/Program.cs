using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Reports.DAL.Entities;

namespace Reports.Clients
{
    internal static class Program
    {
        static readonly private HttpClient _client = new HttpClient();
        internal static void Main(string[] args)
        {
            CreateEmployee("aboba");
            FindEmployeeById("ac8ac3ce-f738-4cd6-b131-1aa0e16eaadc");
            FindEmployeeByName("aboba");
            FindEmployeeByName("kek");
        }

        private static async void CreateEmployee(string name)
        {
            HttpResponseMessage response = await _client.PostAsync(
                $"https://localhost:5001/employees/?name={name}",
                new StringContent(name));
            
            Stream responseStream = await response.Content.ReadAsStreamAsync();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            string responseString = await readStream.ReadToEndAsync();
            
            Employee employee = JsonConvert.DeserializeObject<Employee>(responseString);

            Console.WriteLine("Created employee:");
            Console.WriteLine($"Id: {employee.Id}");
            Console.WriteLine($"Name: {employee.Name}");
        }

        private static async void FindEmployeeById(string id)
        {
            HttpResponseMessage response = await _client.GetAsync($"https://localhost:5001/employees/?id={id}");

            try
            {
                Stream responseStream = await response.Content.ReadAsStreamAsync();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                string responseString = await readStream.ReadToEndAsync();
                
                Employee employee = JsonConvert.DeserializeObject<Employee>(responseString);

                Console.WriteLine("Found employee by id:");
                if (employee == null) return;
                Console.WriteLine($"Id: {employee.Id}");
                Console.WriteLine($"Name: {employee.Name}");
            }
            catch (WebException e)
            {
                Console.WriteLine("Employee was not found");
                await Console.Error.WriteLineAsync(e.Message);
            }
        }

        private static async void FindEmployeeByName(string name)
        {
            HttpResponseMessage response = 
                await _client.GetAsync($"https://localhost:5001/employees/?name={name}");
            try
            {
                Stream responseStream = await response.Content.ReadAsStreamAsync();
                using var readStream = new StreamReader(responseStream, Encoding.UTF8);
                string responseString = await readStream.ReadToEndAsync();
                
                Employee employee = JsonConvert.DeserializeObject<Employee>(responseString);

                Console.WriteLine("Found employee by name:");
                if (employee == null) return;
                Console.WriteLine($"Id: {employee.Id}");
                Console.WriteLine($"Name: {employee.Name}");
            }
            catch (WebException e)
            {
                Console.WriteLine("Employee was not found");
                await Console.Error.WriteLineAsync(e.Message);
            }
        }
    }
}