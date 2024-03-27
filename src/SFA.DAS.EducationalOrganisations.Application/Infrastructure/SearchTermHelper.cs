using System.Text.RegularExpressions;

namespace SFA.DAS.EducationalOrganisations.Application.Infrastructure
{
    public static class SearchTermHelper
    {
        public static bool IsSearchTermAReference(string searchTerm, TimeSpan timeout)
        {
            try
            {
                return Regex.IsMatch(searchTerm, @"^[124]\d{4,5}$", RegexOptions.None, timeout);
            }
            catch
            {
                return false;
            }
        }
    }
}
