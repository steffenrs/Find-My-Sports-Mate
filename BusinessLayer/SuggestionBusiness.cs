﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace BusinessLayer
{
    public static class SuggestionBusiness
    {
        public static void Create(Suggestion suggestion)
        {
            DataAccessLayer.SuggestionAccess.Create(suggestion);
        }
        public static Suggestion GetById(int suggestionId)
        {
            return DataAccessLayer.SuggestionAccess.Get(suggestionId);
        }
        public static void Update(Suggestion suggestion, int userId)
        {
            ValidateSuggestionAccess(suggestion.Id, userId);
            DataAccessLayer.SuggestionAccess.Update(suggestion);
        }
        public static void Delete(int suggestionId, int userId)
        {
            ValidateSuggestionAccess(suggestionId, userId);
            DataAccessLayer.SuggestionAccess.Delete(suggestionId);
        }

        public static List<Suggestion> GetAll()
        {
            return DataAccessLayer.SuggestionAccess.GetAll();
        }

        public static void OpenCloseSuggestion(int suggestionId, int userId)
        {
            ValidateSuggestionAccess(suggestionId, userId);
            DataAccessLayer.SuggestionAccess.OpenClose(suggestionId);
        }

        public static void ValidateSuggestionAccess(int suggestionId, int userId)
        {
            Suggestion suggestion = DataAccessLayer.SuggestionAccess.Get(suggestionId);
            if (userId != suggestion.CreatorId)
            {
                throw new DomainException("User is not authorized to edit the suggestion.");
            }
        }


        public static List<Suggestion> GetSuggestionsByUserId(int userId)
        {
            return DataAccessLayer.SuggestionAccess.GetAllByCreatorId(userId);
        }
    }
}
