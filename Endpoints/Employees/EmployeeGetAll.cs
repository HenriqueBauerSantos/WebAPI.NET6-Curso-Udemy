﻿using Dapper;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;
    [Authorize(Policy = "Employee006Policy")]
    public static async Task<IResult> Action(int? page, int? rows, QueryAllUsersWithName query)
    {
        var result = await query.Execute(page.Value, rows.Value);
        return Results.Ok(result);
    }
}
