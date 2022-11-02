using AutoMapper;
using BuyMyHouse.Services.Abstract;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Requests;

namespace BuyMyHouse.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IValidator<User> _userValidator;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IValidator<User> userValidator, IMapper mapper)
    {
        _userService = userService;
        _userValidator = userValidator;
        _mapper = mapper;
    }

    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _userService.GetAllUsers());
    }

    [HttpGet("GetUser")]
    public async Task<IActionResult> GetUser(int id)
    {
        return Ok(await _userService.GetById(id));
    }
    
    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser(UserRequest req)
    {
        User newUser = _mapper.Map<User>(req);

        var existingUser = await _userService.GetByEmail(req.Email);
        if (existingUser != null)
        {
            return BadRequest("User already exists!");
        }

        var validation = _userValidator.Validate(newUser);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors);
        }

        return Ok(await _userService.AddAsync(newUser));
    }

    [HttpPut("EditUser")]
    public async Task<IActionResult> EditUser(int id, UserRequest req)
    {
        var user = await _userService.GetById(id);
        if (user == null)
        {
            return NotFound("User not found!");    
        }
        
        var updatedUser = _mapper.Map<User>(req);
        var validation = _userValidator.Validate(updatedUser);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors);
        }

        return Ok(await _userService.UpdateAsync(user, updatedUser));
    }

    [HttpDelete("DeleteUser")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _userService.GetById(id);

        if (user != null)
        {
            await _userService.RemoveAsync(user);
            return Ok("User deleted");
        }

        return NotFound("User does not exist");
    }
}