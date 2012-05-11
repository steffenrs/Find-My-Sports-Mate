using System;
using System.Collections.Generic;
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

            //List<Location> locations = BusinessLayer.LocationBusiness.getAll();
            List<Location> locations = new List<Location>();

            foreach(Location location in locations){
                foreach(JoinedUser joinedUser in suggestion.JoinedUsers)
                {
                    User user = BusinessLayer.UserBusiness.GetUserById(joinedUser.UserId);
                    getDistanceBetweenUserAddressAndLocation(user, location);
                }
            }


            

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


        public static 

    }
}
