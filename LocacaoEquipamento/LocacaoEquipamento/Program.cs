using LocacaoEquipamento;

internal class Program
{


    private static void Main(string[] args)
    {

        TiposEquipamentos tipos = new TiposEquipamentos();
        Equipamentos equipamentos = new Equipamentos();
        Contratos contratos = new Contratos();


        string opcao = "";

        while (opcao != "0")
        {

            Console.WriteLine("1. Cadastrar tipo de equipamento\r\n2. Consultar tipo de equipamento\r\n" +
        "3. Cadastrar equipamento\r\n4. Registrar Contrato de Locação\r\n5. Consultar Contratos de Locação" +
        "\r\n6. Liberar de Contrato de Locação\r\n7. Consultar Contratos de Locação liberados\r\n" +
        "8. Devolver equipamentos de Contrato de Locação liberado", "9. Sair");

            opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    {
                        CadastrarTipoEquipamento();
                        break;
                    }
                case "2":
                    {
                        ConsultaTiposPorEquipamentos();
                        break;
                    }
                case "3":
                    {
                        CadastrarEquipamento();
                        break;
                    }
                case "4":
                    {
                        CadastrarContrato();
                        break;
                    }
                case "5":
                    {
                        MostraContratos();
                        break;
                    }
                case "6":
                    {

                        LiberarContrato();
                        break;
                    }
                case "7":
                    {
                        MostraContratosLiberados();
                        break;
                    }
                case "8":
                    {
                        DevolverEquipamentos();
                        break;
                    }
                case "9":
                    {

                        break;
                    }
                case "10":
                    {
                        ConsultarEquipamentoDisponivel();
                        break;
                    }


                case "0":
                    {
                        Environment.Exit(0);
                        break;
                    }

            }


        void DevolverEquipamentos()
        {

            Console.WriteLine("Devolver equipamentos: ");
            Console.Write("Digite o cod. Contrato: ");
            int id = int.Parse(Console.ReadLine());
            Contrato contrato_achado = contratos.PesquisaContrato(id);

          

            if (contrato_achado != null)
            {

                for(int i = 0; i <= contrato_achado.PilhaEquipamento.Count; i++)
                    {

                        Equipamento eq_achado = equipamentos.PesquisaEquipamento(contrato_achado.PilhaEquipamento.First().IdEquipamento);
                        eq_achado.Quantidade += contrato_achado.PilhaEquipamento.First().Quantidade;
                        contrato_achado.RemoveEquipamento();
                    }
             
                contratos.RemoveContrato(contrato_achado);
                Console.WriteLine("Contrato Removido com sucesso!");


                }
            else
            {
                Console.WriteLine("Contrato não encontrado!");
            }


        }


    }

    void CadastrarTipoEquipamento()
    {


        Console.WriteLine("CADASTRAR TIPO DE EQUIPAMENTO: \n");
        Console.WriteLine("------------------");
        Console.Write("Descrição: ");
        string descricao_tipo = Console.ReadLine();

        TipoEquipamento tipo_equipamento = new TipoEquipamento(tipos.ListaTiposEquipamentos.Count + 1, descricao_tipo);
        tipos.ListaTiposEquipamentos.Add(tipo_equipamento);
        Console.WriteLine("Tipo cadastrado com sucesso!\n");

    }

    void CadastrarEquipamento()
    {

        Console.WriteLine("CADASTRAR EQUIPAMENTO: \n");
        Console.WriteLine("------------------");
        Console.Write("Id. Patrimonio: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Descrição: ");
        string descricao_equipamento = Console.ReadLine();
        Console.Write("TIPOS POSSIVEIS: \n\n");
        MostraTiposEquipamentos();
        Console.Write("ESCOLHA O TIPO: ");
        string tipo = Console.ReadLine();
        Console.Write("Digite a quantidade: ");
        int quantidade = int.Parse(Console.ReadLine());
        Console.Write("Valor para Aluguel: ");
        float valor_aluguel = float.Parse(Console.ReadLine());

        TipoEquipamento tipo_achado = tipos.PesquisaTipos(int.Parse(tipo));

        if (PodeCadastrarEquipamento(id))
        {

            if (tipo_achado != null)
            {
                Equipamento eq1 = new Equipamento(id, descricao_equipamento, tipo_achado, false, valor_aluguel, quantidade);
                equipamentos.addEquipamento(eq1);
                Console.WriteLine("Equipamento cadastrado com sucesso!\n");
            }
            else
            {
                Console.WriteLine("Tipo não Encontrado!");
            }


        }


    }

