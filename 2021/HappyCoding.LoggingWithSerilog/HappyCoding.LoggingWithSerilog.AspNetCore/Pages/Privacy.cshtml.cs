using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Serilog;

namespace HappyCoding.LoggingWithSerilog.AspNetCore.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private readonly IDiagnosticContext _diagnosticContext;

        public PrivacyModel(ILogger<PrivacyModel> logger, IDiagnosticContext diagnosticContext)
        {
            _logger = logger;
            _diagnosticContext = diagnosticContext;
        }

        public void OnGet()
        {
            _diagnosticContext.Set("Privacy", "arrived");
        }
    }
}
