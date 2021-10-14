using AlgoFit.Utils.Pagination.Interfaces;
using System.Collections.Generic;

namespace AlgoFit.Utils.Pagination.Internal
{
    internal class PaginationResult<T> : IPaginationResult<T>
    {
        public int PagesNumber { get; set; }
        public ICollection<T> Pagination { get; set; }
    }
}
