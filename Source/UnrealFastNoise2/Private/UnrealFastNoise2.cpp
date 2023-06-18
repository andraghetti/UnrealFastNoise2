// Copyright Epic Games, Inc. All Rights Reserved.

#include "UnrealFastNoise2.h"
#include "HAL/PlatformProcess.h"
#include "Interfaces/IPluginManager.h"
#include "Misc/MessageDialog.h"
#include "Misc/Paths.h"
#include "Modules/ModuleManager.h"

void FUnrealFastNoise2Module::StartupModule()
{
#if PLATFORM_WINDOWS
	const FString BaseDir = IPluginManager::Get().FindPlugin("UnrealFastNoise2")->GetBaseDir();
    const FString LibraryPath = FPaths::Combine(*BaseDir, FASTNOISE_LIBRARY_PATH);

    FastNoiseHandle = !LibraryPath.IsEmpty() ? FPlatformProcess::GetDllHandle(*LibraryPath) : nullptr;

    if (FastNoiseHandle == nullptr)
    {
        const FText ErrorFormat =
            NSLOCTEXT("UnrealFastNoise2Module", "ThirdPartyLibraryError", "Failed to load FastNoise library at path [{0}]");
        FMessageDialog::Open(EAppMsgType::Ok, FText::Format(ErrorFormat, FText::FromString(LibraryPath)));
    }
#endif
}

void FUnrealFastNoise2Module::ShutdownModule()
{
#if PLATFORM_WINDOWS
	FPlatformProcess::FreeDllHandle(FastNoiseHandle);
	FastNoiseHandle = nullptr;
#endif
}

IMPLEMENT_MODULE(FUnrealFastNoise2Module, UnrealFastNoise2)
