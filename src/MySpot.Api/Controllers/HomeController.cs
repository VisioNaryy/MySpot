using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MySpot.Domain.Data.IOptions;

namespace MySpot.Api.Controllers;

[Route("")]
public class HomeController : ControllerBase
{
    private readonly AppOptions _appOptions;

    public HomeController(IOptions<AppOptions> applicationOptions)
    {
        _appOptions = applicationOptions.Value;
    }
    
    [HttpGet]
    public ActionResult Get() => Ok(_appOptions.Name);
}