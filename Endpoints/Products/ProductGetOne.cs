using IWantApp.Domain.Products;
using IWantApp.Endpoints.Security;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IWantApp.Endpoints.Products;

public class ProductGetOne
{
    public static string Template => "/products/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action([FromRoute] Guid id,[FromServices] ApplicationDbContext context, ILogger<ProductGetOne> log)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null)
        {
            log.LogInformation("Information Not Found.");
            return Results.NotFound();
        }

        log.LogInformation("Information Found.");
        return Results.Ok(product);
    }
}
