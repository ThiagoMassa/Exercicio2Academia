using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consoleintegracao.Model
{
    class Util
    {
        public void ValorTotal(int desconto, Money valor)
        {
            double Valor = (double)valor.Value;
            double ValorDoDesconto = (desconto * Valor) / 100;
            double ValorTotal = Valor - ValorDoDesconto;

            string rank = "undefined";
            if (desconto == 10)
            {
                rank = "Diamond";
            }
            else if (desconto == 7)
            {
                rank = "Platinum";
            }
            else if (desconto == 5)
            {
                rank = "Gold";
            }
            else if (desconto == 3)
            {
                rank = "Silve";
            }
            else
            {
                Console.WriteLine("Você não possui desconto");
            }
            Console.WriteLine($"\nSeu nivel é {rank}, você tem {desconto}% de desconto, o valor total com o desconto é R${ValorTotal}.");
        }
        public double Porcentagem(int desconto, Money valor)
        {
            double Valor = (double)valor.Value;
            return (desconto * Valor) / 100;
        }
    }
}
