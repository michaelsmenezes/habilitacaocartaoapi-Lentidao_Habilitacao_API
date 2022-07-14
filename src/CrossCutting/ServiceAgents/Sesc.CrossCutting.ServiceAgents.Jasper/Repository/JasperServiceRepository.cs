using Microsoft.Extensions.Options;
using Sesc.CrossCutting.ServiceAgents.Jasper.Api;
using Sesc.CrossCutting.ServiceAgents.Jasper.Config;
using Sesc.CrossCutting.ServiceAgents.Jasper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesc.CrossCutting.ServiceAgents.Jasper.Repository
{
    public class JasperServiceRepository : IJasperServiceRepository
    {
        private readonly JasperConfig jc;
        private readonly Dictionary<string, string> parameters;

        public JasperServiceRepository(
            IOptions<JasperConfig> jasperConfig
            )
        {
            this.jc = jasperConfig.Value;

            this.parameters = new Dictionary<string, string>();
            this.parameters.Add("userLocale", this.jc.UserLocale);
            this.parameters.Add("userTimezone", this.jc.UserTimezone);
        }

        public Report run(string path)
        {
            JasperserverRestClient jasperserver = new JasperserverRestClient(this.jc.UserName, this.jc.Password, this.jc.ApiUrl);
            return new Report(jasperserver.Get(path, this.parameters));
        }

        public GenerateReportCommand ConfigureGenerateReport(GenerateReportCommand command)
        {
            var inputs = this.GetInputControlByReportUri(command.Path).Result;

            inputs.InputControl.ToList().ForEach(i =>
            {
                if (InputControlIds.ignoreInputControls.Any(ign => ign == i.Id))
                {
                    command.Parameters.Remove(i.Id);
                    command.Parameters.Add(i.Id, GetValueInputControl(i.Id));
                }
            });

            return command;
        }

        public string GetValueInputControl(string inputControlId)
        {
            switch (inputControlId)
            {
                default:
                    return null;
            }
        }

        public Report GetReport(GenerateReportCommand command)
        {
            command = ConfigureGenerateReport(command);

            command.Parameters.ToList().ForEach(keyPair => this.parameters.Add(keyPair.Key, keyPair.Value));

            var cmbCombo = command.Parameters.Where(p => p.Key == "cmbFormato");

            var extension = "pdf";

            if (cmbCombo.Count() > 0 && !string.IsNullOrEmpty(cmbCombo.First().Value))
            {
                extension = cmbCombo.First().Value.ToLower();
            }

            return this.run("/rest_v2/reports" + command.Path + "." + extension);
        }

        public Task<ResourceLookupJasper> GetAllReports(string reportFolder)
        {
            JasperserverRestClient jasperserver = new JasperserverRestClient(this.jc.UserName, this.jc.Password, this.jc.ApiUrl);

            return jasperserver.Get<ResourceLookupJasper>("/rest_v2/resources?type=reportUnit&folderUri=" + reportFolder);
        }

        public Task<InputControlResourceJasper> GetInputControlByReportUri(string reportUri)
        {
            JasperserverRestClient jasperserver = new JasperserverRestClient(this.jc.UserName, this.jc.Password, this.jc.ApiUrl);

            return jasperserver.Get<InputControlResourceJasper>("/rest_v2/reports" + reportUri + "/inputControls");
        }
    }
}
