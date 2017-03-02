using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SearchGoogle
{
    class Program
    {
        static void Main(string[] args)
        {
            /* apikey with searchEngineID is setup when create google custom key
             * U can create more than one searchEngineId
             */
            const string apiKey = "AIzaSyCUqfT2QmAZA4668MPLGzLR0TxGOmqAvoM";
            string[] searchEngine = { "014643630663003986242:kxn6ai0cdom","014643630663003986242:7vybp9qqc4y",
                                      "014643630663003986242:bbkgkcu0nzw","014643630663003986242:2xzxd8s8pbu",
                                      "014643630663003986242:n9qaxhzeukq","014643630663003986242:fbdjtqdrwsy",
                                      "014643630663003986242:apoeesjvmq4","014643630663003986242:ry__xhewmma",
                                      "014643630663003986242:hqmstvd27xi","014643630663003986242:xyjlkndt-68",
                                      "014643630663003986242:pga9pplj_5w","014643630663003986242:bbx5ubgnoti",
                                      "014643630663003986242:traumvifdgc","014643630663003986242:ld_-gigmcbu",
                                      "014643630663003986242:edd7oeqtf50","014643630663003986242:p8hvgjehmt4",
                                      "014643630663003986242:aegk6ogs0em","014643630663003986242:a0skashuy6w",
                                      "014643630663003986242:iqbhww3mgly","014643630663003986242:maelom8dwwm",
                                      "014643630663003986242:zvrnazoxz-w","014643630663003986242:oy1nlwhwaac"};
                                                                            
            const string query = "developer";
            string searchEngineId;
            string body = "";
            int counter = 1;

            for (var i = 0; i < searchEngine.Count(); i++)
            {

                searchEngineId = searchEngine[i];
                CustomsearchService customSearchService = new CustomsearchService(new Google.Apis.Services.BaseClientService.Initializer() { ApiKey = apiKey });

                Google.Apis.Customsearch.v1.CseResource.ListRequest listRequest = customSearchService.Cse.List(query);

                listRequest.Cx = searchEngineId;
                //  listRequest.ExactTerms = query;

                Search search = listRequest.Execute();







                foreach (var item in search.Items)
                {
                    //prepare the body for email sent
                    body = body + counter + " Title : " + item.Title + Environment.NewLine + "Link : " + item.Link + Environment.NewLine + Environment.NewLine;
                    //sent the output to console screen

                    //    Console.WriteLine(counter +" Title : " + item.Title + Environment.NewLine + "Link : " + item.Link + Environment.NewLine + Environment.NewLine);
                    counter++;
                }
                //   Console.ReadLine();






            } //end of for loop


            /*
            * The results above and be used to result to send a Email
           */
            counter = counter - 1;

            var fromAddress = new MailAddress("jkanimea@gmail.com", "SystemSearch");
            var toAddress = new MailAddress("jkanimea@gmail.com", "Developer search");
            const string fromPassword = "QTPvz766@";

            string subject = counter.ToString() + " Developer Search Results";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
