using Business.Abstract;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
         private IInvoiceService rep;

        public InvoiceController(IInvoiceService rep)
        {
            this.rep = rep;
        }
        [HttpGet]
        public IActionResult GetList(int id)
        {
            var values = rep.GetList(id);
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
        [HttpGet("Update/{id}")]

         public IActionResult Update(int id)
        {
            if (rep.GetById(id)!=null)
            {
                var values = rep.PaymentInvoice(id);
                return Ok(values);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
