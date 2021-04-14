using System;
using System.Linq;
using System.Collections.Generic;
using BlazorApp.Models;
using System.Threading.Tasks;
using BlazorApp.Enums;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages
{
    public partial class Index : ComponentBase
    {
        List<Item> items { get; set; } = new List<Item>();
        Item MainItem { get; set; }
        DateTime expirationDate { get; set; } = DateTime.Today;
        DateTime activationDate { get; set; } = DateTime.Today;
        int Quantity { get; set; }
        double TotalCost { get; set; } = 0;
        PriceList PriceList { get; set; }
        List<PriceListItem> _priceListItems = new List<PriceListItem>();
        List<Modules> AvailableModules = new List<Modules>();
        List<Modules> AllModules { get; set; } = Enum.GetValues(typeof(Modules)).Cast<Modules>().ToList();
        public bool IsSearching { get; set; }
        public bool IsMainFormVisible { get; set; }

        public string Query { get; set; } = string.Empty;

        public bool IsSearchFormVisible { get; set; }

        private List<Modules> GetAvailabeModules()
        {
            return AllModules.Except(items.Select(item => item.Module)).ToList();
        }

        protected override async Task OnInitializedAsync()
        {
            items.Add(new Item(module: Enums.Modules.Student, showQuantity: true, quantity: 50, included: true, canEditQuantity: true));
            items.Add(new Item(module: Enums.Modules.Billing, showQuantity: false, quantity: 0, included: false, canEditQuantity: false));
            items.Add(new Item(module: Enums.Modules.Academic, showQuantity: false, quantity: 0, included: false, canEditQuantity: false));
            items.Add(new Item(module: Enums.Modules.Admission, showQuantity: false, quantity: 0, included: false, canEditQuantity: false));
            items.Add(new Item(module: Enums.Modules.Core, showQuantity: false, quantity: 0, included: true, canEditQuantity: false));
            items.Add(new Item(module: Enums.Modules.Email, showQuantity: true, quantity: 100, included: true, canEditQuantity: false));
            items.Add(new Item(module: Enums.Modules.Notification, showQuantity: true, quantity: 200, included: true, canEditQuantity: false));
            items.OrderBy(items => items.Included);
            MainItem = items.FirstOrDefault(it => it.Module == Enums.Modules.Student);

            _priceListItems.Add(new PriceListItem() { Module = Enums.Modules.Student, Price = 5 });
            _priceListItems.Add(new PriceListItem() { Module = Enums.Modules.Billing, Price = 5 });
            _priceListItems.Add(new PriceListItem() { Module = Enums.Modules.Academic, Price = 5 });
            _priceListItems.Add(new PriceListItem() { Module = Enums.Modules.Crm, Price = 5 });

            PriceList = new PriceList(_priceListItems);
            AvailableModules = GetAvailabeModules();
            IsSearchFormVisible = true;

            await CalculatePrice();

            await Task.Delay(2000);

        }

        public async Task RemoveItem(Item item)
        {
            items.RemoveAll(it => it.Module == item.Module);
            AvailableModules = GetAvailabeModules();
            await CalculatePrice();
            await Task.CompletedTask;
        }

        public async Task CalculatePrice()
        {
            TotalCost = PriceList.Items
                .Where(priceItem => items.Select(transactionItem => transactionItem.Module)
                .Contains(priceItem.Module)).Select(priceItem => priceItem.Price)
                .Aggregate((total, price) => total + price) * MainItem.Quantity;
            await Task.CompletedTask;
        }

        public async Task AddItem(Enums.Modules module)
        {
            var newItem = new Item(module: module, canEditQuantity: false, showQuantity: false, quantity: 0, included: false);
            items.Add(newItem);
            AvailableModules = GetAvailabeModules();
            await CalculatePrice();
            newItem.IsVisible = true;
            await Task.CompletedTask;

        }

        public async Task SubmitSearch()
        {
            IsSearching = true;
            await Task.Delay(2000); // Make the API call
            // Success
            IsMainFormVisible = true;
            IsSearchFormVisible = false;
            IsSearching = false;

        }



    }
}
