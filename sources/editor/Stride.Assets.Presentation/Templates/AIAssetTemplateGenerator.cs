// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Stride.Assets.Models;
using Stride.Assets.Textures;
using Stride.Core;
using Stride.Core.Assets;
using Stride.Core.Assets.Templates;
using Stride.Core.IO;

namespace Stride.Assets.Presentation.Templates
{
    public class AIAssetTemplateGenerator : AssetFromFileTemplateGenerator
    {
        public new static readonly AIAssetTemplateGenerator Default = new AIAssetTemplateGenerator();

        public static Guid Id = new Guid("87FBB8AF-26A6-4B14-B4A0-C156D7BF4B0C");

        public override bool IsSupportingTemplate(TemplateDescription templateDescription)
        {
            return templateDescription.Id == Id;
        }

        protected override Task<bool> PrepareAssetCreation(AssetTemplateGeneratorParameters parameters)
        {
            var projectRoot = parameters.Package.RootDirectory;

            var aiFolder = UPath.Combine(projectRoot, new UDirectory("Resources/AI"));
            
            if (!Directory.Exists(aiFolder.FullPath))
                Directory.CreateDirectory(aiFolder.FullPath);

            var fileName = $"ai_texture_{Guid.NewGuid():N}.png";

            var fullPath = UPath.Combine(aiFolder, new UFile(fileName));

            if (!File.Exists(fullPath))
                File.Copy(@"D:\Leather033A.png", fullPath);

            var relativePath = fullPath.MakeRelative(projectRoot);

            parameters.SourceFiles.Add(fullPath);

            parameters.Tags.Add(SourceFilesPathKey, [relativePath]);

            return Task.FromResult(true);
        }

        protected override IEnumerable<AssetItem> CreateAssets(AssetTemplateGeneratorParameters parameters)
        {
            var files = parameters.Tags.Get(SourceFilesPathKey);

            var path = files.FirstOrDefault() ?? "";

            var aiTexture = new AITextureAsset()
            {
                Source = parameters.SourceFiles.FirstOrDefault() ?? "",
            };
            
            var item = new AssetItem(GenerateLocation(parameters), aiTexture);

            yield return item;
        }
    }
}
