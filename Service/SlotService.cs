using System.Linq;
using System;
using System.Threading.Tasks;
using DK_API.Entities;
using DK_API.Repository.InfRepository;
using System.Collections.Generic;
using AutoMapper;
using DK_API.Dtos.SlotDto;

namespace DK_API.Service
{
    public class SlotService
    {
        private readonly ISlotDataRepository repository;
        private readonly IStationSlotInfoRepository stationSlotInfoRepo;
        private readonly IStationSlotDataRepository stationSlotDataRepo;
        private readonly IMapper mapper;
        public SlotService(ISlotDataRepository repository,
            IStationSlotInfoRepository stationSlotInfoRepo,
            IStationSlotDataRepository stationslotdataRepo,
            IMapper mapper)
        {
            this.repository = repository;
            this.stationSlotInfoRepo = stationSlotInfoRepo;
            this.stationSlotDataRepo = stationslotdataRepo;
            this.mapper = mapper;
        }
        public async Task<bool> IsSlotStation(DateTime date, int stationId)
        {
            bool result = false;
            string dateT = date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
            bool IsSlotStation = (await stationSlotInfoRepo.GetDateSlotByDateAsync(dateT))
                .ToList().Exists(s => s.stationId==stationId);
            if(IsSlotStation) 
                result = true;
            return await Task.FromResult(result);
        }
        public async Task<Object> GetSlotByDateTimeAsync(DateTime dateTime, int stationId)
        {
            string date = dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
            bool isnull = await IsScheduleNullAsync(dateTime, stationId);
            if (isnull)
            {
                await CreateScheduleSlotAsync(dateTime, stationId);
            }

            var data = (await stationSlotInfoRepo.GetDateSlotsAsync())
                .Where(s => (s.stationId.Equals(stationId) && s.date.Equals(date)))
                .FirstOrDefault();
            var slot = data.slotLists?
                .Where(s => s.status).Select(s => mapper.Map<SlotDto>(s));
            return slot;
        }
        public async Task<bool> CheckingDateSlotAsync(DateTime date, string timeslot, int stationId)
        {
            string datetime = date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
            if (await IsScheduleNullAsync(date, stationId))
                await CreateScheduleSlotAsync(date, stationId);

            StationSlotInfo slotdate = (await stationSlotInfoRepo.GetDateSlotByDateAsync(datetime))
                .Where(s => s.stationId == stationId).FirstOrDefault();
            Slot s = slotdate.slotLists.Where(t => t.Time == timeslot).SingleOrDefault();

            if (s.slot <= 0)
                return await Task.FromResult(true);

            return await Task.FromResult(false);
        }
        protected async Task<bool> IsScheduleNullAsync(DateTime datetime, int stationId)
        {
            string date = datetime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
            List<StationSlotInfo> scheduleList = (await stationSlotInfoRepo.GetDateSlotsAsync()).ToList();
            StationSlotInfo s = scheduleList
                .Where(d => ((d.stationId.Equals(stationId)
                    && d.date.Equals(date)))).SingleOrDefault();

            if (s != null)
                return await Task.FromResult(false);

            return await Task.FromResult(true);
        }
        public async Task CreateScheduleSlotAsync(DateTime datetime, int stationId)
        {
            int dow = (int)datetime.DayOfWeek;
            if (await CheckingStationSlotDataAsync(stationId))
                await CreateStationSlotDataAsync(stationId);

            var dowSlot = (await stationSlotDataRepo
                .GetStationSlotDatasAsync()).ToList()
                    .Where(s => s.stationId == stationId)
                    .SingleOrDefault();

            var dowslot2 = dowSlot.slotData
            .Where(s => (s.dayofWeek == dow && s.status)).SingleOrDefault()?.slotLists;

            StationSlotInfo slot = new StationSlotInfo()
            {
                stationId = stationId,
                slotLists = (dowslot2 is null) ? null : dowslot2,
                date = datetime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
            };
            await stationSlotInfoRepo.CreateDateSlotAsync(slot);
            await Task.CompletedTask;
        }

        public async Task<StationSlotData> GetStationSlotDataByStationIdAsync(int _stationId)
        {
            if (await CheckingStationSlotDataAsync(_stationId))
                await CreateStationSlotDataAsync(_stationId);

            return (await stationSlotDataRepo.GetStationSlotDatasAsync()).
                Where(s => s.stationId == _stationId).FirstOrDefault();
        }

        protected async Task<bool> CheckingStationSlotDataAsync(int stationId)
        {
            bool result = false;

            var existSlotData = (await stationSlotDataRepo.GetStationSlotDatasAsync())
                .ToList().Where(s => s.stationId == stationId)
                .SingleOrDefault();
            if (existSlotData is null) result = true;
            return await Task.FromResult(result);
        }

        protected async Task CreateStationSlotDataAsync(int stationId)
        {
            var _slotdata = (await repository.GetTimesAsync()).ToList();
            StationSlotData data = new StationSlotData()
            {
                stationId = stationId,
                updateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"),
                slotData = _slotdata
            };
            await stationSlotDataRepo.CreateStationSlotDataAsync(data);
        }

        public async Task UpdateReduceScheduleSlotAsync(DateTime datetime, string timeslot, int stationId)
        {
            string date = datetime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
            StationSlotInfo slotdate = (await stationSlotInfoRepo.GetDateSlotByDateAsync(date))
                .Where(s => s.stationId == stationId).FirstOrDefault();
            slotdate.slotLists.ForEach(s =>
            {
                if (s.Time.Equals(timeslot) && s.slot > 0)
                {
                    s.slot--;
                }
            });
            await stationSlotInfoRepo.UpdateDateSlotAsync(slotdate);
        }
    }
}