using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Timing;
using Bing.Utils.Transplants.Atomics;

namespace Bing.Utils.Helpers
{
    /// <summary>
    /// 流水号操作
    /// </summary>
    public static class SerialNumber
    {
        /// <summary>
        /// 流水号原子整数
        /// </summary>
        private static readonly AtomicInteger Serial=new AtomicInteger(Int32.MaxValue);

        /// <summary>
        /// 时间戳偏移量
        /// </summary>
        private const int ShiftsForTimestamp = 17;

        /// <summary>
        /// 唯一键偏移量
        /// </summary>
        private const int ShiftsForUnion = 14;

        /// <summary>
        /// 类型偏移量
        /// </summary>
        private const int ShiftsForType = 4;

        /// <summary>
        /// 节点偏移量
        /// </summary>
        private const int ShiftsForNode = 4;

        /// <summary>
        /// 流水号偏移量
        /// </summary>
        private const int ShiftsForSerial = 24;

        /// <summary>
        /// 伪流水
        /// </summary>
        private const int MaskForSerial = (1 << ShiftsForSerial) - 1;

        /// <summary>
        /// 伪唯一键
        /// </summary>
        private const long MaskForUnion = (1 << ShiftsForUnion) - 1;

        /// <summary>
        /// 伪类型
        /// </summary>
        private const long MaskForType = (1 << ShiftsForType) - 1;

        /// <summary>
        /// 获取订单流水号。
        /// 生成方式：日期+long(商家ID+订单类型+主机ID+AtomicInteger)
        /// 实现方式：通过位移与或运算
        /// 符号位(1)+当前秒数(17)+商家ID(14)+订单类型(4)+服务器ID(4)+AtomichInteger(24)
        /// 参考：http://www.cnblogs.com/nele/p/7882071.html
        /// </summary>
        /// <param name="mechId">商户ID</param>
        /// <param name="type">订单类型</param>
        /// <returns></returns>
        public static string Next(long mechId, long type)
        {
            long second = ToSeconds() - ToSeconds(DateTime.Now.BeginOfDay());            
            long serverId = 1;//这个地方可以根据服务器参数来设置的
            long serial = Serial.IncrementAndGet();
            long secondShift = second << (64 - 1 - ShiftsForTimestamp);
            long unionShift = mechId << (64 - 1 - ShiftsForTimestamp - ShiftsForUnion);
            long typeShift = type << (64 - 1 - ShiftsForTimestamp - ShiftsForUnion - ShiftsForType);
            long nodeShift = serverId << (64 - 1 - ShiftsForTimestamp - ShiftsForUnion - ShiftsForType - ShiftsForNode);
            long number = secondShift | unionShift | typeShift | nodeShift | (serial & MaskForSerial);
            return string.Format("{0}", DateTime.Now.ToString("yyyyMMdd")+number);
        }

        private static long ToSeconds()
        {
            return DateTime.Now.GetMillisecondsSince1970()/ 1000;
        }

        private static long ToSeconds(DateTime dateTime)
        {
            return (dateTime.GetMillisecondsSince1970() / 1000);            
        }

        /// <summary>
        /// 获取秒数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static long GetSecond(long id)
        {
            return id >> (ShiftsForUnion + ShiftsForType + ShiftsForNode + ShiftsForSerial);
        }

        /// <summary>
        /// 获取商户ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static long GetMechId(long id)
        {
            return (id >> (ShiftsForType + ShiftsForNode + ShiftsForSerial)) & MaskForUnion;
        }

        /// <summary>
        /// 获取订单类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static long GetType(long id)
        {
            return (id >> (ShiftsForNode + ShiftsForSerial)) & MaskForType;
        }
    }
}
