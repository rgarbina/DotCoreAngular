using System.Linq;
using System.Threading.Tasks;
using Angular_ASPNETCore.Data;
using Angular_ASPNETCore.Repositories;
using ASPNETCore_Angular.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Angular_ASPNETCore.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IDataRepository<Produto> _repo;

        public ProdutoController(IDataRepository<Produto> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProdutos()
        {
            return Ok(await _repo.GetAllAsNoTracking().ToListAsync());
        }

        // GET: api/Supplier/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduto([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Produto = await _repo.GetById(id);

            if (Produto == null)
            {
                return NotFound();
            }

            return Ok(Produto);
        }

        // PUT: api/Supplier/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto([FromRoute] long id, [FromBody] Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produto.Id)
            {
                return BadRequest();
            }

            _repo.Update(produto);

            try
            {
                await _repo.SaveAsync(produto);
            }
            catch (DbUpdateConcurrencyException)
            {
                bool exist = await _repo.Exist(id);
                if (!exist)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Supplier
        [HttpPost]
        public async Task<IActionResult> PostProduto([FromBody] Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            produto.Id = 0;
            _repo.Add(produto);
            await _repo.SaveAsync(produto);

            return CreatedAtAction("GetProduto", new { id = produto.Id }, produto);
        }

        // DELETE: api/Supplier/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Produto = await _repo.GetById(id);
            if (Produto == null)
            {
                return NotFound();
            }

            _repo.Delete(Produto);
            await _repo.SaveAsync(Produto);

            return Ok(Produto);
        }
    }
}