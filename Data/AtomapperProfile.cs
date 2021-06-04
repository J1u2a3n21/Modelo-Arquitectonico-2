using AutoMapper;
using McNutsAPI.Data.Entities;
using McNutsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McNutsAPI.Data
{
    public class AtomapperProfile:Profile
    {
        public AtomapperProfile()
        {
            this.CreateMap<PeanutModel, PeanutEntity>().ReverseMap();
            this.CreateMap<ClientModel, ClientEntity>()
                .ForMember(ent => ent.Peanut, mod => mod.MapFrom(modSrc => new PeanutEntity() { Id = modSrc.PeanutId }))
                .ReverseMap()
                .ForMember(mod => mod.PeanutId, ent => ent.MapFrom(entSrc => entSrc.Peanut.Id));

            this.CreateMap<PeanutWithClientModel, PeanutEntity>().ReverseMap();
        }
    }
}
