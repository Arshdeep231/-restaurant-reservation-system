using AutoMapper;
using RestaurantTableReservation.DataAccessLayer.Entity;
using RestaurantTableReservation.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableReservation.BusinessAccessLayer.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ModalTableDto, TableDetail>().ReverseMap();
            CreateMap<TableDetailDto, TableDetail>().ReverseMap();
            CreateMap<ReservationDetail, ModalReservationDto>().ReverseMap();
        }
    }
}