    bool PodeCadastrarEquipamento(int cd)
    {

        foreach (Equipamento equipamento in equipamentos.ListaEquipamentos)
        {
            if (cd == equipamento.IdEquipamento)
            {
                Console.WriteLine("Equipamento já existe");
                return false;
            }
        }

        foreach (Contrato contrato in contratos.ListaDeContratos)
        {
            foreach (Equipamento equipamento in contrato.PilhaEquipamento)
            {

                if (cd == equipamento.IdEquipamento)
                {
                    Console.WriteLine("Equipamento já alugado para o contrato: " + contrato.IdContrato);
                    return false;
                }
            }
        }

        return true;

    }

    void CadastrarContrato()
    {

        float valor_total_contrato = 0;

        Console.WriteLine("CADASTRAR CONTRATO");
        Console.WriteLine("------------------");

        int cod = contratos.ListaDeContratos.Count + 1;
        Console.Write("Data registro: ");
        DateTime data_registro = DateTime.Parse(Console.ReadLine());
        Console.Write("Data retorno: ");
        DateTime data_retorno = DateTime.Parse(Console.ReadLine());

        //Colocando equipamentos no contrato
        Stack<Equipamento> pilhaEquipamentos = new Stack<Equipamento>(); // Cria pilha de equipamentos do contrato
        string opcao = "";

        while (opcao != "n" && opcao != "N")
        {
            Console.Write("Id Equipamento: ");
            int id = int.Parse(Console.ReadLine());

            Equipamento equipamento_achado = equipamentos.PesquisaEquipamento(id);
            int quantidade_estoque = equipamento_achado.Quantidade;

            if (equipamento_achado != null)
            {
                Console.Write("Quantidade a ser alugada: ");
                int quantidade_alugada = int.Parse(Console.ReadLine());


                Equipamento equipamento_pilha = new Equipamento(equipamento_achado.IdEquipamento, equipamento_achado.NomeEquipamento, equipamento_achado.Tipo, equipamento_achado.Avaria, equipamento_achado.ValorAluguel, quantidade_alugada);
                valor_total_contrato += (equipamento_pilha.ValorAluguel * equipamento_pilha.Quantidade);
                pilhaEquipamentos.Push(equipamento_pilha);
                equipamento_achado.DiminuirEstoque(quantidade_alugada);

                Console.WriteLine("Novo equipamento? [S]-SIM  [N]-NÃO: ");
                opcao = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Equipamento não encontrato");
                Console.WriteLine("Novo equipamento? [S]-SIM  [N]-NÃO: ");
                opcao = Console.ReadLine();

            }

        }

        Contrato contrato = new Contrato(cod, data_registro, data_retorno, pilhaEquipamentos, false, valor_total_contrato);
        contratos.ListaDeContratos.Add(contrato);
        Console.WriteLine("Contrato Cadastrado com Sucesso!\n");

    }

    void ConsultarEquipamentoDisponivel()
    {


        foreach (Equipamento e in equipamentos.ListaEquipamentos)
        {

            Console.WriteLine("EQUIPAMENTOS\n");
            Console.WriteLine(e);

        }


    }

