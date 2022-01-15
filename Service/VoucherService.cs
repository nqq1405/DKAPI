using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DK_API.Repository.InfRepository;

namespace DK_API.Service
{
    public class VoucherService
    {
        private readonly IMapper mapper;
        private readonly IVoucherRepository _voucherRepository;
        public VoucherService(IVoucherRepository voucherRepository, IMapper mapper)
        {
            _voucherRepository = voucherRepository;
            this.mapper = mapper;
        }
        public async Task<bool> IsVoucher(string vouchercode)
        {
            bool result = false;
            var vouchers = (await _voucherRepository.GetVouchersAsync())
                .Where(voucher => voucher.Code.Equals(vouchercode)).SingleOrDefault();

                if (vouchers is not null && vouchers.Code.Equals(vouchercode)
                && vouchers.IsDeleted.Equals(false)
                && vouchers.IsDisable.Equals(false)
                && (DateTime.Compare(DateTime.Parse(vouchers.StartTime), DateTime.Now) <= 0)
                && (DateTime.Compare(DateTime.Parse(vouchers.EndTime), DateTime.Now) >= 0))
                    result = true;
            return await Task.FromResult(result);
        }

        public async Task<bool> CheckCountVoucher(string vouchercode)
        {
            bool result = false;
            if (!await IsVoucher(vouchercode))
                return await Task.FromResult(result);
            var vouchers = (await _voucherRepository.GetVouchersAsync()).Where(v => v.Code.Equals(vouchercode)).FirstOrDefault();

            if (vouchers.Count > 0)
                result = true;

            return await Task.FromResult(result);
        }

        public async Task<bool> existingVoucher(string vouchercode)
        {
            var existingVoucher = (await _voucherRepository.GetVouchersAsync())
                .ToList().Select(v => v.Code);
            if (existingVoucher.Contains(vouchercode)) return await Task.FromResult(true);
            return await Task.FromResult(false);
        }

        public async Task reduceCountVoucher(string vouchercode)
        {
            var existingVoucher = (await _voucherRepository.GetVouchersAsync())
                .ToList().Where(x => x.Code == vouchercode).SingleOrDefault();

            if (existingVoucher.Count > 0)
                existingVoucher.Count--;

            await _voucherRepository.UpdateVoucherAsync(existingVoucher);
        }
    }
}