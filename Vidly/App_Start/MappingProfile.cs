using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.Initialize(cfg => {

                cfg.CreateMap<Customer, CustomerDto>()
                    .ForMember(d => d.MembershipType,
                        opt => opt.MapFrom(src => src.MembershipType)
                );
                
                cfg.CreateMap<CustomerDto, Customer>()
                    .ForMember(d => d.MembershipType,
                        opt => opt.MapFrom(src => src.MembershipType)
                );

                cfg.CreateMap<Movie, MovieDto>()
                    .ForMember(d => d.Genre,
                        opt => opt.MapFrom(src => src.Genre)
                    );

                cfg.CreateMap<MovieDto, Movie>()
                    .ForMember(d => d.Genre,
                        opt => opt.MapFrom(src => src.Genre)
                );

                cfg.CreateMap<MembershipType, MembershipTypeDto>();
                cfg.CreateMap<Genre, GenreDto>();
                    

                cfg.CreateMap<MovieDto, Movie>()
                    .ForMember(d => d.Genre,
                        opt => opt.MapFrom(src => src.Genre)
                    );
            });

        }
    }
}