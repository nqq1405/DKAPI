
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using  DK_API .Entities;
using  DK_API .Repository;
using  DK_API .Repository.InfRepository;
using  DK_API .Repository.InterfaceRepo;
using Microsoft.Extensions.Logging;

namespace  DK_API .Service
{
    public class UserService
    {
        private int CostsService = 1000000;
        public int CostsServicePublic;
        private readonly string FULLNAME_PATTERN =
            @"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹý\s]+$";
        private readonly string FULLNAME_licenseplates = @"((\d{2})[A-Z])(\-|)(\d{4}([A-Z]|)|(\d{3}\.\d{2}))";
        private readonly IScheUserRepository _userRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly ILogger<UserService> _logger;
        public UserService(IPDBRepository pdbRepository,
            IPKDRepository pkdRepository,
            IScheUserRepository userRepository,
            IStationRepository stationRepository,
            ICityRepository cityRepository,
            ISlotDataRepository timeRepository,
            IVoucherRepository voucherRepository,ILogger<UserService> logger)
        {
            this._userRepository = userRepository;
            this._voucherRepository = voucherRepository;
            this._logger = logger;
            CostsServicePublic = CostsService;
        }
        public void UpdateCostsService(int _CostsService)
        {
            this.CostsService = _CostsService;
            CostsServicePublic = CostsService;
        }
        public bool CheckUserCityId(int id)
        {
            bool result = false;
            List<City> city = MongodbCityRepo.CityData;
            city.ForEach(c =>
            {
                if (c.CityId.Equals(id)) result = true;
            });
            return result;
        }
        public bool CheckUserStationId(int id)
        {
            bool result = false;
            MongodbStationDataRepo.DataStation.ForEach(c =>
            {
                if (c.StationId.Equals(id)) result = true;
            });
            return result;
        }
        public bool CheckUserSlotString(string slot)
        {
            bool result = false;
            MongodbSlotDataRepo.DataSlot.ForEach(c =>
            {
                if (c.Time.Equals(slot)) result = true;
            });
            return result;
        }
        public bool IsFullName(string name)
        {
            if (Regex.IsMatch(name, FULLNAME_PATTERN))
                return true;
            return false;
        }
        public bool IsLicensePlates(string licenseplates)
        {
            if (Regex.IsMatch(licenseplates, FULLNAME_licenseplates))
                return true;
            return false;
        }
        private Task<int> monthDK(int vehiclePKDId, bool uses, int year)
        {
            int yeartem = (DateTime.Today.Year - year);
            int month = 6;
            if (vehiclePKDId == 1 || vehiclePKDId == 2)
            {
                if (!uses)
                {
                    if (yeartem < 7) month = 18;
                    else if (yeartem < 12) month = 12;
                    else month = 6;
                }
            }
            return Task.FromResult(month);
        }

        public async Task<bool> checkingVoucherOneCar(string voucherCode, string LicensePlates)
        {
            bool result = false;

            var uservoucher = (await _userRepository.GetAllUsersAsync())
                .ToList().Where(u => u.LicensePlates.Equals(LicensePlates))?.Select(u => u.VoucherCode);

            if (!String.IsNullOrEmpty(voucherCode) && uservoucher.Contains(voucherCode))
                result = true;

            return await Task.FromResult(result);
        }


        /// <summary>
        /// chi phí tạm tính
        /// </summary>
        /// <param name="vehiclePKDId">id của phương tiện đăng kiểm</param>
        /// <param name="vehiclePDBId">id của phương tiện đường bộ</param>
        /// <param name="year">năm sản xuất trong 25 gần nhất</param>
        /// <param name="uses">mục đích sử dụng của phương tiện</param>
        /// <param name="service">Sử Dụng dịch vụ đăng kiểm ?</param>
        /// <param name="vouchercode"></param>
        /// <returns>Object</returns>
        public async Task<Object> TempCostKDAsync(int vehiclePKDId, int vehiclePDBId, int year, bool uses, bool service, string vouchercode)
        {
            var Voucher = (await _voucherRepository.GetVouchersAsync()).ToList()
                .Where(v => (v.Code.Equals(vouchercode))).FirstOrDefault();

            int costPdb = MongodbRoadCostDataRepo.DataPDB.Where(x => x.VehicleId == vehiclePDBId).SingleOrDefault().PriceDB;
            int costPkd = MongodbRegistrationCostDataRepo.DataPKD.Where(x => x.VehicleId == vehiclePKDId).SingleOrDefault().PriceKD;
            int costCert = MongodbRegistrationCostDataRepo.DataPKD.Where(x => x.VehicleId == vehiclePKDId).SingleOrDefault().PriceCert;
            int month = await monthDK(vehiclePKDId, uses, year);
            int costTemp = costCert + (costPdb * month) + costPkd;
            int costservice = CostsService;
            if (service)
            {
                if (!string.IsNullOrEmpty(vouchercode))
                {
                    var voucher = (await _voucherRepository.GetVouchersAsync())
                        .ToList()
                        .Where(v => v.Code == vouchercode)
                        .SingleOrDefault();
                    if (voucher != null)
                        costservice -= voucher.Discount;
                }
                costTemp += costservice;
            }

            CultureInfo cultureInfo = new CultureInfo("vi-VN");
            string format = "{0:0,0 VNĐ}";

            return await Task.FromResult(new
            {
                CostPkd = string.Format(cultureInfo, format, costPkd),
                CostCert = string.Format(cultureInfo, format, costCert),
                CostPdb = string.Format(cultureInfo, format, (costPdb * month)),
                CostService = service ? string.Format(cultureInfo, format, costservice) : null,
                CostTotalTemp = string.Format(cultureInfo, format, costTemp),
                VoucherCode = vouchercode
            });
        }

        public async Task<Object> TempCostKDInfoTempAsync(int vehiclePKDId, int vehiclePDBId, int year, bool uses, bool service, string vouchercode)
        {
            var Voucher = (await _voucherRepository.GetVouchersAsync()).ToList()
                .Where(v => (v.Code.Equals(vouchercode))).FirstOrDefault();

            int costPdb = MongodbRoadCostDataRepo.DataPDB.Where(x => x.VehicleId == vehiclePDBId).SingleOrDefault().PriceDB;
            int costPkd = MongodbRegistrationCostDataRepo.DataPKD.Where(x => x.VehicleId == vehiclePKDId).SingleOrDefault().PriceKD;
            int costCert = MongodbRegistrationCostDataRepo.DataPKD.Where(x => x.VehicleId == vehiclePKDId).SingleOrDefault().PriceCert;
            int month = await monthDK(vehiclePKDId, uses, year);
            int costTemp = costCert + (costPdb * month) + costPkd;
            int costservice = CostsService;
            if (service)
            {
                if (!string.IsNullOrEmpty(vouchercode))
                {
                    var voucher = (await _voucherRepository.GetVouchersAsync())
                        .ToList()
                        .Where(v => v.Code == vouchercode)
                        .SingleOrDefault();
                    if (voucher != null)
                        costservice -= voucher.Discount;
                }
                costTemp += costservice;
            }

            return await Task.FromResult(new InfoCosts()
            {
                CostPkd = costPkd,
                CostCert = costCert,
                CostPdb = (costPdb * month),
                CostService = service ? costservice : 0,
                CostTotalTemp = costTemp
            });
        }

    }
}