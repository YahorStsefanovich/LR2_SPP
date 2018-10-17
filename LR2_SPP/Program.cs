using LR2_SPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR2_SPP
{
     class Program
     {
          static void Main(string[] args)
          {
               Faker faker = new Faker();
               TestClass test,test2;
               ConsoleWriter printer;

               faker.dtoAdd(typeof(Foo));
               faker.dtoAdd(typeof(Bar));
               test = faker.Create<TestClass>();
               Console.WriteLine();
               printer = new ConsoleWriter();
               printer.dtoListAdd(typeof(Foo));
               printer.dtoListAdd(typeof(Bar));
               printer.Print(test, "");
               
          }
     }
}
