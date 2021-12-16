using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImportApi.Dtos.Response
{
    public class BaseResponse
    {
        public string Codigo { get; set; }
        public bool Status { get; set; }
        public string Mensagem { get; set; }
        public object Resultado { get; set; }

    }
}
