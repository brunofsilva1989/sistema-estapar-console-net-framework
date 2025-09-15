using SistemaEstapar.Domain.Entidades;
using SistemaEstapar.Teste.Interfaces;
using System;
using System.Collections.Generic;

namespace SistemaEstapar.Business.Service
{
    public class EstacionamentoService
    {
        private readonly IEstacionamentoRepository _repo;
        private readonly Estacionamento _regras;

        public EstacionamentoService(IEstacionamentoRepository repo, Estacionamento regras)
        {
            _repo = repo;
            _regras = regras;
        }

        public EstacionamentoService()
        {
            
        }

        /// <summary>
        /// Cadastrar um veículo no sistema
        /// </summary>
        /// <param name="veiculosEstacionados"></param>
        public void CadastrarVeiculo(string placaEntrada)
        {
            Console.Write("Digite a placa do veículo: ");            
            DateTime dateTime = DateTime.Now;
            var placa = new Veiculo { Placa = placaEntrada };
            try
            {
                _repo.AdicionarVeiculo(placa, dateTime);
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
        public void RegistrarEntrada(string placaEntrada, DateTime dateTime)
        {            
            try
            {                
                var placa = new Entradas { Placa = placaEntrada };

                _repo.RegistrarEntrada(placa, dateTime);
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
        public decimal RegistrarSaida(string placa, DateTime horaSaida, decimal valorPago)
        {
            //Console.Write("Digite a placa do veículo: ");
            //string placaDigitada = Console.ReadLine();
            //Veiculo placa = new Veiculo { Placa = placaDigitada };
            //decimal taxaTotal = precoInicial * precoPorHora;
            //Console.WriteLine($"Total a ser pago R$ {taxaTotal}");
            //Console.Write("Deseja pagar o valor 'S', 'N': ");
            //string opcaoPagamento = Console.ReadLine().ToUpper();
            //Console.Write($"Digite o valor pago: ");
            //taxaTotal = Convert.ToDecimal(Console.ReadLine());

            ////Usuário não escolheu pagar
            //if (!string.Equals(opcaoPagamento?.Trim(), "S", StringComparison.OrdinalIgnoreCase))
            //{
            //    Console.WriteLine("Pagamento cancelado/não selecionado");
            //    return;
            //}

            ////Escolheu pagar: mas não aprovou?
            //bool pagamentoAprovado = ValidaPagamentoTaxa(taxaTotal);
            //if (!pagamentoAprovado)
            //{
            //    Console.WriteLine("Pagamento não aprovado. Saindo do processo de saída.");
            //    return;
            //}

            //// Aprovou: segue o fluxo
            //Console.WriteLine("Pagamento realizado!");
            //try
            //{
            //    Console.WriteLine("Removendo carro do sistema após pagamento!");
            //    _repo.RemoverVeiculo(placa);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Erro: {ex.Message}");
            //}
            //return;
            var v = _repo.ObterPorPlaca(placa) ?? throw new InvalidOperationException("Veículo não encontrado.");

            var taxaTotal = _regras.CalcularTaxa(v.HoraEntrada, horaSaida);

            if(!ValidaPagamentoTaxa(taxaTotal, valorPago))
                throw new InvalidOperationException("Pagamento não aprovado. Saindo do processo de saída.");

            _repo.RemoverVeiculo(v);
            return taxaTotal;
        }

        /// <summary>
        /// Consulta os veículos estacionados
        /// </summary>
        /// <param name="veiculosEstacionados"></param>
        public IReadOnlyList<Veiculo> ConsultarVeiculos()
        {
            return _repo.ListarVeiculos();
        }

        /// <summary>
        /// Validar o pagamento da taxa total
        /// </summary>
        /// <param name="taxaTotal"></param>
        /// <returns></returns>
        public bool ValidaPagamentoTaxa(decimal taxaTotal, decimal valorPago)
        {
            return valorPago >= taxaTotal;
        }
    }
}
