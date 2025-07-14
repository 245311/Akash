using System.Text;

namespace WBS_API.Infrastructure
{
    public class Base64EncodeDecode
    {
        public Base64EncodeDecode()
        {
        }
        public static string EncodeString(string data)
        {
            string result = string.Empty;
            try
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(data);
                var plainTextBytes1 = System.Text.Encoding.UTF8.GetBytes(Convert.ToBase64String(plainTextBytes));
                result = Convert.ToBase64String(plainTextBytes1);
            }
            catch (Exception ex)
            {
                // //Logger.Log(Convert.ToString(ex.Message) + ",ClassName : Base64EncodeDecode | MethodName: EncodeString()", "", Convert.ToInt32(Logtype.Error));
            }
            return result;

        }

        public static string DecodeString(string data)
        {
            string result = string.Empty;

            try
            {
                var plainTextBytes = System.Convert.FromBase64String(data);
                var plainTextBytes1 = System.Convert.FromBase64String(Encoding.UTF8.GetString(plainTextBytes));
                result = System.Text.Encoding.UTF8.GetString(plainTextBytes1);

            }
            catch (Exception ex)
            {
                // //Logger.Log(Convert.ToString(ex.Message) + ",ClassName : Base64EncodeDecode | MethodName: DecodeString()", "", Convert.ToInt32(Logtype.Error));
            }
            return result;
        }

    }
}
