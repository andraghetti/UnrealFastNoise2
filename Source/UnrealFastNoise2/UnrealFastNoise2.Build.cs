// Copyright Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;
using System.IO;

public class UnrealFastNoise2 : ModuleRules
{
    public UnrealFastNoise2(ReadOnlyTargetRules Target) : base(Target)
    {
        PCHUsage = ModuleRules.PCHUsageMode.UseExplicitOrSharedPCHs;

        PublicIncludePaths.AddRange(new string[] {});
        PrivateIncludePaths.AddRange(new string[] {});
        PublicDependencyModuleNames.AddRange(
            new string[]
            {
                "Core",
                "Engine",
                "Projects",
                "FastNoise2"
            }
        );


        PrivateDependencyModuleNames.AddRange(
            new string[]
            {
                "CoreUObject"
            }
        );


        DynamicallyLoadedModuleNames.AddRange(
            new string[]
            {
            }
            );
        CMakeTarget.add(
            Target, this, "FastNoise", Path.Combine(this.PluginDirectory, "../../Deps/FastNoise2"),
            "-D FASTNOISE2_NOISETOOL=OFF -D FASTNOISE2_TESTS=OFF -D BUILD_SHARED_LIBS=ON", true
        );
    }
}
