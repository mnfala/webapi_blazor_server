using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.API
{
    public class Resposta
    {
        public string usuario { get; set; }
        public string senha { get; set; }
        public string token { get; set; }
        public string erro { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class WebApi : ControllerBase
    {
        /*
        WebApi()
        {
            var header = this.Response.Headers;
            header.AccessControlAllowOrigin = "*";
        }
        */

        // GET: api/<WebApi>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<WebApi>
        [HttpPost]

        public string Post()
        {
            this.Response.Headers.AccessControlAllowOrigin = "*";
            var task = Task.Run(async () => await ReadBodyAsync());
            var param = task.Result;

            Resposta parametro = new Resposta();
            string msgErro = "";
            try
            {
                parametro = JsonSerializer.Deserialize<Resposta>(param);
            }
            catch (JsonException ex)
            {
                msgErro = ex.Message;
            }
            if (msgErro == "")
                if (parametro.usuario != "edson" || parametro.senha != "1234")
                    msgErro = "Usuário ou senha inválidos";
            Resposta result = new Resposta { usuario = "teste", senha = "", token = "tk1", erro = msgErro };
            string jsonResult = JsonSerializer.Serialize(result);
            return jsonResult;
        }

        private async Task<string> ReadBodyAsync()
        {
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                return await reader.ReadToEndAsync();
            }
        }

        #region nao utilizado
        // GET api/<WebApi>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        // PUT api/<WebApi>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WebApi>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion
    }
}
