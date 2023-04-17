﻿using Dapper;
using IWantApp.Endpoints.Products;
using Microsoft.Data.SqlClient;

namespace IWantApp.Infra.Data;

public class QueryOrdersBrQtdProductSold
{
    private readonly IConfiguration configuration;
    public QueryOrdersBrQtdProductSold(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public async Task<IEnumerable<ProductSoldResponse>> Execute()
    {
        var db = new SqlConnection(configuration["ConnectionString:IWantDb"]);
        var query =
           @"SELECT p.Id, p.Name, COUNT(*) qtd
            FROM Orders o 
            INNER JOIN OrderProducts op ON o.Id = op.OrdersId
            INNER JOIN Products p on p.Id = op.ProductsId
            GROUP BY
            p.Id, p.Name
            ORDER BY qtd desc";
        return await db.QueryAsync<ProductSoldResponse>(query);
    }
}
