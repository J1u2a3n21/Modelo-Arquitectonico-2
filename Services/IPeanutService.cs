using McNutsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McNutsAPI.Services
{
    public interface IPeanutService
    {
        public Task<IEnumerable<PeanutModel>> GetPeanutsAsync(string orderBy = "Id");
        public Task<PeanutWithClientModel> GetPeanutAsync(long peanutId);
        public Task<PeanutModel> CreatePeanutAsync(PeanutModel newPeanut);
        public Task<bool> DeletePeanutAsync(long peanutId);
        public Task<PeanutModel> UpdatePeanutAsync(long peanutId, PeanutModel updatePeanut);
        public Task<PeanutModel> RestoreProductionAsync(long peanutId);
        public Task<PeanutModel> UpdateStockAsync(long peanutId, long? amount);
    }
    //inventario peanutReport linq 
    //borrar un sabor con un query param
}
