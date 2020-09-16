using System;
using System.Data;
using System.Threading.Tasks;

using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities;
using LittleLogBook.Data.SqlConnectivity;

namespace LittleLogBook.Data.Managers
{
    public class IpLocationManager : ManagerBase, IIpLocationManager
    {
        private readonly IDataHandler _dataHandler;

        internal IpLocationManager(IDataHandler dataHandler, IUser currentUser)
            : base(currentUser)
        {
            _dataHandler = dataHandler;
        }

        public static bool IsIpv4Address(string IpAddress)
        {
            if (IpAddress == null) return false;

            if (IpAddress.Contains("."))
            {
                if (IpAddress.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Length == 4) return true;
            }

            return false;
        }

        public static bool IsLocalhost(string DottedIpAddress)
        {
            if (DottedIpAddress == null) return false;

            DottedIpAddress = DottedIpAddress.ToUpper();

            if (DottedIpAddress.Contains("LOCALHOST")
                || DottedIpAddress.Contains("::1")
                || DottedIpAddress.Contains("127.0.0.1")
                ) return true;

            return false;
        }

        public static bool IsPrivateAddress(string DottedIpAddress)
        {
            if (DottedIpAddress == null) return false;

            if (DottedIpAddress.StartsWith("10.")
                || DottedIpAddress.StartsWith("192.")
                || DottedIpAddress.StartsWith("172.")
                || !DottedIpAddress.Contains(".")
                ) return IsLocalhost(DottedIpAddress);

            return false;
        }

        public async Task<IpLocation> GetIpLocationAsync(string dottedIpAddress)
        {
            if (dottedIpAddress == null || IsPrivateAddress(dottedIpAddress) || !IsIpv4Address(dottedIpAddress)) return null;

            var segments = dottedIpAddress.Split('.');
            var ipAddressValue = 16777216 * double.Parse(segments[0]) + 65536 * double.Parse(segments[1]) + 256 * double.Parse(segments[2]) + double.Parse(segments[3]);

            using (var command = _dataHandler.CreateCommand("GetIpLocation"))
            {
                command.AddParameter("@IpAddress", ipAddressValue, DbType.Int64);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new IpLocation(reader);
                    }
                }

                return null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataHandler?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}