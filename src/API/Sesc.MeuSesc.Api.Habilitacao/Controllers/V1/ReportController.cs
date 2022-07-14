using Microsoft.AspNetCore.Mvc;
using Sesc.CrossCutting.ServiceAgents.Jasper.Model;
using Sesc.CrossCutting.ServiceAgents.Jasper.Repository;
using Sesc.MeuSesc.Api.Habilitacao.Base;
using Sesc.MeuSesc.Api.Habilitacao.Base.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sesc.MeuSesc.Api.Habilitacao.Controllers.V1
{
    [Route("api/v1/report")]
    public class ReportController : BaseController
    {
        IJasperServiceRepository _jasperServiceRepository;

        public ReportController(
            IJasperServiceRepository jasperServiceRepository
            )
        {
            _jasperServiceRepository = jasperServiceRepository;
        }

        [HttpGet]
        public IActionResult GetAllReportsByFolderName([FromQuery] string reportFolder)
        {
            return Ok(_jasperServiceRepository.GetAllReports(reportFolder).Result);
        }

        [HttpGet("input-control")]
        public IActionResult GetInputControlsByReportUri([FromQuery] string reportUri)
        {
            var inputs = _jasperServiceRepository.GetInputControlByReportUri(reportUri).Result;

            if (inputs.InputControl != null && inputs.InputControl.Count() > 0)
            {
                inputs.InputControl = inputs.InputControl.Where(i => !InputControlIds.ignoreInputControls.Any(ing => ing == i.Id)).ToList();
            }

            return Ok(inputs);
        }

        [HttpPost("get-report")]
        public IActionResult GetReport([FromBody] GenerateReportCommand command)
        {
            var report = _jasperServiceRepository.GetReport(command);
            byte[] stream = report.ArquivoMemoryStream.ToArray();
            string base64 = Convert.ToBase64String(stream);

            return Ok(base64);
        }

    }
}
