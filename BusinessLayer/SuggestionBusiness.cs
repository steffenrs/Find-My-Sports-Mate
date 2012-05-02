﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using DataAccessLayer;

namespace BusinessLayer
{
    public static class SuggestionBusiness
    {
        static Suggestion suggestion = new Suggestion
        {
            Title = "Volley in winter?",
            Id = 0,
            Creator = new Domain.User { FirstName = "Frank the tank" },
            IsClosed = false,
            Description = "Keen på å spille volleyball eller?",
            Sport = new Sport { Name = "Volleyball" },
            MinimumUsers = 5,
            MaximumUsers = 10,
            StartDate = new DateTime(2012, 5, 5),
            EndDate = new DateTime(2012, 5, 15),
            Location = new Location { Name = "Helvete" },
            JoinedUsers = new List<JoinedUser> { new JoinedUser { User = new User { FirstName = "PETAH" } } }
        };

        static Suggestion suggestion2 = new Suggestion
        {
            Title = "Football in winter?",
            Id = 1,
            Creator = new Domain.User { FirstName = "Frank the ball" },
            IsClosed = false,
            Description = "Keen på å spille Football eller?",
            Sport = new Sport { Name = "Football" },
            MinimumUsers = 5,
            MaximumUsers = 10,
            StartDate = new DateTime(2012, 5, 5),
            EndDate = new DateTime(2012, 5, 15),
            Location = new Location { Name = "Footiete" },
            JoinedUsers = new List<JoinedUser> { new JoinedUser { User = new User { FirstName = "STUBBEN" } } }
        };

        static Suggestion suggestion3 = new Suggestion
        {
            Title = "Volley in summer?",
            Id = 2,
            Creator = new Domain.User { FirstName = "B aks" },
            IsClosed = true,
            Description = "Keen på å spille volleyball eller?",
            Sport = new Sport { Name = "Volleyball" },
            MinimumUsers = 5,
            MaximumUsers = 10,
            StartDate = new DateTime(2012, 5, 5),
            EndDate = new DateTime(2012, 5, 15),
            Location = new Location { Name = "Bak deg" },
            JoinedUsers = new List<JoinedUser> { new JoinedUser { User = new User { FirstName = "han andre" } } }
        };

        private static List<Suggestion> temp = new List<Suggestion>()
        {
            suggestion,
            suggestion2,
            suggestion3
        };

        public static void New(Suggestion suggestion)
        {
            throw new DomainException("Database not implemented for 'Suggestion'");
            SuggestionAccess.CreateSuggestion(suggestion);
        }

        public static Suggestion Get(int id)
        {
            return temp.Single(t => t.Id == id);
        }

        public static List<Suggestion> GetAll()
        {
            return temp;
        }

        public static List<Suggestion> GetByUser(int userId)
        {
            return new List<Suggestion>();
        }
    }
}
