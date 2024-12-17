// Fill out your copyright notice in the Description page of Project Settings.

using System.IO;
using UnrealBuildTool;

public class FastNoise2 : ModuleRules
{
	public FastNoise2(ReadOnlyTargetRules Target) : base(Target)
	{
		if (Target.Platform.IsInGroup(UnrealPlatformGroup.Microsoft))
		{	
			Type = ModuleType.External;
			PublicIncludePaths.Add(Path.Combine(ModuleDirectory, "include"));

			PublicAdditionalLibraries.Add(LibraryPath);
			// Delay-load the library, so we can load it from the right place first
			PublicDelayLoadDLLs.Add(LibraryName + RuntimeLibExtension);
			// Ensure that the library is staged along with the executable
			RuntimeDependencies.Add(RuntimePath);

			PublicDefinitions.Add("FASTNOISE_LIBRARY_PATH=\"" + RelativeRuntimePath.Replace("\\", "\\\\") + "\"");
		} else {
			CMakeTarget.add(
				Target, this, "FastNoise", Path.Combine(this.PluginDirectory, "Source/ThirdParty/FastNoise2/Source"),
				"-D FASTNOISE2_NOISETOOL=OFF -D FASTNOISE2_TESTS=OFF -D BUILD_SHARED_LIBS=ON", true
			);
		}
	}

	private string ConfigName
	{
		get
		{
			return Target.Configuration == UnrealTargetConfiguration.Debug ? "Debug" : "Release";
		}
	}

	private string PlatformString
	{
		get
		{
			return Target.Platform.ToString();
		}
	}

	private string RuntimePath
	{
		get
		{
			return Path.Combine("$(PluginDir)", RelativeRuntimePath);
		}
	}

	private string RelativeRuntimePath
	{
		get
		{
			return Path.Combine("Resources", "ThirdParty", "FastNoise2", PlatformString, LibraryName + RuntimeLibExtension);
		}
	}

	private string LibraryPath
	{
		get
		{
			if (Target.Platform.IsInGroup(UnrealPlatformGroup.Microsoft))
			{
				return Path.Combine(ModuleDirectory, PlatformString, ConfigName, LibraryName + LibraryExtension);
			}
			else
			{
				return RuntimePath;
			}
		}
	}

	private string LibraryName
	{
		get
		{
			if (Target.Configuration == UnrealTargetConfiguration.Debug)
			{
				if (Target.Platform == UnrealTargetPlatform.Mac ||
				    Target.Platform == UnrealTargetPlatform.IOS ||
				    Target.Platform == UnrealTargetPlatform.Android ||
				    Target.Platform == UnrealTargetPlatform.Linux ||
				    Target.Platform == UnrealTargetPlatform.LinuxArm64)
				{
					return "libFastNoiseD";
				}
				else if (Target.Platform.IsInGroup(UnrealPlatformGroup.Microsoft))
				{
					return "FastNoiseD";
				}
			}
			else
			{

				if (Target.Platform == UnrealTargetPlatform.Mac ||
				    Target.Platform == UnrealTargetPlatform.IOS ||
				    Target.Platform == UnrealTargetPlatform.Android ||
				    Target.Platform == UnrealTargetPlatform.Linux ||
				    Target.Platform == UnrealTargetPlatform.LinuxArm64)
				{
					return "libFastNoise";
				}
				else if (Target.Platform.IsInGroup(UnrealPlatformGroup.Microsoft))
				{
					return "FastNoise";
				}
			}

			throw new BuildException("Unsupported platform");
		}
	}

	private string LibraryExtension
	{
		get
		{
			if (Target.Platform.IsInGroup(UnrealPlatformGroup.Microsoft))
			{
				return ".lib";
			}
			else
			{
				return RuntimeLibExtension;
			}
		}
	}

	private string RuntimeLibExtension
	{
		get
		{
			if (Target.Platform == UnrealTargetPlatform.Mac)
			{
				return ".dylib";
			}
			else if (Target.Platform == UnrealTargetPlatform.IOS)
			{
				return ".framework";
			}
			else if (Target.Platform == UnrealTargetPlatform.Android ||
			         Target.Platform == UnrealTargetPlatform.Linux ||
			         Target.Platform == UnrealTargetPlatform.LinuxArm64)
			{
				return ".so";
			}
			else if (Target.Platform.IsInGroup(UnrealPlatformGroup.Microsoft))
			{
				return ".dll";
			}

			throw new BuildException("Unsupported platform");
		}
	}
}
