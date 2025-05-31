using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Core.Assets.Editor.Quantum.NodePresenters.Keys
{
    public static class ActionData
    {
        public static readonly PropertyKey<Action> ActionKey =
            new PropertyKey<Action>("Action", typeof(ActionData));

        public static readonly PropertyKey<string> ActionLabelKey =
            new PropertyKey<string>("ActionLabel", typeof(ActionData));
    }
}
