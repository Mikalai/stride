// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Assets.Textures;
using Stride.Core.Assets;
using Stride.Core.IO;

namespace Stride.Assets.Models
{
    public class AIColorTextureFactory : AssetFactory<AITextureAsset>
    {
        public static AITextureAsset Create()
        {                        
            if (!Directory.Exists("assets/ai"))
                Directory.CreateDirectory("assets/ai");

            return new AITextureAsset
            {
                Type = new ColorTextureType(),
                Source = new UFile($"assets/ai/ai_texture_{Guid.NewGuid():N}.png")
            };
        }

        public override AITextureAsset New()
        {
            return Create();
        }
    }

    public class ColorTextureFactory : AssetFactory<TextureAsset>
    {
        public static TextureAsset Create()
        {
            return new TextureAsset { Type = new ColorTextureType() };
        }

        public override TextureAsset New()
        {
            return Create();
        }
    }

    public class NormalMapTextureFactory : AssetFactory<TextureAsset>
    {
        public static TextureAsset Create()
        {
            return new TextureAsset { Type = new NormapMapTextureType() };
        }

        public override TextureAsset New()
        {
            return Create();
        }
    }

    public class GrayscaleTextureFactory : AssetFactory<TextureAsset>
    {
        public static TextureAsset Create()
        {
            return new TextureAsset { Type = new GrayscaleTextureType() };
        }

        public override TextureAsset New()
        {
            return Create();
        }
    }
}
