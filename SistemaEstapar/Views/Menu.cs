using SistemaEstapar.Business.Service;
using SistemaEstapar.DataAccess.Data;
using SistemaEstapar.DataAccess.Respositorios;
using SistemaEstapar.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaEstapar.ConsoleApp.Views
{
    public class Menu
    {        
        /// <summary>
        /// Função para exibir o logo do sistema
        /// </summary>
        public static void Logo()
        {

          Console.WriteLine(@"
░██████╗██╗░██████╗████████╗███████╗███╗░░░███╗░█████╗░███████╗░██████╗████████╗░█████╗░██████╗░░█████╗░██████╗░
██╔════╝██║██╔════╝╚══██╔══╝██╔════╝████╗░████║██╔══██╗██╔════╝██╔════╝╚══██╔══╝██╔══██╗██╔══██╗██╔══██╗██╔══██╗
╚█████╗░██║╚█████╗░░░░██║░░░█████╗░░██╔████╔██║███████║█████╗░░╚█████╗░░░░██║░░░███████║██████╔╝███████║██████╔╝
░╚═══██╗██║░╚═══██╗░░░██║░░░██╔══╝░░██║╚██╔╝██║██╔══██║██╔══╝░░░╚═══██╗░░░██║░░░██╔══██║██╔═══╝░██╔══██║██╔══██╗
██████╔╝██║██████╔╝░░░██║░░░███████╗██║░╚═╝░██║██║░░██║███████╗██████╔╝░░░██║░░░██║░░██║██║░░░░░██║░░██║██║░░██║
╚═════╝░╚═╝╚═════╝░░░░╚═╝░░░╚══════╝╚═╝░░░░░╚═╝╚═╝░░╚═╝╚══════╝╚═════╝░░░░╚═╝░░░╚═╝░░╚═╝╚═╝░░░░░╚═╝░░╚═╝╚═╝░░╚═╝");
            Console.WriteLine();
        }

        public static void Opcoes()
        {
            // Configura a codificação do console para UTF-8
            Console.OutputEncoding = Encoding.UTF8;

            //Variáveis
            DateTime horaSaida = DateTime.Now;            
            string asterisco = string.Empty.PadLeft(57, '*');

            // Solicita ao usuário os preços do estacionamento
            Console.Write("Digite o preço inicial do estacionamento: ");
            var precoInicial = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Digite o preço por hora adicional: ");
            var precoPorHora = Convert.ToDecimal(Console.ReadLine());

            var db = new EstacionamentoDb();            
            var repo = new EstacionamentoRepository(db);
            var regras = new Estacionamento(precoInicial, precoPorHora);
            var svc = new EstacionamentoService(repo, regras);
            
            Console.WriteLine("Bem-vindo ao Sistema de Estacionamento");
            Console.WriteLine(asterisco);
            Logo();
            Console.WriteLine(asterisco);
            Console.WriteLine("╔════════════════════════════════╗");
            Console.WriteLine("║           MAIN MENU            ║");
            Console.WriteLine("╚════════════════════════════════╝");
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1. Cadastrar veículo");
            Console.WriteLine("2. Registrar entrada");
            Console.WriteLine("3. Registrar saída");
            Console.WriteLine("4. Consultar veículos estacionados");
            Console.WriteLine("5. Sair");

            while (true)
            {
                try
                {
                    Veiculo veiculosEstacionados = new Veiculo();
                    Entradas entradas = new Entradas();
                    Console.Write("Opção: ");
                    string opcao = Console.ReadLine();
                    switch (opcao)
                    {
                        case "1":
                            Console.Write("Placa: ");
                            svc.CadastrarVeiculo(Console.ReadLine());
                            Console.WriteLine("Veículo cadastrado com sucesso.");
                            break;
                        case "2":
                            Console.Write("Placa: ");
                            svc.RegistrarEntrada(Console.ReadLine(), DateTime.Now);
                            Console.WriteLine("Entrada registrada com sucesso.");
                            break;
                        case "3":
                            Console.Write("Placa: ");
                            string placa = Console.ReadLine();
                            var valorDevido = db.Entradas.Find(e => e.Placa == placa);
                            Console.Write($"Valor Devido {valorDevido} | Valor Pago: ");
                            var valorPago = Convert.ToDecimal(Console.ReadLine());
                            var total = svc.RegistrarSaida(placa, horaSaida, valorPago);
                            Console.WriteLine($"Pagamento OK. Total: R$ {total:F2}");
                            break;
                        case "4":
                            var lista = svc.ConsultarVeiculos();
                            if(lista.Count == 0) Console.WriteLine("Nenhum veículo estacionado no momento.");
                            else
                            {
                                Console.WriteLine("Veículos atualmente estacionados:");
                                foreach (var veiculo in lista)
                                {
                                    Console.WriteLine($"- Placa: {veiculo.Placa}, Hora de Entrada: {veiculo.HoraEntrada}");
                                }
                            }
                            break;
                        case "5":
                            Console.WriteLine("Saindo do sistema...( ͡~ ͜ʖ ͡° )");
                            Environment.Exit(0);
                            return;
                        default:
                            Console.WriteLine("Opção inválida. Tente novamente.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }                
            }
        }

    }
}
