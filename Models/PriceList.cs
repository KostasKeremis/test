using System.Collections.Generic;

namespace BlazorApp.Models
{
    public class PriceList
    {
        public PriceList() { }

        public PriceList(List<PriceListItem> items)
        {
            Items = items;
        }

        public List<PriceListItem> Items { get; set; }
    }
}
