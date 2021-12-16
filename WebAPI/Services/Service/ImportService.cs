using ImportApi.Dtos.Response;
using ImportApi.Models;
using ImportApi.Repositories.Interface;
using ImportApi.Services.Interface;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ImportApi.Services.Service
{
    public class ImportService : IImportService
    {
        private readonly IArquivoRepository _arquivoRepository;
        private readonly IRegistroRepository _registroRepository;

        public ImportService(IArquivoRepository arquivoRepository, IRegistroRepository registroRepository)
        {
            _arquivoRepository = arquivoRepository;
            _registroRepository = registroRepository;
        }

        public async Task<BaseResponse> Importar(IFormFile file)
        {
            try
            {
                var id = int.Parse(file.FileName.Split(".")[0]);

                //Validar se arquivo já foi importado
                var arquivoExists = _arquivoRepository.ExisteRegistro(id);
                if (arquivoExists)
                {
                    return new BaseResponse { Status = false, Mensagem = $"Arquivo {file.FileName} já importado." };
                }

                var arquivo = new TblArquivo
                {
                    ArquivoId = id,
                    DthImport = DateTime.Now
                };

                var adicionarArquivo = await _arquivoRepository.Adicionar(arquivo);

                if(adicionarArquivo == 0)
                {
                    return new BaseResponse { Status = false, Mensagem = "Erro ao adicionar arquivo." };
                }

                int quantidadeRegistros = 0;
                int quantidadeRegistrosImportados = 0;

                if (file.FileName.EndsWith(".tsv"))
                {
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        if(id == 1)
                        {
                            string[] headers = reader.ReadLine().Split("\t");
                        }
                        while (!reader.EndOfStream)
                        {
                            string[] rows = reader.ReadLine().Split("\t");
                            var objeto = new TblRegistro
                            {
                                ArquivoId = arquivo.ArquivoId,
                                DthInsert = DateTime.Now,
                                Tconst = rows[0],
                                TitleType = rows[1],
                                PrimaryTitle = rows[2],
                                OriginalTitle = rows[3],
                                IsAdult = rows[4],
                                StartYear = rows[5],
                                EndYear = rows[6],
                                RuntimeMinutes = rows[7],
                                Genres = rows[8]
                            };

                            quantidadeRegistros++;

                            var adicionarRegistro = await _registroRepository.Adicionar(objeto);

                            if(adicionarRegistro > 0)
                            {
                                quantidadeRegistrosImportados++;
                            }
                        }
                    }
                }

                arquivo.QuantidadeRegistros = quantidadeRegistros;
                arquivo.QuantidadeRegistrosImportados = quantidadeRegistrosImportados;

                var updateArquivo = await _arquivoRepository.Alterar(arquivo);


                return new BaseResponse { Status = true, Mensagem = $"{quantidadeRegistrosImportados} registros importados de um total de {quantidadeRegistros} para o arquivo {file.FileName}" };
                
            }
            catch (Exception ex)
            {
                return (new BaseResponse { Status = false, Mensagem = ex.Message });
            }
        }

        public async Task<BaseResponse> ListarArquivosImportados()
        {
            try
            {
                var listagem = await _arquivoRepository.Listar();

                if(listagem.Count > 0)
                {
                    return new BaseResponse { Status = true, Mensagem = "Esses são os arquivos importados: ", Resultado = JsonConvert.SerializeObject(listagem) };
                }

                return new BaseResponse { Status = false, Mensagem = "Erro ao listar arquivos importados." };


            }
            catch (Exception ex)
            {
                return (new BaseResponse { Status = false, Mensagem = ex.Message });
            }
        }

        public async Task<BaseResponse> ListarRegistrosImportados(int ArquivoId)
        {
            try
            {
                var listagem = await _registroRepository.Listar(ArquivoId);

                if (listagem.Count > 0)
                {
                    return new BaseResponse { Status = true, Mensagem = "Esses são os registros importados: ", Resultado = listagem };
                }

                return new BaseResponse { Status = false, Mensagem = "Erro ao listar registros importados." };


            }
            catch (Exception ex)
            {
                return (new BaseResponse { Status = false, Mensagem = ex.Message });
            }
        }
    }
}
