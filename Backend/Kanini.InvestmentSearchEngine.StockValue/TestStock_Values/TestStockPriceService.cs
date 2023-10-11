using Kanini.InvestmentSearchEngine.StockValue.Interfaces;
using Kanini.InvestmentSearchEngine.StockValue.Models;
using Kanini.InvestmentSearchEngine.StockValue.Models.DTOs;
using Kanini.InvestmentSearchEngine.StockValue.Repositories;
using Kanini.InvestmentSearchEngine.StockValue.Services;
using Kanini.InvestmentSearchEngine.StockValue.Utilities.Exceptions;
using Moq;

namespace TestStock_Values
{
    [TestClass]
    public class TestStockPriceService
    {
        private Mock<IRepository<int, StockPrice>> _stockPriceRepositoryMock;
        private Mock<IRepository<int, StockTransaction>> _stockTransactionRepositoryMock;
        private Mock<IMapperService> _mapperServiceMock;
        private StockPriceService _stockPriceServiceMock;

        public TestStockPriceService()
        {
            _stockPriceRepositoryMock = new Mock<IRepository<int, StockPrice>>();
            _stockTransactionRepositoryMock = new Mock<IRepository<int, StockTransaction>>();
            _mapperServiceMock = new Mock<IMapperService>();
            _stockPriceServiceMock = new StockPriceService(_stockPriceRepositoryMock.Object,_stockTransactionRepositoryMock.Object, _mapperServiceMock.Object);

        }
        [TestMethod]
        public async Task AddStockPriceService_ValidStockPrice_ReturnsAddedStockPrice()
        {
            // Arrange
            var stockPrice = new StockPrice()
            {
                Id = 0,
                CompanyId = 1001,
                CurrentStockPrice = 50.25,
                UpdatedStockPrice = 52.10,
                UpdatedStockPercent = 3.5,
                Date = DateTime.Now.AddDays(-1),
                StockTransactions = new List<StockTransaction>
                    {
                        new StockTransaction
                        {
                            Id = 1,
                            StockId = 1,
                            StockValue = 50.25,
                            Date = DateTime.Now.AddDays(-2)
                        },
                        new StockTransaction
                        {
                            Id = 2,
                            StockId = 1,
                            StockValue = 52.10,
                            Date = DateTime.Now.AddDays(-1)
                        }
                    }
            };

            // Set up the mock repository to return the added stock price
            _stockPriceRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<StockPrice>());
            _stockPriceRepositoryMock.Setup(repo => repo.Add(It.IsAny<StockPrice>())).ReturnsAsync(stockPrice);

            // Act
            var addedStockPrice = await _stockPriceServiceMock.AddStockPriceService(stockPrice);

            // Assert
            Assert.IsNotNull(addedStockPrice);
            Assert.AreEqual(stockPrice, addedStockPrice);
        }

        [TestMethod]
        public async Task AddStockPrice_ThrowUserExceptionTest()
        {
            var stockPrice = new StockPrice()
            {
                Id = 1,
                CompanyId = 1001,
                CurrentStockPrice = 50.25,
                UpdatedStockPrice = 52.10,
                UpdatedStockPercent = 3.5,
                Date = DateTime.Now.AddDays(-1),
                StockTransactions = new List<StockTransaction>
                {
                    new StockTransaction
                    {
                        Id = 1,
                        StockId = 1,
                        StockValue = 50.25,
                        Date = DateTime.Now.AddDays(-2)
                    },
                    new StockTransaction
                    {
                        Id = 2,
                        StockId = 1,
                        StockValue = 52.10,
                        Date = DateTime.Now.AddDays(-1)
                    }
                }
            };

            HashSet<StockPrice> stockPrices = new HashSet<StockPrice>
            {
                new StockPrice
                {
                    Id = 1,
                    CompanyId = 1001,
                    CurrentStockPrice = 50.25,
                    UpdatedStockPrice = 52.10,
                    UpdatedStockPercent = 3.5,
                    Date = DateTime.Now.AddDays(-1),
                    StockTransactions = new List<StockTransaction>
                    {
                        new StockTransaction
                        {
                            Id = 1,
                            StockId = 1,
                            StockValue = 50.25,
                            Date = DateTime.Now.AddDays(-2)
                        },
                        new StockTransaction
                        {
                            Id = 2,
                            StockId = 1,
                            StockValue = 52.10,
                            Date = DateTime.Now.AddDays(-1)
                        }
                    }
                },
                new StockPrice
                {
                    Id = 2,
                    CompanyId = 1002,
                    CurrentStockPrice = 60.75,
                    UpdatedStockPrice = 62.50,
                    UpdatedStockPercent = 2.9,
                    Date = DateTime.Now.AddDays(-2),
                    StockTransactions = new List<StockTransaction>
                    {
                        new StockTransaction
                        {
                            Id = 1,
                            StockId = 2,
                            StockValue = 60.75,
                            Date = DateTime.Now.AddDays(-3)
                        },
                        new StockTransaction
                        {
                            Id = 2,
                            StockId = 2,
                            StockValue = 62.50,
                            Date = DateTime.Now.AddDays(-2)
                        }
                    }
                }
            };
            _stockPriceRepositoryMock.Setup(s => s.GetAll()).ReturnsAsync(stockPrices);

            //Assert
            await Assert.ThrowsExceptionAsync<UserException>(async () => { await _stockPriceServiceMock.AddStockPriceService(stockPrice); });
        }

