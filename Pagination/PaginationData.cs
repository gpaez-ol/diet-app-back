using System;
using System.Runtime.Serialization;

namespace Outland.Utils.Pagination
{
    public enum OrderType
    {
        /// <summary>
        /// Ascending Order Type
        /// </summary>
        [EnumMember(Value = "asc")]
        Asc,
        /// <summary>
        /// Descending Order Type
        /// </summary>
        [EnumMember(Value = "desc")]
        Desc
    }
    public class PaginationData
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class PaginationDataOrderBy : PaginationData
    {
        public string OrderBy { get; set; }
        public OrderType? OrderType { get; set; }
    }

    public class PaginationDataParams : PaginationDataOrderBy
    {
    }

    public class PaginationDataParams<T>  where T : struct, Enum
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public OrderType? OrderType { get; set; }
        public T? OrderBy { get; set; }
    }
}
