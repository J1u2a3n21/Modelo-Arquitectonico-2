using McNutsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McNutsAPI.Services
{
    public interface IClientService
    {
        public Task<IEnumerable<ClientModel>> GetClientsAsync(long peanutId);
        public Task<ClientModel> GetClientAsync(long peanutId, long ci);
        public Task<ClientModel> CreateClientAsync(long peanutId, ClientModel newClient);
        public Task<bool> DeleteClientAsync(long peanutId, long ci);
        public Task<ClientModel> UpdateClientAsync(long peanutId, long ci, ClientModel updateClient);
    }
}
