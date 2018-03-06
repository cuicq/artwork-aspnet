using Artworks.Infrastructure.Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.Examples.Codes.Application.Code.CommonModel
{
    public class Model : AggregateRoot
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