        [TestMethod]
        public async Task DeleteStockPriceService_ValidId_ReturnsDeletedStockPrice()
        {
            // Arrange
            var stockPriceIdToDelete = 1;
            var deletedStockPrice = new StockPrice
            {
                Id = stockPriceIdToDelete,
                CompanyId = 1,
                CurrentStockPrice = 50.0,
                UpdatedStockPrice = 51.0,
                UpdatedStockPercent = 2.0,
                Date = DateTime.Now,
                StockTransactions = new List<StockTransaction>()
            };

            _stockPriceRepositoryMock.Setup(repo => repo.Delete(stockPriceIdToDelete)).ReturnsAsync(deletedStockPrice);

            // Act
            var result = await _stockPriceServiceMock.DeleteStockPriceService(stockPriceIdToDelete);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(stockPriceIdToDelete, result.Id);
        }

        [TestMethod]
        public async Task GetStockPriceByCompanyID_ValidCompanyID_ReturnsStockPrice()
        {
            // Arrange
            var companyId = 1;
            var expectedStockPrice = new StockPrice { CompanyId = companyId };

            _stockPriceRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<StockPrice> { expectedStockPrice });

            // Act
            var result = await _stockPriceServiceMock.GetStockPriceByCompanyID(companyId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedStockPrice, result);
        }

        [TestMethod]
        public async Task GetAllStockDetailsByCompanyId_InvalidCompanyId_ThrowsNullReferenceException()
        {
            // Arrange
            var invalidCompanyId = 9999;

            var stockDetails = new List<StockPrice>
            {
                new StockPrice
                {
                    CompanyId = 1001,
                    CurrentStockPrice = 60.0,
                    UpdatedStockPercent = 2.5,
                    UpdatedStockPrice = 62.5,
                    Date = DateTime.Now.AddDays(-1)
                },
            };

            _stockPriceRepositoryMock.Setup(s => s.GetAll()).ReturnsAsync(stockDetails);

            // Assert
            await Assert.ThrowsExceptionAsync<NullReferenceException>(async () =>
            {
                // Act
                await _stockPriceServiceMock.GetStockPriceByCompanyID(invalidCompanyId);
            });
        }

        [TestMethod]
        public async Task CalculateStockAveragesByCompanyID_ValidCompanyID_ReturnsStockPriceAveragesDTO()
        {
            // Arrange
            int validCompanyID = 1001;
            var stockPrices = new List<StockPrice>
            {
                new StockPrice
                {
                    Id = 1,
                    CompanyId = validCompanyID,
                    CurrentStockPrice = 50.25,
                    UpdatedStockPrice = 52.10,
                    UpdatedStockPercent = 3.5,
                    Date = DateTime.Now.AddDays(-1),
                    StockTransactions = new List<StockTransaction>
                    {
                        new StockTransaction
                        {
                            Id = 1,
                            StockId = 1,
                            StockValue = 50.25,
                            Date = DateTime.Today
                        },
                        new StockTransaction
                        {
                            Id = 2,
                            StockId = 1,
                            StockValue = 52.10,
                            Date = DateTime.Today
                        }
                    }
                }
            };
            _stockPriceRepositoryMock.Setup(s => s.GetAll()).ReturnsAsync(stockPrices);

            // Act
            var result = await _stockPriceServiceMock.CalculateStockAveragesByCompanyID(validCompanyID);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(validCompanyID, result.CompanyID);
            Assert.AreEqual(52.10, result.YearHigh);
            Assert.AreEqual(50.25, result.YearLow);
        }

        [TestMethod]
        public async Task CalculateStockAveragesByCompanyID_InvalidCompanyID_ThrowsException()
        {
            // Arrange
            int invalidCompanyID = 9999;
            var stockPrices = new List<StockPrice>
            {
                new StockPrice
                {
                    Id = 1,
                    CompanyId = 1001,
                    CurrentStockPrice = 50.25,
                    UpdatedStockPrice = 52.10,
                    UpdatedStockPercent = 3.5,
                    Date = DateTime.Now.AddDays(-1),
                    StockTransactions = new List<StockTransaction>
                    {
                        new StockTransaction
                        {
                            Id = 1,
                            StockId = 1,
                            StockValue = 50.25,
                            Date = DateTime.Now.AddDays(-2)
                        },
                        new StockTransaction
                        {
                            Id = 2,
                            StockId = 1,
                            StockValue = 52.10,
                            Date = DateTime.Now.AddDays(-1)
                        }
                    }
                }
            };
            _stockPriceRepositoryMock.Setup(s => s.GetAll()).ReturnsAsync(stockPrices);

            // Assert
            await Assert.ThrowsExceptionAsync<NullReferenceException>(
                async () => await _stockPriceServiceMock.CalculateStockAveragesByCompanyID(invalidCompanyID)
            );
        }

        [TestMethod]
        public async Task GetAllStockPriceService_ValidData_ReturnsStockPrices()
        {
            // Arrange
            var expectedStockPrices = new List<StockPrice>
            {
                new StockPrice { Id = 1, CompanyId = 1, CurrentStockPrice = 100.0 },
                new StockPrice { Id = 2, CompanyId = 2, CurrentStockPrice = 50.0 },
                new StockPrice { Id = 3, CompanyId = 3, CurrentStockPrice = 75.0 },
            };

            _stockPriceRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(expectedStockPrices);

            // Act
            var result = await _stockPriceServiceMock.GetAllStockPriceService();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedStockPrices.Count, result.Count);
            CollectionAssert.AreEqual(expectedStockPrices, result.ToList());
        }

        [TestMethod]
        public async Task GetAllStockPriceService_EmptyData_ReturnsEmptyList()
        {
            // Arrange
            var expectedStockPrices = new List<StockPrice>();

            _stockPriceRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(expectedStockPrices);

            // Act
            var result = await _stockPriceServiceMock.GetAllStockPriceService();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedStockPrices.Count, result.Count);
        }

        [TestMethod]
        public async Task GetCurrentStockPriceDetails_ValidCompanyId_ReturnsCurrentStockPriceDetails()
        {
            // Arrange
            int companyId = 1; // Valid company ID
            var expectedStockPriceDetails = new CurrentStockPriceDTO
            {
                CurrentStockPrice = 100.0,
                UpdatedStockPercent = 2.5,
                UpdatedStockPrice = 102.5,
                Date = DateTime.Now
            };

            var stockPrice = new StockPrice
            {
                CompanyId = companyId,
                CurrentStockPrice = expectedStockPriceDetails.CurrentStockPrice,
                UpdatedStockPercent = expectedStockPriceDetails.UpdatedStockPercent,
                UpdatedStockPrice = expectedStockPriceDetails.UpdatedStockPrice,
                Date = expectedStockPriceDetails.Date
            };
            _stockPriceRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<StockPrice> { stockPrice });

            // Act
            var result = await _stockPriceServiceMock.GetCurrentStockPriceDetails(companyId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedStockPriceDetails.CurrentStockPrice, result.CurrentStockPrice);
            Assert.AreEqual(expectedStockPriceDetails.UpdatedStockPercent, result.UpdatedStockPercent);
            Assert.AreEqual(expectedStockPriceDetails.UpdatedStockPrice, result.UpdatedStockPrice);
            Assert.AreEqual(expectedStockPriceDetails.Date, result.Date);
        }


        [TestMethod]
        public async Task GetCurrentStockPriceDetails_InvalidCompanyId_ThrowsNullReferenceException()
        {
            // Arrange
            int companyId = 999; 
            var stockPrices = new List<StockPrice>();

            _stockPriceRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(stockPrices);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<NullReferenceException>(async () =>
            {
                var result = await _stockPriceServiceMock.GetCurrentStockPriceDetails(companyId);
            });
        }

        [TestMethod]
        public async Task GetInitialValueOfTheStock_ValidStockId_ReturnsInitialValue()
        {
            // Arrange
            int stockId = 1;
            var yesterday = DateTime.Today.AddDays(-1);

            var stockPrice1 = new StockPrice
            {
                Id = 1,
                StockTransactions = new List<StockTransaction>
                {
                    new StockTransaction { Date = yesterday.AddDays(-2), StockValue = 95.0 },
                    new StockTransaction { Date = yesterday.AddDays(-1), StockValue = 100.0 },
                    new StockTransaction { Date = yesterday, StockValue = 105.0 }
                }
            };
            var stockPrice2 = new StockPrice
            {
                Id = 2,
                StockTransactions = new List<StockTransaction>
                {
                    new StockTransaction { Date = yesterday.AddDays(-2), StockValue = 85.0 },
                    new StockTransaction { Date = yesterday.AddDays(-1), StockValue = 90.0 },
                    new StockTransaction { Date = yesterday, StockValue = 95.0 }
                }
            };

            _stockPriceRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<StockPrice> { stockPrice1, stockPrice2 });

            // Act
            var result = await _stockPriceServiceMock.GetInitialValueOfTheStock(stockId);

            // Assert
            Assert.AreEqual(105.0, result);
        }

        [TestMethod]
        public async Task GetInitialValueOfTheStock_InvalidStockId_ReturnsDefaultValue()
        {
            // Arrange
            int stockId = 999;
            var yesterday = DateTime.Today.AddDays(-1);

            var stockPrice1 = new StockPrice
            {
                Id = 1,
                StockTransactions = new List<StockTransaction>
        {
            new StockTransaction { Date = yesterday.AddDays(-2), StockValue = 95.0 },
            new StockTransaction { Date = yesterday.AddDays(-1), StockValue = 100.0 },
            new StockTransaction { Date = yesterday, StockValue = 105.0 }
        }
            };
            var stockPrice2 = new StockPrice
            {
                Id = 2,
                StockTransactions = new List<StockTransaction>
        {
            new StockTransaction { Date = yesterday.AddDays(-2), StockValue = 85.0 },
            new StockTransaction { Date = yesterday.AddDays(-1), StockValue = 90.0 },
            new StockTransaction { Date = yesterday, StockValue = 95.0 }
        }
            };

            _stockPriceRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<StockPrice> { stockPrice1, stockPrice2 });

            // Act
            var result = await _stockPriceServiceMock.GetInitialValueOfTheStock(stockId);

            // Assert
            Assert.AreEqual(0.0, result);
        }

        [TestMethod]
        public async Task UpdateCurrentStockValue_ValidDTO_ReturnsUpdatedStockPrice()
        {
            // Arrange
            var stockId = 1;
            var companyId = 1001;
            var initialStockValue = 50.0;
            var stockPriceChangeDTO = new StockPriceUpdateDTO
            {
                companyId = companyId,
                UpdatedStockPrice = 2.0,
                Date = DateTime.Today
            };

            var stockPrice = new StockPrice
            {
                Id = stockId,
                CompanyId = companyId,
                CurrentStockPrice = initialStockValue,
                UpdatedStockPrice = 0.0,
                UpdatedStockPercent = 0.0,
                Date = DateTime.Now.AddDays(-1),
                StockTransactions = new List<StockTransaction>
                {
                        new StockTransaction
                        {
                            Id = 1,
                            StockId = 1,
                            StockValue = 60.75,
                            Date = DateTime.Today
                        },
                        new StockTransaction
                        {
                            Id = 2,
                            StockId = 1,
                            StockValue = 50.25,
                            Date = DateTime.Today
                        }
                }
            };

            var stockDetails = new List<StockPrice>
            {
                 stockPrice
            };

            _stockPriceRepositoryMock.Setup(s => s.GetAll()).ReturnsAsync(stockDetails);
            _stockPriceRepositoryMock.Setup(s => s.Get(stockId)).ReturnsAsync(stockPrice);
            _stockPriceRepositoryMock.Setup(s => s.Update(stockPrice)).ReturnsAsync(stockPrice);
            _stockTransactionRepositoryMock.Setup(s => s.Add(It.IsAny<StockTransaction>())).ReturnsAsync(new StockTransaction());

            // Act
            var result = await _stockPriceServiceMock.UpdateCurrentStockValue(stockPriceChangeDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(initialStockValue + stockPriceChangeDTO.UpdatedStockPrice, result.CurrentStockPrice);
        }

        [TestMethod]
        public async Task GetYearHigh_ValidStockId_ReturnsYearHigh()
        {
            // Arrange
            var stockId = 1;
            var expectedYearHigh = 60.75;
            var stockDetails = new List<StockPrice>
            {
                new StockPrice
                {
                    Id = 1,
                    CompanyId = 1001,
                    CurrentStockPrice = 50.25,
                    UpdatedStockPrice = 52.10,
                    UpdatedStockPercent = 3.5,
                    Date = DateTime.Now.AddDays(-1),
                    StockTransactions = new List<StockTransaction>
                    {
                        new StockTransaction
                        {
                            Id = 1,
                            StockId = 1,
                            StockValue = 60.75,
                            Date = DateTime.Now.AddDays(-2)
                        },
                        new StockTransaction
                        {
                            Id = 2,
                            StockId = 1,
                            StockValue = 52.10,
                            Date = DateTime.Now.AddDays(-1)
                        }
                    }
                }
            };
            _stockPriceRepositoryMock.Setup(s => s.GetAll()).ReturnsAsync(stockDetails);

            // Act
            var result = await _stockPriceServiceMock.GetYearHigh(stockId);

            // Assert
            Assert.AreEqual(expectedYearHigh, result);
        }

        [TestMethod]
        public async Task GetYearHigh_InvalidStockId_ReturnsZero()
        {
            // Arrange
            var stockId = 999;
            var expectedYearHigh = 0.0;
            var stockDetails = new List<StockPrice>
            {
                new StockPrice
                {
                    Id = 1,
                    CompanyId = 1001,
                    CurrentStockPrice = 50.25,
                    UpdatedStockPrice = 52.10,
                    UpdatedStockPercent = 3.5,
                    Date = DateTime.Now.AddDays(-1),
                    StockTransactions = new List<StockTransaction>
                    {
                        new StockTransaction
                        {
                            Id = 1,
                            StockId = 1,
                            StockValue = 60.75,
                            Date = DateTime.Now.AddDays(-2)
                        },
                        new StockTransaction
                        {
                            Id = 2,
                            StockId = 1,
                            StockValue = 52.10,
                            Date = DateTime.Now.AddDays(-1)
                        }
                    }
                }
            };
            _stockPriceRepositoryMock.Setup(s => s.GetAll()).ReturnsAsync(stockDetails);

            // Act
            var result = await _stockPriceServiceMock.GetYearHigh(stockId);

            // Assert
            Assert.AreEqual(expectedYearHigh, result);
        }

        [TestMethod]
        public async Task GetYearLow_ValidStockId_ReturnsYearLow()
        {
            // Arrange
            var stockId = 1;
            var expectedYearLow = 50.25;
            var stockDetails = new List<StockPrice>
            {
                new StockPrice
                {
                    Id = 1,
                    CompanyId = 1001,
                    CurrentStockPrice = 50.25,
                    UpdatedStockPrice = 52.10,
                    UpdatedStockPercent = 3.5,
                    Date = DateTime.Now.AddDays(-1),
                    StockTransactions = new List<StockTransaction>
                    {
                        new StockTransaction
                        {
                            Id = 1,
                            StockId = 1,
                            StockValue = 60.75,
                            Date = DateTime.Now.AddDays(-2)
                        },
                        new StockTransaction
                        {
                            Id = 2,
                            StockId = 1,
                            StockValue = 50.25,
                            Date = DateTime.Now.AddDays(-1)
                        }
                    }
                }
            };
            _stockPriceRepositoryMock.Setup(s => s.GetAll()).ReturnsAsync(stockDetails);

            // Act
            var result = await _stockPriceServiceMock.GetYearLow(stockId);

            // Assert
            Assert.AreEqual(expectedYearLow, result);
        }
        [TestMethod]
        public async Task GetYearLow_InvalidStockId_ReturnsZero()
        {
            // Arrange
            var stockId = 999;
            var expectedYearLow = 0.0;
            var stockDetails = new List<StockPrice>
            {
                new StockPrice
                {
                    Id = 1,
                    CompanyId = 1001,
                    CurrentStockPrice = 50.25,
                    UpdatedStockPrice = 52.10,
                    UpdatedStockPercent = 3.5,
                    Date = DateTime.Now.AddDays(-1),
                    StockTransactions = new List<StockTransaction>
                    {
                        new StockTransaction
                        {
                            Id = 1,
                            StockId = 1,
                            StockValue = 60.75,
                            Date = DateTime.Now.AddDays(-2)
                        },
                        new StockTransaction
                        {
                            Id = 2,
                            StockId = 1,
                            StockValue = 50.25,
                            Date = DateTime.Now.AddDays(-1)
                        }
                    }
                }
            };
            _stockPriceRepositoryMock.Setup(s => s.GetAll()).ReturnsAsync(stockDetails);

            // Act
            var result = await _stockPriceServiceMock.GetYearLow(stockId);

            // Assert
            Assert.AreEqual(expectedYearLow, result);
        }

        [TestMethod]
        public async Task GetTodayHigh_ValidStockId_ReturnsTodayHigh()
        {
            // Arrange
            var stockId = 1;
            var expectedTodayHigh = 52.10;
            var stockDetails = new List<StockPrice>
            {
                new StockPrice
                {
                    Id = 1,
                    CompanyId = 1001,
                    CurrentStockPrice = 50.25,
                    UpdatedStockPrice = 52.10,
                    UpdatedStockPercent = 3.5,
                    Date = DateTime.Now.AddDays(-1),
                    StockTransactions = new List<StockTransaction>
                    {
                        new StockTransaction
                        {
                            Id = 1,
                            StockId = 1,
                            StockValue = 52.10,
                            Date = DateTime.Today
                        },
                        new StockTransaction
                        {
                            Id = 2,
                            StockId = 1,
                            StockValue = 50.25,
                            Date = DateTime.Today
                        }
                    }
                }
            };
            _stockPriceRepositoryMock.Setup(s => s.GetAll()).ReturnsAsync(stockDetails);

            // Act
            var result = await _stockPriceServiceMock.GetTodayHigh(stockId);

            // Assert
            Assert.AreEqual(expectedTodayHigh, result);
        }

        [TestMethod]
        public async Task GetTodayHigh_InvalidStockId_ReturnsZero()
        {
            // Arrange
            var stockId = 999;
            var expectedTodayHigh = 0.0;
            var stockDetails = new List<StockPrice>
            {
                new StockPrice
                {
                    Id = 1,
                    CompanyId = 1001,
                    CurrentStockPrice = 50.25,
                    UpdatedStockPrice = 52.10,
                    UpdatedStockPercent = 3.5,
                    Date = DateTime.Now.AddDays(-1),
                    StockTransactions = new List<StockTransaction>
                    {
                        new StockTransaction
                        {
                            Id = 1,
                            StockId = 1,
                            StockValue = 60.75,
                            Date = DateTime.Today
                        },
                        new StockTransaction
                        {
                            Id = 2,
                            StockId = 1,
                            StockValue = 50.25,
                            Date = DateTime.Today
                        }
                    }
                }
            };
            _stockPriceRepositoryMock.Setup(s => s.GetAll()).ReturnsAsync(stockDetails);

            // Act
            var result = await _stockPriceServiceMock.GetTodayHigh(stockId);

            // Assert
            Assert.AreEqual(expectedTodayHigh, result);
        }

        [TestMethod]
        public async Task GetTodayLow_ValidStockId_ReturnsTodayLow()
        {
            // Arrange
            var stockId = 1;
            var expectedTodayLow = 50.25;
            var stockDetails = new List<StockPrice>
            {
                new StockPrice
                {
                    Id = 1,
                    CompanyId = 1001,
                    CurrentStockPrice = 50.25,
                    UpdatedStockPrice = 52.10,
                    UpdatedStockPercent = 3.5,
                    Date = DateTime.Now.AddDays(-1),
                    StockTransactions = new List<StockTransaction>
                    {
                        new StockTransaction
                        {
                            Id = 1,
                            StockId = 1,
                            StockValue = 60.75,
                            Date = DateTime.Today
                        },
                        new StockTransaction
                        {
                            Id = 2,
                            StockId = 1,
                            StockValue = 50.25,
                            Date = DateTime.Today
                        }
                    }
                }
            };
            _stockPriceRepositoryMock.Setup(s => s.GetAll()).ReturnsAsync(stockDetails);

            // Act
            var result = await _stockPriceServiceMock.GetTodayLow(stockId);

            // Assert
            Assert.AreEqual(expectedTodayLow, result);
        }

        [TestMethod]
        public async Task UpdateStockPriceService_ValidStockPrice_ReturnsUpdatedStockPrice()
        {
            // Arrange
            var existingStockPrice = new StockPrice
            {
                Id = 1,
                CompanyId = 1001,
                CurrentStockPrice = 50.25, 
                UpdatedStockPrice = 52.10,
                UpdatedStockPercent = 3.5,
                Date = DateTime.Now,
                StockTransactions = new List<StockTransaction>
                {
                    new StockTransaction
                    {
                        Id = 1,
                        StockId = 1,
                        StockValue = 50.25,
                        Date = DateTime.Now.AddDays(-1)
                    },
                    new StockTransaction
                    {
                        Id = 2,
                        StockId = 1,
                        StockValue = 52.10,
                        Date = DateTime.Now
                    }
                }
            };

            var stockPrice = new StockPrice
            {
                Id = 1,
                CompanyId = 1001,
                CurrentStockPrice = 50.25,
                UpdatedStockPrice = 52.10,
                UpdatedStockPercent = 3.5,
                Date = DateTime.Now,
                StockTransactions = new List<StockTransaction>
                {
                    new StockTransaction
                    {
                        Id = 1,
                        StockId = 1,
                        StockValue = 50.25,
                        Date = DateTime.Now.AddDays(-1)
                    },
                    new StockTransaction
                    {
                        Id = 2,
                        StockId = 1,
                        StockValue = 52.10,
                        Date = DateTime.Now
                    }
                }
            };

            _stockPriceRepositoryMock.Setup(repo => repo.Get(stockPrice.Id))
                .ReturnsAsync(existingStockPrice);
            _stockPriceRepositoryMock.Setup(repo => repo.Update(It.IsAny<StockPrice>()))
                .ReturnsAsync((StockPrice updatedStockPrice) => updatedStockPrice);
            _mapperServiceMock.Setup(mapper => mapper.StockPriceMapper(stockPrice, existingStockPrice))
                .ReturnsAsync(existingStockPrice);


            // Act
            var updatedStockPrice = await _stockPriceServiceMock.UpdateStockPriceService(stockPrice);

            // Assert
            Assert.IsNotNull(updatedStockPrice);
            Assert.AreEqual(stockPrice.Id, updatedStockPrice.Id);
            Assert.AreEqual(stockPrice.CompanyId, updatedStockPrice.CompanyId);
            Assert.AreEqual(stockPrice.CurrentStockPrice, updatedStockPrice.CurrentStockPrice);
        }

        [TestMethod]
        public async Task GetStockPriceService_ValidStockPriceId_ReturnsStockPrice()
        {
            // Arrange
            int stockPriceId = 1;
            var expectedStockPrice = new StockPrice { Id = stockPriceId, CurrentStockPrice = 105.0 };

            _stockPriceRepositoryMock.Setup(repo => repo.Get(stockPriceId)).ReturnsAsync(expectedStockPrice);

            // Act
            var result = await _stockPriceServiceMock.GetStockPriceService(stockPriceId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(stockPriceId, result.Id);
        }

    }
}