// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using Stride.Assets.Effect;
using Stride.Assets.Materials;
using Stride.Assets.Textures;
using Stride.Core.Assets.Editor.Annotations;
using Stride.Core.Assets.Editor.ViewModel;
using Stride.Core.Assets.Quantum;
using Stride.Core.Diagnostics;
using Stride.Core.Extensions;
using Stride.Core.Quantum;
using Stride.Core.Shaders.Ast;
using Stride.Core.Shaders.Ast.Stride;
using Stride.Rendering.Materials;
using Stride.Rendering.Materials.ComputeColors;

namespace Stride.Assets.Presentation.ViewModel
{
    [AssetViewModel<AITextureAsset>]
    public class AITextureViewModel : AssetViewModel<AITextureAsset>
    {
        public AITextureViewModel(AssetViewModelConstructionParameters parameters)
            : base(parameters)
        {
        }

        public static Type RootNodeType => typeof(AITextureAsset);


        public Action Generate => () =>
        {
            UpdateAsset(Asset, new LoggerResult());
        };
    }
}
