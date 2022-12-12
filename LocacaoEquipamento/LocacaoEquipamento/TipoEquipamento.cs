using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocacaoEquipamento
{
    internal class TipoEquipamento
    {
        public int IdTipo;
        public string Descricao;

        public TipoEquipamento(int idTipo, string descricao)
        {
            IdTipo = idTipo;
            Descricao = descricao;
        }

        public override string ToString()
        {
            return "\nTipo: " + Descricao;
        }

       
    }
}
