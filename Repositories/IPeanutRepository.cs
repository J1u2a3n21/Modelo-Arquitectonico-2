using McNutsAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McNutsAPI.Data.Repositories
{
    public interface IPeanutRepository
    {
        //peanuts
        public Task<IEnumerable<PeanutEntity>> GetPeanutsAsync(string orderBy = "Id");
        public Task<PeanutEntity> GetPeanutAsync(long peanutId);
        public void CreatePeanut(PeanutEntity newPeanut);
        public Task DeletePeanutAsync(long peanutId);
        public Task UpdatePeanutAsync(long peanutId, PeanutEntity updatePeanut);
        public Task<PeanutEntity> RestoreProductionAsync(long peanutId);
        public Task<PeanutEntity> UpdateStockAsync(long peanutId, long? amount);
        //inventario peanutReport linq 
        //borrar un sabor con un query param
        //clients
        public Task<IEnumerable<ClientEntity>> GetClientsAsync(long peanutId);
        public Task<ClientEntity> GetClientAsync(long peanutId, long ci);
        public void CreateClient(long peanutId, ClientEntity newClient);
        public Task DeleteClientAsync(long peanutId, long ci);
        public Task UpdateClientAsync(long peanutId, long ci, ClientEntity updateClient);
        Task<bool> SaveChangesAsync();
    }
}
