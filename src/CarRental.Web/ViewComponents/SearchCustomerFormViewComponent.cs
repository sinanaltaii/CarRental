using CarRental.Web.Models.Customer;
using Microsoft.AspNetCore.Mvc;
namespace CarRental.Web.ViewComponents;

public class SearchCustomerFormViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult(View("~/Views/Customer/Components/SearchCustomerForm/Default.cshtml", new CustomerIdentityNumber(string.Empty)));
    }
}