using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.Helpers
{
    public static class WebhookChecker
    {
        public static bool IsWebhook(string url)
        {
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(Regex.Replace(url, @"\s+", ""));
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                if (myHttpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    myHttpWebResponse.Close();
                    return true;
                }
                myHttpWebResponse.Close();
            }
            catch (WebException e)
            {
            }
            catch (Exception e)
            {
            }

            return false;
        }
    }
}
