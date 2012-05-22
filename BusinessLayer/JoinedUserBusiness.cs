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
        public static List<JoinedUser> GetBySuggestion(int id)
        {
            return JoinedUserAccess.GetForSuggestion(id);
        }

        public static String GetMostPopularDaysForSuggestion(int id)
        {
            List<JoinedUser> jUSers = JoinedUserBusiness.GetBySuggestion(id);

            int[] weekdayVotes = new int[7];
            foreach(JoinedUser user in jUSers)
            {
                String weeklyParticipation = user.Weekdays;
                weekdayVotes = JoinedUserBusiness.castWeekdayVotes(weeklyParticipation, weekdayVotes);
            }

            StringBuilder mostPopDays = new StringBuilder();
            int highestVoteCount = -1;

            for (int pos = 0; pos < weekdayVotes.Length; pos++)
            {
                int voteCount = weekdayVotes[pos];

                if (voteCount == highestVoteCount)
                {
                    mostPopDays.Append(JoinedUserBusiness.getDayFromPosition(pos) + ", ");
                }
                else if (voteCount > highestVoteCount)
                {
                    highestVoteCount = voteCount;
                    mostPopDays.Clear();
                    mostPopDays.Append(JoinedUserBusiness.getDayFromPosition(pos) + ", ");
                }
            }

            // Removes last comma in string
            if (mostPopDays.Length >= 2)
                mostPopDays.Remove(mostPopDays.Length - 2, 2);

            return mostPopDays.ToString();   
        }

        private static int[] castWeekdayVotes(String weeklyActivity, int[] week)
        {
            String[] days = weeklyActivity.Split(',');

            foreach (String day in days)
            {
                switch (day)
                {
                    case "Mo":
                        week[0]++;
                        break;
                    case "Tu":
                        week[1]++;
                        break;
                    case "We":
                        week[2]++;
                        break;
                    case "Th":
                        week[3]++;
                        break;
                    case "Fr":
                        week[4]++;
                        break;
                    case "Sa":
                        week[5]++;
                        break;
                    case "Su":
                        week[6]++;
                        break;
                }
            }
            return week;
        }

        private static String getDayFromPosition(int pos)
        {
            switch (pos)
            {
                case 0:
                    return "Mo";
                case 1:
                    return "Tu";
                case 2:
                    return "We";
                case 3:
                    return "Th";
                case 4:
                    return "Fr";
                case 5:
                    return "Sa";
                case 6:
                    return "Su";
                default:
                    return "";
            }
        }
    }
}
