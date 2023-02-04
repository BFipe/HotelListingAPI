using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI_MC.Models
{
    public class QueryParameters
    {
        private int _pageSize = 15;

        [Range(0, int.MaxValue)]
        public int PageNumber { get; set; }

        [Range(1, int.MaxValue)]
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

    }
}
