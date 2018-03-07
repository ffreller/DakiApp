using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DakiApp.webapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }


        public static IEnumerable<int> GetNumbers(int de, int para)
        {
            List<int> numeros = new List<int>();

            while(de < para) numeros.Add(de++);

            return numeros;
        }

        public static IEnumerable<int> GetNumbersLazy(int de, int para)
        {
            while(de < para) yield return de++;
        }


        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
