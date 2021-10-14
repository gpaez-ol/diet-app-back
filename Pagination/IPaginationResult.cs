using System.Collections.Generic;

namespace AlgoFit.Utils.Pagination.Interfaces
{
    /// <summary>
    /// Expose the result of pagination
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPaginationResult<T>
    {
        /// <Summary>
        /// Total Pages
        /// </Summary>
        int PagesNumber { get; set; }

        /// <Summary>
        /// List of items
        /// </Summary>
        ICollection<T> Pagination { get; set; }
    }
}
