using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using MinhaApi.Models;
using MinhaApi.Dtos;

namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesMinerioController : ControllerBase
    {
        private readonly AppDbContext _db;

        public LotesMinerioController(AppDbContext db) => _db = db;

        // --- GET ALL ---
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lotes = await _db.LotesMinerio.ToListAsync();
            return Ok(lotes);
        }

        // --- GET BY ID ---
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var lote = await _db.LotesMinerio.FindAsync(id);
            return lote is null ? NotFound() : Ok(lote);
        }

        // --- POST: CREATE ---
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLoteMinerioDto input)
        {
            if (input.TeorFe is < 0 or > 100) return BadRequest("TeorFe inválido.");
            
            var exists = await _db.LotesMinerio.AnyAsync(x => x.CodigoLote == input.CodigoLote);
            if (exists) return Conflict("Código de lote já existe.");

            var lote = new LoteMinerio
            {
                CodigoLote = input.CodigoLote,
                MinaOrigem = input.MinaOrigem,
                TeorFe = input.TeorFe,
                Umidade = input.Umidade,
                SiO2 = input.SiO2,
                P = input.P,
                Toneladas = input.Toneladas,
                DataProducao = input.DataProducao.HasValue 
                    ? DateTime.SpecifyKind(input.DataProducao.Value, DateTimeKind.Utc) 
                    : DateTime.UtcNow,
                Status = (StatusLote)input.Status,
                LocalizacaoAtual = input.LocalizacaoAtual
            };

            _db.LotesMinerio.Add(lote);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = lote.Id }, lote);
        }

        // --- PUT: UPDATE ---
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateLoteMinerioDto input)
        {
            var lote = await _db.LotesMinerio.FindAsync(id);
            if (lote == null) return NotFound("Lote não encontrado.");

            lote.MinaOrigem = input.MinaOrigem;
            lote.TeorFe = input.TeorFe;
            lote.Umidade = input.Umidade;
            lote.SiO2 = input.SiO2;
            lote.P = input.P;
            lote.Toneladas = input.Toneladas;
            lote.LocalizacaoAtual = input.LocalizacaoAtual;
            lote.Status = (StatusLote)input.Status;

            if (input.DataProducao.HasValue)
                lote.DataProducao = DateTime.SpecifyKind(input.DataProducao.Value, DateTimeKind.Utc);

            await _db.SaveChangesAsync();
            return Ok(lote);
        }

        // --- DELETE ---
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var lote = await _db.LotesMinerio.FindAsync(id);
            if (lote == null) return NotFound("Lote não encontrado para exclusão.");

            _db.LotesMinerio.Remove(lote);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}