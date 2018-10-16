using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR2_SPP
{
     class Faker
     {
          Random random = new Random((int)DateTime.Now.Ticks);

          public DateTime generateDateTime()
          {
               Random rnd = new Random()
               DateTime start = new DateTime(1995, 1, 1);
               int range = (DateTime.Today - start).Days;
               long ticks = start.AddDays(generateInt(range)).Ticks +
                   generateLong();
               return new DateTime(ticks);
          }    

          public bool generateBoolean()
          {
               if (generateInt() % 2 == 0)
               {
                    return false;
               }
               return true;
          }

          public string generateString()
          {               
               int length = generateInt(25) + 1; 
               string symbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890^@*+-+%()!?";
		     StringBuilder result = new StringBuilder(length);
               for (int i = 0; i < length; i++)
               {
                    int pos = random.Next(0, symbols.Length);
                    result.Append(symbols[pos]);
               }
               return result.ToString();     
          }

          public char generateChar()
          {
               return (char)generateInt();
          }

          public byte generateByte()
          {
               return (byte)generateInt();
          }

          public int generateInt()
          {
               
               return (int)random.Next();
          }

          public int generateInt(int value)
          {
               return (int)random.Next(value);
          }

          public double generateDouble()
          {
               return random.NextDouble();
          }

          public float generateFloat()
          {
               return (float)generateDouble();
          }

          public long generateLong()
          {
               return (long)random.Next();
          }

     }
}
