using System;
namespace Estacionamento{
    class Vaga{
        public static int NextId;

        public Vaga(){
           try{
                NextId++;
            }catch{
                NextId = 1;
            } 
            Codigo = NextId;
            Status = false;
        }

        public Vaga(bool ocupada, string acessoVeiculo){
            try{
                NextId++;
            }catch{
                NextId = 1;
            } 
             Codigo = NextId;
             Status = ocupada;
             AcessoVeiculo = acessoVeiculo;
        }

        public int Codigo{get;}
        public bool Status{get; set;}
        public string AcessoVeiculo{get;}
        public Veiculo? Tipo{get; set;}

        public void ExibirVaga(){
            Console.WriteLine(Codigo);
            Console.WriteLine(Status);
            Console.WriteLine(AcessoVeiculo);
            Console.WriteLine(Tipo.Propietario);
        }


    }
}