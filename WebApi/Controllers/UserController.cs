using Business.Abstract;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService rep;
        private IInvoiceService repInvo;

        public UserController(IUserService rep, IInvoiceService repInvo)
        {
            this.rep = rep;
            this.repInvo = repInvo;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var values = rep.GetList();
            return Ok(values);

        }
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var values = rep.GetById(id);
            if (values != null)
            {
                return Ok(values);
            }
            return BadRequest(ModelState);
        }
        [HttpPost]
        public IActionResult Create(UserEntity entites)
        {
             if (ModelState.IsValid)
            {
                 var value = rep.CreateUser(entites);
                  return Ok(value);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Update(UserEntity entites)
        {
            if (rep.GetById(entites.Id) != null)
            {
                var values = rep.UpdateUser(entites);
                return Ok(values);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
