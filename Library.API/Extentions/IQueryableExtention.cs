using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Library.API.Configs;

namespace Library.API.Extentions;

public static class IQueryableExtention
{
    private const string OrderSequence_Asc = "asc";
    private const string OrderSequence_Desc = "desc";

    public static IQueryable<T> Sort<T>(this IQueryable<T> source, string orderBy,
        Dictionary<string, PropertyMapping> mapping) where T : class
    {
        var allQueryParts = orderBy.Split(',');
        var sortParts = new List<string>();
        foreach (var item in allQueryParts)
        {
            var isDescending = false;
            string property;
            if (item.ToLower().EndsWith(OrderSequence_Desc))
            {
                property = item.Substring(0, item.Length - OrderSequence_Desc.Length).Trim();
                isDescending = true;
            }
            else
            {
                property = item.Trim();
            }

            if (mapping.ContainsKey(property))
            {
                if (mapping[property].IsRevert) isDescending = !isDescending;
                if (isDescending)
                    sortParts.Add($"{mapping[property].TargetProperty} {OrderSequence_Desc}");
                else
                    sortParts.Add($"{mapping[property].TargetProperty} {OrderSequence_Asc}");
            }
        }

        var finalExpression = string.Join(',', sortParts);
        source = source.OrderBy(finalExpression);
        return source;
    }
}