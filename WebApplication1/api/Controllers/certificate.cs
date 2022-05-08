using db;
using WebApplication1.models;

namespace WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]")]
public class certificate_controller: ControllerBase
{
    protected context _context;
    public certificate_controller(context dataAccessProvider)  
    {  
        _context = dataAccessProvider;  
    }  
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(_context.Certificates.ToList());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody]Certificate new_certificate)
    {
        if (ModelState.IsValid)  
        {    
            _context.Certificates.Add(new_certificate);
            _context.SaveChanges();
            return Ok(new_certificate);  
        }  
        return BadRequest();  
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> detail_get(int id)
    {
        var ans = _context.Certificates.FirstOrDefault(obj => obj.id == id);
        if (ans == null)
            return NotFound(404);
        return Ok(ans);
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> detail_put(int id, [FromBody]Certificate update_certificate)
    {
        var ans = _context.Certificates.FirstOrDefault(obj => obj.id == id);
        
        if (ans == null)
            return NotFound();
        foreach (var property in update_certificate.GetType().GetProperties())
        {
            if(property.Name != "id")
                property.SetValue(ans, property.GetValue(update_certificate));
        }
        _context.SaveChanges();
        return Ok(ans);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> detail_delete(int id)
    {
        var ans = _context.Certificates.FirstOrDefault(obj => obj.id == id);
        if (ans == null)
            return NotFound();
        _context.Remove(ans);
        _context.SaveChanges();
        return Ok(ans);
    }
}

