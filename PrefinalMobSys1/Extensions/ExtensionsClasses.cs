using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Extensions
{
    /// <summary>
    /// Meant for MAUI Blazor Only
    /// This applies a custom extension to the BlazorWebView to load the AppData directory virtual within the Blazor App Server 0.0.0.0
    /// DO NOT Create folders in wwwroot with the same name as intended folders to your AppData Directory
    /// </summary>
    public class CustomFilesBlazorWebView : BlazorWebView
    {
        public override IFileProvider CreateFileProvider(string contentRootDir)
        {
            var lPhysicalFiles = new PhysicalFileProvider(FileSystem.Current.AppDataDirectory);
            return new CompositeFileProvider(lPhysicalFiles, base.CreateFileProvider(contentRootDir));
        }
    }
}

/// <summary>
/// Extends List Class to have a shuffle method
/// </summary>
public static class ListsExtention
{
    private static Random rng = new Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}