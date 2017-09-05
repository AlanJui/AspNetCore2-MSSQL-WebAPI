using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_001.Models
{
    [Table("Shippers")]
    public class Shipper
    {
        public Shipper()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int ShipperID { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string Phone { get; set; }

        [JsonIgnore]
        [InverseProperty("Shipper")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
