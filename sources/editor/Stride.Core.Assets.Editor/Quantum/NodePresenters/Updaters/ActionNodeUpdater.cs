using System.Reflection;
using Stride.Core.Annotations;
using Stride.Core.Assets.Editor.Quantum.NodePresenters.Keys;
using Stride.Core.Presentation.Quantum.Presenters;
using Stride.Core.Reflection;

namespace Stride.Core.Assets.Editor.Quantum.NodePresenters.Updaters
{
    public class ActionNodeUpdater : NodePresenterUpdaterBase
    {
        public override void UpdateNode(INodePresenter node)
        {
            var modelNode = node as AssetMemberNodePresenter;
            var memberDescriptor = modelNode?.MemberDescriptor;
            if (memberDescriptor == null)
                return;

            UpdateNode(node, memberDescriptor.MemberInfo);
        }

        public void UpdateNode(INodePresenter node, MemberInfo memberInfo)
        {
            if (TypeDescriptorFactory.Default.AttributeRegistry.GetAttribute<DataMemberActionAttribute>(memberInfo) is DataMemberActionAttribute actionAttr)
            {
                if (actionAttr != null && node.Type == typeof(Action))
                {
                    var action = node.Value as Action;
                    if (action != null)
                    {
                        node.AttachedProperties.Add(ActionData.ActionKey, action);
                        node.AttachedProperties.Add(ActionData.ActionLabelKey, actionAttr.Label);
                        node.IsVisible = true;
                    }
                }
            }
        }
    }
}
