using System.ComponentModel.DataAnnotations;

namespace ValeMonitoramento.Models;

public enum TipoEquipamento { Caminhao, Escavadeira, Perfuratriz, Carregadeira, Trator }
public enum StatusOperacional { Operacional, EmManutencao, Parado }

public class Equipamento
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public TipoEquipamento Tipo { get; set; }
    public string Modelo { get; set; } = string.Empty;
    public decimal Horimetro { get; set; }
    public StatusOperacional StatusOperacional { get; set; }
    public DateOnly DataAquisicao { get; set; } 
    public string LocalizacaoAtual { get; set; } = string.Empty;
}