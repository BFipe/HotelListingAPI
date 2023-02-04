﻿namespace HotelListingAPI_MC.Models
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }

        public int PageNumber { get; set; }

        public int ItemsPerPage { get; set; }

        public List<T> Items { get; set; }
    }
}
