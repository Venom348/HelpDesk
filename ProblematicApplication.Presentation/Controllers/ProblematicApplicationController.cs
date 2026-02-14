using HelpDesk.Contracts.Requests.ProblematicApplication;
using Microsoft.AspNetCore.Mvc;
using ProblematicApplication.Application.Exceptions;
using ProblematicApplication.Domain.Abstractions.Services;

namespace ProblematicApplication.Presentation.Controllers;

/// <summary>
///     Контроллер ProblematicApplication
/// </summary>
[ApiController]
[Route("api/problematicapplication")]
public class ProblematicApplicationController : ControllerBase
{
    private readonly IProblematicApplicationService _problematicApplicationService;

    public ProblematicApplicationController(IProblematicApplicationService problematicApplicationService)
    {
        _problematicApplicationService = problematicApplicationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int userId, int categoryId, int page = 0, int limit = 20)
    {
        try
        {
            var response = await _problematicApplicationService.GetAll(userId, categoryId, page, limit);
            return Ok(response);
        }
        catch (ProblematicApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var response = await _problematicApplicationService.GetId(id);
            return Ok(response);
        }
        catch (ProblematicApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PostProblematicApplicationRequest request)
    {
        try
        {
            var response = await _problematicApplicationService.Create(request);
            return Ok(response);
        }
        catch (ProblematicApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("watcher")]
    public async Task<IActionResult> AddWatcher([FromBody] AddWatcherRequest request)
    {
        try
        {
            await _problematicApplicationService.AddWatcher(request);
            return Ok();
        }
        catch (ProblematicApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] PatchProblematicApplicationRequest request)
    {
        try
        {
            var response = await _problematicApplicationService.Update(request);
            return Ok(response);
        }
        catch (ProblematicApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("status")]
    public async Task<IActionResult> UpdateStatus([FromBody] PatchProblematicApplicationStatusRequest request)
    {
        try
        {
            var response = await _problematicApplicationService.UpdateStatus(request);
            return Ok(response);
        }
        catch (ProblematicApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var response = await _problematicApplicationService.Delete(id);
            return Ok(response);
        }
        catch (ProblematicApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}