using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PractAssignment.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IDataProtectionProvider _dataProtectionProvider;
    public IndexModel(ILogger<IndexModel> logger, IDataProtectionProvider dataProtectionProvider)
    {
        _logger = logger;
        _dataProtectionProvider = dataProtectionProvider;
    }

    public void OnGet()
    {
    }
    
    public string DecryptData(string protectedData)
    {
        var protector = _dataProtectionProvider.CreateProtector("FreshFarmMarket");
        string data = protector.Unprotect(protectedData);
        return data;
    }
}