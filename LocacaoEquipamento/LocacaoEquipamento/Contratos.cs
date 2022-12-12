using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LocacaoEquipamento
{
    internal class Contratos
    {
        public List<Contrato> ListaDeContratos = new List<Contrato>();

        public bool addContrato(Contrato contrato)
        {

            ListaDeContratos.Add(contrato);
            return true; 

        }

        public Contrato PesquisaContrato(int cod)
        {

            foreach (Contrato contrato  in ListaDeContratos)
            {

                if (cod == contrato.IdContrato)
                {
                    return contrato;
                }

            }
            return null;
        }


        public bool RemoveContrato(Contrato contrato)
        {
            foreach (Contrato c in ListaDeContratos)
            {
                if (contrato.IdContrato == c.IdContrato)
                {
                    ListaDeContratos.Remove(contrato);
                    return true;
                }

            }
            return false;
          
        }




    }
}
