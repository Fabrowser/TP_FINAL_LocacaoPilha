using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocacaoEquipamento
{
    internal class Equipamento
    {
        public int IdEquipamento;
        public string NomeEquipamento;
        public TipoEquipamento Tipo;
        public bool Avaria;
        public float ValorAluguel;
        public int Quantidade;

        public Equipamento(int idEquipamento, string nomeEquipamento, TipoEquipamento tipo, bool avaria, float valorAluguel, int quantidade)
        {
            IdEquipamento = idEquipamento;
            NomeEquipamento = nomeEquipamento;
            Tipo = tipo;
            Avaria = avaria;
            ValorAluguel = valorAluguel;
            Quantidade = quantidade;
        }

        public void DiminuirEstoque(int qtd_aluguel)
        {

            this.Quantidade -= qtd_aluguel;

        }

        public override string ToString()
        {
            return "\nNome: " + this.NomeEquipamento +
                    "\nTipo: " + this.Tipo.Descricao +
                    "\nValor Aluguel: " + this.ValorAluguel +
                    "\nQuantidade: " + this.Quantidade;
        }

    }
}
