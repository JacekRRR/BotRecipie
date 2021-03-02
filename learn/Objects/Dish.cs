using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learn.Objects
{

    public class Dish
    {

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string directions1 { get; set; }
        public int timeForPrepare { get; set; }
        public int calories { get; set; }
        public string kitchenFrom { get; set; }
        public string directions2 { get; set; }
        public string directions3 { get; set; }
        public string directions4 { get; set; }
        public string pictUrl { get; set; }
        public string video { get; set; }
        public IEnumerable<Ingridient> ingridients { get; set; }
        public string ingridientsList { get; set; }
    }


}



    
    
