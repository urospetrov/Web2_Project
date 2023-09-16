using Google.Apis.Auth.OAuth2;
using Microsoft.VisualBasic;
using System.IO;


using Google.Apis.Services;
using Google.Apis.Gmail.v1;

namespace WEB2_Projekat.EmailServices
{
    public class GmailApiHelper
    {
        public GmailService GetGmailService(string credentialsPath)
        {
            GoogleCredential credential;
            using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(GmailService.Scope.GmailSend);
            }

            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Web2-projekat"
            });

            return service;
        }



    }
}
