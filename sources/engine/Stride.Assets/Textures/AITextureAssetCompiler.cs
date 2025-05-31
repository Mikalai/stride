// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Stride.Core.Assets;
using Stride.Core.Assets.Analysis;
using Stride.Core.Assets.Compiler;
using Stride.Core.BuildEngine;
using Stride.Core;
using Stride.Core.IO;
using Stride.Core.Serialization;
using Stride.Core.Serialization.Contents;
using Stride.Core.Streaming;
using Stride.Graphics;
using Stride.TextureConverter;
using Stride.Graphics.Data;
using Stride.Streaming;

namespace Stride.Assets.Textures
{
    /// <summary>
    /// Texture asset compiler.
    /// </summary>
    [AssetCompiler(typeof(AITextureAsset), typeof(AssetCompilationContext))]
    public class AITextureAssetCompiler : AssetCompilerBase
    {
        public override IEnumerable<BuildDependencyInfo> GetInputTypes(AssetItem assetItem)
        {
            yield return new BuildDependencyInfo(typeof(GameSettingsAsset), typeof(AssetCompilationContext), BuildDependencyType.CompileAsset);
        }

        protected override void Prepare(AssetCompilerContext context, AssetItem assetItem, string targetUrlInStorage, AssetCompilerResult result)
        {
            var asset = (AITextureAsset)assetItem.Asset;
            // Get absolute path of asset source on disk
            var assetSource = GetAbsolutePath(assetItem, asset.Source);

            var gameSettingsAsset = context.GetGameSettingsAsset();
            var colorSpace = context.GetColorSpace();

            var parameter = new AITextureConvertParameters(assetSource, asset, context.Platform, context.GetGraphicsPlatform(assetItem.Package), gameSettingsAsset.GetOrCreate<RenderingSettings>(context.Platform).DefaultGraphicsProfile, gameSettingsAsset.GetOrCreate<TextureSettings>().TextureQuality, colorSpace);
            result.BuildSteps = new AssetBuildStep(assetItem);
            result.BuildSteps.Add(new AITextureConvertCommand(targetUrlInStorage, parameter, assetItem.Package));
        }

        /// <summary>
        /// Command used to convert the texture in the storage
        /// </summary>
        public class AITextureConvertCommand : AssetCommand<AITextureConvertParameters>
        {
            public AITextureConvertCommand(string url, AITextureConvertParameters description, IAssetFinder assetFinder)
                : base(url, description, assetFinder)
            {
                Version = 3;
            }

            public override IEnumerable<ObjectUrl> GetInputFiles()
            {
                yield return new ObjectUrl(UrlType.File, Parameters.SourcePathFromDisk);
            }

            private ResultStatus Import(ICommandContext commandContext, TextureTool textureTool, TexImage texImage, TextureHelper.ImportParameters convertParameters)
            {
                var assetManager = new ContentManager(MicrothreadLocalDatabases.ProviderService);
                var useSeparateDataContainer = TextureHelper.ShouldUseDataContainer(Parameters.IsStreamable, texImage.Dimension);

                // Note: for streamable textures we want to store mip maps in a separate storage container and read them on request instead of whole asset deserialization (at once)

                return useSeparateDataContainer
                    ? TextureHelper.ImportStreamableTextureImage(assetManager, textureTool, texImage, convertParameters, CancellationToken, commandContext)
                    : TextureHelper.ImportTextureImage(assetManager, textureTool, texImage, convertParameters, CancellationToken, commandContext.Logger);
            }

            protected override Task<ResultStatus> DoCommandOverride(ICommandContext commandContext)
            {
                var convertParameters = new TextureHelper.ImportParameters(Parameters) { OutputUrl = Url };

                using (var texTool = new TextureTool())
                using (var texImage = texTool.Load(Parameters.SourcePathFromDisk, convertParameters.IsSRgb))
                {
                    var importResult = Import(commandContext, texTool, texImage, convertParameters);

                    return Task.FromResult(importResult);
                }
            }

            protected override void ComputeAssemblyHash(BinarySerializationWriter writer)
            {
                writer.Write(DataSerializer.BinaryFormatVersion);
                writer.Write(TextureSerializationData.Version);

                // Since Image format is quite stable, we want to manually control it's assembly hash here
                writer.Write(1);
            }
        }
    }

    /// <summary>
    /// SharedParameters used for converting/processing the texture in the storage.
    /// </summary>
    [DataContract]
    public class AITextureConvertParameters
    {
        public AITextureConvertParameters()
        {
        }

        public AITextureConvertParameters(
            UFile sourcePathFromDisk,
            AITextureAsset texture,
            PlatformType platform,
            GraphicsPlatform graphicsPlatform,
            GraphicsProfile graphicsProfile,
            TextureQuality textureQuality,
            ColorSpace colorSpace)
        {
            SourcePathFromDisk = sourcePathFromDisk;
            Texture = texture;
            IsStreamable = texture.IsStreamable;
            Platform = platform;
            GraphicsPlatform = graphicsPlatform;
            GraphicsProfile = graphicsProfile;
            TextureQuality = textureQuality;
            ColorSpace = colorSpace;
        }

        public UFile SourcePathFromDisk;

        public AITextureAsset Texture;

        public bool IsStreamable;

        public PlatformType Platform;

        public GraphicsPlatform GraphicsPlatform;

        public GraphicsProfile GraphicsProfile;

        public TextureQuality TextureQuality;

        public ColorSpace ColorSpace;
    }
}
