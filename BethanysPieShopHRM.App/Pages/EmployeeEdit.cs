using BethanysPieShopHRM.App.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.App.Pages
{
    public partial class EmployeeEdit : ComponentBase
    {
        [Inject]
        NavigationManager _navigate { get; set; }
        [Inject]
        public IEmplyeeDataService _emplyeeDataService { get; set; }
        [Inject]
        public ICountryDataService _countryDataService { get; set; }
        [Inject]
        public IJobCategoryDataService _jobCategoryDataService { get; set; }
        [Parameter]
        public int EmployeeId { get; set; }

        protected string CountryId = string.Empty;
        protected string JobCategoryId = string.Empty;

        public IEnumerable<Country> Countries { get; set; } = new List<Country>();
        public IEnumerable<JobCategory> JobCategories { get; set; } = new List<JobCategory>();

        public Employee Employee { get; set; } = new Employee();

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;
        protected override async Task OnInitializedAsync()
        {
            Saved = false;
            Countries = (await _countryDataService.GetAllCountry()).ToList();
            JobCategories = (await _jobCategoryDataService.GetAllJobCategories()).ToList();
            
            if(EmployeeId == 0)
            {
                Employee = new Employee { CountryId = 1, JobCategoryId = 1, BirthDate = DateTime.Now,JoinedDate = DateTime.Now };
            }
            else
            {
                Employee = await _emplyeeDataService.GetEmployeeDetails(EmployeeId);
            }

            CountryId = Employee.CountryId.ToString();
            JobCategoryId = Employee.JobCategoryId.ToString();
        }
        protected async Task HandleValidSubmit()
        {
            Saved = false;
            Employee.CountryId = int.Parse(CountryId);
            Employee.JobCategoryId = int.Parse(JobCategoryId);
            if(Employee.EmployeeId == 0)
            {
                var addedEmployee = await _emplyeeDataService.AddEmployee(Employee);
                if(addedEmployee !=null)
                {
                    StatusClass = "alert-success";
                    Message = "New Employee added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new employee. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                await _emplyeeDataService.UpdateEmployee(Employee);
                StatusClass = "alert-success";
                Message = "New Employee updated successfully.";
                Saved = true;
            }
        }
        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task DeleteEmployee()
        {
            await _emplyeeDataService.DeleteEmployee(EmployeeId);
            StatusClass = "alert-success";
            Message = "Deleted successfully.";
            Saved = true;
        }

        protected void NavigateToOverview()
        {
            _navigate.NavigateTo("/employeeoverview");
        }
    }
}
