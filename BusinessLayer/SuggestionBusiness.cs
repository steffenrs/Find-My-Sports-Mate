using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

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
        public static Suggestion GetById(int suggestionId)
        {
            return DataAccessLayer.SuggestionAccess.Get(suggestionId);
        }
        public static void Update(Suggestion suggestion, int userId)
        {
            ValidateUserAccess(suggestion.Id, userId);
            DataAccessLayer.SuggestionAccess.Update(suggestion);
        }
        public static void Delete(int suggestionId, int userId)
        {
            ValidateUserAccess(suggestionId, userId);
            DataAccessLayer.SuggestionAccess.Delete(suggestionId);
        }

        public static List<Suggestion> GetAll()
        {
            return DataAccessLayer.SuggestionAccess.GetAll();
        }

        public static void Open(int id, int userId)
        {
            ValidateUserAccess(id, userId);
            Suggestion suggestion = GetById(id);
            if (suggestion.JoinedUsers.Count >= suggestion.MaximumUsers)
                return;

            suggestion.IsClosed = false;
            DataAccessLayer.SuggestionAccess.Update(suggestion);
        }

        public static void Close(int id, int userId)
        {
            ValidateUserAccess(id, userId);
            Suggestion suggestion = GetById(id);
            suggestion.LocationId = LocationBusiness.NearestLocationForUsers(suggestion).Id;
            suggestion.IsClosed = true;
            SuggestionBusiness.Update(suggestion, userId);
        }

        private static void ValidateUserAccess(int id, int userId)
        {
            Suggestion suggestion = DataAccessLayer.SuggestionAccess.Get(id);
            if (userId != suggestion.CreatorId)
            {
                throw new DomainException("User is not authorized to edit the suggestion.");
            }
        }

        public static List<Suggestion> GetByUser(int userId)
        {
            return DataAccessLayer.SuggestionAccess.GetByJoinedUser(userId);
        }

        public static List<Suggestion> GetByCreator(int userId)
        {
            return DataAccessLayer.SuggestionAccess.GetAllByCreator(userId);
        }

        public static bool JoinSuggestion(int userId, int suggestionId, string weekdays) 
        {
            Suggestion suggestion = SuggestionBusiness.GetById(suggestionId);
            if (!suggestion.IsClosed && suggestion.JoinedUsers.Count < suggestion.MaximumUsers && suggestion.JoinedUsers.FindAll(j => j.UserId == userId).Count == 0)
            {
                var joinedUser = new JoinedUser { UserId = userId, SuggestionId = suggestionId, Weekdays = weekdays };
                suggestion.JoinedUsers.Add(joinedUser);
                DataAccessLayer.JoinedUserAccess.Add(joinedUser);

                if (suggestion.JoinedUsers.Count >= suggestion.MinimumUsers)
                {
                    suggestion.Location = LocationBusiness.NearestLocationForUsers(suggestion);
                    
                    if (suggestion.JoinedUsers.Count == suggestion.MaximumUsers)
                    {
                        suggestion.IsClosed = true;
                    }

                    SuggestionBusiness.Update(suggestion, suggestion.CreatorId);
                }
                return true;
            }
            else 
                return false;
        }
    }
}
