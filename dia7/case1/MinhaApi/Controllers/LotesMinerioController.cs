using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using MinhaApi.Models;
using MinhaApi.Dtos;
using MinhaApi.Queues;


namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesMinerioController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly RedisQueueService _queue; // <--- Adicionei o campo da fila

        // O CONSTRUTOR AGORA ACEITA 2 ARGUMENTOS (Isso resolve o erro do teste!)
        public LotesMinerioController(AppDbContext db, RedisQueueService queue)
        {
            _db = db;
            _queue = queue;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lotes = await _db.LotesMinerio.ToListAsync();
            return Ok(lotes);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var lote = await _db.LotesMinerio.FindAsync(id);
            return lote is null ? NotFound() : Ok(lote);
        }

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

            // --- SAÍDA PARA O REDIS (ENVIA PARA A FILA) ---
            await _queue.EnviarParaFila("lotes_criados", lote);

            return CreatedAtAction(nameof(GetById), new { id = lote.Id }, lote);
        }

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

            // --- AVISO NO REDIS ---
            await _queue.EnviarParaFila("lotes_atualizados", lote);

            return Ok(lote);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var lote = await _db.LotesMinerio.FindAsync(id);
            if (lote == null) return NotFound("Lote não encontrado para exclusão.");

            _db.LotesMinerio.Remove(lote);
            await _db.SaveChangesAsync();

            // --- AVISO NO REDIS ---
            await _queue.EnviarParaFila("lotes_deletados", new { Id = id });

            return NoContent();
        }
    }
}