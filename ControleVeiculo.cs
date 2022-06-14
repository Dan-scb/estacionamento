using System;
namespace Estacionamento{
    class ControleVeiculo{
        public static void Menu(){
            try{
            int opcao;
                do{
                Console.Clear();
                Console.WriteLine("Escolha a operação que deseja realizar");
                Console.WriteLine("1 - Cadastrar veiculo | 2 - Buscar veiculo | 0 - Sair");
                opcao = int.Parse(Console.ReadLine());
                switch(opcao){
                    case 1: ControleVeiculo.CadastroVeiculo(); break;
                    case 2: ControleVeiculo.BuscarVeiculo(); break;
                    case 0: Console.WriteLine("Saindo menu veículos");
                            opcao = 0;     break;
                    default: Console.WriteLine("Operação inválida"); break;
                }
                }while(opcao != 0);
            }catch{
                Console.WriteLine("Entrada inválida");
                ControleVeiculo.Menu();
            }
                
        }

        public static void CadastroVeiculo(){
            Console.Clear();
            Console.WriteLine("Selecione o tipo do veiculo");
            Console.WriteLine("1 - Carro | 2 - Moto | 3 - Van | 0 - Voltar");
            Console.Write("Tipo: ");
            try{
            int opcaoVeiculo = int.Parse(Console.ReadLine());
            if(opcaoVeiculo == 0) ControleVeiculo.Menu();
            Console.WriteLine("Digite o nome do Veículo");
            string nomeVeiculo = Console.ReadLine();
            Console.WriteLine("Digite a placa do Veículo");
            string placaVeiculo = Console.ReadLine();
            Console.WriteLine("Digite o nome do proprietario do veículo");
            string nomeProprietario = Console.ReadLine();
            Console.WriteLine("Digite o numero de horas alugadas");
            int horasAlugadas = int.Parse(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Digite o numero da vaga selecionada");
          string tipoVeiculo = ControleEstacionamento.ChecarVagas(opcaoVeiculo);
            Console.Write("Vaga: ");
            int numVagaSelecionada = int.Parse(Console.ReadLine());
            Thread.Sleep(3000);
            Console.Clear();
            ControleEstacionamento.OcuparVaga(numVagaSelecionada, new Veiculo(tipoVeiculo, nomeVeiculo, placaVeiculo, nomeProprietario, horasAlugadas));
            }catch(Exception ex){
                Console.Clear();
                Console.WriteLine("Houve um erro ao registrar o veículo");
                Console.WriteLine("Tente novamente");
                if(ex.Message.Equals("Input string was not in a correct format.")){
                    Console.WriteLine("Entrada incorreta ao registrar veículo");
                }
                Thread.Sleep(2000);
                CadastroVeiculo();
            }

        }

        public static void BuscarVeiculo(){
            Console.Clear();
            Console.WriteLine("Por favor! Digite o código do cliente");
            Console.Write("Código: ");
            int codCliente = int.Parse(Console.ReadLine());
            if(ControleEstacionamento.VagasGeradas.Exists(x => x.Tipo?.Id == codCliente)){
                Console.Clear();
                var veiculoSelecionado = ControleEstacionamento.VagasGeradas.Find(x => x.Tipo?.Id == codCliente);
                Console.WriteLine("Cliente Encontrado");
                Console.WriteLine(@"---------------------------------------------------------------------
                ");
                string estadoVeiculo = veiculoSelecionado.Tipo.HoraSaida > DateTime.Now ? "OK" : "Atrasado";
                string dadosVeiculo = String.Format(@"
                Código: {0}
                Cliente: {1}
                Veículo: {2}
                Vaga: {3}
                Chegada: {4}
                Saida: {5}
                Estado: {6}
                ", veiculoSelecionado.Tipo.Id, veiculoSelecionado.Tipo.Propietario, veiculoSelecionado.Tipo.Modelo, veiculoSelecionado.Codigo, veiculoSelecionado.Tipo.HoraChegada, veiculoSelecionado.Tipo.HoraSaida, estadoVeiculo);
                /*Console.WriteLine("Código: " + );
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Chegada: " + );
                Console.WriteLine("Saida: " + );
                
                Console.WriteLine("Estado: " + estadoVeiculo);*/
                Console.WriteLine(dadosVeiculo);
                Console.WriteLine(@"---------------------------------------------------------------------
                ");
                Console.ReadKey();
                OperacoesVeiculo(veiculoSelecionado);
                Console.ReadKey();
            }else{
                Console.WriteLine("Cliente não Encontrado");
                Console.ReadKey();
            }
        }

        public static void OperacoesVeiculo(Vaga vagaComVeiculo){
            int operacao;
            do{
               
            Console.WriteLine("Selecione alguma das seguintes operações");
            Console.WriteLine("1 - Aumentar tempo na vaga | 2 -  Liberar veículo | 0 - Sair ");
            Console.Write("Operação: ");
             operacao = int.Parse(Console.ReadLine());
            switch(operacao){
                case 1: vagaComVeiculo.Tipo.AdicionarHoras(); break;
                case 2: ControleEstacionamento.LiberarVaga(vagaComVeiculo.Codigo); 
                        operacao = 0;
                        break;
                case 0: Console.WriteLine("Voltando ao menu de Veículos"); break;
                default: Console.WriteLine("Nenhuma operação válida selecionada"); break;
            }
            }while(operacao != 0);

        }
    }
}