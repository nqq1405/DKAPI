using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DK_API.Entities;

namespace DK_API.Dtos.SlotDto
{
    /// <summary>
    /// Schema StationSlotDataUpdate 
    /// </summary>
    public class StationSlotDataUpdate
    {
        /// <summary>
        /// Schema StationSlot
        /// </summary>
        public SlotData slotData { get; set; }
    }
}