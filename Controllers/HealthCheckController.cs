using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace SimpleApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            string serviceName = "my-dotnet-app"; // Замените на имя вашего сервиса
            string result;

            try
            {
                using (var process = new Process())
                {
                    process.StartInfo.FileName = "systemctl";
                    process.StartInfo.Arguments = $"is-active {serviceName}";
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();

                    result = process.StandardOutput.ReadToEnd().Trim();
                    process.WaitForExit();
                }

                if (result == "active")
                {
                    return Ok(new { Status = "Running" });
                }
                else
                {
                    return Ok(new { Status = "Not Running" });
                }
            }
            catch
            {
                return StatusCode(500, new { Status = "Error" });
            }
        }
    }
}
