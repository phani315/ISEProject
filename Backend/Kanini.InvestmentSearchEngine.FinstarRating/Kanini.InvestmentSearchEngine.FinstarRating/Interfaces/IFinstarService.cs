using Kanini.InvestmentSearchEngine.FinstarRating.Models;
using Kanini.InvestmentSearchEngine.FinstarRating.Models.DTOs;

namespace Kanini.InvestmentSearchEngine.FinstarRating.Interfaces
{
    public interface IFinstarService
    {
        #region Finstart Services Interfaces
        public Task<FinstarAverageRatingDTO?> AddFinstarDetails(FinstarDTO item);
        public Task<FinstarAverageRatingDTO?> DeleteFinstarDetails(int id);
        public Task<FinstarAverageRatingDTO?> GetFinstarDetails(int id);
        public Task<ICollection<FinstarAverageRatingDTO>?> GetAllFinstarDetails();
        public Task<FinstarAverageRatingDTO?> UpdateFinstarDetails(FinstarDTO item);
        public double FindAverageRating(FinstarDTO id);
        public int FindTotalReviewCount(FinstarDTO id);

        #endregion

    }

}
