using ImportApi.Dtos.Response;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ImportApi.Services.Interface
{
    public interface IImportService
    {
        Task<BaseResponse> ListarArquivosImportados();
        Task<BaseResponse> ListarRegistrosImportados(int ArquivoId);
        Task<BaseResponse> Importar(IFormFile model);
    }
}
