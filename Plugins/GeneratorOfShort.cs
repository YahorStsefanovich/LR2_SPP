﻿using PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins
{
     class GeneratorOfShort : IGenerator
     {
          private Random random = new Random((int)DateTime.Now.Ticks);

          public object generateValue()
          {
               return (Int16)random.Next();
          }

          public Type GetValueType()
          {
               return typeof(Int16);
          }
     }
}
