using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValeMonitoramento.Data;
using ValeMonitoramento.Models;
using ValeMonitoramento.Dtos;

namespace ValeMonitoramento.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipamentosController : ControllerBase
{
    private readonly AppDbContext _context;

    public EquipamentosController(AppDbContext context) => _context = context;

    [HttpPost]
    public async Task<IActionResult> Create(EquipamentoCreateDto dto)
    {
        var codigoClean = dto.Codigo.Trim();

        if (await _context.Equipamentos.AnyAsync(e => e.Codigo == codigoClean))
            return Conflict(new { message = "Erro: O código do equipamento já existe." });

        
        
        var equipamento = new Equipamento
        {
            Codigo = codigoClean,
            Tipo = dto.Tipo,
            Modelo = dto.Modelo,
            Horimetro = dto.Horimetro,
            StatusOperacional = dto.StatusOperacional,
            DataAquisicao = dto.DataAquisicao,
            LocalizacaoAtual = dto.LocalizacaoAtual
        };

        _context.Equipamentos.Add(equipamento);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = equipamento.Id }, equipamento);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? tipo, 
        [FromQuery] string? status, 
        [FromQuery] string? codigo,
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 10)
    {
        var query = _context.Equipamentos.AsQueryable();

        if (!string.IsNullOrEmpty(tipo))
            query = query.Where(e => e.Tipo.ToString() == tipo);

        if (!string.IsNullOrEmpty(status))
            query = query.Where(e => e.StatusOperacional.ToString() == status);

        if (!string.IsNullOrEmpty(codigo))
            query = query.Where(e => e.Codigo.Contains(codigo));

        var total = await query.CountAsync();
        var itens = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return Ok(new { total, page, pageSize, items = itens });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var equip = await _context.Equipamentos.FindAsync(id);
        return equip == null ? NotFound("Equipamento não encontrado.") : Ok(equip);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, EquipamentoCreateDto dto)
    {
        var equipamento = await _context.Equipamentos.FindAsync(id);
        if (equipamento == null) return NotFound();

        equipamento.Codigo = dto.Codigo.Trim();
        equipamento.Tipo = dto.Tipo;
        equipamento.Modelo = dto.Modelo;
        equipamento.Horimetro = dto.Horimetro;
        equipamento.StatusOperacional = dto.StatusOperacional;
        equipamento.DataAquisicao = dto.DataAquisicao;
        equipamento.LocalizacaoAtual = dto.LocalizacaoAtual;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var equip = await _context.Equipamentos.FindAsync(id);
        if (equip == null) return NotFound();

        _context.Equipamentos.Remove(equip);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}