using System;
using Microsoft.Extensions.DependencyInjection;
using TddWorkshop;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var iban = Console.ReadLine();

            var services = new ServiceCollection()
                .AddSingleton<IBankCodesProvider, DatabaseBankCodesProvider>()
                .AddSingleton<IbanValidatorNL>()
                .BuildServiceProvider();

            var validator = services.GetService<IbanValidatorNL>();
            Console.WriteLine(validator.IsValid(iban));
        }
    }
}