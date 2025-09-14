using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEstapar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Configura a codificação do console para UTF-8
            Console.OutputEncoding = Encoding.UTF8;

            // Inicializa os preços do estacionamento
            decimal precoInicial = 0;
            decimal precoPorHora = 0;
            DateTime horaSaida = DateTime.Now;
            decimal precoHoraAdicional = 0;

            //iniciar as listas com exemplos
            


            // Solicita ao usuário os preços do estacionamento
            Console.Write("Digite o preço inicial do estacionamento: ");
            precoInicial = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Digite o preço por hora adicional: ");
            precoPorHora = Convert.ToDecimal(Console.ReadLine());

            // Solicita ao usuário os preços do estacionamento
            Estacionamento estacionamento = new Estacionamento(precoInicial, precoPorHora);
            string asterisco = string.Empty.PadLeft(57, '*');


            Console.WriteLine("Bem-vindo ao Sistema de Estacionamento");            
            Console.WriteLine(asterisco);
            Console.WriteLine("\r\n███████╗░██████╗████████╗░█████╗░██████╗░░█████╗░██████╗░\r\n██╔════╝██╔════╝╚══██╔══╝██╔══██╗██╔══██╗██╔══██╗██╔══██╗\r\n█████╗░░╚█████╗░░░░██║░░░███████║██████╔╝███████║██████╔╝\r\n██╔══╝░░░╚═══██╗░░░██║░░░██╔══██║██╔═══╝░██╔══██║██╔══██╗\r\n███████╗██████╔╝░░░██║░░░██║░░██║██║░░░░░██║░░██║██║░░██║\r\n╚══════╝╚═════╝░░░░╚═╝░░░╚═╝░░╚═╝╚═╝░░░░░╚═╝░░╚═╝╚═╝░░╚═╝");
            Console.WriteLine(asterisco);
            Console.WriteLine();
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1. Cadastrar veículo");
            Console.WriteLine("2. Registrar entrada");
            Console.WriteLine("3. Registrar saída");
            Console.WriteLine("4. Consultar veículos estacionados");
            Console.WriteLine("5. Sair");
            
            while (true)
            {
                List<Veiculo> veiculosEstacionados = new List<Veiculo>();
                Console.Write("Opção: ");
                string opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":                        
                        CadastrarVeiculo(veiculosEstacionados, estacionamento);
                        break;
                    case "2":
                        RegistrarEntrada(veiculosEstacionados, estacionamento);
                        break;
                    case "3":
                        RegistrarSaida(veiculosEstacionados, estacionamento, precoInicial, precoPorHora, horaSaida);
                        break;
                    case "4":
                        ConsultarVeiculos(veiculosEstacionados, estacionamento);
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
        }

        /// <summary>
        /// Cadastrar um veículo no sistema
        /// </summary>
        /// <param name="veiculosEstacionados"></param>
        private static void CadastrarVeiculo(List<Veiculo> veiculosEstacionados, Estacionamento estacionamento)
        {            
            Console.Write("Digite a placa do veículo: ");
            string placa = Console.ReadLine();
            try
            {
                estacionamento.AdicionarVeiculo(placa);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Registra a entrada de um veículo no estacionamento
        /// </summary>
        /// <param name="veiculosEstacionados"></param>
        private static void RegistrarEntrada(List<Veiculo> veiculosEstacionados, Estacionamento estacionamento)
        {            
            Console.Write("Digite a placa do veículo para adicionar entrada: ");
            string placa = Console.ReadLine();
            DateTime horaEntrada = DateTime.Now;
            try
            {
                estacionamento.RegistrarEntrada(placa, horaEntrada);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Registra a saída de um veículo do estacionamento
        /// </summary>
        /// <param name="veiculosEstacionados"></param>
        /// <param name="precoInicial"></param>
        /// <param name="precoPorHora"></param>
        /// <param name="horaSaida"></param>
        private static void RegistrarSaida(List<Veiculo> veiculosEstacionados, Estacionamento estacionamento, decimal precoInicial, decimal precoPorHora, DateTime horaSaida)
        {            
            Console.Write("Digite a placa do veículo: ");
            string placa = Console.ReadLine();            
            decimal taxaTotal = precoInicial * precoPorHora;
            Console.WriteLine($"Total a ser pago R$ {taxaTotal}");
            Console.Write("Deseja pagar o valor 'S', 'N': ");
            string opcaoPagamento = Console.ReadLine().ToUpper();
            Console.Write($"Digite o valor pago: ");
            taxaTotal = Convert.ToDecimal(Console.ReadLine());
            if (opcaoPagamento == "S")
            {
                bool pagamentoAprovado = new Program().ValidaPagamentoTaxa(taxaTotal);
                if (!pagamentoAprovado)
                {
                    Console.WriteLine("Pagamento não aprovado. Saindo do processo de saída.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Pagamento realizado!");
                try
                {
                    Console.WriteLine("Removendo carro do sistema após pagamento!");
                    estacionamento.RemoverVeiculo(placa, taxaTotal);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
                return;
            }            
        }

        /// <summary>
        /// Consulta os veículos estacionados
        /// </summary>
        /// <param name="veiculosEstacionados"></param>
        private static void ConsultarVeiculos(List<Veiculo> veiculosEstacionados, Estacionamento estacionamento)
        {            
            estacionamento.ListarVeiculos();
        }


        /// <summary>
        /// Validar o pagamento da taxa total
        /// </summary>
        /// <param name="taxaTotal"></param>
        /// <returns></returns>
        private bool ValidaPagamentoTaxa(decimal taxaTotal)
        {
            if (taxaTotal == taxaTotal) // Lógica de validação do pagamento
            {
                Console.WriteLine("Pagamento aprovado.");
                return true;
            }
            return true;
        }

    }
}
