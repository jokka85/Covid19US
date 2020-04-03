using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid19Texas.Helpers;
using Covid19Texas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Covid19Texas.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TexasController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public TexasController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<dynamic> Get()
        {
            try
            {
                var obj = new Dictionary<string, List<Dictionary<string, string>>>();

                var package = await ExcelHelper.GetPackage("Texas.xlsx", Constants.LinkList.State.Texas.Link);

                for (var i = 0; i < package.Workbook.Worksheets.Count; i++)
                {
                    var ws = package.Workbook.Worksheets[i];

                    ws.DeleteRow(1);
                    ws.DeleteRow(ws.Dimension.End.Row);

                    int rowEnd = ws.Dimension.End.Row;
                    int colEnd = ws.Dimension.End.Column;

                    var headers = new string[colEnd];

                    for (var j = 0; j < colEnd; j++)
                    {
                        headers[j] = package.Workbook.Worksheets[i].Cells[1, (j + 1)].Text;
                    }

                    var sheet = new List<Dictionary<string, string>>();

                    for (int row = 2; row <= rowEnd; row++)
                    {
                        var dict = new Dictionary<string, string>();
                        for (int col = 1; col <= colEnd; col++)
                        {
                            if (ws.Cells[row, 1].Text != "Total" && ws.Cells[row, 1].Text != "" && ws.Cells[row, 1].Text != null && ws.Cells[row, col].Text != null)
                            {
                                dict.Add(headers[col - 1], (ws.Cells[row, col].Text == null) ? "" : ws.Cells[row, col].Text);
                            }
                        }

                        if (dict.Count > 0) sheet.Add(dict);
                    }

                    obj.Add(ws.Name, sheet);

                }

                return obj;

            }
            catch (Exception e)
            {
                return e;
            }
        }
    }
}