using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using WebAPI_MongoBD.Models;

namespace WebAPI_MongoBD.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        DataAccess objds;

        public ProductAPIController(DataAccess _dataAccess)
        {
            objds = _dataAccess;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return objds.GetProducts();
        }
        [HttpGet("{id:length(24)}")]
        public IActionResult Get(string id)
        {
            var product = objds.GetProduct(new ObjectId(id));
            if (product == null)
            {
                return NotFound();
            }
            return new ObjectResult(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product p)
        {
            objds.Create(p);
            return new OkObjectResult(p);
        }
        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, [FromBody]Product p)
        {
            var recId = new ObjectId(id);
            var product = objds.GetProduct(recId);
            if (product == null)
            {
                return NotFound();
            }

            objds.Update(recId, p);
            return new OkResult();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var product = objds.GetProduct(new ObjectId(id));
            if (product == null)
            {
                return NotFound();
            }

            objds.Remove(product.Id);
            return new OkResult();
        }
    }
}