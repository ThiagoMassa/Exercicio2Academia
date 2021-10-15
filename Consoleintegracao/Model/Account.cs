using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consoleintegracao.Model
{
    class Account
    {
        public IOrganizationService Service { get; set; }


        public string TableName = "account";

        public Account(IOrganizationService service)
        {
            this.Service = service;
        }
        #region QueryConta
        public EntityCollection RetrieveMultipleNivelByAccount(Guid accountId)
        {
            QueryExpression queryAccount = new QueryExpression(this.TableName);
            queryAccount.ColumnSet.AddColumns("grp_niveldocliente");
            queryAccount.Criteria.AddCondition("accountid", ConditionOperator.Equal, accountId);

            return this.Service.RetrieveMultiple(queryAccount);
        }
        #endregion
    }
}
