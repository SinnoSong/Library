using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Configs
{
    /// <summary>
    /// 描述要映射的实体类属性
    /// </summary>
    public class PropertyMapping
    {
        public PropertyMapping(string targetProperty, bool revert = false)
        {
            TargetProperty = targetProperty;
            IsRevert = revert;
        }

        /// <summary>
        /// 要映射的目标属性
        /// </summary>
        public string TargetProperty { get; private set; }

        public bool IsRevert { get; private set; }
    }
}