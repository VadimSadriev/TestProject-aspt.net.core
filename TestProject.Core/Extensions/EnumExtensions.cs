using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProject.Core
{
    public static class EnumExtensions
    {
        public static string GetRuName(this RequestFieldType item)
        {
            switch (item)
            {
                case RequestFieldType.Date:
                    return "Дата";
                case RequestFieldType.File:
                    return "Файл";
                case RequestFieldType.Number:
                    return "Число";
                case RequestFieldType.String:
                    return "Строка";
                case RequestFieldType.Time:
                    return "Время";
                case RequestFieldType.Unknown:
                    return "Неопределено";
                default:
                    return "Неопределено";
            }
        }

        public static string GetRuName(this RequestStatus item)
        {
            switch (item)
            {
                case RequestStatus.Accepted:
                    return "Принято";
                case RequestStatus.Rejected:
                    return "Отклонено";
                case RequestStatus.UnderConsideration:
                    return "На рассмотрении";
                default:
                    return "Неопределено";
            }
        }

        public static RequestStatus ToEnumStatus(this string str)
        {
            switch (str)
            {
                case "Принято":
                    return RequestStatus.Accepted;
                case "Accepted":
                    return RequestStatus.Accepted;
                case "Отклонено":
                    return RequestStatus.Rejected;
                case "Rejected":
                    return RequestStatus.Rejected;
                case "На рассмотрении":
                    return RequestStatus.UnderConsideration;
                case "UnderConsideration":
                    return RequestStatus.UnderConsideration;
                default:
                    return RequestStatus.Unknown;
            }
        }

        public static RequestFieldType ToEnumFieldType(this string str)
        {
            switch (str)
            {
                case "Дата":
                    return RequestFieldType.Date;
                case "Date":
                    return RequestFieldType.Date;
                case "Файл":
                    return RequestFieldType.File;
                case "File":
                    return RequestFieldType.File;
                case "Число":
                    return RequestFieldType.Number;
                case "Number":
                    return RequestFieldType.Number;
                case "Строка":
                    return RequestFieldType.String;
                case "String":
                    return RequestFieldType.String;
                case "Время":
                    return RequestFieldType.Time;
                case "Time":
                    return RequestFieldType.Time;
                default:
                    return RequestFieldType.Unknown;
            }
        }
    }
}
