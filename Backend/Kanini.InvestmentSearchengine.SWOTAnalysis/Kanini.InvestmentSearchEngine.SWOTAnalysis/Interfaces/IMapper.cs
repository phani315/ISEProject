using Kanini.InvestmentSearchEngine.SWOTAnalysis.Models;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.DTOs;

namespace Kanini.InvestmentSearchEngine.SWOTAnalysis.Interfaces
{
    public interface IMapper
    {
        public SwotDTO SwotToSwotDTO(SWOT swot);
    }
}
