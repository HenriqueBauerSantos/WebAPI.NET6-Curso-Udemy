namespace IWantApp.Endpoints.Products;

public class ProductsSoldGet
{
    public static string Template => "/products/sold";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;
    [Authorize(Policy = "Employee006Policy")]
    public static async Task<IResult> Action(QueryOrdersBrQtdProductSold query)
    {
        var result = await query.Execute();
        return Results.Ok(result);
    }
}
