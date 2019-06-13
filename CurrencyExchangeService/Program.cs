using CurrencyExchangeService.Core;
using System;

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
