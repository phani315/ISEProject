using Kanini.InvestmentSearchEngine.FinstarRating.CustomExceptions;
using Kanini.InvestmentSearchEngine.FinstarRating.Interfaces;
using Kanini.InvestmentSearchEngine.FinstarRating.Models;
using Kanini.InvestmentSearchEngine.FinstarRating.Models.DTOs;


namespace Kanini.InvestmentSearchEngine.FinstarRating.Services
{
    public class FinstarService : IFinstarService
    {
        #region Private Variable
        private readonly IRepo<int, Finstar> _finstarRepo;
        private readonly IMapper _mapper;
        #endregion 

        #region Constructor
        public FinstarService(IRepo<int, Finstar> finstarRepo,
                              ILogger<FinstarService> logger,
                              IMapper mapper) // Add ILogger parameter
        {
            _finstarRepo = finstarRepo;
            _mapper = mapper;
        }
        #endregion

        #region AddFinstarDetails
        /// <summary>
        /// Adds Finstar details for a company.
        /// </summary>
        /// <param name="item">The Finstar data to be added.</param>

        public async Task<FinstarAverageRatingDTO?> AddFinstarDetails(FinstarDTO item)

        {
            var existingFinstar = await _finstarRepo.Get(item.CompanyId);
            if (existingFinstar != null) throw new CompanyIdAlreadyExistsException("Company ID already exists in the database");
            var averageRating = FindAverageRating(item);
            var totalReviewCount = FindTotalReviewCount(item);
            var finstar = await _mapper.MapFinstarDTO(item, averageRating, totalReviewCount);
            var result = await _finstarRepo.Add(finstar) ?? throw new AddObjectException("Adding finstar rating failed ");
            return await _mapper.MapFinstarToFinstarAverageRatingDTO(result);
        }
        #endregion

        #region DeleteFinstarDetails
        /// <summary>
        /// Deletes Finstar details by ID.
        /// </summary>
        /// <param name="id">The ID of the Finstar to delete.</param>
        public async Task<FinstarAverageRatingDTO?> DeleteFinstarDetails(int id)
        {
            if (id <= 0) throw new InvalidIdException("The Rating Id is Invalid ");
            var finstar = await _finstarRepo.Delete(id);
            return finstar == null
                ? throw new CompanyNotFound("Company not found with the given ID")
                : await _mapper.MapFinstarToFinstarAverageRatingDTO(finstar);
        }

        #endregion

        #region GetAllFinstarDetails
        /// <summary>
        /// Gets all Finstar details.
        /// </summary>
        public async Task<ICollection<FinstarAverageRatingDTO>?> GetAllFinstarDetails()
        {
            var finstars = await _finstarRepo.GetAll();
            var finstarDTOs = new List<FinstarAverageRatingDTO>();
            if (finstars != null)
            {
                foreach (var finstar in finstars)
                {
                    var finstarDTO = await _mapper.MapFinstarToFinstarAverageRatingDTO(finstar);
                    finstarDTOs.Add(finstarDTO);
                }
                return finstarDTOs;
            }
            throw new EmptyFinstarDetails("There are no records in the Database to fetch");
        }

        #endregion

        #region GetFinstarDetails
        /// <summary>
        /// Gets a Finstar's details by ID.
        /// </summary>
        /// <param name="id">The ID of the Finstar to retrieve.</param>
        public async Task<FinstarAverageRatingDTO?> GetFinstarDetails(int id)
        {
            if (id <= 0) throw new InvalidIdException("The Company Id is Invalid ");
            var finstar = await _finstarRepo.Get(id) ?? throw new CompanyNotFound("Company not found with the given ID");
            var finstarDTO = await _mapper.MapFinstarToFinstarAverageRatingDTO(finstar);
            return finstarDTO;
        }
        #endregion

        #region UpdateFinstarDetails
        /// <summary>
        /// Updates Finstar details.
        /// </summary>
        /// <param name="item">The FinstarDTO containing updated information.</param>
        public async Task<FinstarAverageRatingDTO?> UpdateFinstarDetails(FinstarDTO item)
        {
            if (item.CompanyId < 0) throw new InvalidIdException("Invalid Company Id");
            var averageRating = FindAverageRating(item);
            var totalReviewCount = FindTotalReviewCount(item);
            var finstar = await _mapper.MapFinstarDTO(item, averageRating, totalReviewCount);
            var updatedfinstar = await _finstarRepo.Update(finstar) ?? throw new UpdateFailedException("Update Failed");
            return await _mapper.MapFinstarToFinstarAverageRatingDTO(updatedfinstar);
        }
        #endregion

        #region FindAverageRating
        /// <summary>
        /// Calculates the average rating based on a FinstarDTO.
        /// </summary>
        /// <param name="finstarDTO">The FinstarDTO containing rating information.</param>
        public double FindAverageRating(FinstarDTO finstarDTO)
        {
            if (finstarDTO == null) throw new ArgumentNullException(nameof(finstarDTO), "FinstarDTO cannot be null");
            return (finstarDTO.FinancialRate + finstarDTO.OwnerShipRate + finstarDTO.EfficiencyRate + finstarDTO.ValuationRate) / 4;
        }

        #endregion

        #region FindTotalReviewCount 
        /// <summary>
        /// Calculates the total review count based on a FinstarDTO.
        /// </summary>
        /// <param name="finstarDTO">The FinstarDTO containing review count information.</param>
        public int FindTotalReviewCount(FinstarDTO finstarDTO)
        {
            if (finstarDTO == null) throw new ArgumentNullException(nameof(finstarDTO), "FinstarDTO cannot be null");
            return finstarDTO.FinancialReviewCount + finstarDTO.OwnerShipReviewCount + finstarDTO.EfficienncyReviewCount + finstarDTO.ValuationReviewCount;
        }
        #endregion

    }



}
