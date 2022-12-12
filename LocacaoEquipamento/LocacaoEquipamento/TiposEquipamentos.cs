using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocacaoEquipamento
{
    internal class TiposEquipamentos
    {

        public List<TipoEquipamento> ListaTiposEquipamentos = new List<TipoEquipamento>();


        public bool addTipoEquipamento(TipoEquipamento tipo)
        {
            ListaTiposEquipamentos.Add(tipo);
            return true;
        }

         public TipoEquipamento PesquisaTipos(int id)
        {

            foreach (TipoEquipamento tipo in ListaTiposEquipamentos)
            {

                if (tipo.IdTipo == id)
                {
                    return tipo;
                }

            }

            return null;

        }




    }
}
