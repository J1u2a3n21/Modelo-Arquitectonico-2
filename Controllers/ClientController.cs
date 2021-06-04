using McNutsAPI.Exceptions;
using McNutsAPI.Models;
using McNutsAPI.Services;
using MCNutsAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McNutsAPI.Controllers
{
    [Route("api/peanuts/{peanutId:long}/[controller]")]
    public class ClientsController : ControllerBase
    {
        private IClientService _clientService;

        public ClientsController(IClientService clientsService)
        {
            _clientService = clientsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientModel>>> GetClientsAsync(long peanutId)
        {
            try
            {
                var clients = await _clientService.GetClientsAsync(peanutId);
                return Ok(clients);
            }
            catch (InvalidOperationClientException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso. ");
            }

        }

        [HttpGet("{ci:long}")]
        public async Task<IActionResult> GetClientAsync(long peanutId, long ci)
        {
            try
            {
                var client = await _clientService.GetClientAsync(peanutId, ci);
                return Ok(client);
            }
            catch (NotFoundClientException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso. ");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClientModel>> CreateClientAsync(long peanutId, [FromBody] ClientModel newClient)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var createdClient = await _clientService.CreateClientAsync(peanutId, newClient);
                return Created($"/api/peanuts/{peanutId}/clients/{createdClient.Ci}", createdClient);

            }
            catch (ClientExistException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso. ");
            }
        }

        [HttpDelete("{ci:long}")]
        public async Task<ActionResult<bool>> DeleteClientAsync(long peanutId, long ci)
        {
            try
            {
                var result = await _clientService.DeleteClientAsync(peanutId, ci);
                return Ok(result);
            }
            catch (NotFoundClientException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso. ");
            }
        }

        [HttpPut("{ci:long}")]
        public async Task<ActionResult<ClientModel>> UpdateClientAsync(long peanutId, long ci, [FromBody] ClientModel updateClient)
        {
            try
            {
                var clientUpdate = await _clientService.UpdateClientAsync(peanutId, ci, updateClient);
                return Ok(clientUpdate);
            }
            catch (NotFoundClientException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Algo Inesperado Paso. ");
            }
        }

        //Hacer un end point restablecer stock 

    }
}
