using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR2_SPP
{
     class CollectionGenerator
     {
          private Random random;
          private Generator baseGenerator;

          public CollectionGenerator()
          {
               random = new Random((int)DateTime.Now.Ticks);
               baseGenerator = new Generator();
          }

          public object generateList(Type type, Generator generator)
          {
               object list = Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
               int count = (int)baseGenerator.generateByte();

               for (int i = 0; i < count; i++)
               {
                    ((IList)list).Add(generator);
               }
          }         
     }
}
