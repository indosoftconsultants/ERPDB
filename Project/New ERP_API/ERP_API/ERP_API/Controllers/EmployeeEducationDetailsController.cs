﻿using CustomerAPI.Models;
using ERP_API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeEducationDetailsController : ControllerBase
    {
        // GET: api/<EmployeeEducationDetailsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<EmployeeEducationDetailsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EmployeeEducationDetailsController>
        [HttpPost]
        public PostResult Post([FromBody] EmployeeEducationDetailsModel value)
        {
            try
            {
                if (value != null)
                {
                    if (EmployeeEducationDetailsModel.Insert(value))
                    {
                        return new PostResult(true);
                        // var messaGE=Request.Create
                    }
                    else
                        return new PostResult(false);
                }
                return new PostResult(EmployeeEducationDetailsModel.errorMsg);
            }
            catch (Exception ex)
            {
                return new PostResult("Error : " + ex.Message);
            }
        }

        // PUT api/<EmployeeEducationDetailsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeEducationDetailsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
