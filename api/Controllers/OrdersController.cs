using Microsoft.AspNetCore.Mvc;
using mormordagnysbageri_del1_api.Interfaces;
using mormordagnysbageri_del1_api.ViewModel.SalesOrder;

namespace mormordagnysbageri_del1_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpPost()]
    public async Task<IActionResult> AddOrder(SalesOrderPostViewModel model)
    {
        try
        {
            if (await _unitOfWork.OrderRepository.Add(model))
            {
                if (await _unitOfWork.Complete())
                {
                    return StatusCode(201);
                }
                else
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

    [HttpGet()]
    public async Task<IActionResult> ListAllOrders()
    {
        try
        {
            return Ok(new { success = true, statusCode = 200, data = await _unitOfWork.OrderRepository.List() });
        }
        catch (Exception ex)
        {
            return NotFound($"Tyvärr hittade vi inget {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> FindOrder(int id)
    {
        try
        {
            var order = await _unitOfWork.OrderRepository.Find(id);
            return Ok(new { success = true, StatusCode = 200, data = await _unitOfWork.OrderRepository.Find(id) });

        }
        catch (Exception ex)
        {

            return NotFound(new { success = false, message = ex.Message });

        }
    }

    [HttpGet("orderDate")]
    public async Task<IActionResult>FindOrder(DateTime orderDate)
    {
        
        try
        {
            var orders = await _unitOfWork.OrderRepository.Find(orderDate);
            if(orders == null)
            {
            return NotFound(new { success = false, message = $"Inga ordrar hittades för datumet {orderDate}" });

            }
            return Ok(new { success = true, StatusCode = 200, data = orders});

        }
        catch (Exception ex)
        {

            return StatusCode(500, new { success = false, message = ex.Message });

        }
    }
    
} 
