using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocacaoEquipamento
{
    internal class Equipamentos
    {

        public List<Equipamento> ListaEquipamentos = new List<Equipamento>();



        public bool addEquipamento(Equipamento equipamento)
        {

            ListaEquipamentos.Add(equipamento);
            return true; 


        }

        public bool removeEquipamento(Equipamento equipamento)
        {

            ListaEquipamentos.Remove(equipamento);
            return true;

        }

        public Equipamento PesquisaEquipamento(int id)
        {

            foreach (Equipamento eq in ListaEquipamentos)
            {

                if (eq.IdEquipamento == id)
                {
                    return eq;
                }

            }

            return null;

        }


        public void AtualizaEstoque(int id, int quantidade)
        {


            foreach (Equipamento e in ListaEquipamentos)
            {
                if (e.IdEquipamento == id)
                {
                    e.Quantidade = quantidade;
                }

            }


        }

    }
}
