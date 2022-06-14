using System;
namespace Estacionamento{
    class Veiculo{
        public static int ClienteId;
        public Veiculo(){
            try{
                ClienteId++;
            }catch{
                ClienteId = 1;
            }
            Id = ClienteId;
            Tipo= "";
            Modelo = "";
            Placa = "";
            Propietario = "";
            HoraChegada = DateTime.Now;
        }

        public Veiculo(string tipo, string modelo, string placa, string proprietario, int horasReservadas){
            try{
                ClienteId++;
            }catch{
                ClienteId = 1;
            }
            Id = ClienteId;
            Tipo = tipo;
            Modelo = modelo;
            Placa = placa;
            Propietario = proprietario;
            HoraChegada = DateTime.Now;
            HorasReservadas = horasReservadas;
            this.SetHoraSaida();
        }

        public int Id{get; set;}
        public string Tipo{get; set;}
        public string Modelo{get; set;}
        public string Placa{get; set;}
        public string Propietario{get; set;}
        public DateTime HoraChegada{get;}
        public int HorasReservadas{get; set;}
        public DateTime HoraSaida{get; set;}


        public  void SetHoraSaida(){
            HoraSaida = HoraChegada.Add(new TimeSpan(HorasReservadas, 0, 0));
        }

        public  void AdicionarHoras(){
            Console.Clear();
            Console.WriteLine("Digite o numero de horas adicionais");
            Console.Write("Horas: ");
            int horaExtra = int.Parse(Console.ReadLine());
            HorasReservadas += horaExtra;
            this.SetHoraSaida();
            Console.WriteLine("Novo horario de saida: " + HoraSaida);
        }

    }
}