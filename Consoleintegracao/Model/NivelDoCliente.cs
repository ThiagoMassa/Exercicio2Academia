using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consoleintegracao.Model
{
    class NivelDoCliente
    {
        public string TableName = "grp_niveldocliente";
        public IOrganizationService Service { get; set; }
        public NivelDoCliente(IOrganizationService service)
        {
            this.Service = service;
        }

        #region QueryNivelDoCliente
        public EntityCollection RetrieveMultipleNivelDoCliente(Guid nivelDoClienteId)
        {
            QueryExpression queryNivel = new QueryExpression(this.TableName);
            queryNivel.ColumnSet.AddColumns("grp_valordodesconto");
            queryNivel.Criteria.AddCondition("grp_niveldoclienteid", ConditionOperator.Equal, nivelDoClienteId);

            return this.Service.RetrieveMultiple(queryNivel);
        }
        #endregion
    }
}
