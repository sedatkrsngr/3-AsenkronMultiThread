using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TaskWebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
       

        [HttpGet]
        public async Task<IActionResult> GetContentAsync()
        {
            Thread.Sleep(5000);
            var myTask =  new HttpClient().GetStringAsync("https://www.google.com");

            var data = await myTask;

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetContenttAsync(CancellationToken cancellationToken)
        {
            try
            {
                await Task.Delay(5000, cancellationToken);//kullanılan herhangi bir async metodda sayfa kapandığı an işlem bitmediyse hata fırlatır. Örneğin istek yaptık ve hemen sayfayı kapattık. Kaynakları kullanması gereksiz olacağı için isteği keseriz Böylelikle.Burada 5 saniye beklemeden kapatırsak hata fırlatır
                var myTask = new HttpClient().GetStringAsync("https://www.google.com");

                var data = await myTask;

                //diyelimki asenkron bir metod yok ama ben bir işlem yapıyorum ve o yarıda kesilirse yine hata fırlatmak istiyorum 
                //cancellationToken.ThrowIfCancellationRequested(); metodunu kullanırız İşlem yarıda kesilirse hata verir.

                return Ok(data);
            }
            catch (Exception ex)
            {

                //istek iptal edildi diyip hatayı basabiliriz ex.message ile

                return BadRequest();
            }


          
        }

    }
}
