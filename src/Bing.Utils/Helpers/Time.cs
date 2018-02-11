using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Helpers
{
    /// <summary>
    /// 时间操作
    /// </summary>
    public static class Time
    {
        #region Property(属性)
        /// <summary>
        /// 日期
        /// </summary>
        private static DateTime? _dateTime;

        /// <summary>
        /// 1970年1月1日
        /// </summary>
        internal static readonly DateTime Date1970 = new DateTime(1970, 1, 1);
        #endregion

        #region SetTime(设置时间)

        /// <summary>
        /// 设置时间
        /// </summary>
        /// <param name="dateTime">时间</param>
        public static void SetTime(DateTime? dateTime)
        {
            _dateTime = dateTime;
        }

        /// <summary>
        /// 设置时间
        /// </summary>
        /// <param name="dateTime">时间</param>
        public static void SetTime(string dateTime)
        {
            _dateTime = Utils.Helpers.Conv.ToDateOrNull(dateTime);
        }

        #endregion

        #region Reset(重置时间)

        /// <summary>
        /// 重置时间
        /// </summary>
        public static void Reset()
        {
            _dateTime = null;
        }

        #endregion

        #region GetDateTime(获取当前时间)

        /// <summary>
        /// 获取当前日期时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDateTime()
        {
            if (_dateTime == null)
            {
                return DateTime.Now;
            }
            return _dateTime.Value;
        }

        #endregion

        #region GetDate(获取当前日期，不带时间)

        /// <summary>
        /// 获取当前日期，不带时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDate()
        {
            return GetDateTime().Date;
        }

        #endregion

        #region GetUnixTimestamp(获取Unix时间戳)

        /// <summary>
        /// 获取当前时间的Unix时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetUnixTimestamp()
        {
            return GetUnixTimestamp(DateTime.Now);
        }

        /// <summary>
        /// 获取指定时间的Unix时间戳
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public static long GetUnixTimestamp(DateTime time)
        {
            var start = TimeZoneInfo.ConvertTime(Date1970, TimeZoneInfo.Local);
            long ticks = (time - start.Add(new TimeSpan(8, 0, 0))).Ticks;
            return Utils.Helpers.Conv.ToLong(ticks / TimeSpan.TicksPerSecond);
        }

        #endregion

        #region GetTimeFromUnixTimestamp(从Unix时间戳获取时间)

        /// <summary>
        /// 从Unix时间戳获取时间
        /// </summary>
        /// <param name="timestamp">Unix时间戳</param>
        /// <returns></returns>
        public static DateTime GetTimeFromUnixTimestamp(long timestamp)
        {
            var start = TimeZoneInfo.ConvertTime(Date1970, TimeZoneInfo.Local);
            TimeSpan span = new TimeSpan(long.Parse(timestamp + "0000000"));
            return start.Add(span).Add(new TimeSpan(8, 0, 0));
        }

        #endregion

        #region Format(格式化时间间隔)

        /// <summary>
        /// 格式化时间间隔
        /// </summary>
        /// <param name="span">时间间隔</param>
        /// <returns></returns>
        public static string Format(TimeSpan span)
        {
            StringBuilder result = new StringBuilder();
            if (span.Days > 0)
            {
                result.AppendFormat("{0}天", span.Days);
            }
            if (span.Hours > 0)
            {
                result.AppendFormat("{0}小时", span.Hours);
            }
            if (span.Minutes > 0)
            {
                result.AppendFormat("{0}分", span.Minutes);
            }
            if (span.Seconds > 0)
            {
                result.AppendFormat("{0}秒", span.Seconds);
            }
            return result.ToString();
        }

        #endregion


    }
}
