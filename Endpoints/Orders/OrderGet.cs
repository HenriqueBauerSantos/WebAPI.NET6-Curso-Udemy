namespace IWantApp.Endpoints.Orders;

public class OrderGet
{
    public static string Template => "/order/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;
    [Authorize]
    public static async Task<IResult> Action( HttpContext http, [FromRoute] Guid id,
        [FromServices] ApplicationDbContext context,
        ILogger<OrderGet> log)
    {
        string accessToken = http.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        JwtSecurityToken jwt = new JwtSecurityToken(accessToken);    
        var Employ = jwt.Claims.FirstOrDefault(claim => claim.Type == "EmployeeCode");
        var order = await context.Orders.FindAsync(id);
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        // Para test:
        log.LogWarning("EmployCode: " + Employ);
        log.LogWarning("idPedido: " + id);
        log.LogWarning("Order: " + order);
        if (order == null)
        {
            return Results.NotFound("Order not found.");
        }
        else if (Employ != null || userId == order.ClientId)
        {
            return Results.Ok(order);
        }
        return Results.BadRequest("Não autorizado.");
    }
}
