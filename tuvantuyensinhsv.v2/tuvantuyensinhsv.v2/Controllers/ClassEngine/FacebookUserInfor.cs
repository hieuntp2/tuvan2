using Facebook;
using System;
using System.Collections.Generic;

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

        public FBUserInforReturn getEmail(string username)
        {
            //dynamic user = fbclient.Get(username);
            //return user.email;
            var me = (IDictionary<string, object>)fbclient.Get("https://graph.facebook.com/" + username + "?fields=name,birthday,education,picture.type(large)");
            FBUserInforReturn user = new FBUserInforReturn();

            
            user.name = me["name"].ToString();

            var picture_data = (IDictionary<string, object>)me["picture"];
            var picture_url = (IDictionary<string, object>)picture_data["data"];
            user.picture = picture_url["url"].ToString();
            return user;
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

    public class FBUserInforReturn
    {
        public string name;
        public DateTime birthday;
        public string picture;
        public string university_name;
        public string major_name;
    }
}