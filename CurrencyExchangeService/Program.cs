using CurrencyExchangeService.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyExchangeService
{
    public static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        public static void Main()
        {
            UpdateServices.Start();
            Console.WriteLine("для остановки приложения нажмите любую клавищу...");
            Console.ReadKey();
            UpdateServices.Stop();
        }
    }
}
