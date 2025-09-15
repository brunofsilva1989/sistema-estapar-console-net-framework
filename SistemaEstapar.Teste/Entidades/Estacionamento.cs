using System;

namespace SistemaEstapar.Domain.Entidades
{
    public class Estacionamento
    {
        #region Propriedades                            
        private decimal precoInicial;
        private decimal precoPorHora;
        #endregion Propriedades

        #region Construtor
        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {            
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public Estacionamento() { }        
        #endregion Construtor

        public decimal CalcularTaxa(DateTime entrada, DateTime saida)
        {
            if(saida < entrada) throw new ArgumentException("A hora de saída não pode ser anterior à hora de entrada.");
            var horas = (decimal)Math.Ceiling((saida - entrada).TotalHours);
            return horas <= 1 ? precoInicial : precoInicial + (horas - 1) * precoPorHora;
        }


        //#region Métodos
        ///// <summary>
        ///// Função para adicionar veículo ao estacionamento
        ///// </summary>
        ///// <param name="placa"></param>
        ///// <exception cref="ArgumentException"></exception>
        ///// <exception cref="InvalidOperationException"></exception>
        //public async Task AdicionarVeiculo(string placa)
        //{
        //    if (string.IsNullOrWhiteSpace(placa))
        //    {
        //        throw new ArgumentException("A placa do veículo não pode ser vazia.");
        //    }
        //    if (veiculos.Any(v => v.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase)))
        //    {
        //        throw new InvalidOperationException("Veículo com esta placa já está estacionado.");
        //    }
        //    await Task.Delay(2000); 
        //    veiculos.Add(new Veiculo { Placa = placa, HoraEntrada = DateTime.Now });
        //    Console.WriteLine($"Veículo com placa {placa} adicionado ao estacionamento.");
        //}

        ///// <summary>
        ///// Remove um veículo do estacionamento com base em sua placa e calcula a taxa total de estacionamento
        ///// </summary>
        ///// <remarks> taxa total de estacionamento é calculada usando o preço inicial e a taxa horária,
        ///// arredondou para a próxima hora inteira. A taxa é exibida no console após a remoção.</remarks>
        ///// <param name="placa">A placa do veículo a ser removida. A comparação é insensível ao caso.</param>
        ///// <exception cref="InvalidOperationException">Thrown if a vehicle with the specified license plate is not found in the parking lot.</exception>
        //public async Task RemoverVeiculo(string placa, decimal valorTotal)
        //{
        //    var veiculo = veiculos.FirstOrDefault(v => v.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase));
        //    if (veiculo == null)
        //    {
        //        throw new InvalidOperationException("Veículo não encontrado.");
        //    }
        //    //var tempoEstacionado = DateTime.Now - veiculo.HoraEntrada;
        //    //var horasEstacionado = Math.Ceiling(tempoEstacionado.TotalHours);
        //    //var valorTotal = precoInicial + (decimal)horasEstacionado * precoPorHora;
        //    await Task.Delay(2000);
        //    veiculos.Remove(veiculo);
        //    Console.WriteLine($"Veículo com placa {placa} removido. Total a pagar: R$ {valorTotal:F2}");
        //}

        ///// <summary>
        ///// Exibe uma lista de todos os veículos estacionados atualmente.
        ///// </summary>
        ///// <remarks>Se nenhum veículo estiver estacionado, uma mensagem indicando que nenhum veículo está presente está
        ///// exibido. Este método gera informações diretamente para o console e não retorna nenhum dado. </remarks>
        //public void ListarVeiculos()
        //{           
        //    if (!veiculos.Any())
        //    {
        //        Console.WriteLine("Nenhum veículo estacionado.");
        //        return;
        //    }
        //    Console.WriteLine("Veículos estacionados:");
        //    foreach (var veiculo in veiculos)
        //    {
        //        Console.WriteLine($"- Placa: {veiculo.Placa}, Hora de entrada: {veiculo.HoraEntrada}");
        //    }
        //}

        ///// <summary>
        ///// Registra a entrada de um veículo no estacionamento
        ///// </summary>
        ///// <param name="placa"></param>
        ///// <param name="horaEntrada"></param>
        //public async Task RegistrarEntrada(string placa, DateTime horaEntrada)
        //{
        //    await Task.Delay(2000);
        //    entradas.Add(new Entradas
        //    {
        //        Placa = placa,
        //        HoraEntrada = horaEntrada
        //    });
        //    Console.WriteLine($"Entrada registrada para o veículo com placa {placa}.");
        //}
        //#endregion Métodos
    }
}
