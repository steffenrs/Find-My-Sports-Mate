using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using DataAccessLayer;

namespace BusinessLayer
{
    public static class SuggestionBusiness
    {
        public static void Create(Suggestion suggestion, string weekdays)
        {
            JoinedUser u = new JoinedUser
            {
                UserId = suggestion.CreatorId,
                Weekdays = weekdays
            };

            suggestion.JoinedUsers.Add(u);

            DataAccessLayer.SuggestionAccess.Create(suggestion);
        }

        public static void Join(int userId, int suggestionId, string weekdays)
        {
            Suggestion suggestion = SuggestionBusiness.Get(suggestionId);

            // Join if not closed, not full and not already joined
            if (!suggestion.IsClosed && suggestion.JoinedUsers.Count < suggestion.MaximumUsers && HasUserJoined(userId, suggestion))
            {
                var joinedUser = new JoinedUser { UserId = userId, SuggestionId = suggestionId, Weekdays = weekdays };
                suggestion.JoinedUsers.Add(joinedUser);
                DataAccessLayer.JoinedUserAccess.Add(joinedUser);

                // Find a location and playdays if min users are set
                if (suggestion.JoinedUsers.Count >= suggestion.MinimumUsers)
                {
                    suggestion.MostPopularDays = SuggestionBusiness.GetBestDays(suggestion.Id);
                    suggestion.LocationId = LocationBusiness.Calculate(suggestion).Id;

                    // Close suggestion if max users reached
                    if (suggestion.JoinedUsers.Count == suggestion.MaximumUsers)
                    {
                        suggestion.IsClosed = true;
                    }

                    SuggestionBusiness.Update(suggestion, suggestion.CreatorId);
                }
            }
        }

        public static void Open(int id, int userId)
        {
            ValidateUserAccess(id, userId);
            Suggestion suggestion = Get(id);
            if (suggestion.JoinedUsers.Count >= suggestion.MaximumUsers)
                return;

            suggestion.IsClosed = false;
            DataAccessLayer.SuggestionAccess.Update(suggestion);
        }

        public static void Close(int id, int userId)
        {
            ValidateUserAccess(id, userId);
            Suggestion suggestion = Get(id);
            suggestion.LocationId = LocationBusiness.Calculate(suggestion).Id;
            suggestion.IsClosed = true;
            SuggestionBusiness.Update(suggestion, userId);
        }

       
        public static void Update(Suggestion suggestion, int userId)
        {
            ValidateUserAccess(suggestion.Id, userId);
            DataAccessLayer.SuggestionAccess.Update(suggestion);
        }

        public static List<Suggestion> GetAll()
        {
            return DataAccessLayer.SuggestionAccess.GetAll();
        }

        public static Suggestion Get(int suggestionId)
        {
            return DataAccessLayer.SuggestionAccess.Get(suggestionId);
        }

        public static List<Suggestion> GetByUser(int userId)
        {
            return DataAccessLayer.SuggestionAccess.GetByJoinedUser(userId);
        }

        public static List<Suggestion> GetByCreator(int userId)
        {
            return DataAccessLayer.SuggestionAccess.GetAllByCreator(userId);
        }

        public static List<JoinedUser> GetJoinedUsers(int suggestionId)
        {
            return JoinedUserAccess.GetForSuggestion(suggestionId);
        }
        public static string GetBestDays(int suggestionId)
        {
            List<JoinedUser> users = GetJoinedUsers(suggestionId);

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

        private static void ValidateUserAccess(int id, int userId)
        {
            Suggestion suggestion = DataAccessLayer.SuggestionAccess.Get(id);
            if (userId != suggestion.CreatorId)
            {
                throw new DomainException("User is not authorized to edit the suggestion.");
            }
        }
    
        private static bool HasUserJoined(int userId, Suggestion suggestion)
        {
            return suggestion.JoinedUsers.FindAll(j => j.UserId == userId).Count == 0;
        }
    }
}