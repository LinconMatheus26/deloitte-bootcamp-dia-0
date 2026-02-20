using System.ComponentModel.DataAnnotations;
using ValeMonitoramento.Models;

namespace ValeMonitoramento.Dtos;

public class EquipamentoCreateDto
{
    [Required(ErrorMessage = "O código do equipamento é obrigatório.")]
    public string Codigo { get; set; } = string.Empty;

    [Required(ErrorMessage = "O tipo do equipamento deve ser informado.")]
    [EnumDataType(typeof(TipoEquipamento), ErrorMessage = "Tipo de equipamento inválido.")]
    public TipoEquipamento Tipo { get; set; }

    [Required(ErrorMessage = "O modelo é obrigatório.")]
    public string Modelo { get; set; } = string.Empty;

    [Range(0, double.MaxValue, ErrorMessage = "O horímetro não pode ser negativo.")]
    public decimal Horimetro { get; set; }

    [Required(ErrorMessage = "O status operacional deve ser informado.")]
    [EnumDataType(typeof(StatusOperacional), ErrorMessage = "Status operacional inválido.")]
    public StatusOperacional StatusOperacional { get; set; }

    [Required(ErrorMessage = "A data de aquisição é obrigatória.")]
    public DateOnly DataAquisicao { get; set; } 
    
    [Required(ErrorMessage = "A localização atual é obrigatória.")]
    public string LocalizacaoAtual { get; set; } = string.Empty;
}