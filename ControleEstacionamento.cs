using System;
using System.Threading;
namespace Estacionamento{
    class ControleEstacionamento{

        
        private static List<Vaga> vagasGeradas;
        public static List<Vaga> VagasGeradas{get{return vagasGeradas;}}


        private static void GerarInstancia(){
            try{
                if(VagasGeradas.Count != 0){
                Console.WriteLine("Numero de vagas: "+ VagasGeradas.Count);
                }
            }catch{
                vagasGeradas = new List<Vaga>();
            }
            
        }

       public static void Menu(){
           
            GerarInstancia();
            try{ 
           int opcao;
            do{
            Console.Clear();    
            Console.WriteLine("Selecione alguma das seguintes opções para proseguir");
           Console.WriteLine("1- Adicionar vagas | 2 - Checar Vagas | 4 - Liberar vaga | 0 - Sair ");
           Console.Write("Opção: ");
            opcao = int.Parse(Console.ReadLine());
           switch(opcao){
               case 1: ControleEstacionamento.ConfigurarEstacionamento(); break;
               case 2: Console.WriteLine("Selecione o tipo da vaga: 1 - Carro | 2 - Moto | 3 - Van");
                        int tipoVaga = int.Parse(Console.ReadLine());
                        ControleEstacionamento.ChecarVagas(tipoVaga); break;
               case 4: Console.WriteLine("Digite o numero da vaga");
                   int numVagaLiberada = int.Parse(Console.ReadLine());
                   ControleEstacionamento.LiberarVaga(numVagaLiberada); break;         
               case 0: Console.WriteLine("Saindo do sistema"); break;    
               default: Console.WriteLine("Nenhuma opção válida foi selecionada");break;
           }
            }while(opcao != 0);

            }catch{
                Console.Clear();
                Console.WriteLine("Erro ao realizar operação");
                Console.WriteLine("Entrada errada ou inválida");
                Thread.Sleep(2000);
                ControleEstacionamento.Menu();
            }  
           
        }
        
        public static string ChecarVagas(int opcao){
            Console.Clear();
            string tipo = "Neutro";
            uint contagemVagas = 0;
            switch(opcao){
                case 1: tipo = "Carro"; break;
                case 2: tipo = "Moto"; break;
                case 3: tipo = "Van"; break;
                default: Console.WriteLine("Tipo não encontrado");
                ControleVeiculo.CadastroVeiculo(); break;
            }

            for(int i =0; i < VagasGeradas.Count; i++){
                if(VagasGeradas[i].AcessoVeiculo == tipo){
                    string estadoVaga = VagasGeradas[i].Status ? $"Vaga {VagasGeradas[i].Codigo}: Indisponível" : $"Vaga {VagasGeradas[i].Codigo}: Disponível";
                    if(estadoVaga.EndsWith("Disponível")){
                    contagemVagas++;
                    }
                    Console.WriteLine(estadoVaga);
                }
            }
            Console.WriteLine("-------------------------------");
            Console.WriteLine(contagemVagas + " Vagas disponíveis");
            Thread.Sleep(5000);
            return tipo;
        }

        public static void OcuparVaga(int numVaga, Veiculo automovel){
           numVaga--;
            if(VagasGeradas[numVaga].Status == false && automovel.Tipo == VagasGeradas[numVaga].AcessoVeiculo){
                VagasGeradas[numVaga].Tipo = automovel;
                VagasGeradas[numVaga].Status = true;
                Console.WriteLine("Aberta");
                GerarTiket(numVaga, automovel);
            }else{
                Console.WriteLine("Vaga ocupada");
                Console.WriteLine("Cliente: "+VagasGeradas[numVaga].Tipo.Propietario);
            }
        }

       private static void GerarTiket(int numVaga, Veiculo automovel){
            string ticket = String.Format(@"
            ------------------------------------
            Vaga: {0}
            Horario Chegada: {1}
            Horario Saida: {2}
            ------------------------------------
            Código cliente: {3}
            Nome: {4}
            ------------------------------------
            ", VagasGeradas[numVaga].Codigo, automovel.HoraChegada, automovel.HoraSaida, automovel.Id, automovel.Propietario);

            Console.WriteLine(ticket);
            Console.ReadKey();
        }

        public static void LiberarVaga(int numVaga){
            Console.Clear();
            numVaga--;
            if(VagasGeradas.Exists(x => x.Codigo == numVaga)){
             Console.WriteLine("Vaga encontrada");
             Vaga dataVaga = VagasGeradas[numVaga];
            if(dataVaga.Status){
                string textoLiberarVagA = String.Format(@"
                Cliente: {0}
                Hora Chegada: {1}
                Hora limite Saida: {2}
                ", dataVaga.Tipo.Propietario, dataVaga.Tipo.HoraChegada, dataVaga.Tipo.HoraSaida);
                Console.WriteLine(textoLiberarVagA);
                Console.WriteLine("Liberar vaga?");
                Console.WriteLine("(SIM) - S | (NÃO) - N");
                Console.Write("Opção: ");
                string opcao = Console.ReadLine();
                if(opcao.ToLower() == "s"){
                    dataVaga.Status = false;
                    dataVaga.Tipo = null;
                }else{
                    Console.WriteLine("Operação Cancelada");
                    Console.ReadKey();
                }

            }else{
                Console.WriteLine("Vaga já se encontra desocupada");
                Console.ReadKey();
            }
              
            }else{
                Console.WriteLine("Vaga não encontrada no sistema");
                Console.ReadKey();
            }
        }

        private static void ConfigurarEstacionamento(){
            Console.Clear();
            Console.WriteLine("Digite a quantidade de vagas para cada um dos tipos");
            Console.Write("Carros: ");
            int numVagas = int.Parse(Console.ReadLine());
            GerarVagas(numVagas, "Carro");
            Console.Clear();
            Console.Write("Motos: ");
            numVagas = int.Parse(Console.ReadLine());
            GerarVagas(numVagas, "Moto");
            Console.Clear();
            Console.Write("Vans: ");
            numVagas = int.Parse(Console.ReadLine());
            GerarVagas(numVagas, "Van");
            Console.WriteLine(numVagas);
        }

        private static void GerarVagas(int numVagas, string tipoVaga){
            
            for (int i = 0; i < numVagas; i++){
                Vaga vga = new Vaga(false, tipoVaga);
                vagasGeradas.Add(vga);  
            }
            Console.WriteLine("Vagas: " + VagasGeradas.Count);
        }
    }
}