using DocumentFormat.OpenXml.Packaging;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WBS_API.DAL;
using WBS_API.Interface;
using WBS_API.Model;
using WBS_API.Utility;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using Newtonsoft.Json.Linq;



namespace WBS_API.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class UploadFileandRemarkController : ControllerBase
    {
        private readonly DBInsert dBInsert;
        private readonly ICommonRepository commonRepository;
        private readonly IRequestResponseLogRepository requestResponseLogRepository;

        public UploadFileandRemarkController(DBInsert dBInsert, ICommonRepository commonRepository, IRequestResponseLogRepository requestResponseLogRepository)
        {

            this.dBInsert = dBInsert;
            this.commonRepository = commonRepository;
            this.requestResponseLogRepository = requestResponseLogRepository;
        }
        string reqDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        [HttpPost("UploadAndRemark")]

        public async Task<object> UploadAndRemark([FromForm] IFormFile uploadedFile, [FromForm] string remarks, int userid,  int taskid, int tasktypeid)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "File or Remarks required." };
            
            string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

            try
            {
                if ((uploadedFile == null || uploadedFile.Length == 0) && string.IsNullOrWhiteSpace(remarks))
                {
                    returnResponse.ResponseMessage = "Please provide either a file or remarks.";
                    return returnResponse;
                }

                string storedFilePath = null;

                if (uploadedFile != null && uploadedFile.Length > 0)
                {
                    string[] allowedExtensions = { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".csv" };
                    string fileExtension = Path.GetExtension(uploadedFile.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        returnResponse.ResponseMessage = "Invalid file type. Allowed types: pdf, doc, docx, xls, xlsx, csv.";
                        return returnResponse;
                    }

                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    string fileName = $"{Guid.NewGuid()}{fileExtension}";
                    storedFilePath = Path.Combine(uploadFolder, fileName);

                    using (var stream = new FileStream(storedFilePath, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(stream);
                    }
                    // ➤ Validate the file content
                    bool isValid = ValidateFileStructure(fileExtension, storedFilePath);
                    if (!isValid)
                    {
                        returnResponse.ResponseMessage = "Required fields are missing in the uploaded file.";
                        return returnResponse;
                    }
                }

                var jsonRequest = new
                {
                    userid = userid, // Hardcoded or pass from token
                    taskid = taskid,   // Pass from UI
                    tasktypeid = tasktypeid,
                    filepath = storedFilePath ?? string.Empty,
                    remarks = remarks ?? string.Empty
                };

                string jsonRequestString = JsonConvert.SerializeObject(jsonRequest);
                returnResponse = commonRepository.CommonRepositoryAsync(jsonRequestString, "fun_process_taskdoc_or_remark");
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "UploadFileandRemarkController",
                    sourcepagemethod = "UploadandRemark",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = uploadedFile?.FileName ?? "No File",
                    errortype = "Controller"
                };

                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong while processing.";
            }
            finally
            {
                RequestResponseLog requestResponseLog = new RequestResponseLog
                {
                    request = JsonConvert.SerializeObject(new {  fileName = uploadedFile?.FileName ?? "", remarks }),
                    response = JsonConvert.SerializeObject(returnResponse),
                    participantid = "",
                    reqtype = "UploadandRemark",
                    reqdate = reqDate,
                    rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };

                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }

        [HttpPost("SaveRemark")]
        public async Task<object> SaveRemark(string remarks, int userid, int taskid)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Remarks required." };

            try
            {
                // Ensure remarks is provided
                if (string.IsNullOrWhiteSpace(remarks))
                {
                    returnResponse.ResponseMessage = "Please provide remarks.";
                    return returnResponse;
                }

                // Create JSON request
                var jsonRequest = new
                {
                    userid = userid,
                    taskid = taskid,
                    remarks = remarks ?? string.Empty
                };

                string jsonRequestString = JsonConvert.SerializeObject(jsonRequest);
                returnResponse = commonRepository.CommonRepositoryAsync1(jsonRequestString, "fun_saveremark");
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "UploadFileandRemarkController",
                    sourcepagemethod = "SaveRemark",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = remarks ?? "remarks",
                    errortype = "Controller"
                };

                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong while processing.";
            }
            finally
            {
                RequestResponseLog requestResponseLog = new RequestResponseLog
                {
                    request = JsonConvert.SerializeObject(new { remarks =  remarks }),
                    response = JsonConvert.SerializeObject(returnResponse),
                    participantid = "",
                    reqtype = "SaveRemark",
                    reqdate = reqDate,
                    rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };

                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }

        [HttpPost("GetRemark")]
        public async Task<object> GetRemark(int taskid)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "taskid is required." };

            try
            {
                var jsonRequest = new
                {
                  taskid = taskid,
                   
                };

                string jsonRequestString = JsonConvert.SerializeObject(jsonRequest);
                returnResponse = commonRepository.CommonRepositoryAsync1(jsonRequestString, "fun_getremarksbytaskid_api");
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "UploadFileandRemarkController",
                    sourcepagemethod = "GetRemark",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = "taskid",
                    errortype = "Controller"
                };

                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong while processing.";
            }
            finally
            {
                RequestResponseLog requestResponseLog = new RequestResponseLog
                {
                    request = JsonConvert.SerializeObject(new { taskid = taskid }),
                    response = JsonConvert.SerializeObject(returnResponse),
                    participantid = "",
                    reqtype = "GetRemark",
                    reqdate = reqDate,
                    rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };

                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }

        private bool ValidateFileStructure(string extension, string filePath)
        {
            //string[] requiredHeaders = { "taskname", "tasktypeid", "assignto", "startdate", "enddate", "estimatedtime" };
            string[] requiredHeaders = {  };

            try
            {
                if (extension == ".csv")
                {
                    var lines = System.IO.File.ReadAllLines(filePath);
                    var headers = lines[0].Split(',').Select(h => h.Trim().ToLower()).ToArray();
                    return requiredHeaders.All(r => headers.Contains(r));
                }
                else if (extension == ".xls" || extension == ".xlsx")
                {
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                    using (var stream =System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                 
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var dataset = reader.AsDataSet();
                        var table = dataset.Tables[0];
                        var headers = table.Rows[0].ItemArray.Select(x => x.ToString().Trim().ToLower()).ToArray();
                        return requiredHeaders.All(r => headers.Contains(r));
                    }
                }
                //else if (extension == ".pdf")
                //{
                //    // PDF validation using PdfPig
                //    using (var pdf = PdfPig.PdfDocument.Open(filePath))
                //    {
                //        var text = string.Join(" ", pdf.GetPages().Select(p => p.Text.ToLower()));
                //        return requiredHeaders.All(header => text.Contains(header));
                //    }
                //}
                else if (extension == ".docx")
                {
                    using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(filePath, false))
                    {
                        var body = wordDoc.MainDocumentPart.Document.Body.InnerText.ToLower();
                        return requiredHeaders.All(header => body.Contains(header));
                    }
                }
                else if (extension == ".doc")
                {
                    // Legacy .doc format support requires Interop or third-party tools.
                    // Skipping .doc for now, or show a message to convert to .docx
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }
        public class UploadandRemark
        {
            public string remarks { get; set; }
            public IFormFile uploadedFile { get; set; }
        }

    }
}
