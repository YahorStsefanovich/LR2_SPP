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
               Console.WriteLine(faker.generateDouble().ToString());
               Console.WriteLine(faker.generateFloat().ToString());
               Console.WriteLine(faker.generateInt().ToString());
               Console.WriteLine(faker.generateLong().ToString());
               Console.WriteLine(faker.generateString());
               Console.WriteLine(faker.generateBoolean());
               Console.WriteLine(faker.generateDateTime());
          }
     }
}
