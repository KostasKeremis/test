using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Components;
using BlazorApp.Enums;
using System.Linq;
using BlazorApp.Models;
using System.Threading.Tasks;

namespace BlazorApp.Components
{
    public partial class ModuleSelect
    {
        [Parameter]
        public List<Item> Items { get; set; }

        [Parameter]
        public EventCallback<Modules> AddNewItem { get; set; }

        [Parameter]
        public List<Modules> AvailableModules { get; set; }
        private Modules SelectedModule { get; set; }



        private void SetModule(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                var parsed = Enum.TryParse(e.Value.ToString(), out Modules _selectedModule);
                if (parsed)
                {
                    SelectedModule = _selectedModule;
                }
            }
        }

        private async Task SubmitModuleChange()
        {
            await AddNewItem.InvokeAsync(SelectedModule);
            SelectedModule = Modules.None;
        }


    }
}
