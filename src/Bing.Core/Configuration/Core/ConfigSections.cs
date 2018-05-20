namespace Bing.Configuration.Core
{
    /// <summary>
    /// 配置节点
    /// </summary>
    public class ConfigSections
    {
        /// <summary>
        /// 目标数据
        /// </summary>
        private string _target;

        /// <summary>
        /// 目标数据
        /// </summary>
        public string Target
        {
            get => _target;
            set => _target = value.ToLower();
        }

        /// <summary>
        /// 节点数据
        /// </summary>
        public string SectionData { get; set; }
    }
}
