using System;
using AutoMapper;
using DK_API.Controllers.Temp;
using DK_API.Dtos;
using DK_API.Dtos.RegisCost;
using DK_API.Dtos.RoadCost;
using DK_API.Dtos.ScheduleUser;
using DK_API.Dtos.SlotDto;
using DK_API.Dtos.UserSchedule;
using DK_API.Dtos.Voucher;
using DK_API.Entities;

namespace DK_API.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map City
            CreateMap<City, CityDto>();
            // Map RoadCost
            CreateMap<RoadCost, RoadCostDto>();
            CreateMap<RoadCostUpdate, RoadCost>();

            // Map RegistrationCost
            CreateMap<RegisCost, RegisCostDto>();
            CreateMap<RegisCostUpdate, RegisCost>();
            // Map Station
            CreateMap<Station, StationDto>();
            // Map Vehicle
            CreateMap<Vehicle, VehicleDto>();

            // Map ScheduleUser
            CreateMap<UserSchedule, UserScheduleCreate>()
                .ForMember(des => des.Schedule,
                    act => act.MapFrom(src => DateTime.Parse(src.Schedule)));
            CreateMap<UserSchedule, UserScheduleDto>();
            CreateMap<UserScheduleCreate, UserSchedule>()
                .ForMember(des => des.Schedule,
                    act => act.MapFrom(src => src.Schedule.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")))
                .ForMember(des => des.CreatedDate,
                    act => act.MapFrom(src => DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")));

            // Map VoucherCreate
            CreateMap<Voucher, VoucherDto>();
            CreateMap<Voucher, VoucherCreate>()
                .ForMember(des => des.StartTime, act => act
                    .MapFrom(src => DateTime.Parse(src.StartTime)))
                .ForMember(des => des.EndTime, act => act
                    .MapFrom(src => DateTime.Parse(src.EndTime)));
            CreateMap<VoucherCreate, Voucher>()
                .ForMember(des => des.StartTime, act => act
                    .MapFrom(src => src.StartTime.ToString(("yyyy'-'MM'-'dd'T'HH':'mm':'ss"))))
                .ForMember(des => des.EndTime, act => act
                    .MapFrom(src => src.EndTime.ToString(("yyyy'-'MM'-'dd'T'HH':'mm':'ss"))))
                .ForMember(des => des.CreatedDate, act => act
                    .MapFrom(src => DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")))
                .ForMember(des => des.UpdatedDate, act => act
                    .MapFrom(src => DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")))
                .ForMember(des => des.IsDeleted, act => act
                    .MapFrom(src => false));

            // Map StationSlotData
            CreateMap<StationSlotData, StationSlotDataDto>();

            CreateMap<SlotData, SlotDataDto>();
            CreateMap<Slot, SlotDto>();
            CreateMap<SlotDto, Slot>();
            CreateMap<SlotDataDto, SlotData>();

            // Map UserServiceCreate
            CreateMap<UserServiceOrderCreate, UserServiceOrder>()
                .ForMember(des => des.Date,
                    act => act.MapFrom(src => src.Date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")))
                .ForMember(des => des.CreatedDate,
                    act => act.MapFrom(src => DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")));
            CreateMap<UserServiceOrder, UserServiceOrderDto>();

            // Map IpGeolocation
            CreateMap<IpInfoEntity,IpGeolocation>(); 
        }
    }
}