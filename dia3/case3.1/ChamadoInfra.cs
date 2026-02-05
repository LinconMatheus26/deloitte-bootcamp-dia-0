namespace SistemaChamados;

public class ChamadoInfra : Chamado
{
    public ChamadoInfra(int id, string titulo, string solicitante, string descricao, Prioridade prioridade) 
        : base(id, titulo, solicitante, descricao, prioridade) 
    { 
    }

    public override string ObterResumoTipo() => "Tipo: INFRAESTRUTURA (Hardware/Redes/Servidores)";
}