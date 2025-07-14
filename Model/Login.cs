using System.ComponentModel.DataAnnotations;

namespace WBS_API.Model
{
    public class Login
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class ErrorLog
    {
        public string user_id { get; set; }
        public string sourcepage { get; set; }
        public string sourcepagemethod { get; set; }
        public string message { get; set; }
        public string stacktrace { get; set; }
        public string param { get; set; }
        public string errortype { get; set; }
        public string checkedcomment { get; set; }
        public string checkedby { get; set; }
    }

    public class ReturnResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public object RspData { get; set; }
        public object RspJson { get; set; }
        public string rsplist { get; set; }


    }

    public class ReturnResponseDownloadAPI
    {
        public string RspCode { get; set; }
        public string RspMessage { get; set; }
        public string Data { get; set; }
        public string ApkFileName { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string RspMsg { get; set; }

    }

    public class RequestResponseLog
    {
        public string request { get; set; }
        public string response { get; set; }
        public string serial_no { get; set; }
        //START:03APRIL2023:ADD PROPERTY TO STORE REQUEST RESPONSE
        public string participantid { get; set; }
        public string reqtype { get; set; }
        public string rspdate { get; set; }
        public string reqdate { get; set; }
        //END:03APRIL2023:ADD PROPERTY TO STORE REQUEST RESPONSE
    }
}
