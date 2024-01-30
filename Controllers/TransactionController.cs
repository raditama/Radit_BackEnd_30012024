using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Radit_BackEnd_30012024.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace Radit_BackEnd_30012024.Controllers;

[Route("api/transaction")]
[ApiController]
public class TransactionController : Controller
{
    [HttpPost]
    [Route("create")]
    public IActionResult Create()
    {
        try
        {
            string accountId = HttpContext.Request.Query["accountId"].ToString();
            string transactionDate = HttpContext.Request.Query["transactionDate"].ToString();
            string description = HttpContext.Request.Query["description"].ToString();
            string debitCreditStatus = HttpContext.Request.Query["debitCreditStatus"].ToString();
            string amountS = HttpContext.Request.Query["amount"].ToString();
            int amount = int.Parse(amountS);

            if (!(debitCreditStatus.ToLower() == "d" || debitCreditStatus.ToLower() == "c"))
            {
                object res = new { code = 400, status = "Error: debitCreditStatus harus berisi D atau C!" };
                return Ok(res);
            }

            using (var db = new DbMainContext())
            {
                Transaction data = new Transaction();

                data.accountId = int.Parse(accountId);
                data.transactionDate = DateTime.Parse(transactionDate);
                data.description = description;
                data.debitCreditStatus = debitCreditStatus;
                data.amount = amount;

                db.Transaction.Add(data);

                int point = 0;

                if (description.ToLower() == "beli pulsa")
                {
                    if (amount > 10000)
                    {
                        // POINT TAMBAH 1 KELIPATAN 1000
                        int tempValue = amount >= 30000 ? 30000 - 10000 : amount - 10000;
                        decimal result = (decimal)tempValue / 1000;
                        point += (int)result;
                    }
                    if (amount > 30000)
                    {
                        // POINT TAMBAH 2 KELIPATAN 1000
                        int tempValue = amount - 30000;
                        decimal result = (decimal)tempValue / 1000 * 2;
                        point += (int)result;
                    }
                }
                else if (description.ToLower() == "bayar listrik")
                {
                    if (amount > 50000)
                    {
                        // POINT TAMBAH 1 KELIPATAN 2000
                        int tempValue = amount >= 100000 ? 100000 - 50000 : amount - 50000;
                        decimal result = (decimal)tempValue / 2000;
                        point += (int)result;
                    }
                    if (amount > 100000)
                    {
                        // POINT TAMBAH 2 KELIPATAN 2000
                        int tempValue = amount - 100000;
                        decimal result = (decimal)tempValue / 2000 * 2;
                        point += (int)result;
                    }
                }

                var data2 = db.Customer.Where(x => x.accountId == int.Parse(accountId)).First();
                data2.points += point;
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
            string accountId = HttpContext.Request.Query["accountId"].ToString();
            string startDate = HttpContext.Request.Query["startDate"].ToString();
            string endDate = HttpContext.Request.Query["endDate"].ToString();

            var data = new List<object>();

            using (var db = new DbMainContext())
            {
                using (var cmd = db.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = $"SELECT transactionDate, description, debitCreditStatus, amount FROM transaction WHERE accountId = {accountId} AND transactionDate >= '{startDate}' AND transactionDate <= '{endDate}';";

                    cmd.CommandType = CommandType.Text;
                    db.Database.OpenConnection();

                    var result = cmd.ExecuteReader();

                    while (result.Read())
                    {
                        string debitCreditStatus = result.IsDBNull(2) ? "" : result.GetString(2).ToLower();
                        int amount = result.IsDBNull(3) ? 0 : result.GetInt32(3);

                        data.Add(
                            new
                            {
                                transactionDate = result.IsDBNull(0) ? "" : result.GetDateTime(0).ToString("yyyy-MM-dd"),
                                description = result.IsDBNull(1) ? "" : result.GetString(1),
                                credit = debitCreditStatus == "c" ? amount.ToString() : "-",
                                debit = debitCreditStatus == "d" ? amount.ToString() : "-",
                                amount,
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
