using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocacaoEquipamento
{
    internal class Contrato
    {

        public int IdContrato;
        public DateTime DataContrato;
        public DateTime DataRetorno;
        public bool ContratoLiberado;
        public double ValorToTalContrato;

        public Stack<Equipamento> PilhaEquipamento = new Stack<Equipamento>();

        public Contrato()
        {

        }

        public Contrato(int idContrato, DateTime dataContrato, DateTime dataRetorno, Stack<Equipamento> equipamentos, bool contratoLiberado,float valor_total)
        {
            IdContrato = idContrato;
            DataContrato = dataContrato;
            DataRetorno = dataRetorno;
            ContratoLiberado = contratoLiberado;
            ValorToTalContrato = valor_total;
            PilhaEquipamento = equipamentos;
        }

        public void RemoveEquipamento()
        {

            PilhaEquipamento.Pop();
        }

        public override string ToString()
        {
            return 
                    "\nData Contrato: " + DataContrato.Date.ToString("dd/MM/yyyy") +
                    "\nData Retorno: " + DataRetorno.Date.ToString("dd/MM/yyyy")+
                    "\nValor Total Aluguel: " + ValorToTalContrato.ToString("C2");
        }



    }
}
