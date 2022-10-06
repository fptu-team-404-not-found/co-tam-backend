﻿using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;
using System.ComponentModel.DataAnnotations;

namespace CoTamApp.Controllers
{
    /// <summary>
    /// Everything about customers.
    /// </summary>
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Get a list of customers.
        /// </summary>
        /// 
        /// <returns>A list of customers.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a list of customers.
        /// - Sample request: GET /api/customers
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="404">List of customers not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(Response<List<Customer>>), 200)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<Response<List<Customer>>>> GetListCustomers([FromBody] Pagination pagination)
        {
            try
            {
                var response = await _customerService.GetReponseCustomers(pagination.PageIndex, pagination.PageSize);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Get a specific customer.
        /// </summary>
        /// 
        /// <param name="id">
        /// Customer Id which is needed for finding a customer.
        /// </param>
        /// 
        /// <returns>A specific customer by Id.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a specific customer by Id.
        /// - Sample request: GET /api/customers/1
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="400">If Invalid Id supplied</response>
        /// <response code="404">Customer not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(Response<Customer>), 200)]
        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<Customer>>> GetCustomerById(string id)
        {
            try
            {
                var response = await _customerService.GetReponseCustomereById(id);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Create a new customer.
        /// </summary>
        /// 
        /// <param name="customer">
        /// Customer object that needs to be created.
        /// </param>
        /// 
        /// <returns>A new customer.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a new customer.
        /// - Sample request: POST /api/customers
        ///     
        ///       {
        ///           "name": "string",
        ///           "description": "string",
        ///           "price": 0
        ///       }
        ///     
        /// </remarks>
        /// 
        /// <response code="201">Successfully</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Response<Customer>), 201)]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult<Response<Customer>>> CreateACustomer([Required][FromBody] Customer customer)
        {
            try
            {
                var response = await _customerService.GetResponseCreateACustomer(customer);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Update an existing customer.
        /// </summary>
        /// 
        /// <param name="id">
        /// Customer Id which is needed for updating a customer.
        /// </param>
        /// 
        /// <param name="customer">
        /// Customer object that needs to be updated.
        /// </param>
        /// 
        /// <returns>An update existing customer.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return an update existing customer.
        /// - Sample request: PUT /api/customers/{id}
        /// - Sample request body: 
        ///     
        ///       {
        ///           "name": "string",
        ///           "description": "string",
        ///           "price": 0
        ///       }
        ///     
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="400">If Invalid ID supplied</response>
        /// <response code="404">Customer not found</response>
        /// <response code="500">Internal server error</response>
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Response<Customer>), 200)]
        [Produces("application/json")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Response<Customer>>> UpdateCustomer(string id, [Required][FromBody] Customer customer)
        {
            try
            {
                var response = await _customerService.GetReponseUpdateCustomer(id, customer);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Change status of a specific customer.
        /// </summary>
        /// 
        /// <param name="id">
        /// Customer Id which is needed for deleting a customer.
        /// </param>
        /// 
        /// <returns>Status change action status.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return change Status action status.
        /// - Sample request: DELETE /api/customers/1
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="400">If Invalid Id supplied</response>
        /// <response code="404">Customer not found</response>
        /// <response code="500">Internal server error</response>
        [Produces("application/json")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<Customer>>> DeleteCustomer(string id)
        {
            try
            {
                var response = await _customerService.GetReponseChangeStatusCustomer(id);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Get the amount of customers.
        /// </summary>
        /// 
        /// <returns>A number of customers.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a number of customers.
        /// - Sample request: GET /api/customers
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="404">List of customers not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(Response<int>), 200)]
        [Produces("application/json")]
        [HttpGet("count")]
        public async Task<ActionResult<Response<int>>> CountCustomers()
        {
            try
            {
                var response = await _customerService.GetResponseCustomerNumber();
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}