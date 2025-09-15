🅿️ Sistema de Estacionamento (Console)

Aplicação C#/.NET de console para gerenciar um estacionamento: cadastrar veículos, registrar entradas, calcular taxa de saída e listar veículos estacionados — tudo em camadas (Domain, Business, DataAccess e ConsoleApp) pra ficar fácil de manter e evoluir.

✨ Funcionalidades

Cadastrar veículo com validação de duplicidade

Registrar entrada com DateTime.Now

Registrar saída com cálculo de taxa por hora cheia (Ceiling)

Validar pagamento (valor pago ≥ taxa)

Listar veículos estacionados

Persistência in-memory (pronto para trocar por banco depois)


🧱 Arquitetura & Pastas
SistemaEstapar/
├─ SistemaEstapar.ConsoleApp/         # UI (menu e interação no console)
├─ SistemaEstapar.Business/           # Serviços (casos de uso)
├─ SistemaEstapar.Domain/             # Entidades + Regras + Interfaces
└─ SistemaEstapar.DataAccess/         # Repositório (in-memory)

Dependências:
ConsoleApp → Business → Domain
                 ↑
            DataAccess (implementa as interfaces do Domain)


🧠 Regras de negócio (exemplo)
// Cálculo da taxa por hora cheia
public decimal CalcularTaxa(DateTime entrada, DateTime saida)
{
    if (saida < entrada) throw new ArgumentException("Saída antes da entrada.");
    var horas = (decimal)Math.Ceiling((saida - entrada).TotalHours);
    return horas <= 1 ? PrecoInicial : PrecoInicial + (horas - 1) * PrecoPorHora;
}


Contrato do Repositório (no Domain):
public interface IEstacionamentoRepository
{
    bool PlacaExiste(string placa);
    void AdicionarVeiculo(Veiculo v);
    Veiculo? ObterPorPlaca(string placa);
    void RemoverVeiculo(Veiculo v);
    IReadOnlyList<Veiculo> ListarVeiculos();

    void RegistrarEntrada(Entradas e);
    IReadOnlyList<Entradas> ListarEntradas();
}

▶️ Como rodar

Pré-requisitos:
.NET SDK 8.0+

Build & Run:
dotnet restore
dotnet build
dotnet run --project ./SistemaEstapar.ConsoleApp

🖥️ Exemplo (fluxo no console)
╔════════════════════════════════╗
║           MAIN MENU            ║
╚════════════════════════════════╝
1. Cadastrar veículo
2. Registrar entrada
3. Registrar saída
4. Consultar veículos estacionados
5. Sair

Opção: 1
Placa: ABC1D23
Veículo cadastrado!

Opção: 3
Placa: ABC1D23
Valor pago: 20,00
Pagamento OK. Total: R$ 15,00

Notas de implementação:

Domain: entidades (Veiculo, Entradas) e regra (Estacionamento) — sem Console e sem acesso a dados.
Business: EstacionamentoService orquestra o caso de uso (usa repositório + regra de cálculo).
DataAccess: EstacionamentoRepository com List<T> (in-memory) — pronto para evoluir pra EF/SQL.
ConsoleApp: apenas I/O e composição das dependências.

Validação de pagamento usada:
public static bool ValidaPagamentoTaxa(decimal taxaTotal, decimal valorPago)
    => valorPago >= taxaTotal;

👤 Autor
Bruno Silva — Desenvolvedor .NET



