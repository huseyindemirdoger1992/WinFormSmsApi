using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WinFormSmsApi.Models
{
    internal class SmsApiService
    {
        public string XMLPOST(string PostAddress, string xmlData)
        {
            try
            {
                var res = "";
                byte[] bytes = Encoding.UTF8.GetBytes(xmlData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(PostAddress);
                request.Method = "POST";
                request.ContentLength = bytes.Length;
                request.ContentType = "text/xml";
                request.Timeout = 300000000;
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        string message = String.Format(
                        "POST failed. Received HTTP {0}",
                        response.StatusCode);
                        throw new ApplicationException(message);
                    }
                    Stream responseStream = response.GetResponseStream();
                    using (StreamReader rdr = new StreamReader(responseStream))
                    {
                        res = rdr.ReadToEnd();
                    }
                    return res;
                }
            }
            catch
            {
                return "-1";
            }
        }

        public void SmsSender(string TelNo, string SmsText)
        {
            //Aşağıdaki açıklamasını yaptığım 3 alan dolu olmalı. Aksi halde servis sağlayıcı hizmeti alamazsınız.
            //Benimle iletişime geçmek için https://memleketyazilim.com/iletisim/ adresini kullanabilirsiniz.
            //Hizmetten yararlanmak isterseniz url: https://www.iletimerkezi.com/

            String testXml = "<request>";
            testXml += "<authentication>";
            testXml += "<username></username>";// ileti merkezi kullanııc adınız
            testXml += "<password></password>";// ileti merkezi kullanıcı şifreniz
            testXml += "</authentication>";
            testXml += "<order>";
            testXml += "<sender></sender>";// Size ait olan sms başlığınız
            testXml += "<sendDateTime></sendDateTime>";
            testXml += "<message>";
            testXml += $"<text>{SmsText}</text>";
            testXml += "<receipents>";
            testXml += $"<number>{TelNo}</number>";
            testXml += "</receipents>";
            testXml += "</message>";
            testXml += "</order>";
            testXml += "</request>";
            this.XMLPOST("http://api.iletimerkezi.com/v1/send-sms", testXml);
        }
    }
}
