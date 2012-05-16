using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using BusinessLayer;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;

namespace BusinessLayer
{
    public class LocationBusiness
    {
        private static string API_KEY = "";

        /// <summary>
        /// Finds the nearest location for all users that have joined a suggestion
        /// </summary>
        /// <param name="suggestion">A suggestion</param>
        /// <returns>The specific location</returns>
        public static Location nearestLocationForUsers(Suggestion suggestion)
        {
            List<Location> locations = DataAccessLayer.LocationAccess.getAll();
            Dictionary<User,List<Element>> distanceElements = new Dictionary<User,List<Element>>();
            
            /// generate a string of stationary locations that fit the Google Distance API
            string origins = null;
            foreach (Location location in locations)
            {
                origins += location.Latitude + "," + location.Longitude + "|";
            }

            foreach (JoinedUser joinedUser in suggestion.JoinedUsers)
            {
                User user = UserBusiness.GetUserById(joinedUser.UserId);
                distanceElements.Add(user,getDistanceBetweenUserAddressAndLocations(user, origins));
            }

            int[] distances = new int[locations.Count];

            foreach(KeyValuePair<User, List<Element>> item in distanceElements)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    distances[i] += item.Value[i].distance.value;
                }
            }

            int index = Array.IndexOf(distances, distances.Min());
            return locations[index];
        }

        /// <summary>
        /// Gets the distance between user address and stationary locations by means of the Google Distance API.
        /// </summary>
        /// <param name="user">A specific user</param>
        /// <param name="origins">The stationary locations parsed to a Google Distance API string</param>
        /// <returns>List of elements</returns>
        public static List<Element> getDistanceBetweenUserAddressAndLocations(User user, string origins)
        {
            string address = user.StreetAddress + "+" + user.Area + "+" + user.State;
            var url = "http://maps.googleapis.com/maps/api/distancematrix/json?origins=" + origins +
                        "&destinations=" + address +
                        "&mode=driving&language=en&sensor=false";

            var request = WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var reader = new StreamReader(response.GetResponseStream());
                JavaScriptSerializer js = new JavaScriptSerializer();
                var objText = reader.ReadToEnd();
                var elements = js.Deserialize<RootObject>(objText);
                var el = elements.rows[0];

                return (from e in elements.rows select e.elements[0]).ToList();
            }

            //TODO: handle http error message
            return null;
        }





    }
}
