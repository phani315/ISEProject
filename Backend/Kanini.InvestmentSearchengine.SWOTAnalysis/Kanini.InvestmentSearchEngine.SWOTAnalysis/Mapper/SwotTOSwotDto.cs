using Kanini.InvestmentSearchEngine.SWOTAnalysis.Exceptions;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Interfaces;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Models;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Models.DTOs;
using System.ComponentModel.Design;

namespace Kanini.InvestmentSearchEngine.SWOTAnalysis.Mapper
{
    public class SwotTOSwotDto : IMapper
    {
        public SwotDTO SwotToSwotDTO(SWOT swot)
        {

            if (swot == null)
                throw new Exception("no data ");
            var swotDTO = new SwotDTO();
            if ( swot.Strength != null && swot.Weakness != null && swot.Threat != null && swot.Oppurtunity != null)
            {
                swotDTO.CompanyID = swot.CompanyID;
                swotDTO.StrengthValue = swot.Strength.StrengthValue;
                swotDTO.StrengthDescription = swot.Strength.StrengthDescription;
                swotDTO.WeaknessValue = swot.Weakness.WeaknessValue;
                swotDTO.WeaknessDescription = swot.Weakness.WeaknessDescription;
                swotDTO.ThreatValue = swot.Threat.ThreatValue;
                swotDTO.ThreatDescription = swot.Threat.ThreatDescription;
                swotDTO.OppurtunityValue = swot.Oppurtunity.OppurtunityValue;
                swotDTO.OppurtunityDescription = swot.Oppurtunity.OppurtunityDescription;
                return swotDTO;

            };
            throw new NullCompanyDetailsException("No swot details available with given company Id");
            
        }
    }
}
