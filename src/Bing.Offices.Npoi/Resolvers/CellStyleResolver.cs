using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Offices.Core.Styles;
using NPOI.SS.UserModel;
using Bing.Utils.Extensions;

namespace Bing.Offices.Npoi.Resolvers
{
    /// <summary>
    /// 单元格样式解析器
    /// </summary>
    public class CellStyleResolver
    {
        #region 字段

        /// <summary>
        /// 工作簿
        /// </summary>
        private readonly IWorkbook _workbook;

        /// <summary>
        /// 单元格样式
        /// </summary>
        private readonly CellStyle _style;

        /// <summary>
        /// Npoi单元格样式
        /// </summary>
        private ICellStyle _result;

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化一个<see cref="CellStyleResolver"/>类型的实例
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <param name="cellStyle">单元格样式</param>
        private CellStyleResolver(IWorkbook workbook, CellStyle cellStyle)
        {
            _workbook = workbook;
            _style = cellStyle;
        }

        #endregion

        #region Resolve(解析为Npoi单元格样式)

        /// <summary>
        /// 解析为Npoi单元格样式
        /// </summary>
        /// <returns></returns>
        public ICellStyle Resolve()
        {
            _result = _workbook.CreateCellStyle();
            _result.Alignment = GetHorizontalAlignment();
            _result.VerticalAlignment = GetVerticalAlignment();
            SetBackgroundColor();
            SetFillPattern();
            SetBorderColor();
            SetFont();
            _result.WrapText = _style.IsWrap;
            return _result;
        }

        /// <summary>
        /// 解析单元格样式
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <param name="style">单元格样式</param>
        /// <returns></returns>
        public static ICellStyle Resolve(IWorkbook workbook, CellStyle style)
        {
            return new CellStyleResolver(workbook, style).Resolve();
        }

        /// <summary>
        /// 获取水平对齐
        /// </summary>
        /// <returns></returns>
        private NPOI.SS.UserModel.HorizontalAlignment GetHorizontalAlignment()
        {
            if (_style.Alignment == Bing.Offices.Core.Styles.HorizontalAlignment.Left)
            {
                return NPOI.SS.UserModel.HorizontalAlignment.Left;
            }

            if (_style.Alignment == Bing.Offices.Core.Styles.HorizontalAlignment.Right)
            {
                return NPOI.SS.UserModel.HorizontalAlignment.Right;
            }
            return NPOI.SS.UserModel.HorizontalAlignment.Center;
        }

        /// <summary>
        /// 获取垂直对齐
        /// </summary>
        /// <returns></returns>
        private NPOI.SS.UserModel.VerticalAlignment GetVerticalAlignment()
        {
            if (_style.VerticalAlignment == Bing.Offices.Core.Styles.VerticalAlignment.Top)
            {
                return NPOI.SS.UserModel.VerticalAlignment.Top;
            }
            if (_style.VerticalAlignment == Bing.Offices.Core.Styles.VerticalAlignment.Bottom)
            {
                return NPOI.SS.UserModel.VerticalAlignment.Bottom;
            }
            return NPOI.SS.UserModel.VerticalAlignment.Center;
        }

        /// <summary>
        /// 设置背景色
        /// </summary>
        private void SetBackgroundColor()
        {
            _result.FillBackgroundColor = ColorResolver.Resolve(_style.BackgroundColor);
        }

        /// <summary>
        /// 设置填充模式
        /// </summary>
        private void SetFillPattern()
        {
            _result.FillPattern =
                Bing.Utils.Helpers.Enum.Parse<NPOI.SS.UserModel.FillPattern>(_style.FillPattern.Value());
        }
        /// <summary>
        /// 设置边框颜色
        /// </summary>
        private void SetBorderColor()
        {
            _result.BorderTop = BorderStyle.Thin;
            _result.BorderRight = BorderStyle.Thin;
            _result.BorderBottom = BorderStyle.Thin;
            _result.BorderLeft = BorderStyle.Thin;
            _result.TopBorderColor = ColorResolver.Resolve(_style.BorderColor);
            _result.RightBorderColor = ColorResolver.Resolve(_style.BorderColor);
            _result.BottomBorderColor = ColorResolver.Resolve(_style.BorderColor);
            _result.LeftBorderColor = ColorResolver.Resolve(_style.BorderColor);
        }

        /// <summary>
        /// 设置字体
        /// </summary>
        private void SetFont()
        {
            var font = _workbook.CreateFont();
            font.FontHeightInPoints = _style.FontSize;
            font.Color = ColorResolver.Resolve(_style.FontColor);
            font.Boldweight = _style.FontBoldWeight;
            _result.SetFont(font);
        }

        #endregion
    }
}
