using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappCoding.AspNetSecurity.ReflectedXss.Pages;

public class IndexModel : PageModel
{
    public string TxtSearch { get; set; } = string.Empty;

    public IndexModel(ILogger<IndexModel> logger)
    {
   
    }

    public void OnGet()
    {
    }

    public void OnPost()
    {
        var txtSearch = Request.Form["txtSearch"];
        this.TxtSearch = txtSearch.ToString();
    }
}