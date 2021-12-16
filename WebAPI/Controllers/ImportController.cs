using ImportApi.Dtos.Response;
using ImportApi.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly IImportService _importService;

        public ImportController(IImportService importService)
        {
            _importService = importService;
        }

        [HttpPost("Importar")]
        public async Task<IActionResult> Importar([FromForm] IFormFile file)
        {
            try
            {
                return Ok(await _importService.Importar(file));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse { Status = false, Mensagem = "Erro ao importar registros.", Resultado = ex.Message });
            }
        }

        [HttpGet("ListarArquivosImportados")]
        public async Task<IActionResult> ListarArquivosImportados()
        {
            try
            {
                return Ok(await _importService.ListarArquivosImportados());
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse { Status = false, Mensagem = "Erro ao listar arquivos.", Resultado = ex.Message });
            }
        }

        [HttpGet("ListarRegistrosImportados")]
        public async Task<IActionResult> ListarRegistrosImportados([FromQuery] int ArquivoId)
        {
            try
            {
                return Ok(await _importService.ListarRegistrosImportados(ArquivoId));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse { Status = false, Mensagem = "Erro ao listar registros.", Resultado = ex.Message });
            }
        }
    }
}
