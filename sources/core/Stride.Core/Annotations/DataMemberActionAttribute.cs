using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Core.Annotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DataMemberActionAttribute : Attribute
    {
        public string Label { get; }

        public DataMemberActionAttribute(string label)
        {
            Label = label;
        }
    }
}
