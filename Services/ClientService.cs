using AutoMapper;
using McNutsAPI.Data.Entities;
using McNutsAPI.Data.Repositories;
using McNutsAPI.Exceptions;
using McNutsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McNutsAPI.Services
{
    public class ClientService : IClientService
    {
        private IPeanutRepository _peanutRepository;
        private IMapper _mapper;
        public ClientService(IPeanutRepository peanutRepository, IMapper mapper)
        {
            _peanutRepository = peanutRepository;
            _mapper = mapper;
        }

        public async Task<ClientModel> CreateClientAsync(long peanutId, ClientModel newClient)
        {
            await ValidatePeanutAsync(peanutId);
            newClient.PeanutId = peanutId;
            var clientEntity = _mapper.Map<ClientEntity>(newClient);
            _peanutRepository.CreateClient(peanutId, clientEntity);
            var result = await _peanutRepository.SaveChangesAsync();
            if (!result)
            {
                throw new Exception("DataBase Error");
            }
            await _peanutRepository.UpdateStockAsync(peanutId, -newClient.CantidadCompra);
            return _mapper.Map<ClientModel>(clientEntity);
        }

        public async Task<bool> DeleteClientAsync(long peanutId, long ci)
        {
            await ValidateClientAndPeanutAsync(peanutId, ci);
            await _peanutRepository.DeleteClientAsync(peanutId, ci);
            var result = await _peanutRepository.SaveChangesAsync();
            if (!result)
            {
                throw new Exception("DataBase Error");
            }
            return true;
        }

        public async Task<ClientModel> GetClientAsync(long peanutId, long ci)
        {
            await ValidatePeanutAsync(peanutId);
            var client = await _peanutRepository.GetClientAsync(peanutId, ci);
            if (client == null)
            {
                throw new NotFoundClientException($"El cliente con el ci {ci} no compro el sabor del mani con codigo {peanutId}");
            }
            var clientModel = _mapper.Map<ClientModel>(client);
            clientModel.PeanutId = peanutId;
            return clientModel;
        }

        public async Task<IEnumerable<ClientModel>> GetClientsAsync(long peanutId)
        {
            await ValidatePeanutAsync(peanutId);
            var clients = await _peanutRepository.GetClientsAsync(peanutId);
            return _mapper.Map<IEnumerable<ClientModel>>(clients);
        }

        public async Task<ClientModel> UpdateClientAsync(long peanutId, long ci, ClientModel updateClient)
        {
            await ValidateClientAndPeanutAsync(peanutId, ci);
            await _peanutRepository.UpdateClientAsync(peanutId, ci, _mapper.Map<ClientEntity>(updateClient));
            await _peanutRepository.UpdateStockAsync(peanutId, updateClient.CantidadCompra);
            var result = await _peanutRepository.SaveChangesAsync();
            if (!result)
            {
                throw new Exception("DataBase Error");
            }
            return updateClient;
        }

        private async Task ValidatePeanutAsync(long peanutId)
        {
            var peanut = await _peanutRepository.GetPeanutAsync(peanutId);
            if (peanut == null)
            {
                throw new NotFoundPeanutException($"El mani con id {peanutId} no existe");
            }
        }

        private async Task ValidateClientAndPeanutAsync(long peanutId, long clientId)
        {
            var client = await GetClientAsync(peanutId, clientId);
        }
    }
}
