using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace Plugins
{
    public class AccountCustom : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            if (context.MessageName == "Create")
            {
                Entity conviteDoEvento = (Entity)context.InputParameters["Target"];

                QueryExpression query = new QueryExpression("grp_convitedeeventos");
                query.AddOrder("createdon", OrderType.Ascending);
                query.ColumnSet.AddColumns("grp_cliente", "grp_evento");
                EntityCollection retrieveDeEventos = service.RetrieveMultiple(query);

                foreach (var RetrieveDeEventos in retrieveDeEventos.Entities)
                {
                    Entity account = new Entity("account");

                    if (conviteDoEvento.Contains("grp_cliente"))
                    {
                        account.Id = ((EntityReference)conviteDoEvento["grp_cliente"]).Id;
                    }
                    account["grp_ultimoevento"] = RetrieveDeEventos["grp_evento"];
                    service.Update(account);
                }
            }
            else
            {
                if (context.MessageName == "Delete")
                {
                    Entity preDelete = context.PreEntityImages["preDelete"];

                    Entity account = new Entity("account");
                    account.Id = ((EntityReference)preDelete["grp_cliente"]).Id;
                    account["grp_ultimoevento"] = null;

                    service.Update(account);
                }
            }
        }
    }
}