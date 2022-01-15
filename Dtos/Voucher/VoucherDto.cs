using System;
using System.ComponentModel.DataAnnotations;
using DK_API.Entities;

namespace DK_API.Dtos.Voucher
{
    public class VoucherDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public int Discount { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public bool IsDisable { get; set; }
        public bool IsDeleted { get; set; }
    }
}