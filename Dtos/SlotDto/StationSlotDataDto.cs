using System.Collections.Generic;
using DK_API.Entities;

namespace DK_API.Dtos.SlotDto
{
    public class StationSlotDataDto
    {
        public int stationId { get; set; }
        public List<SlotDataDto> slotData { get; set; }
        public string updateTime { get; set; }

    }
}