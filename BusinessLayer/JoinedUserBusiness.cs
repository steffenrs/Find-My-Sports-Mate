using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using DataAccessLayer;

namespace BusinessLayer
{
    public class JoinedUserBusiness
    {
        public static List<JoinedUser> Get(int suggestionId)
        {
            return JoinedUserAccess.GetForSuggestion(suggestionId);
        }

        public static string GetBestDays(int suggestionId)
        {
            List<JoinedUser> users = JoinedUserBusiness.Get(suggestionId);

            Dictionary<String, int> votesPerDay = new Dictionary<string, int>();

            foreach (var user in users)
            {
                String[] days = user.Weekdays.Split(',');
                foreach (var day in days)
                {
                    int count;
                    if (votesPerDay.TryGetValue(day, out count))
                        votesPerDay[day] = count + 1;
                    else
                        votesPerDay.Add(day, 1);
                }
            }

            int highest = votesPerDay.Max(kvp => kvp.Value);
            var values = "";

            foreach (var kvp in votesPerDay)
            {
                if (kvp.Value == highest)
                    values += kvp.Key + ", ";
            }

            if (values.Length >= 2)
                values = values.Remove(values.Length - 2, 2);

            return values;
        }
    }
}
