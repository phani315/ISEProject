using Kanini.InvestmentSearchEngine.StockValue.Interfaces;
using Kanini.InvestmentSearchEngine.StockValue.Models.Error;
using Kanini.InvestmentSearchEngine.StockValue.Models;
using Microsoft.AspNetCore.Mvc;
using Kanini.InvestmentSearchEngine.StockValue.Models.DTOs;
using System.Diagnostics.CodeAnalysis;
using Kanini.InvestmentSearchEngine.StockValue.Utilities.Exceptions;
using Kanini.InvestmentSearchEngine.StockValue.Utilities.Message;
using Microsoft.AspNetCore.Authorization;

namespace Kanini.InvestmentSearchEngine.StockValue.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class StockPriceController : ControllerBase
    {
        #region Private field
        private readonly IStockPriceService _stockPriceService;
        private readonly ILogger<StockPriceController> _logger;
        #endregion

        #region Constructor
        public StockPriceController(IStockPriceService stockPriceService,
                                    ILogger<StockPriceController> logger)
        {
            _stockPriceService = stockPriceService;
            _logger = logger;
        }
        #endregion

        #region Controller method for Adding Stock Price
        [HttpPost]
        [ProducesResponseType(typeof(StockPrice), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StockPrice>> AddStockPrice(StockPrice stockPrice)
        {
            try
            {
                var result = await _stockPriceService.AddStockPriceService(stockPrice);
                if (result != null)
                {
                    return Created("StockPrice", result);
                }
                return BadRequest(new Error(400, ResponseMessage.Messages[6]));
            }
            catch (UserException ue)
            {
                _logger.LogError(ue.Message);
                return BadRequest(new Error(400, ue.Message));
            }
            catch (AddObjectException ae)
            {
                _logger.LogError(ae.Message);
                return BadRequest(new Error(400, ae.Message));
            }
            catch (ContextException ce)
            {
                _logger.LogError(ce.Message);
                return BadRequest(new Error(400, ce.Message));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                var errorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
                return BadRequest(new Error(400,errorMessage));
            }
        }
        #endregion

        #region Controller method for Getting All Stock Price Details
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<StockPrice>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<StockPrice>>> GetAllStockPrices()
        {
            try
            {
                var result = await _stockPriceService.GetAllStockPriceService();
                if (result != null && result.Count != 0)
                {
                    return Ok(result);
                }
                return NotFound(new Error(404, ResponseMessage.Messages[7]));
            }
            catch (NullReferenceException ne)
            {
                _logger.LogError(ne.Message);
                return BadRequest(new Error(400, ne.Message));
            }
            catch (ContextException ce)
            {
                _logger.LogError(ce.Message);
                return BadRequest(new Error(400, ce.Message));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                var errorMessage = e.InnerException!=null?e.InnerException.Message : e.Message; 
                return BadRequest(new Error(400, errorMessage));
            }
        }
        #endregion

        #region Controller method for Get Stock Price By Id
        [HttpPost]
        [ProducesResponseType(typeof(StockPrice), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StockPrice>> GetStockPrice(int stockPriceId)
        {
            try
            {
                var result = await _stockPriceService.GetStockPriceService(stockPriceId);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound(new Error(404, ResponseMessage.Messages[2]));
            }
            catch (NullReferenceException ne)
            {
                _logger.LogError(ne.Message);
                return BadRequest(new Error(400, ne.Message));
            }
            catch (ContextException ce)
            {
                _logger.LogError(ce.Message);
                return BadRequest(new Error(400, ce.Message));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                var errorMessage = e.InnerException!=null?e.InnerException.Message:e.Message;
                return BadRequest(new Error(400, errorMessage));
            }
        }
        #endregion

        #region Controller method for Getting Stock Price Averages By Company Id
        [HttpPost]
        [ProducesResponseType(typeof(StockPriceAveragesDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StockPriceAveragesDTO>> GetStockDetailsAveragesCompanyID(int companyId)
        {
            try
            {
                var stockDTO = await _stockPriceService.CalculateStockAveragesByCompanyID(companyId);
                if (stockDTO == null)
                {
                    return NotFound("No Details are available at the moment");
                }
                return Ok(stockDTO);
            }
            catch (NullReferenceException ne)
            {
                _logger.LogError(ne.Message);
                return BadRequest(new Error(400, ne.Message));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                var errorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
                return BadRequest(new Error(400, errorMessage));
            }
        }
        #endregion

        #region Controller method for Getting Stock Price Details By Company Id
        [HttpPost]
        [ProducesResponseType(typeof(StockPrice), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StockPrice>> GetAllStockDetailsCompanyID(int companyId)
        {
            try
            {
                var stockDTO = await _stockPriceService.GetStockPriceByCompanyID(companyId);
                if (stockDTO == null)
                {
                    return NotFound("No Details are available at the moment");
                }
                return Ok(stockDTO);
            }
            catch (NullReferenceException ne)
            {
                _logger.LogError(ne.Message);
                return BadRequest(new Error(400, ne.Message));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                var errorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
                return BadRequest(new Error(400, errorMessage));
            }
        }
        #endregion

        #region Controller method for Getting Current Stock Price Details By Company Id
        [HttpPost]
        [ProducesResponseType(typeof(CurrentStockPriceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CurrentStockPriceDTO>> GetCurrentStockDetailsByCompanyID(int companyId)
        {
            try
            {
                var stockDTO = await _stockPriceService.GetCurrentStockPriceDetails(companyId);
                if (stockDTO == null)
                {
                    return NotFound("No Details are available at the moment");
                }
                return Ok(stockDTO);
            }
            catch (NullReferenceException ne)
            {
                _logger.LogError(ne.Message);
                return BadRequest(new Error(400, ne.Message));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                var errorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
                return BadRequest(new Error(400, errorMessage));
            }
        }
        #endregion

        #region Controller method for Updating Stock Price
        [HttpPut]
        [ProducesResponseType(typeof(StockPrice), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StockPrice>> UpdateStockPrice(StockPrice stockPrice)
        {
            try
            {
                var result = await _stockPriceService.UpdateStockPriceService(stockPrice);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound(new Error(404, ResponseMessage.Messages[8]));
            }
            catch (UpdateObjectException ue)
            {
                _logger.LogError(ue.Message);
                return BadRequest(new Error(400, ue.Message));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                var errorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
                return BadRequest(new Error(400, errorMessage));
            }
        }
        #endregion

        #region Controller method for Updating Current Stock Price
        [HttpPut]
        [ProducesResponseType(typeof(ActionResult<StockPrice>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StockPrice>> UpdateCurrentStockPrice(StockPriceUpdateDTO stockPriceUpdateDTO)
        {
            try
            {
                var result = await _stockPriceService.UpdateCurrentStockValue(stockPriceUpdateDTO);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("Unable to update stock details");
            }
            catch (NullReferenceException ne)
            {
                _logger.LogError(ne.Message);
                return BadRequest(new Error(400, ne.Message));
            }
            catch (UnableToUpdateCurrentStockPrice ue)
            {
                _logger.LogError(ue.Message);
                return BadRequest(new Error(400, ue.Message));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                var errorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
                return BadRequest(new Error(400, errorMessage));
            }
        }
        #endregion

        #region Controller method for Deleting Stock Price By Id
        [HttpDelete]
        [ProducesResponseType(typeof(StockPrice), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StockPrice>> DeleteStockPrice(int stockPriceId)
        {
            try
            {
                var result = await _stockPriceService.DeleteStockPriceService(stockPriceId);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound(new Error(404, ResponseMessage.Messages[9]));
            }
            catch (NullReferenceException ne)
            {
                _logger.LogError(ne.Message);
                return BadRequest(new Error(400, ne.Message));
            }
            catch (UnableToUpdateCurrentStockPrice ce)
            {
                _logger.LogError(ce.Message);
                return BadRequest(new Error(400, ce.Message));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                var errorMessage = e.InnerException != null? e.InnerException.Message: e.Message;
                return BadRequest(new Error(400, errorMessage));
            }
        }
        #endregion
    }
}
