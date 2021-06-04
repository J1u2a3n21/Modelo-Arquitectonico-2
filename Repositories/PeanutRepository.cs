using McNutsAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McNutsAPI.Data.Repositories
{
    public class PeanutRepository : IPeanutRepository
    {
        private PeanutDbContext _dbContext;
        public PeanutRepository(PeanutDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public void CreateClient(long peanutId, ClientEntity newClient)
        {
            _dbContext.Entry(newClient.Peanut).State = EntityState.Unchanged;
            _dbContext.Clients.Add(newClient);
        }

        public void CreatePeanut(PeanutEntity newPeanut)
        {
            _dbContext.Peanuts.Add(newPeanut);
        }

        public async Task DeleteClientAsync(long peanutId, long ci)
        {
            var clientToDelete = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Ci == ci);
            _dbContext.Clients.Remove(clientToDelete);
        }

        public async Task DeletePeanutAsync(long peanutId)
        {
            var peanutToDelete = await _dbContext.Peanuts.FirstAsync(p => p.Id == peanutId);
            _dbContext.Peanuts.Remove(peanutToDelete);
        }

        public async Task<ClientEntity> GetClientAsync(long peanutId, long ci)
        {
            IQueryable<ClientEntity> query = _dbContext.Clients;
            query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(c => c.Peanut.Id == peanutId && c.Ci == ci);
        }

        public async Task<IEnumerable<ClientEntity>> GetClientsAsync(long peanutId)
        {
            IQueryable<ClientEntity> query = _dbContext.Clients;
            query = query.Where(c => c.Peanut.Id == peanutId);
            query = query.Include(p => p.Peanut);
            query = query.AsNoTracking();
            return await query.ToListAsync();
        }

        public async Task<PeanutEntity> GetPeanutAsync(long peanutId)
        {
            IQueryable<PeanutEntity> query = _dbContext.Peanuts;
            query = query.AsNoTracking();
            query = query.Include(p => p.Clients);
            return await query.FirstOrDefaultAsync(p => p.Id == peanutId);
        }

        public async Task<IEnumerable<PeanutEntity>> GetPeanutsAsync(string orderBy = "Id")
        {
            IQueryable<PeanutEntity> query = _dbContext.Peanuts;
            query = query.AsNoTracking();
            switch (orderBy.ToLower())
            {
                case "name":
                    query=query.OrderBy(t => t.Name);
                    break;
                case "elaborationDate":
                    query = query.OrderBy(t => t.ElaborationDate);
                    break;
                case "expirationDate":
                    query = query.OrderBy(t => t.ExpirationDate);
                    break;
                default:
                    query = query.OrderBy(t => t.Id);
                    break;
            }
            return await query.ToListAsync();
        }

        public async Task<PeanutEntity> RestoreProductionAsync(long peanutId)
        {
            var peanut = await _dbContext.Peanuts.FirstOrDefaultAsync(p => p.Id == peanutId);
            if (peanut.DiscontinuationDate == null)
            {
                peanut.ProductionStatus = false;
                peanut.DiscontinuationDate = DateTime.Now.ToUniversalTime();
                peanut.Amount = 0;
            }
            else
            {
                peanut.ProductionStatus = true;
                peanut.ProductionStartDate = DateTime.Now.ToUniversalTime();
                peanut.DiscontinuationDate = null;
            }
            return peanut;
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var res = await _dbContext.SaveChangesAsync();
                return res > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task UpdateClientAsync(long peanutId, long ci, ClientEntity updateClient)
        {
            var clientToUpdate = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Ci == ci);
            clientToUpdate.Nombre = updateClient.Nombre ?? clientToUpdate.Nombre;
            clientToUpdate.Apellido = updateClient.Apellido ?? clientToUpdate.Apellido;
            clientToUpdate.Celular = updateClient.Celular ?? clientToUpdate.Celular;
            clientToUpdate.LugarCompra = updateClient.LugarCompra ?? clientToUpdate.LugarCompra;
            clientToUpdate.FechaCompra = updateClient.FechaCompra ?? clientToUpdate.FechaCompra;
            clientToUpdate.CantidadCompra = clientToUpdate.CantidadCompra + updateClient.CantidadCompra ?? clientToUpdate.CantidadCompra;
        }

        public async Task UpdatePeanutAsync(long peanutId, PeanutEntity updatePeanut)
        {
            var peanut = await _dbContext.Peanuts.FirstOrDefaultAsync(p => p.Id == peanutId);
            peanut.Name = updatePeanut.Name ?? peanut.Name;
            peanut.ElaborationDate = updatePeanut.ElaborationDate ?? peanut.ElaborationDate;
            peanut.ExpirationDate = updatePeanut.ExpirationDate ?? peanut.ExpirationDate;
            peanut.UnitCost = updatePeanut.UnitCost ?? peanut.UnitCost;
            peanut.WholesalePrice = updatePeanut.WholesalePrice ?? peanut.WholesalePrice;
            peanut.Amount = updatePeanut.Amount ?? peanut.Amount;
            peanut.ProductionStatus = updatePeanut.ProductionStatus ?? peanut.ProductionStatus;
        }

        public async Task<PeanutEntity> UpdateStockAsync(long peanutId, long? amount)
        {
            var peanut = await _dbContext.Peanuts.FirstOrDefaultAsync(p => p.Id == peanutId);
            peanut.Amount = peanut.Amount + amount;
            return peanut;
        }



    }
}
