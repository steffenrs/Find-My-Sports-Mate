using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Domain;

namespace BusinessLayer
{
    public class GMatrix
    {
        private static string API_KEY = "";

        public static Location nearestLocationForUsers(Suggestion suggestion)
        {






            

            return null;
        }







        public static int getDistanceBetweenUserAddressAndLocation(User user, Location location)
        {
            var url = "http://maps.googleapis.com/maps/api/distancematrix/json?origins=" +
                        HttpUtility.UrlEncode(address) +
                        "&destinations=" + to +
                        "&mode=driving&language=en&sensor=false";

            var request = WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {

            }
            



            return 0;
        }
    }
}
