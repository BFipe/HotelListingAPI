using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListingAPI_DATA.Entities
{
    public class HotelEntity
    {
        public int HotelEntityId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public double? Rating { get; set; }


        public int CountryId { get; set; }
        public CountryEntity Country { get; set; }
    }
}
