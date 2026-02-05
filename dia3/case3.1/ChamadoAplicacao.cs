namespace SistemaChamados;

public class ChamadoAplicacao : Chamado
{
    // O construtor recebe todos os parâmetros e repassa para o base(...) em Chamado.cs
    public ChamadoAplicacao(int id, string titulo, string solicitante, string descricao, Prioridade prioridade) 
        : base(id, titulo, solicitante, descricao, prioridade) 
    { 
    }

    public override string ObterResumoTipo() => "Tipo: APLICAÇÃO (Software/Bugs/Sistemas)";
}