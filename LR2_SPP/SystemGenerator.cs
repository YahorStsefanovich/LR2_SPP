using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR2_SPP
{
     class SystemGenerator
     {
          public DateTime generateDateTime()
          {
               Random rnd = new Random();
               DateTime start = new DateTime(1995, 1, 1);
               int range = (DateTime.Today - start).Days;
               long ticks = start.AddDays(generateInt(range)).Ticks +
                   generateLong();
               return new DateTime(ticks);
          }
     }
}
