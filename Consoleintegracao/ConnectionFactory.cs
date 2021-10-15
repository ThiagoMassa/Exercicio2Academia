using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consoleintegracao
{
    class ConnectionFactory
    {
        public static IOrganizationService GetCrmService()
        {
            string connectionString =
                "AuthType=OAuth;" +
                "Username=grupo4@grupo4dynamics.onmicrosoft.com;" +
                "Password=123@mudar$;" +
                "Url=https://org65d6b401.crm2.dynamics.com/;" +
                "AppId=6cc1a7f0-7022-4152-a4d6-a2bae8470a62;" +
                "RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;";

            CrmServiceClient crmServiceClient = new CrmServiceClient(connectionString);
            return crmServiceClient.OrganizationWebProxyClient;

        }
    }
}
