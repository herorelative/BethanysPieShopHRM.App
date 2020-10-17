using BethanysPieShopHRM.App.Services;
using BethanysPieShopHRM.ComponentsLibrary.Map;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.App.Pages
{
    public partial class EmployeeDetail:ComponentBase
    {
        [Parameter]
        public int Id { get; set; }
        public Employee Employee { get; set; } = new Employee();

        public List<Marker> MapMarkers { get; set; } = new List<Marker>();

        [Inject]
        IEmplyeeDataService EmplyeeDataService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Employee = await EmplyeeDataService.GetEmployeeDetails(Id);
            MapMarkers = new List<Marker>
            {
                new Marker{ 
                    Description = $"{Employee.FirstName} {Employee.LastName}",
                    ShowPopup=false,
                    X = Employee.Longitude,
                    Y = Employee.Latitude
                }
            };
        }
    }
}