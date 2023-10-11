using Kanini.InvestmentSearchEngine.FinstarRating.CustomExceptions;
using Kanini.InvestmentSearchEngine.FinstarRating.Interfaces;
using Kanini.InvestmentSearchEngine.FinstarRating.Models;
using Kanini.InvestmentSearchEngine.FinstarRating.Models.DTOs;
using Kanini.InvestmentSearchEngine.FinstarRating.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace YourNamespaceHere.Tests
{
    [TestClass]
    public class FinstarServiceTests
    {
        private FinstarService _finstarService=null!;
        private Mock<IRepo<int, Finstar>> _finstarRepoMock = null!;
        private Mock<IMapper> _mapperMock = null!;
        private Mock<ILogger<FinstarService>> _loggerMock = null!;

        [TestInitialize]
        public void Initialize()
        {
            _finstarRepoMock = new Mock<IRepo<int, Finstar>>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<FinstarService>>();
            //An instance of FinstarService is made, passing in the mock objects as dependencies.
            _finstarService = new FinstarService(_finstarRepoMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public async Task AddFinstarDetails_ValidInput_ReturnsDTO()
        {
            // Arrange
            var finstarDto = new FinstarDTO()
            {
                CompanyId = 5,
                EfficiencyRate = 4,
                EfficienncyReviewCount = 25,
                FinancialRate = 4,
                FinancialReviewCount = 25,
                OwnerShipRate = 4,
                OwnerShipReviewCount = 25,
                ValuationRate = 4,
                ValuationReviewCount = 25

            };
            var finstar = new Finstar()
            {
                CompanyId = 5,
                TotalReviewCount = 100,
                TotalRating = 4,
                Financial = new Financial
                {
                    FinancialRate = 4,
                    ReviewCount = 25
                },
                Efficiency = new Efficiency
                {
                    EfficiencyRate = 4,
                    ReviewCount = 25,
                },
                OwnerShip = new OwnerShip
                {
                    OwnerShipRate = 4,
                    ReviewCount = 25
                },
                Valuation = new Valuation
                {
                    ValuationRate = 4,
                    ReviewCount = 25
                }

            };

            var finstarAverageRatingDTO = new FinstarAverageRatingDTO()
            {
                CompanyId = 5,
                TotalReviewCount = 100,
                TotalRating = 2,
                EfficiencyRate = 4,
                EfficienncyReviewCount = 25,
                FinancialRate = 4,
                FinancialReviewCount = 25,
                OwnerShipRate = 4,
                ValuationRate = 4,
                ValuationReviewCount = 2
            };
             _finstarRepoMock.Setup(repo => repo.Get(finstarDto.CompanyId)).ReturnsAsync((Finstar)null);
            _mapperMock.Setup(mapper => mapper.MapFinstarDTO(finstarDto, 4, 100)).ReturnsAsync(finstar);
            _finstarRepoMock.Setup(repo => repo.Add(finstar)).ReturnsAsync(finstar);
            _mapperMock.Setup(mapper => mapper.MapFinstarToFinstarAverageRatingDTO(finstar)).ReturnsAsync(finstarAverageRatingDTO);

            // Act
            var result = await _finstarService.AddFinstarDetails(finstarDto);

            // Assert
            Assert.IsNotNull(result);
            // Assert that the TotalRating of the result matches the TotalRating of finstarAverageRatingDTO
            Assert.AreEqual(finstarAverageRatingDTO.TotalRating, result.TotalRating);

        }

        [TestMethod]
        public async Task AddFinstarDetails_CompanyIdExists_ThrowsCompanyIdAlreadyExistsException()
        {
            // Arrange
            var finstarDto = new FinstarDTO
            {

                CompanyId = 5,
                EfficiencyRate = 4,
                EfficienncyReviewCount = 25,
                FinancialRate = 4,
                FinancialReviewCount = 25,
                OwnerShipRate = 4,
                OwnerShipReviewCount = 25,
                ValuationRate = 4,
                ValuationReviewCount = 25


            };
            var existingFinstar = new Finstar() {

                CompanyId = 5,
                TotalReviewCount = 100,
                TotalRating = 4,
                Financial = new Financial
                {
                    FinancialRate = 4,
                    ReviewCount = 25
                },
                Efficiency = new Efficiency
                {
                    EfficiencyRate = 4,
                    ReviewCount = 25,
                },
                OwnerShip = new OwnerShip
                {
                    OwnerShipRate = 4,
                    ReviewCount = 25
                },
                Valuation = new Valuation
                {
                    ValuationRate = 4,
                    ReviewCount = 25
                }
        };

            _finstarRepoMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(existingFinstar);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<CompanyIdAlreadyExistsException>(() => _finstarService.AddFinstarDetails(finstarDto));
        }


        [TestMethod]
        public async Task AddFinstarDetails_AddingFinstarFailed_ThrowsAddObjectException()
        {
            // Arrange
            var finstarDto = new FinstarDTO()
            {
                CompanyId = 5,
                EfficiencyRate = 4,
                EfficienncyReviewCount = 25,
                FinancialRate = 4,
                FinancialReviewCount = 25,
                OwnerShipRate = 4,
                OwnerShipReviewCount = 25,
                ValuationRate = 4,
                ValuationReviewCount = 25

            };
            var existingFinstar = new Finstar()
            {
                CompanyId = 5,
                TotalReviewCount = 100,
                TotalRating = 4,
                Financial = new Financial
                {
                    FinancialRate = 4,
                    ReviewCount = 25
                },
                Efficiency = new Efficiency
                {
                    EfficiencyRate = 4,
                    ReviewCount = 25,
                },
                OwnerShip = new OwnerShip
                {
                    OwnerShipRate = 4,
                    ReviewCount = 25
                },
                Valuation = new Valuation
                {
                    ValuationRate = 4,
                    ReviewCount = 25
                }

            };

            _finstarRepoMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync((Finstar)null);
            _mapperMock.Setup(mapper => mapper.MapFinstarDTO(It.IsAny<FinstarDTO>(), It.IsAny<double>(), It.IsAny<int>())).ReturnsAsync(existingFinstar);
            _finstarRepoMock.Setup(repo => repo.Add(It.IsAny<Finstar>())).ReturnsAsync((Finstar)null);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<AddObjectException>(() => _finstarService.AddFinstarDetails(finstarDto));
        }









        [TestMethod]
    public async Task DeleteFinstarDetails_ValidId_ReturnsDeletedDTO()
        {
        // Arrange
            var finstarId = 5;
        var finstar = new Finstar();
        var expectedDto = new FinstarAverageRatingDTO();
        _finstarRepoMock.Setup(repo => repo.Delete(finstarId)).ReturnsAsync(finstar);
        _mapperMock.Setup(mapper => mapper.MapFinstarToFinstarAverageRatingDTO(finstar)).ReturnsAsync(expectedDto);

        // Act
            var result = await _finstarService.DeleteFinstarDetails(finstarId);

        // Assert
            Assert.AreEqual(expectedDto, result);
        }

        [TestMethod]
        public async Task DeleteFinstarDetails_InvalidId_ThrowsInvalidIdException()
        {
            // Arrange
            var invalidId = -1;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidIdException>(() => _finstarService.DeleteFinstarDetails(invalidId));
        }

        [TestMethod]
        public async Task DeleteFinstarDetails_FinstarNotFound_ThrowsCompanyNotFoundException()
        {
            // Arrange
            var CompanyId = 5;
            _finstarRepoMock.Setup(repo => repo.Delete(CompanyId)).ReturnsAsync((Finstar)null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<CompanyNotFound>(() => _finstarService.DeleteFinstarDetails(CompanyId));
        }


        [TestMethod]
        public async Task GetAllFinstarDetails_FinstarsExist_ReturnsDTOCollection()
        {
            // Arrange
            var finstars = new List<Finstar> { new Finstar()
            {

                CompanyId = 5,
                TotalReviewCount = 100,
                TotalRating = 4,
                Financial = new Financial
                {
                    FinancialRate = 4,
                    ReviewCount = 25
                },
                Efficiency = new Efficiency
                {
                    EfficiencyRate = 4,
                    ReviewCount = 25,
                },
                OwnerShip = new OwnerShip
                {
                    OwnerShipRate = 4,
                    ReviewCount = 25
                },
                Valuation = new Valuation
                {
                    ValuationRate = 4,
                    ReviewCount = 25
                }


        }, new Finstar(){



                CompanyId = 5,
                TotalReviewCount = 100,
                TotalRating = 4,
                Financial = new Financial
                {
                    FinancialRate = 4,
                    ReviewCount = 25
                },
                Efficiency = new Efficiency
                {
                    EfficiencyRate = 4,
                    ReviewCount = 25,
                },
                OwnerShip = new OwnerShip
                {
                    OwnerShipRate = 4,
                    ReviewCount = 25
                },
                Valuation = new Valuation
                {
                    ValuationRate = 4,
                    ReviewCount = 25
                }
        }
        };

            _finstarRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(finstars);
            _mapperMock.Setup(mapper => mapper.MapFinstarToFinstarAverageRatingDTO(It.IsAny<Finstar>()))
                .ReturnsAsync(new FinstarAverageRatingDTO());

            // Act
            var result = await _finstarService.GetAllFinstarDetails();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(finstars.Count, result.Count);
        }

        [TestMethod]
        public async Task GetAllFinstarDetails_NoFinstars_ReturnsEmptyCollection()
        {
            // Arrange
            _finstarRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync((List<Finstar>)null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<EmptyFinstarDetails>(() => _finstarService.GetAllFinstarDetails());
        }


        #region Get method test cases
        [TestMethod]
        public async Task GetFinstarDetails_ValidId_ReturnsDTO()
        {
            // Arrange
            var companyId = 5;
            var finstar = new Finstar
            {
                CompanyId = 5,
                TotalReviewCount = 100,
                TotalRating = 4,
                Financial = new Financial
                {
                    FinancialRate = 4,
                    ReviewCount = 25
                },
                Efficiency = new Efficiency
                {
                    EfficiencyRate = 4,
                    ReviewCount = 25,
                },
                OwnerShip = new OwnerShip
                {
                    OwnerShipRate = 4,
                    ReviewCount = 25
                },
                Valuation = new Valuation
                {
                    ValuationRate = 4,
                    ReviewCount = 25
                }
            }; 
            var expectedDto = new FinstarAverageRatingDTO {
                CompanyId = 5,
                TotalReviewCount = 100,
                TotalRating = 4,
                EfficiencyRate = 4,
                EfficienncyReviewCount = 25,
                FinancialRate = 4,
                FinancialReviewCount = 25,
                OwnerShipRate = 4,
                ValuationRate = 4,
                ValuationReviewCount = 2
            }; 

            _finstarRepoMock.Setup(repo => repo.Get(companyId)).ReturnsAsync(finstar);
            _mapperMock.Setup(mapper => mapper.MapFinstarToFinstarAverageRatingDTO(finstar))
                .ReturnsAsync(expectedDto);

            // Act
            var result = await _finstarService.GetFinstarDetails(companyId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedDto, result);
        }

        [TestMethod]
        public async Task GetFinstarDetails_InvalidId_ThrowsInvalidIdException()
        {
            // Arrange
            var invalidId = -1;

            // Act and Assert
            await Assert.ThrowsExceptionAsync<InvalidIdException>(
                async () => await _finstarService.GetFinstarDetails(invalidId));
        }
        [TestMethod]
        public async Task GetFinstarDetails_CompanyNotFound_ThrowsCompanyNotFoundException()
        {
            // Arrange
            var companyId = 5;
            _finstarRepoMock.Setup(repo => repo.Get(companyId)).ReturnsAsync((Finstar)null);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<CompanyNotFound>(
                async () => await _finstarService.GetFinstarDetails(companyId));
        }



        #endregion

        #region Update method test cases 
        [TestMethod]
        public async Task UpdateFinstarDetails_ValidInput_ReturnsUpdatedDTO()
        {
            // Arrange
            var finstarDto = new FinstarDTO
            {
                CompanyId = 5,
                EfficiencyRate = 4,
                EfficienncyReviewCount = 25,
                FinancialRate = 4,
                FinancialReviewCount = 25,
                OwnerShipRate = 4,
                OwnerShipReviewCount = 25,
                ValuationRate = 4,
                ValuationReviewCount = 25
            };

            var updatedFinstar = new Finstar
            {
                CompanyId = 5,
                TotalReviewCount = 100,
                TotalRating = 4,
                Financial = new Financial
                {
                    FinancialRate = 4,
                    ReviewCount = 25
                },
                Efficiency = new Efficiency
                {
                    EfficiencyRate = 4,
                    ReviewCount = 25,
                },
                OwnerShip = new OwnerShip
                {
                    OwnerShipRate = 4,
                    ReviewCount = 25
                },
                Valuation = new Valuation
                {
                    ValuationRate = 4,
                    ReviewCount = 25
                }
            };
            var expectedDto = new FinstarAverageRatingDTO
            {
                CompanyId = 5,
                TotalReviewCount = 100,
                TotalRating = 4,
                EfficiencyRate = 4,
                EfficienncyReviewCount = 25,
                FinancialRate = 4,
                FinancialReviewCount = 25,
                OwnerShipRate = 4,
                ValuationRate = 4,
                ValuationReviewCount = 2
            };

            _mapperMock.Setup(mapper => mapper.MapFinstarDTO(finstarDto, It.IsAny<double>(), It.IsAny<int>()))
                .ReturnsAsync(updatedFinstar);
            _finstarRepoMock.Setup(repo => repo.Update(updatedFinstar)).ReturnsAsync(updatedFinstar);
            _mapperMock.Setup(mapper => mapper.MapFinstarToFinstarAverageRatingDTO(updatedFinstar))
                .ReturnsAsync(expectedDto);

            // Act
            var result = await _finstarService.UpdateFinstarDetails(finstarDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedDto, result);
        }

        [TestMethod]
        public async Task UpdateFinstarDetails_InvalidCompanyId_ThrowsInvalidIdException()
        {
            // Arrange
            var finstarDto = new FinstarDTO { CompanyId = -1  };

            // Act and Assert
            await Assert.ThrowsExceptionAsync<InvalidIdException>(
                async () => await _finstarService.UpdateFinstarDetails(finstarDto));
        }

        [TestMethod]
        public async Task UpdateFinstarDetails_UpdateFailed_ThrowsUpdateFailedException()
        {
            // Arrange
            var finstarDto = new FinstarDTO
            {
                CompanyId = 5,
                EfficiencyRate = 4,
                EfficienncyReviewCount = 25,
                FinancialRate = 4,
                FinancialReviewCount = 25,
                OwnerShipRate = 4,
                OwnerShipReviewCount = 25,
                ValuationRate = 4,
                ValuationReviewCount = 25
            };
            _finstarRepoMock.Setup(repo => repo.Update(It.IsAny<Finstar>())).ReturnsAsync((Finstar)null);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<UpdateFailedException>(
                async () => await _finstarService.UpdateFinstarDetails(finstarDto));
        }
        #endregion

        #region FindAverageRating Tests 

        [TestMethod]
        public void FindAverageRating_AllRatesValid_ReturnsCorrectAverage()
        {
            // Arrange
            var finstarDTO = new FinstarDTO
            {

                CompanyId = 5,
                EfficiencyRate = 4,
                EfficienncyReviewCount = 25,
                FinancialRate = 4,
                FinancialReviewCount = 25,
                OwnerShipRate = 4,
                OwnerShipReviewCount = 25,
                ValuationRate = 4,
                ValuationReviewCount = 25
            };

            // Act
            var result = _finstarService.FindAverageRating(finstarDTO);

            // Assert
            Assert.AreEqual(4.0, result);
        }

        [TestMethod]
        public void FindAverageRating_NullFinstarDTO_ThrowsArgumentNullException()
        {
            // Arrange
            FinstarDTO? finstarDTO = null;

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => _finstarService.FindAverageRating(finstarDTO));
        }

        #endregion

        #region FindTotalReviewCount Tests
        [TestMethod]
        public void FindTotalReviewCount_ValidFinstarDTO_ReturnsTotalReviewCount()
        {
            // Arrange
            var finstarDTO = new FinstarDTO
            {
                CompanyId = 5,
                EfficiencyRate = 4,
                EfficienncyReviewCount = 25,
                FinancialRate = 4,
                FinancialReviewCount = 25,
                OwnerShipRate = 4,
                OwnerShipReviewCount = 25,
                ValuationRate = 4,
                ValuationReviewCount = 25
            };

            // Act
            var result = _finstarService.FindTotalReviewCount(finstarDTO);

            // Assert
            Assert.AreEqual(100, result);
        }
        [TestMethod]
        public void FindTotalReviewCount_NullFinstarDTO_ThrowsArgumentNullException()
        {
            // Arrange
            FinstarDTO? finstarDTO = null;

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => _finstarService.FindTotalReviewCount(finstarDTO));
        }

        #endregion
        [TestCleanup]
        public void Cleanup()
        {
            _finstarService = null;
            _finstarRepoMock = null;
            _mapperMock = null;
            _loggerMock = null;
        }
    }
}
