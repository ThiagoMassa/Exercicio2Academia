using System;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Query;

namespace Plugins
{
    public class EventoCustom : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            Entity evento = (Entity)context.InputParameters["Target"];

            
            QueryExpression query = new QueryExpression("grp_evento");
            query.AddOrder("createdon", OrderType.Descending);
            query.TopCount = 1;
            query.ColumnSet.AddColumns("grp_numerodoevento");
            EntityCollection retrieveDeEventos = service.RetrieveMultiple(query);

            evento["grp_numerodoevento"] =  1;

            foreach (var retrieveEvento in retrieveDeEventos.Entities)
            {
                int numeroDoEvento = retrieveEvento.Contains("grp_numerodoevento") ? (int)retrieveEvento["grp_numerodoevento"] : 0;

                evento["grp_numerodoevento"] = numeroDoEvento + 1;
            }
        }
    }
}