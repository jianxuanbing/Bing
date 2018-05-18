using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Applications.Dtos
{
    /// <summary>
    /// 分页查询
    /// </summary>
    [Serializable]
    public class PagedSearch
    {
        #region 属性

        /// <summary>
        /// 每页显示记录数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 页索引，从第几页，从1开始
        /// </summary>
        public int Page { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化一个<see cref="PagedSearch"/>类型的实例
        /// </summary>
        public PagedSearch() : this(1, 15)
        {
        }

        /// <summary>
        /// 初始化一个<see cref="PagedSearch"/>类型的实例
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="pageSize">每页显示记录数</param>
        public PagedSearch(int page, int pageSize)
        {
            this.Page = page == 0 ? 1 : page;
            this.PageSize = pageSize == 0 ? 15 : pageSize;
        }

        #endregion
    }

    /// <summary>
    /// 分页查询，带分页条件信息
    /// </summary>
    /// <typeparam name="TEntity">查询条件</typeparam>
    [Serializable]
    public class PagedSearch<TEntity>:PagedSearch where TEntity:new()
    {
        #region 属性

        /// <summary>
        /// 查询条件
        /// </summary>
        public TEntity Condition { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化一个<see cref="PagedSearch{TEntity}"/>类型的实例
        /// </summary>
        public PagedSearch():this(1,15,new TEntity()) { }

        /// <summary>
        /// 初始化一个<see cref="PagedSearch{TEntity}"/>类型的实例
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="condition">查询条件</param>
        public PagedSearch(int page, int pageSize, TEntity condition) : base(page, pageSize)
        {
            this.Condition = condition;
        }

        #endregion
    }
}
