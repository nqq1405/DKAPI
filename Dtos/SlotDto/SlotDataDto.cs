using System.Collections.Generic;
using DK_API.Entities;

namespace DK_API.Dtos.SlotDto
{
    public class SlotDto
    {
        public string Time { get; set; }
        public int slot { get; set; }
    }
    public class SlotDataDto
    {
        public int dayofWeek { get; set; }
        public List<Slot> slotLists { get; set; }
    }
}