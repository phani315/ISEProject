using Kanini.InvestmentSearchEngine.FinstarRating.Models.DTOs;
using Kanini.InvestmentSearchEngine.FinstarRating.Models;

namespace Kanini.InvestmentSearchEngine.FinstarRating.Interfaces
{
    public interface IMapper
    {
        #region Finstar Mapper
        public Task<Finstar> MapFinstarDTO(FinstarDTO finstarDTO, double totalRating,int totalReviewCount);
        public Task<FinstarAverageRatingDTO> MapFinstarToFinstarAverageRatingDTO(Finstar finstar);
        #endregion 
    }
}
