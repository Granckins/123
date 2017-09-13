using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WarehouseDB.Models;

namespace WarehouseDB.Controllers
{
    public class UploadController : Controller
    {
        //
        // GET: /Upload/

      
        [HttpPost]
        public FineUploaderResult UploadFile(FineUpload upload, string extraParam1 = null, int extraParam2 = 0)
        {
            // asp.net mvc will set extraParam1 and extraParam2 from the params object passed by Fine-Uploader

            var dir = Server.MapPath("../Content/images");

            var filePath = Path.Combine(dir, upload.Filename);

            try
            {

                upload.SaveAs(filePath);


                return new FineUploaderResult(true, new { FilePath = filePath });
            }
            catch (Exception ex)
            {
                return new FineUploaderResult(false, error: ex.Message);
            }

            // the anonymous object in the result below will be convert to json and set back to the browser
            return new FineUploaderResult(true, new { extraInformation = 12345 });
        }
    }
}
