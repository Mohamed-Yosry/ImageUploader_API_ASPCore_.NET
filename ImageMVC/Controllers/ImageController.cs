using ImageMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace ImageMVC.Controllers
{
    public class ImageController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44362/api/");

        public ImageController()
        {
         
        }
        public ActionResult Index()
        {
            IEnumerable<ImageModel> models = null;
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var responcse = client.GetAsync("Image/GetImage");
                responcse.Wait();

                var image = responcse.Result;

                if (image.IsSuccessStatusCode)
                {
                    var read = image.Content.ReadAsAsync<IList<ImageModel>>();
                    read.Wait();
                    models = read.Result;
                }
            }
            return View(models);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ImageModel model)
        {
            var file = Request.Form.Files.FirstOrDefault();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/Image/SaveImage");
                var postTask = client.PostAsJsonAsync<ImageModel>(client.BaseAddress, model);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(model);
        }
    }
}
