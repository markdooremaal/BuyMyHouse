using AutoMapper;
using BuyMyHouse.Helpers.Abstract;
using BuyMyHouse.Services.Abstract;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Requests;

namespace BuyMyHouse.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HouseController : ControllerBase
{
    private readonly IHouseService _houseService;
    private readonly IValidator<House> _houseValidator;
    private readonly IMapper _mapper;
    private readonly IUploadImageHelper _uploadImageHelper;

    public HouseController(IHouseService houseService, IValidator<House> houseValidator, IMapper mapper, IUploadImageHelper uploadImageHelper)
    {
        _houseService = houseService;
        _houseValidator = houseValidator;
        _mapper = mapper;
        _uploadImageHelper = uploadImageHelper;
    }
    
    [HttpGet("GetAllHouses")]
    public async Task<IActionResult> GetAllHouses()
    {
        return Ok(await _houseService.GetAllHouses());
    }

    [HttpGet("GetHouse")]
    public async Task<IActionResult> GetHouse(int id)
    {
        var house = await _houseService.GetById(id);
        if (house != null)
        {
            return NotFound("House not found!");
        }

        return Ok(house);
    }

    [HttpGet("GetHousesWithinPriceRange")]
    public async Task<IActionResult> GetHousesWithinPriceRange(double min = 0, double max = 5000000)
    {
        return Ok(await _houseService.GetWithinPriceRange(min, max));
    }

    [HttpPost("AddPictureToHouse")]
    public async Task<IActionResult> AddPictureToHouse(IFormFile image, int houseId)
    {
        var house = await _houseService.GetById(houseId);
        if (house != null)
        {
            return NotFound("House not found!");
        }
        
        var url = await _uploadImageHelper.Upload(image);

        var newHouse = house;
        newHouse.Picture = url;
        await _houseService.UpdateAsync(house, newHouse);

        return Ok(newHouse);
    }

    [HttpPost("CreateHouse")]
    public async Task<IActionResult> CreateHouse(HouseRequest req)
    {
        var newHouse = _mapper.Map<House>(req);

        var existingHouse = await _houseService.GetByTitle(req.Title);
        if (existingHouse != null)
        {
            return BadRequest("House already exists!");
        }

        var validation = _houseValidator.Validate(newHouse);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors);
        }

        return Ok(await _houseService.AddAsync(newHouse));
    }

    [HttpPut("EditHouse")]
    public async Task<IActionResult> EditHouse(int id, HouseRequest req)
    {
        var house = await _houseService.GetById(id);
        if (house == null)
        {
            return NotFound("House not found!");    
        }
        
        var updatedHouse = _mapper.Map<House>(req);
        var validation = _houseValidator.Validate(updatedHouse);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors);
        }

        return Ok(await _houseService.UpdateAsync(house, updatedHouse));
    }

    [HttpDelete("DeleteHouse")]
    public async Task<IActionResult> DeleteHouse(int id)
    {
        var house = await _houseService.GetById(id);

        if (house != null)
        {
            await _houseService.RemoveAsync(house);
            return Ok("House deleted");
        }

        return NotFound("House does not exist");
    }
    
    
}