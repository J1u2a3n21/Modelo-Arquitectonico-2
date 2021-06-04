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
    public class PeanutService : IPeanutService
    {
        private IPeanutRepository _peanutRepository;
        private IMapper _mapper;
        private HashSet<string> _allowedOrderByValues = new HashSet<string>()
        {
            "id",
            "name",
            "elaborationdate",
            "expirationdate"
        };
        public PeanutService(IPeanutRepository mCNutsRepository, IMapper mapper)
        {
            _peanutRepository = mCNutsRepository;
            _mapper = mapper;
        }
        public async Task<PeanutModel> CreatePeanutAsync(PeanutModel newPeanut)
        {
            var peanutEntity = _mapper.Map<PeanutEntity>(newPeanut);
            _peanutRepository.CreatePeanut(_mapper.Map<PeanutEntity>(peanutEntity));
            var result = await _peanutRepository.SaveChangesAsync();
            if (result)
            {
                return _mapper.Map<PeanutModel>(peanutEntity);
            }
            throw new Exception("Database Error");
        }

        public async Task<bool> DeletePeanutAsync(long peanutId)
        {
            await ValidatePeanutAsync(peanutId);
            await _peanutRepository.DeletePeanutAsync(peanutId);
            var result = await _peanutRepository.SaveChangesAsync();
            if (!result)
            {
                throw new Exception("DataBase Error");
            }
            return true;
        }

        public async Task<PeanutWithClientModel> GetPeanutAsync(long peanutId)
        {
            var peanut = await _peanutRepository.GetPeanutAsync(peanutId);
            if (peanut == null)
            {
                throw new NotFoundPeanutException($"El sabor del mani con id {peanutId} no existe. ");
            }
            return _mapper.Map<PeanutWithClientModel>(peanut);
        }

        private async Task<PeanutModel> GetPeanutAsyncII(long peanutId)
        {
            var peanut = await _peanutRepository.GetPeanutAsync(peanutId);
            if (peanut == null)
            {
                throw new NotFoundPeanutException($"El sabor del mani con id {peanutId} no existe. ");
            }
            var peanutII= _mapper.Map<PeanutModel>(peanut);
            return peanutII;
        }

        public async Task<IEnumerable<PeanutModel>> GetPeanutsAsync(string orderBy = "Id")
        {
            if (!_allowedOrderByValues.Contains(orderBy.ToLower()))
                throw new InvalidOperationPeanutException($"La forma de ordenar mediante {orderBy} no es valida, por favor use uno de estos parametros {String.Join(',', _allowedOrderByValues.ToArray())}");
            var entityList = await _peanutRepository.GetPeanutsAsync(orderBy.ToLower());
            var modelList = _mapper.Map<IEnumerable<PeanutModel>>(entityList);
            return modelList;
        }

        public async Task<PeanutModel> RestoreProductionAsync(long peanutId)
        {
            await ValidatePeanutAsync(peanutId);
            var result = await _peanutRepository.RestoreProductionAsync(peanutId);
            return _mapper.Map<PeanutModel>(result);

        }

        public async Task<PeanutModel> UpdatePeanutAsync(long peanutId, PeanutModel updatePeanut)
        {
            await ValidatePeanutAsync(peanutId);
            var peanut = await GetPeanutAsyncII(peanutId);
            if (peanut.ProductionStatus == false)
            {
                throw new NotFoundPeanutException($"El sabor del mani con id {peanutId} no esta en produccion por lo cual no puede ser modificado. ");

            }
            updatePeanut.Id = peanutId;
            await _peanutRepository.UpdatePeanutAsync(peanutId, _mapper.Map<PeanutEntity>(updatePeanut));
            var result = await _peanutRepository.SaveChangesAsync();
            if (!result)
            {
                throw new Exception("DataBase Error");
            }
            return _mapper.Map<PeanutModel>(updatePeanut);
        }

        public async Task<PeanutModel> UpdateStockAsync(long peanutId, long? amount)
        {
            var peanut = await GetPeanutAsyncII(peanutId);
            if (peanut.ProductionStatus == false)
            {
                throw new NotFoundPeanutException($"El sabor del mani con id {peanutId} no esta en produccion por lo cual no puede ser modificado. ");

            }
            else
            {
                if (peanut.Amount <= 0)
                {
                    throw new InsufficientAmountPeanuts($"El sabor del mani con id {peanutId} no tiene la cantidad suficiente");
                }
            }
            var stockUpdated = await _peanutRepository.UpdateStockAsync(peanutId, amount);
            var respuesta = _mapper.Map<PeanutModel>(stockUpdated);
            return respuesta;
        }

        private async Task ValidatePeanutAsync(long peanutId)
        {
            await GetPeanutAsync(peanutId);
        }



        //agregar stock
    }
}
