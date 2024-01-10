using Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Catalogs
{
    [Table("COUNTIES")]
    public class County: DomainModelBase, IDomainModel
    {
        public string NAME { get; set; }

        public IEnumerable<City> Cities { get; set; } 
    }
}
