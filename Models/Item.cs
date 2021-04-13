using BlazorApp.Enums;

namespace BlazorApp.Models
{
    public class Item
    {
        public Item(Modules module, bool showQuantity, int quantity, bool included, bool canEditQuantity)
        {
            Module = module;
            ShowQuantity = showQuantity;
            Quantity = quantity;
            Included = included;
            CanEditQuantity = canEditQuantity;

        }

        public Modules Module { get; set; }
        public bool ShowQuantity { get; set; }
        public int Quantity { get; set; }
        public bool Included { get; set; }
        public bool CanEditQuantity { get; set; }

    }
}
