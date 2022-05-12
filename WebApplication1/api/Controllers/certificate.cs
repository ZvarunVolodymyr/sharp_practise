using db;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]

    public async Task<IActionResult> Get(string? sort_by = null, string? sort_type = null, string? search = null)
    {
        sort_type = sort_type ?? "desc";
        var ans = _context.Certificates.Where(x => x.username == User.Identity.Name).ToList();
        Console.WriteLine(search);
        if (search != null)
        {
            List<Certificate> new_ans = new List<Certificate>();
            foreach (var certificate in ans)
                foreach (var prop in typeof(Certificate).GetProperties())
                    if (prop.GetValue(certificate).ToString().Contains(search))
                    {
                        new_ans.Add(certificate);
                        break;
                    }

            ans = new_ans;
        }
        if (sort_by != null)
        {
            Func<object, object, int> compare;
            if (sort_type != "desc")
                compare = (a, b) => (a.ToString().ToLower()).CompareTo(b.ToString().ToLower());
            else
                compare = (a, b) => (b.ToString().ToLower()).CompareTo(a.ToString().ToLower());

            var prop_info = typeof(Certificate).GetProperty(sort_by);
            ans.Sort((a, b) => compare(prop_info.GetValue(a), prop_info.GetValue(b)));
        }
        return Ok(ans);
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]CertificateWithoutId new_certificate)
    {
        var certificate = new_certificate as Certificate;
        certificate.username = User.Identity.Name;
        if (ModelState.IsValid)  
        {
            // Console.WriteLine(new_certificate.username);
            _context.Certificates.Add(certificate);
            _context.SaveChanges();
            return Ok(certificate);  
        }  
        return BadRequest();  
    }
    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> detail_get(int id)
    {
        var ans = _context.Certificates.Where(x => x.username == User.Identity.Name)
            .FirstOrDefault(obj => obj.id == id);
        if (ans == null)
            return NotFound(404);
        return Ok(ans);
    }
    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> detail_put(int id, [FromBody]CertificateWithoutId update_certificate)
    {
        var ans = _context.Certificates.Where(x => x.username == User.Identity.Name)
            .FirstOrDefault(obj => obj.id == id);
        if (ans == null)
            return NotFound();
        foreach (var property in update_certificate.GetType().GetProperties())
        {
            if(!new []{"id", "username"}.Contains(property.Name))
                property.SetValue(ans, property.GetValue(update_certificate));
        }
        _context.SaveChanges();
        return Ok(ans);
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> detail_delete(int id)
    {
        var ans = _context.Certificates.Where(x => x.username == User.Identity.Name)
            .FirstOrDefault(obj => obj.id == id);
        if (ans == null)
            return NotFound();
        _context.Remove(ans);
        _context.SaveChanges();
        return Ok(ans);
    }
}

