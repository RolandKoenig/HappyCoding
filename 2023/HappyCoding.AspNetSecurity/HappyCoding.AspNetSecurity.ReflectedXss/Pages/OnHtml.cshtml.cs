using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyCoding.AspNetSecurity.ReflectedXss.Pages;

public class OnHtml : PageModel
{
    public string TxtSearch { get; set; } = string.Empty;

    public OnHtml(ILogger<IndexModel> logger)
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