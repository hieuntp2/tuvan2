using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tuvantuyensinhsv.v2.Controllers.ClassEngine
{
    public class FacebookUserInfor
    {
        private static string appid = "1502475853349939";
        private static string key = "cbf3a4270af849724207f40bf5d49892";
        FacebookClient fbclient;

        public FacebookUserInfor()
        {
            fbclient = new FacebookClient();
            dynamic result = fbclient.Get("oauth/access_token", new
            {
                client_id = appid,
                client_secret = key,
                grant_type = "client_credentials"
            });
            fbclient.AccessToken = result.access_token;
        }
        public string getLinkProfilePicture(string username)
        {
            return "";
        }

        public string getEmail(string username)
        {
            dynamic user = fbclient.Get(username);
            return user.email;
        }
        public string getFullName(string username)
        {
            return "";
        }
        public string getAccessToken()
        {
            return fbclient.AccessToken;
        }


    }
}