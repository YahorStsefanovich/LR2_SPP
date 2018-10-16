using PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins
{
    public class Plugins : IGenerator, IGeneratorWithValue
    {
          private Random random = new Random((int)DateTime.Now.Ticks);

          public int generateInt()
          {
               return (int)random.Next();
          }

          public int generateInt(int value)
          {
               return (int)random.Next(value);
          }
     }
}
