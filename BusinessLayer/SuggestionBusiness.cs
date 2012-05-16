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

        public static void ToggleOpenClose(Suggestion suggestion, User user)
        {
            ValidateUserAccess(suggestion.Id, user.Id);
            suggestion = DataAccessLayer.SuggestionAccess.OpenClose(suggestion.Id);
            if (suggestion.IsClosed == true)
            {
                //LOGIC FOR CALCULATING NEAREST POINT
            }
        }

        public static void ValidateUserAccess(int suggestionId, int userId)
        {
            Suggestion suggestion = DataAccessLayer.SuggestionAccess.Get(suggestionId);
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
                    //TODO CALCULATE NEAREST LOCATION
                    CalculateNearestLocation(suggestion);
                }
                else if (suggestion.JoinedUsers.Count == suggestion.MaximumUsers)
                {
                    DataAccessLayer.SuggestionAccess.OpenClose(suggestion.Id);
                }

                return true;
            }
            else 
                return false;
        }

        public static void CalculateNearestLocation(Suggestion suggestion)
        {
            Location nearestLocation = new Location();


            
        }
    }
}
