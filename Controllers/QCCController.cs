using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using WBS_API.DAL;
using WBS_API.Interface;
using WBS_API.Model;
using System.Net.Mail;
using System.Net;

namespace WBS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QCCController : ControllerBase
    {
        private readonly DBInsert dBInsert;
        private readonly ICommonRepository commonRepository;
        private readonly IRequestResponseLogRepository requestResponseLogRepository;

        public QCCController(DBInsert dBInsert, ICommonRepository commonRepository, IRequestResponseLogRepository requestResponseLogRepository)
        {

            this.dBInsert = dBInsert;
            this.commonRepository = commonRepository;
            this.requestResponseLogRepository = requestResponseLogRepository;
        }
        string reqDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        [HttpPost("QCC")]

        public async Task<object> QCC(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };

            try
            {
                string mode = jsonrequest["mode"]?.ToString();
                
                    returnResponse = commonRepository.CommonRepositoryAsync(JsonConvert.SerializeObject(jsonrequest), "fun_check_task_status_and_docs");
                if (returnResponse.ResponseCode == "00" && mode == "assign")
                {
                    // Extract data if needed from request or response
                    var rspDataObj = returnResponse.RspData as JObject;
                    string toEmail = rspDataObj?["emailid"]?.ToString(); // Ideally dynamic
                    //string toEmail ="abhishekpal9022@gmail.com"; // Ideally dynamic
                    string subject = "Task Assigned for Testing - QCC";
                    string body = $"<p>Dear Team,</p><p>You have been assigned a new task for Testing. Please find attached below document to download unit testing document for your reference .</p>";

                    // ✅ Use the dynamic document path
                    string documentPath = rspDataObj?["latest_docpath"]?.ToString();


                    // Call mail
                    MailService mailService = new MailService();
                    mailService.SendQccSuccessEmail(toEmail, subject, body, documentPath);
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "QCCController",
                    sourcepagemethod = "QCC",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonrequest.ToString(),
                    errortype = "Controller",
                    checkedcomment = "",
                    checkedby = "",
                };

                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong";
            }
            finally
            {
                // Always log request and response
                //request = "File Upload: " + file.FileName,
                RequestResponseLog requestResponseLog = new RequestResponseLog();
                requestResponseLog.request = JsonConvert.SerializeObject(jsonrequest);
                requestResponseLog.response = JsonConvert.SerializeObject(returnResponse);
                requestResponseLog.participantid = "";
                requestResponseLog.reqtype = "QCC";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }



        [HttpPost("Get_QA")]

        public async Task<object> Get_QA(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };

            try
            {
                returnResponse = commonRepository.CommonRepositoryAsync(JsonConvert.SerializeObject(jsonrequest), "fun_get_QA_by_teamid");
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "ManageTaskController",
                    sourcepagemethod = "Get_QA",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonrequest.ToString(),
                    errortype = "Controller",
                    checkedcomment = "",
                    checkedby = "",
                };

                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong";
            }
            finally
            {
                // Always log request and response
                //request = "File Upload: " + file.FileName,
                RequestResponseLog requestResponseLog = new RequestResponseLog();
                requestResponseLog.request = JsonConvert.SerializeObject(jsonrequest);
                requestResponseLog.response = JsonConvert.SerializeObject(returnResponse);
                requestResponseLog.participantid = "";
                requestResponseLog.reqtype = "Get_QA";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }



    }


    public class MailService
    {
        public void SendQccSuccessEmail(string toEmail, string subject, string body, string attachmentPath)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("pala53908@gmail.com"); // Replace with actual
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                if (!string.IsNullOrEmpty(attachmentPath) && File.Exists(attachmentPath))
                {
                    Attachment attachment = new Attachment(attachmentPath);
                    mail.Attachments.Add(attachment);
                }

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)) // Configure correctly
                {
                    smtp.Credentials = new NetworkCredential("pala53908@gmail.com", "vjjulwmdborfoaqe");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                // Log mail failure
                Console.WriteLine("Mail failed: " + ex.Message);
            }
        }
    }

}
