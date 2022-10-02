using ImageApi_Task1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace ImageApi_Task1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ImageDbContext context;
        public IHostingEnvironment Environment;

        public ImageController(ImageDbContext context,IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.Environment = hostingEnvironment;
        }
        [HttpPost]
        public ActionResult<string> SaveImage()
        {
            //read the file
            //var file2 = HttpContext.Request.Form.Files.FirstOrDefault();
            var file = Request.Form.Files.FirstOrDefault();
            if (file != null)
            {
                //stroing file info
                FileInfo fileInfo = new FileInfo(file.FileName);
                //generate UUID for the name
                string newFileName = Guid.NewGuid().ToString();//"Image_"+DateTime.Now.Millisecond+fileInfo.Extension;
                string folderPath = "\\UploadedImages\\" + newFileName + fileInfo.Extension;
                //stroing folder path
                string path = Environment.ContentRootPath + folderPath;

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                //stroing the date
                var date = DateTime.Now;
                //create the image model
                var model = new ImageModel()
                {
                    FilePath = path,
                    Metatag = "",
                    InsertionDate = date
                };
                //add the model and save changes in the database
                context.Iamges.Add(model);
                context.SaveChanges();

                //getting the currunt model id in the DB after it was stored and update the metatag and save changes
                model.Metatag = "{" +
                    "\"id\": " + model.Id + "," +
                    "\"filePath\": " + path + ","
                    + "\"insertionDate\": " + date + ","
                    + "}";
                context.SaveChanges();

                return Ok(); ;
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet]
        public ActionResult<List<ImageModel>> GetImage()
        {
            var getImage = context.Iamges.ToList();
            return getImage;
        }
    }
}
