using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GAPS.TSC.CONS.Services;

namespace GAPS.TSC.Consillium.Controllers{
    public class AttachmentsController : BaseController{
        private readonly IAttachmentService _attachmentService;

        public AttachmentsController(IAttachmentService attachmentService) : base(attachmentService) {
            _attachmentService = attachmentService;
        }

        [Authorize]

        // GET: Attachments
        public FileResult Index(int id) {
            var attachment = _attachmentService.GetById(id);
            if (null != attachment) {
                var name = attachment.ActualName;
                byte[] fileBytes = System.IO.File.ReadAllBytes(string.Format("{0}{1}", FilePath, attachment.FileName));
                string fileName = name;
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            return null;
        }
    }
}