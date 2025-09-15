using SistemaEstapar.Domain.Entidades;
using System;
using System.Collections.Generic;

namespace SistemaEstapar.DataAccess.Data
{
    public class EstacionamentoDb
    {
        public List<Veiculo> Veiculos { get; } = new List<Veiculo>();

        public List<Entradas> Entradas { get; } = new List<Entradas>();
        
        public EstacionamentoDb()
        {
            Veiculos.Add(new Veiculo { Id = 1, Placa = "ABC1234", HoraEntrada = DateTime.Now.AddHours(-3), ValorTotal = 20.00m });
            Veiculos.Add(new Veiculo { Id = 2, Placa = "DEF5678", HoraEntrada = DateTime.Now.AddHours(-1), ValorTotal = 10.00m });
            Entradas.Add(new Entradas { Id = 1, Placa = "GHI9012", HoraEntrada = DateTime.Now.AddHours(-2) });
            Entradas.Add(new Entradas { Id = 2, Placa = "JKL3456", HoraEntrada = DateTime.Now.AddHours(-4) });
        }
    }
}
