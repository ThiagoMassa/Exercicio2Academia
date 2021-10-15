using Consoleintegracao.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consoleintegracao
{
    class Program
    {
        static void Main(string[] args)
        {
            IOrganizationService service = ConnectionFactory.GetCrmService();
            Opportunity opportunity = new Opportunity(service);
            Console.WriteLine("Qual oportunidade você deseja aplicar o desconto ? \n");           
            //id  = D67FB1B8-312C-EC11-B6E6-00224837A3F1
            Guid opportunityId = new Guid(Console.ReadLine());

            EntityCollection oportunitysCRM = opportunity.RetrieveMultipleAccountByOportunity(opportunityId);
            foreach (Entity oportunityCRM in oportunitysCRM.Entities)
            {
                Console.WriteLine($"\nO nome da oportunidade é: {oportunityCRM["name"]}");

                EntityReference accountId = (EntityReference)oportunityCRM["parentaccountid"];

                Account account = new Account(service);
                EntityCollection accountsCRM = account.RetrieveMultipleNivelByAccount(accountId.Id);
                foreach (Entity accountCRM in accountsCRM.Entities)
                {
                    EntityReference nivelDoClienteId = (EntityReference)accountCRM["grp_niveldocliente"];

                    NivelDoCliente nivelDoCliente = new NivelDoCliente(service);
                    EntityCollection nivelDoClientesCRM = nivelDoCliente.RetrieveMultipleNivelDoCliente(nivelDoClienteId.Id);
                    foreach (Entity niveldoclienteCRM in nivelDoClientesCRM.Entities)
                    {
                        int desconto = (int)niveldoclienteCRM["grp_valordodesconto"];
                        

                        Util percentage = new Util();
                        double valorComPorcentagem = percentage.Porcentagem(desconto, (Money)oportunityCRM["totallineitemamount"]);

                        percentage.ValorTotal(desconto, (Money)oportunityCRM["totallineitemamount"]);

                        Console.WriteLine("\nVocê deseja atualizar essa oportunidade ? Y/N\n");
                        string resultado = Console.ReadLine().ToLower();
                        
                        if (resultado == "y")
                        {
                            opportunity.UpdateOpportunity(opportunityId, valorComPorcentagem);
                            Console.WriteLine("\nOportunidade atualizada com sucesso!!");
                        }
                        else if (resultado == "n")
                        {
                            Console.WriteLine("\nObrigado pela atenção!");
                        }
                        else
                        {
                            Console.WriteLine("\nNão foi possivel entender sua resposta. Porfavor tente novamente!");
                        }
                    }
                }
            }
            Console.ReadKey();
        }
    }
}

