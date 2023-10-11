using Kanini.InvestmentSearchEngine.FinstarRating.Interfaces;
using Kanini.InvestmentSearchEngine.FinstarRating.Models;
using Kanini.InvestmentSearchEngine.FinstarRating.Models.DTOs;
using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.FinstarRating.Mappers
{
    [ExcludeFromCodeCoverage]

    public class Mapper:IMapper
    {
        #region MapFinstarDTO
        public Task<Finstar> MapFinstarDTO(FinstarDTO finstarDTO, double totalRating,int totalReviewCount)
        {
            var finstar = new Finstar
            {
                TotalRating = totalRating,
                CompanyId = finstarDTO.CompanyId,
                TotalReviewCount = totalReviewCount,
                Financial = new Financial { FinancialRate= finstarDTO.FinancialRate, ReviewCount=finstarDTO.FinancialReviewCount},
                Efficiency = new Efficiency { EfficiencyRate = finstarDTO.EfficiencyRate, ReviewCount = finstarDTO.EfficienncyReviewCount },
                OwnerShip = new OwnerShip { OwnerShipRate = finstarDTO.FinancialRate, ReviewCount = finstarDTO.OwnerShipReviewCount },
                Valuation = new Valuation { ValuationRate = finstarDTO.FinancialRate, ReviewCount = finstarDTO.ValuationReviewCount }
            };
            return Task.FromResult(finstar);
        }
        #endregion
        #region MapFinstarToFinstarAverageRatingDTO

        public Task<FinstarAverageRatingDTO> MapFinstarToFinstarAverageRatingDTO(Finstar finstar)
        {
            var finstarAverageRatingDTO = new FinstarAverageRatingDTO
            { 
                CompanyId = finstar.CompanyId,
                TotalRating = finstar.TotalRating,
                TotalReviewCount = finstar.TotalReviewCount,
            };
            if (finstar.Financial!=null&& finstar.Efficiency != null && finstar.Valuation != null && finstar.OwnerShip != null)
            {
                finstarAverageRatingDTO.FinancialRate = finstar.Financial.FinancialRate;
                finstarAverageRatingDTO.FinancialReviewCount = finstar.Financial.ReviewCount;
                finstarAverageRatingDTO.EfficiencyRate = finstar.Efficiency.EfficiencyRate;
                finstarAverageRatingDTO.EfficienncyReviewCount = finstar.Efficiency.ReviewCount;
                finstarAverageRatingDTO.OwnerShipRate = finstar.OwnerShip.OwnerShipRate;
                finstarAverageRatingDTO.OwnerShipReviewCount = finstar.OwnerShip.ReviewCount;
                finstarAverageRatingDTO.ValuationRate = finstar.Valuation.ValuationRate;
                finstarAverageRatingDTO.ValuationReviewCount = finstar.Valuation.ReviewCount;
            }
            return Task.FromResult(finstarAverageRatingDTO);
        }
        #endregion
    }
}