    void ConsultaTiposPorEquipamentos()
    {
        Console.WriteLine("Consulta tipo e seus equipamentos vinculados ");
        Console.WriteLine();
        Console.Write("Digite o Cod. Tipo: ");
        int id = int.Parse(Console.ReadLine());

        TipoEquipamento tipo_achado = tipos.PesquisaTipos(id);

        if (tipo_achado != null)
        {


            Console.WriteLine(tipo_achado);
            Console.WriteLine("Itens desse tipo disponíveis: ");
            //Pesquisa na lista de equipamentos e mostra o tipo encontrato
            foreach (Equipamento e in equipamentos.ListaEquipamentos)
            {

                if (e.Tipo == tipo_achado)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("------------------------");
                }

            }

            Console.WriteLine("Itens desse tipo Alugados: ");
            //Pesquisa na lista de contratos > pilha Equipamentos e mostra o tipo encontrato
            foreach (Contrato contrato in contratos.ListaDeContratos)
            {
                foreach (Equipamento e in contrato.PilhaEquipamento)
                {
                    if (e.Tipo == tipo_achado)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine("------------------------");
                    }


                }


            }
        }
        else
        {
            Console.WriteLine("Tipo de equipamento não localizado");
            Console.WriteLine();
        }

        Console.WriteLine();
    }

    void LiberarContrato()
    {

        Console.WriteLine("Liberar Contrato: \n");
        MostraListinhaContratos();
        Console.Write("Digite o Id. do contrato: ");
        int cod = int.Parse(Console.ReadLine());
        Contrato contrato_achado = contratos.PesquisaContrato(cod);

        if (contrato_achado != null)
        {
            Console.WriteLine("Liberar Contrato? [S - SIM] ou [N - NÃO]");
            string liberar = Console.ReadLine();
            if (liberar == "S" || liberar == "s")
            {
                contrato_achado.ContratoLiberado = true;
            }
        }

    }

    void MostraListinhaContratos()
    {

        Console.WriteLine("Lista De Contratos não liberados: ");
        foreach (Contrato contrato in contratos.ListaDeContratos)
        {
            if (contrato.ContratoLiberado == false)
            {

                Console.WriteLine("Contrato Id.:" + contrato.IdContrato + " - Data Registro: " + contrato.DataContrato.ToString(("dd/MM/yyyy")));
            }


        }


    }


    void MostraTiposEquipamentos()
    {

        foreach (TipoEquipamento tipo in tipos.ListaTiposEquipamentos)
        {
            Console.Write("Cod.: " + tipo.IdTipo + " - Tipo: " + tipo.Descricao + "\n");

        }

    }



    void MostraContratosLiberados()
    {

        foreach (Contrato contrato in contratos.ListaDeContratos)
        {
            if (contrato.ContratoLiberado)
            {

                Console.WriteLine("---------------------------");
                Console.WriteLine("     Contrato  " + contrato.IdContrato);
                Console.WriteLine("---------------------------");
                Console.WriteLine(contrato);
                Console.WriteLine("Liberado: {0}", contrato.ContratoLiberado ? "SIM" : "NAO");

                foreach (Equipamento equipamento in contrato.PilhaEquipamento)
                {
                    Console.WriteLine(equipamento);
                    Console.WriteLine("Avaria: {0}", equipamento.Avaria ? "SIM" : "NAO");
                }

            }


        }

    }


    void MostraContratos()
    {

        foreach (Contrato contrato in contratos.ListaDeContratos)
        {

            Console.WriteLine("---------------------------");
            Console.WriteLine("     Contrato  " + contrato.IdContrato);
            Console.WriteLine("---------------------------");
            Console.WriteLine(contrato);
            Console.WriteLine("Liberado: {0}", contrato.ContratoLiberado ? "SIM" : "NAO");

            Console.WriteLine("PILHA: " + contrato.PilhaEquipamento.Count);

            foreach (Equipamento equipamento in contrato.PilhaEquipamento)
            {
                Console.WriteLine(equipamento);
                Console.WriteLine("Avaria: {0}", equipamento.Avaria ? "SIM" : "NAO");
            }

        }

    }


}



}