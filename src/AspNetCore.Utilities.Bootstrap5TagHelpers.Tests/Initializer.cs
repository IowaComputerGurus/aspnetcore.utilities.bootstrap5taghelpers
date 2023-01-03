using System.Runtime.CompilerServices;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Tests;

public static class Initializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        VerifyAngleSharpDiffing.Initialize();

        // Shove all the verify files into a custom directory
        VerifierSettings.DerivePathInfo((file, directory, type, method) =>
            new PathInfo(Path.Combine(directory, "VerifySnapshots"), type.Name, method.Name));

        // Automatically "verify" tests on the first run

        VerifierSettings.OnFirstVerify(pair =>
        {
            File.Move(pair.ReceivedPath, pair.VerifiedPath);
            return Task.CompletedTask;
        });
    }
}