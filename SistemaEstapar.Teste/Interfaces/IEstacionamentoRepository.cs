using SistemaEstapar.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEstapar.Teste.Interfaces
{
    public interface IEstacionamentoRepository
    {        
        void AdicionarVeiculo(Veiculo v, DateTime horaEntrada);        
        void RemoverVeiculo(Veiculo v);      
        void RegistrarEntrada(Entradas e, DateTime entrada);
        IReadOnlyList<Veiculo> ListarVeiculos();
        IReadOnlyList<Entradas> ListarEntradas();
        bool PlacaExiste(string placa);
        Veiculo ObterPorPlaca(string placa);
    }
}
