using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TestProject.Core
{
    public class FilterRecord
    {
        public string Field { get; set; }
        public List<string> Value { get; set; }
    }

    public class SortingRecord
    {
        public string Field { get; set; }
        public string Direction { get; set; }

        public int Priority { get; set; }
    }

    public class FilterAndSortingOptions
    {
        public List<FilterRecord> Filters { get; set; }
        public List<SortingRecord> Sortings { get; set; }
    }

    public class FilterMetaRecord
    {
        public Type OperandType { get; set; }
        public Type ParametrType { get; set; }
        public string Name { get; set; }
        public Operators_between OperatorBetween { get; set; }
        public Operators OperatorName { get; set; }
        public string Path { get; set; }
        public bool IsSingleFilter { get { return OperatorBetween == Operators_between.NULL; } }
        public DefaultSorting DefaultSorting { get; set; } = DefaultSorting.asc;

        public FilterMetaRecord()
        {
        }
    }

    public enum Operators_between
    {
        Or, And, NULL
    }

    public enum Operators
    {
        Contains, Equal
    }

    public enum DefaultSorting
    {
        asc,
        desc
    }

    public class FilterList
    {
        public List<FilterMetaRecord> FilterRecordList = new List<FilterMetaRecord>();

        public FilterList()
        {
            FilterRecordList.Add(new FilterMetaRecord { OperandType = typeof(Request), ParametrType = typeof(long), Name = "Default", Path = "Id", OperatorBetween = Operators_between.NULL, OperatorName = Operators.Equal });
            FilterRecordList.Add(new FilterMetaRecord { OperandType = typeof(Request), ParametrType = typeof(long), Name = "id", Path = "Id", OperatorBetween = Operators_between.NULL, OperatorName = Operators.Equal });
            FilterRecordList.Add(new FilterMetaRecord { OperandType = typeof(Request), ParametrType = typeof(string), Name = "creator", Path = "AppUser.UserName", OperatorBetween = Operators_between.NULL, OperatorName = Operators.Contains });
            FilterRecordList.Add(new FilterMetaRecord { OperandType = typeof(Request), ParametrType = typeof(long), Name = "requestType", Path = "RequestType.Id", OperatorBetween = Operators_between.Or, OperatorName = Operators.Equal });
            FilterRecordList.Add(new FilterMetaRecord() { OperandType = typeof(Request), ParametrType = typeof(DateTime), Name = "dateCreated", Path = "DateCreated", OperatorBetween = Operators_between.NULL, OperatorName = Operators.Equal });
        }

        public List<FilterMetaRecord> GetFiltersForCurrentType(Type t)
        {
            return FilterRecordList.Where(j => j.OperandType == t).ToList();
        }
    }


    public static class Filtrator
    {
        public static FilterList list = new FilterList();


        public static IQueryable<T> Filter<T>(IQueryable<T> items, FilterAndSortingOptions options)
        {
            if (options != null)
            {
                List<FilterMetaRecord> filtersForCurrentType = list.GetFiltersForCurrentType(items.ElementType);

                foreach (var f in options.Filters)
                {
                    if (f.Value != null && f.Value.Count > 0)
                    {
                        var now_meta = filtersForCurrentType.FirstOrDefault(j => j.Name == f.Field);
                        if (now_meta != null)
                        {
                            if (!(now_meta.ParametrType != typeof(string) && (f.Value[0] == null || f.Value[0] == "")))
                            {
                                items = FilterByFilterMetaRecord(items, f, now_meta);
                            }
                        }
                    }
                }

                if (options.Sortings != null && options.Sortings.Count > 0)
                {
                    var orderList = options.Sortings.Where(j => filtersForCurrentType.FirstOrDefault(i => i.Name == j.Field) != null).OrderBy(j => j.Priority).ToList();

                    if (orderList.Count > 0)
                    {
                        var orderedItems = SortingBySortingMetaRecord<T>(items, orderList[0], filtersForCurrentType.First(j => j.Name == orderList[0].Field));

                        for (int i = 1; i < orderList.Count; i++)
                        {
                            orderedItems = ThenSortingBySortingMetaRecord<T>(orderedItems, orderList[i], filtersForCurrentType.First(j => j.Name == orderList[i].Field));
                        }

                        items = orderedItems;
                    }
                }
                else
                {
                    var defaultFilter = filtersForCurrentType.First(j => j.Name == "Default");
                    var defultSorting = new SortingRecord() { Direction = defaultFilter.DefaultSorting.ToString(), Field = "Default", Priority = 1 };
                    items = SortingBySortingMetaRecord<T>(items, defultSorting, defaultFilter);
                }
            }

            return items;
        }

        public static IOrderedQueryable<T> SortingBySortingMetaRecord<T>(IQueryable<T> items, SortingRecord sort, FilterMetaRecord meta)
        {
            var param = Expression.Parameter(typeof(T), "x");

            var propertyAccess = meta.Path.Split('.').Aggregate((Expression)param, Expression.Property);

            if (sort.Direction == "asc")
            {
                return (IOrderedQueryable<T>)items.Provider.CreateQuery(Expression.Call(typeof(Queryable), "OrderBy", new Type[] { typeof(T), meta.ParametrType }, items.Expression, Expression.Lambda(propertyAccess, param)));
            }
            else
            {
                return (IOrderedQueryable<T>)items.Provider.CreateQuery(Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { typeof(T), meta.ParametrType }, items.Expression, Expression.Lambda(propertyAccess, param)));
            }

        }

        public static IOrderedQueryable<T> ThenSortingBySortingMetaRecord<T>(IOrderedQueryable<T> items, SortingRecord sort, FilterMetaRecord meta)
        {
            var param = Expression.Parameter(typeof(T), "x");

            var propertyAccess = meta.Path.Split('.').Aggregate((Expression)param, Expression.Property);

            if (sort.Direction == "asc")
            {
                return (IOrderedQueryable<T>)items.Provider.CreateQuery(Expression.Call(typeof(Queryable), "ThenBy", new Type[] { typeof(T), meta.ParametrType }, items.Expression, Expression.Lambda(propertyAccess, param)));
            }
            else
            {
                return (IOrderedQueryable<T>)items.Provider.CreateQuery(Expression.Call(typeof(Queryable), "ThenByDescending", new Type[] { typeof(T), meta.ParametrType }, items.Expression, Expression.Lambda(propertyAccess, param)));
            }
        }

        public static IQueryable<T> FilterByFilterMetaRecord<T>(IQueryable<T> items, FilterRecord filter, FilterMetaRecord meta)
        {
            var param = Expression.Parameter(typeof(T), "x");

            var propertyAccess = meta.Path.Split('.').Aggregate((Expression)param, Expression.Property);

            var constValue = GetConstantExpression(meta, filter.Value[0]);

            var condition = GetFirstExpression(meta, constValue, propertyAccess);

            if (!meta.IsSingleFilter)
            {
                for (int i = 1; i < filter.Value.Count; i++)
                {
                    var now_val_next = GetConstantExpression(meta, filter.Value[i]);
                    condition = GetNextExpression(condition, meta, now_val_next, propertyAccess);
                }
            }

            return items.Where(Expression.Lambda<Func<T, bool>>(condition, param));
        }

        public static Expression GetConstantExpression(FilterMetaRecord meta, string val)
        {
            if (meta.ParametrType.IsEnum)
            {
                return Expression.Constant(Enum.Parse(meta.ParametrType, val));
            }
            else
            {
                return Expression.Constant(Convert.ChangeType(val, meta.ParametrType));
            }
        }

        public static Expression GetNextExpression(Expression condition, FilterMetaRecord meta, Expression ValExpression, Expression propertyAccess)
        {
            Expression res = null;

            switch (meta.OperatorBetween)
            {
                case Operators_between.Or:
                    {
                        res = Expression.Or(condition, GetFirstExpression(meta, ValExpression, propertyAccess));
                        break;
                    }
                case Operators_between.And:
                    {
                        res = Expression.And(condition, GetFirstExpression(meta, ValExpression, propertyAccess));
                        break;
                    }
            }

            return res;
        }

        public static Expression GetFirstExpression(FilterMetaRecord meta, Expression valExperssion, Expression propertyAccess)
        {
            Expression res = null;

            switch (meta.OperatorName)
            {
                case Operators.Equal:
                    {
                        res = Expression.Equal(propertyAccess, valExperssion);
                        break;
                    }
                case Operators.Contains:
                    {
                        res = Expression.Call(propertyAccess, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), valExperssion);
                        break;
                    }
            }

            return res;
        }
    }
}