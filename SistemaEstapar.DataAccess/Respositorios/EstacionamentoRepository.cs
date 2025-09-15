using SistemaEstapar.DataAccess.Data;
using SistemaEstapar.Domain.Entidades;
using SistemaEstapar.Teste.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEstapar.DataAccess.Respositorios
{
    public class EstacionamentoRepository : IEstacionamentoRepository
    {
        private readonly EstacionamentoDb _db;

        public EstacionamentoRepository(EstacionamentoDb db)
        {
            _db = db;
        }

        public bool PlacaExiste(string placa)
        {
            return _db.Veiculos.Any(v => v.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase));
        }

        public Veiculo ObterPorPlaca(string placa)
        {
            return _db.Veiculos.FirstOrDefault(v => v.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase));
        }

        public void AdicionarVeiculo(Veiculo placa, DateTime horaEntrada)
        {
            _db.Veiculos.Add(new Veiculo
            {
                Placa = placa.Placa,
                HoraEntrada = horaEntrada
            });
        }
       
        public void RegistrarEntrada(Entradas placa, DateTime entrada)
        {
            _db.Entradas.Add(new Entradas
            {
                Placa = placa.Placa,
                HoraEntrada = entrada
            });
        }

        public void RemoverVeiculo(Veiculo placa)
        {
            if (placa == null)
            {
                throw new ArgumentNullException(nameof(placa), "A placa do veículo não pode ser nula.");
            }

            _db.Veiculos.Remove(placa);
        }

        public IReadOnlyList<Entradas> ListarEntradas()
        {
            return _db.Entradas.ToList();
        }

        public IReadOnlyList<Veiculo> ListarVeiculos()
        {
            return _db.Veiculos.ToList();
        }
    }
}
