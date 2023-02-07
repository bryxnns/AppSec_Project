using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication3.Pages
{
    [Authorize(Policy = "MustBelongToHRDepartment")]
    public class HumanResourceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
