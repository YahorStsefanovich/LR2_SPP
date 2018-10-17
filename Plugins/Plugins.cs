﻿using PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins
{
    public class Plugins : IGenerator
    {
          private Random random = new Random((int)DateTime.Now.Ticks);

          public object generateValue()
          {
               return (Int32)random.Next();
          }

          public object generateInt(int value)
          {
               return (Int32)random.Next(value);
          }
     }
}
