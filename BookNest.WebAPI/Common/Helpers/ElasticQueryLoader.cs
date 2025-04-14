using System.Text.RegularExpressions;

namespace BookNest.WebAPI.Common.Helpers
{
    public class ElasticQueryLoader
    {
        private const string QueryFolder = "ElasticQueries";

        public static string LoadTemplate(string fileName,Dictionary<string,string> parameters)
        {
            if (parameters == null || !parameters.Any())
                throw new ArgumentException("No parameters provided to the query.");

            fileName = fileName + ".json";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), QueryFolder, fileName);

            if(!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Query template not found {filePath}");
            }

            var queryTemplate = File.ReadAllText(filePath);

            foreach(var kvp in  parameters)
            {
                queryTemplate = Regex.Replace(queryTemplate, $"{{{{{kvp.Key}}}}}", kvp.Value, RegexOptions.IgnoreCase);
            }
            queryTemplate = Regex.Replace(queryTemplate, @"\{\{\s*\w+\s*\}\}", string.Empty);

            return queryTemplate;
        }
    }
}
