namespace SistemaChamados;

public abstract class Chamado
{
    public int Id { get; private set; }
    public string Titulo { get; private set; }
    public string Solicitante { get; private set; }
    public string Descricao { get; set; } // Agora será preenchido
    public Prioridade Prioridade { get; private set; } // Set privado para controle
    public StatusChamado Status { get; private set; }
    public DateTime DataAbertura { get; }
    public List<string> Historico { get; } = new();

    protected Chamado(int id, string titulo, string solicitante, string descricao, Prioridade prioridade)
    {
        if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(solicitante))
            throw new DominioException("Título e Solicitante são obrigatórios!");

        Id = id;
        Titulo = titulo;
        Solicitante = solicitante;
        Descricao = descricao;
        Prioridade = prioridade;
        Status = StatusChamado.Aberto;
        DataAbertura = DateTime.Now;
        RegistrarHistorico($"Chamado aberto. Prioridade inicial: {prioridade}");
    }

    public abstract string ObterResumoTipo();

    // NOVO MÉTODO: Permite ao usuário ou TI alterar a prioridade
    public void AlterarPrioridade(Prioridade novaPrioridade)
    {
        var prioridadeAntiga = Prioridade;
        Prioridade = novaPrioridade;
        RegistrarHistorico($"Prioridade alterada de {prioridadeAntiga} para {novaPrioridade}");
    }

    public void AtualizarStatus(StatusChamado novoStatus)
    {
        bool valido = (Status, novoStatus) switch
        {
            (StatusChamado.Aberto, StatusChamado.EmAndamento) => true,
            (StatusChamado.EmAndamento, StatusChamado.Resolvido) => true,
            (StatusChamado.Resolvido, StatusChamado.Fechado) => true,
            _ => false
        };

        if (!valido)
            throw new DominioException($"Transição inválida: Não é permitido mudar de {Status} para {novoStatus}.");

        Status = novoStatus;
        RegistrarHistorico($"Status alterado para {novoStatus}");
    }

    protected void RegistrarHistorico(string acao) => 
        Historico.Add($"[{DateTime.Now:dd/MM HH:mm}] - {acao}");
}