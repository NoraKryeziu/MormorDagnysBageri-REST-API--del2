using Microsoft.AspNetCore.Mvc;
using mormordagnysbageri_del1_api.Interfaces;
using mormordagnysbageri_del1_api.ViewModel.Customer;

namespace mormordagnysbageri_del1_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpGet()]
    public async Task<IActionResult> List()
    {
        try
        {

            return Ok(new { success = true, statusCode = 200, data = await _unitOfWork.CustomerRepository.List() });
        }
        catch (Exception ex)
        {
            return NotFound($"Tyvärr hittade vi inget {ex.Message}");
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Find(int id)
    {
        try
        {
            var customer = await _unitOfWork.CustomerRepository.Find(id);
            return Ok(new { success = true, StatusCode = 200, data = await _unitOfWork.CustomerRepository.Find(id) });

        }
        catch (Exception ex)
        {

            return NotFound(new { success = false, message = ex.Message });

        }

    }

    [HttpPost()]
    public async Task<ActionResult> Add(CustomerPostViewModel model)
    {
        try
        {
            if (await _unitOfWork.CustomerRepository.Add(model))
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

    [HttpPatch("{id}/contactPerson")]
    public async Task<IActionResult> Update(int id, CustomerBaseViewModel model)
    {
        try
        {
            if(await _unitOfWork.CustomerRepository.Update(id, model))
            {
                if (_unitOfWork.HasChanges())
                {
                    await _unitOfWork.Complete();
                    return NoContent();
                }
                else 
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return StatusCode(500);
            }
        }
        catch (Exception ex)
        {
            
            return BadRequest(ex.Message);
        }
    }
}
