using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Angular_ASPNETCore.Data;
using Angular_ASPNETCore.Repositories;
using ASPNETCore_Angular.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Angular_ASPNETCore.Controllers
{
    [Authorize]
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

        // GET: api/Produto/5
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

        // PUT: api/Produto/5
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

            #region Validacao Imagem
            if (produto.ImagemPath.Contains("data:image"))
            {
                var stringImagem = Regex.Match(produto.ImagemPath, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                var bytes = Convert.FromBase64String(stringImagem);// a.base64image 
                try
                {
                    produto.ImagemPath = ByteToImage(bytes, TruncateLongString(stringImagem));
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Internal server error");
                }
            }

            #endregion

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

        // POST: api/Produto
        [HttpPost]
        public async Task<IActionResult> PostProduto([FromBody] Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            #region Validacao Imagem

            var stringImagem = Regex.Match(produto.ImagemPath, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            var bytes = Convert.FromBase64String(stringImagem);// a.base64image 
            try
            {
                produto.ImagemPath = ByteToImage(bytes, TruncateLongString(stringImagem));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
            #endregion

            produto.Id = 0;
            _repo.Add(produto);
            await _repo.SaveAsync(produto);

            return StatusCode(201, new { produtoResponse = produto });

            //return CreatedAtAction("GetProduto", new { id = produto.Id }, produto);
        }

        // DELETE: api/Produto/5
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

        private string ByteToImage(byte[] bytes, string fileName)
        {
            string dbPath = "";

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = Guid.NewGuid().ToString();
            }

            var folderName = Path.Combine(@"client\", @"src\assets\img");
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);


            if (bytes.Length > 0)
            {
                var fullPath = Path.Combine(pathToSave + "\\" + fileName);

                using (var stream = new FileStream(fullPath + ".jpg", FileMode.Create))
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
                    dbPath =  fileName + ".jpg";
                }
                return dbPath;
            }
            else
            {
                return dbPath;
            }
        }

        private string TruncateLongString(string str, int maxLength = 35)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            str = str.Substring(0, Math.Min(str.Length, maxLength));
            str = Regex.Replace(str, "/", "");
            return str;
        }

    }
}