using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CsvDisposer
{
    class ObjectProperitesMapping
    {
        public object Obj { get; set; }
        public PropertyInfo[] PropertyInfos { get; set; }
    }
}
