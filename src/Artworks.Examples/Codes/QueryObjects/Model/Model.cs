using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.Examples.Codes.QueryObjects.Model
{

    public class TempData
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime crdate { get; set; }
        public DateTime refdate { get; set; }
        public string xtype { get; set; }

    }

    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public int State { get; set; }

    }
}
