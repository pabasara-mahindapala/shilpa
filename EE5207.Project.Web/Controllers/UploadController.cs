using Abp.Domain.Repositories;
using Abp.WebApi.Controllers;
using EE5207.Project.Publications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EE5207.Project.Web.Controllers
{
    public class UploadController : AbpApiController
    {
        private readonly IRepository<Publication, Guid> _publicationRepository;

        public UploadController(IRepository<Publication, Guid> publicationRepository)

        {
            _publicationRepository = publicationRepository;

        }

        [AllowAnonymous]
        public async Task<HttpResponseMessage> Upload()
        {
            string value = "";
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {

                StreamReader reader = new StreamReader(HttpContext.Current.Request.InputStream);
                string requestFromPost = reader.ReadToEnd();

                foreach (string key in HttpContext.Current.Request.Form.AllKeys)
                {
                    value = HttpContext.Current.Request.Form[key];

                }

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];

                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        {
                            var filePath = "";

                            if (value == "1")
                            {
                                filePath = HttpContext.Current.Server.MapPath("~/App/Main/views/files/" + postedFile.FileName);
                            }

                            //


                            if (filePath != "")
                            {
                                postedFile.SaveAs(filePath);
                            }
                        }
                    }

                    var message1 = string.Format("File Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a file.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(Guid id, string key)
        {
            Console.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);

            string directorypath = System.AppDomain.CurrentDomain.BaseDirectory;


            var filepath = "";

            if (key == "1")
            {
                var @publication = _publicationRepository
               .GetAll()
               .Where(e => e.Id == id)
               .ToList().FirstOrDefault();

                filepath = directorypath + @publication.FilePath;

            }

            //

            try
            {

                await Task.Factory.StartNew(() =>
                {
                    if (filepath != null)
                        File.Delete(filepath);
                });

                return Ok(new { message = "Deleted Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest("error deleting file " + ex.GetBaseException().Message);
            }


        }

    }
}