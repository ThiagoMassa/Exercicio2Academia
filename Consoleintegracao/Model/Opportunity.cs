using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consoleintegracao.Model
{
    class Opportunity
    {
        public IOrganizationService Service { get; set; }

        public string TableName = "opportunity";


        public Opportunity(IOrganizationService service)
        {
            this.Service = service;
        }
        #region QueryOpportunity
        public EntityCollection RetrieveMultipleAccountByOportunity(Guid oportunityId)
        {
            QueryExpression queryOpt = new QueryExpression(this.TableName);
            queryOpt.ColumnSet.AddColumns("name", "parentaccountid", "totallineitemamount");
            queryOpt.Criteria.AddCondition("opportunityid", ConditionOperator.Equal, oportunityId);

            return this.Service.RetrieveMultiple(queryOpt);
        }
        #endregion

        #region UpdateOpportunity
        public void UpdateOpportunity(Guid opportunityId, double desconto)
        {
            Entity opportunity = new Entity(this.TableName);
            opportunity.Id = opportunityId;
            opportunity["discountamount"] = desconto;
            this.Service.Update(opportunity);
        }
        #endregion
    }
}
