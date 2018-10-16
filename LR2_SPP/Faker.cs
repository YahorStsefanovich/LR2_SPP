using PluginInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LR2_SPP
{
     class Faker
     {
          private Random random = new Random((int)DateTime.Now.Ticks);

          public DateTime generateDateTime()
          {
               Random rnd = new Random();
               DateTime start = new DateTime(1995, 1, 1);
               int range = (DateTime.Today - start).Days;
               long ticks = start.AddDays(generateInt(range)).Ticks +
                   generateLong();
               return new DateTime(ticks);
          }

          public int generateInt()
          {
               String pluginName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Plugins.dll");

               if (!File.Exists(pluginName))
               {
                    throw new InvalidPluginPathException("Wrong plugin's path");
               }

               Assembly assembly = Assembly.LoadFrom(pluginName);
               foreach (Type t in assembly.GetExportedTypes())
               {
                    if (t.IsClass && typeof(IGenerator).IsAssignableFrom(t))
                    {
                         IGenerator generator = (IGenerator)Activator.CreateInstance(t);
                         return generator.generateInt();
                    }
               }
               return 0;
          }


          public int generateInt(int value)
          {
               String pluginName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Plugins.dll");

               if (!File.Exists(pluginName))
               {
                    throw new InvalidPluginPathException("Wrong plugin's path");
               }

               Assembly assembly = Assembly.LoadFrom(pluginName);
               foreach (Type t in assembly.GetExportedTypes())
               {
                    if (t.IsClass && typeof(IGeneratorWithValue).IsAssignableFrom(t))
                    {
                         IGeneratorWithValue generator = (IGeneratorWithValue)Activator.CreateInstance(t);
                         return generator.generateInt(value);
                    }
               }
               return 0;
          }         

          public List<bool> generateBoolList()
          {
               List<bool> list = new List<bool>();
               byte length = generateByte();
               for (int i = 0; i < length; i++)
               {
                    list.Add(generateBoolean());
               }
               return list;
          }

          public List<int> generateIntList()
          {
               List<int> list = new List<int>();
               byte length = generateByte();
               for (int i = 0; i < length; i++)
               {
                    list.Add(generateInt());
               }
               return list;
          }

          public List<float> generateFloatList()
          {
               List<float> list = new List<float>();
               byte length = generateByte();
               for (int i = 0; i < length; i++)
               {
                    list.Add(generateFloat());
               }
               return list;
          }

          public List<double> generateDobleList()
          {
               List<double> list = new List<double>();
               byte length = generateByte();
               for (int i = 0; i < length; i++)
               {
                    list.Add(generateDouble());
               }
               return list;
          }

          public List<byte> generateByteList()
          {
               List<byte> list = new List<byte>();
               byte length = generateByte();
               for (int i = 0; i < length; i++)
               {
                    list.Add(generateByte());
               }
               return list;
          }

          public List<long> generateLongList()
          {
               List<long> list = new List<long>();
               byte length = generateByte();
               for (int i = 0; i < length; i++)
               {
                    list.Add(generateLong());
               }
               return list;
          }

          public List<char> generateCharList()
          {
               List<char> list = new List<char>();
               byte length = generateByte();
               for (int i = 0; i < length; i++)
               {
                    list.Add(generateChar());
               }
               return list;
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
