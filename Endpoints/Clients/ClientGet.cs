using IWantApp.Domain.Users;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IWantApp.Endpoints.Clients;

public class ClientGet
{
    public static string Template => "/clients";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;
    [AllowAnonymous]
    public static async Task<IResult> Action(int? page, int? rows, QueryAllUsersWithName query)
    {
        var result = await query.Execute(page.Value, rows.Value);
        return Results.Ok(result);
    }
}
