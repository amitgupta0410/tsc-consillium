using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Services;

namespace GAPS.TSC.Consillium.Controllers
{
    public class BaseController : Controller{
        private readonly IAttachmentService _attachmentService;
        protected static readonly string FilePath = ConfigurationManager.AppSettings["Uploads"];
        //
        // GET: /Base/
        public BaseController(IAttachmentService attachmentService) {
            _attachmentService = attachmentService;
        }

       
        protected void SetMessage(MessageType messageType, string message)
        {
            TempData["Message"] = message;
            TempData["MessageType"] = messageType;
        }

        protected Attachment UploadAndSave(string fileName) {
            var file = Request.Files[fileName];

            if (file == null) return null;

            var origName = file.FileName;
            var extension = Path.GetExtension(file.FileName);
            var newName = string.Format("{0}{1}", Guid.NewGuid(), extension);
 
            if (file.ContentLength > 0)
            {
               
                file.SaveAs(string.Format("{0}{1}",FilePath,newName));
                return _attachmentService.SaveAttachment(origName, newName);

            }
            return null;

        }
       
	}
}