using InnoSetupOnline;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get([FromServices] IOptions<AppOptions> opts)
        {
            var workDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Setup");

            var isccdir = Path.Combine(opts.Value.ISCCDir, "ISCC.exe");

            var fileName = isccdir;

            var args = Path.Combine(workDir, "script.iss");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                fileName = "wine";
                args = $@"""{isccdir}"" Z:{args}";
            }
            else
            {
                fileName = $@"""{fileName}""";
            }

            Console.WriteLine($"fileName:{fileName}");
            Console.WriteLine($"args:{args}");

            var process = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = args,
                WindowStyle = ProcessWindowStyle.Hidden,
                //   UseShellExecute = false,
                WorkingDirectory = workDir
            };

            Process.Start(process);

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
