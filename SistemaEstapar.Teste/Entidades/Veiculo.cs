using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEstapar.Domain.Entidades
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public DateTime HoraEntrada { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
