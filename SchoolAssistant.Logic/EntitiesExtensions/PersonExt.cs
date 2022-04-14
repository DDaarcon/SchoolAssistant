﻿using SchoolAssistant.DAL.Interfaces;

namespace SchoolAssistant.Logic
{
    public static class PersonExt
    {
        public static string GetFullName(this IPerson person)
        {
            return $"{person.LastName} {person.FirstName}";
        }
    }
}