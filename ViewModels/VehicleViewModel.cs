using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using angular_netcore.Models;

namespace angular_netcore.ViewModels
{
    [Table("Vehicles")]
    public class VehicleViewModel
    {
        public int ID { get; set; }
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }
        public ContactResource Contact { get; set; }

        public ICollection<int> Features { get; set; }

        public VehicleViewModel()
        {
            Features =  new Collection<int>();
        }
    }
}
