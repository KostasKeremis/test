using System.Reflection.Metadata;
using BlazorApp.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.CartItemsProduct
{
    public partial class ItemCard
    {
        [Parameter] public Item Item { get; set; }

        [Parameter] public EventCallback OnRemoveItem { get; set; }

    }
}
