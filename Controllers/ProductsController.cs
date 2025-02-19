using Microsoft.AspNetCore.Mvc;
using mormordagnysbageri_del1_api.Interfaces;
using mormordagnysbageri_del1_api.ViewModel.Product;

namespace mormordagnysbageri_del1_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpPost()]
    public async Task<IActionResult> AddProduct(ProductViewModel model)
    {
        try
        {
            if (await _unitOfWork.ProductRepository.Add(model))
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

    [HttpGet("{id}")]
    public async Task<IActionResult> FindProduct(int id)
    {
        try
        {
            var product = await _unitOfWork.ProductRepository.Find(id);
            return Ok(new { success = true, StatusCode = 200, data = await _unitOfWork.ProductRepository.Find(id) });

        }
        catch (Exception ex)
        {

            return NotFound(new { success = false, message = ex.Message });

        }
    }

    [HttpGet()]
    public async Task<IActionResult> ListAllProducts()
    {
        try
        {
            return Ok(new { success = true, statusCode = 200, data = await _unitOfWork.ProductRepository.List() });

        }
        catch (Exception ex)
        {

            return NotFound($"Tyvärr hittade vi inget {ex.Message}");

        }
    }
    [HttpPatch("{id}/price")]
    public async Task<IActionResult> Update(int id, ProductViewModel model)
    {
        try
        {
            if (await _unitOfWork.ProductRepository.Update(id, model))
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
