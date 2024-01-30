using System.Data;
using Microsoft.AspNetCore.Mvc;
using Radit_BackEnd_30012024.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace Radit_BackEnd_30012024.Controllers;

[Route("api/customer")]
[ApiController]
public class CustomerController : Controller
{
    [HttpPost]
    [Route("create")]
    public IActionResult Create()
    {
        try
        {
            string name = HttpContext.Request.Query["name"].ToString();

            using (var db = new DbMainContext())
            {
                Customer data = new Customer();

                data.name = name;

                db.Customer.Add(data);
                db.SaveChanges();

                object json = new { code = 200, status = "Create data berhasil" };
                return Ok(json);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            object res = new { code = 400, status = $"Error: {e.Message}" };
            return Ok(res);
        }
    }

    [HttpGet]
    [Route("list")]
    public ActionResult List()
    {
        try
        {
            var data = new List<object>();

            using (var db = new DbMainContext())
            {
                using (var cmd = db.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = $"SELECT accountId, name, points FROM customer;";

                    cmd.CommandType = CommandType.Text;
                    db.Database.OpenConnection();

                    var result = cmd.ExecuteReader();

                    while (result.Read())
                    {
                        data.Add(
                            new
                            {
                                accountId = result.IsDBNull(0) ? 0 : result.GetInt16(0),
                                name = result.IsDBNull(1) ? "" : result.GetString(1),
                                points = result.IsDBNull(2) ? 0 : result.GetInt16(2),
                            }
                        );
                    }
                    db.Database.CloseConnection();
                }
            }

            object json = new { code = 200, status = "Success", data };
            return Ok(json);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            object res = new { code = 400, status = $"Error: {e.Message}" };
            return Ok(res);
        }
    }
};
