using System.Security.Claims;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;

using Saas.Dal;
using static Saas.Entity.Administrator.Restaurants.Types;

namespace Saas.Controllers
{
  [Route("[controller]")]
  [ApiController]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public class RestaurantController : ControllerBase
  {
    private readonly ISpContext spContext;
    private readonly ClaimsPrincipal debugUser;

    public RestaurantController(ISpContext dbcontext)
    {
      spContext = dbcontext;

      var claims = new List<Claim>
      {
        new Claim(ClaimTypes.GroupSid, spContext?.AppId().ToString(), ClaimValueTypes.Integer),
        new Claim(ClaimTypes.Role, Constant.ADMINISTRATOR, ClaimValueTypes.String)
      };
      var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
      debugUser = new ClaimsPrincipal(userIdentity);
    }

    // GET: Restaurant/
    [HttpGet]
    public ActionResult<IEnumerable<Restaurant>> Get()
    {
      using var db = spContext.SpROnly<Restaurant>(debugUser, OperationType.R);
      return (db == null) ? new ForbidResult() : new ActionResult<IEnumerable<Restaurant>>(db.ReadAsync().Result);
    }

    // GET: Restaurant/Lookup/value
    [HttpGet("Lookup/{value}", Name = "LookupRestaurant")]
    public ActionResult<IEnumerable<Restaurant>> Lookup(string value)
    {
      using var db = spContext.SpROnly<Restaurant>(User, OperationType.R);
      return (db == null) ? new ForbidResult() : new ActionResult<IEnumerable<Restaurant>>(db.ReadAsync(value).Result);
    }

    // GET: Restaurant/5
    [HttpGet("{id}", Name = "GetRestaurant")]
    public ActionResult<Restaurant> Get(int id)
    {
      using var db = spContext.SpROnly<Restaurant>(debugUser, OperationType.R);
      return (db == null) ? new ForbidResult() : new ActionResult<Restaurant>(db.ReadAsync(id).Result);
    }

    // POST: Restaurant
    [HttpPost]
    public IActionResult Post([FromBody] Restaurant value)
    {
      if (!(spContext.SpROnly<Restaurant>(User, OperationType.C) is ICrud<Restaurant> db))
        return new ForbidResult();

      return Ok(db.Create(value));
    }

    // PUT: Restaurant/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Restaurant value)
    {
      if (value != null && value.Id != id)
        return BadRequest();

      if (!(spContext.SpROnly<Restaurant>(User, OperationType.U) is ICrud<Restaurant> db))
        return new ForbidResult();

      return Ok(db.Update(value));
    }

    // DELETE: ApiWithActions/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      if (spContext.SpROnly<Restaurant>(User, OperationType.D) is ICrud<Restaurant> db)
        return Ok(db.Delete(id));

      return new ForbidResult();
    }
  }
}