using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Vidly.App_Start;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
           // Mapper.Initialize(c => c.AddProfile<MappingProfile>());
            ConfigureAutoMapper();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ConfigureAutoMapper()
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
