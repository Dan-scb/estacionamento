// See https://aka.ms/new-console-template for more information
using System;
using System.Threading;
namespace Estacionamento{
    class Program{
        static void Main(){
            
            try{ 
            int opcao;
            do{
            Console.Clear();
            Console.WriteLine("Estacionamento");
            Console.WriteLine("Digite um dos digitos para acessar um dos menus");
            Console.WriteLine("1 - Estacionamento | 2 - Veículos | 0 - Sair");
            Console.Write("Opção: ");
            opcao = int.Parse(Console.ReadLine());
            switch(opcao){
                case 1: ControleEstacionamento.Menu(); break;
                case 2: ControleVeiculo.Menu(); break;
                case 0: Console.WriteLine("Fechando o sistema"); break;
                default: Console.WriteLine("Operação inválida"); break;
            }
            
            
           }while(opcao != 0);
           }catch{
                Console.WriteLine("Por favor! Digite uma opção válida");
                Thread.Sleep(2000);
                Main();
            }

            
        }
    }
}
