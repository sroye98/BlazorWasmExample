using System;
using System.Collections.Generic;

namespace BusinessLogic.Helpers
{
    public static class Utilities
    {
        public static string ParseTemplate(
            string content,
            Dictionary<string, string> variables)
        {
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }

            if (variables == null)
            {
                variables = new Dictionary<string, string>();
            }

            foreach (KeyValuePair<string, string> variable in variables)
            {
                content = content.Replace(
                    $"[[{variable.Key.ToUpper()}]]",
                    variable.Value);
            }

            return content;
        }

        public static Dictionary<string, string> AddParseVariables(
            Dictionary<string, string> currentVariables,
            KeyValuePair<string, string> newVariable)
        {
            Dictionary<string, string> retval = currentVariables
                ?? new Dictionary<string, string>();

            if (!retval.ContainsKey(newVariable.Key))
            {
                retval.Add(
                    newVariable.Key,
                    newVariable.Value);
            }
            else
            {
                retval[newVariable.Key] = newVariable.Value;
            }

            return retval;
        }
    }
}
