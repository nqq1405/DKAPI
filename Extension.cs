using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using  DK_API .Dtos;
using  DK_API .Dtos.RegisCost;
using  DK_API .Dtos.RoadCost;
using  DK_API .Entities;

namespace  DK_API 
{
    public static class Extension
    {        
        /// <summary>
        /// check datetime IsPast  
        /// </summary>
        /// <param name="date">datetime</param>
        /// <returns>return true if pass</returns>
        public static bool IsDatePast(this DateTime date)
        {
            DateTime now = DateTime.Now.Date;
            int compare = now.CompareTo(date); 
            if (compare < 0 || compare.Equals(0))
            {
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// Check if IPv4 IP Address is Local or Not
        /// </summary>
        /// <param name="host">ipv4 string</param>
        /// <returns>true if IPv4 IP Address is Local or Not</returns>
        public static bool IsLocalIpAddress(string host)
        {
            try
            {
                // get host IP addresses
                IPAddress[] hostIPs = Dns.GetHostAddresses(host);
                // get local IP addresses
                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

                // test if any host IP equals to any local IP or to localhost
                foreach (IPAddress hostIP in hostIPs)
                {
                    // is localhost
                    if (IPAddress.IsLoopback(hostIP)) return true;
                    // is local address
                    foreach (IPAddress localIP in localIPs)
                    {
                        if (hostIP.Equals(localIP)) return true;
                    }
                }
            }
            catch { }
            return false;
        }

        /// <summary>
        /// Chuyển đổi Tiếng việt sang tiếng việt không dấu
        /// </summary>
        /// <param name="s">Chuỗi Tiếng việt</param>
        /// <returns>Chuỗi tiếng việt không dấu</returns>
        public static string utf8ConvertVietNameseByRegex(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        /// <summary>
        /// Xóa 1 hoặc nhiều ký tự trong chuỗi
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string RemoveCharInString(this string str, char[] c)
        {
            string[] arraystr = Extension.utf8ConvertVietNameseByRegex(str)
                .ToLower()
                .Split(c, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
            return string.Join("", arraystr);
        }
    }
}