# XamarinAndroidBuildTimes
Quick repo I'll use to profile build times on different Xamarin.Android projects

# How To Use

Just build either:

    ./build.sh

Or on Windows:

    .\build.ps1

It will perform the necessary build steps/git operations and place the resulting MSBuild binary logs in `./output`.

# Output

The following projects are setup so far:
- `BlankApp` - File -> New Blank Android App
- `FormsBuildTime` - File -> New Master Detail Forms template
- `Evolve2016` - James Montemagno's Evolve 2016 app

The following logs are recorded so far:
- `-Clean.binlog` - the clean build (after a `git clean -dxf` is performed)
- `-Second.binlog` - the second build after the "clean" build
- `-TouchCS.binlog` - modify a C# file and build after the "clean" build, adds `//comment` to `MainActivity.cs`
- `-TouchAR.binlog` - modify an `AndroidResource` file and build after the "clean" build, adds `<!--comment-->` to `Resource/values/styles.xml`

*NOTE: NuGet restore is not included in timing*

# Results

See the latest [release](https://github.com/jonathanpeppers/XamarinAndroidBuildTimes/releases) for binary log files.

## Windows

| Project        | Clean Build | Second Build | Touch C# File | Touch Resource File |
| ---            | ---:        | ---:         | ---:          | ---:                |
| BlankApp       | 6.271s      | 1.618s       | 2.074s        | 1.769s              |
| FormsBuildTime | 39.612s     | 3.685s       | 4.681s        | 12.126s             |
| Evolve 2016    | 1m 0.335s   | 8.788s       | 11.248s       | 24.577s             |

*NOTE: this was with VS 2017 15.6.4, Intel 2.8ghz Core i7*

## Mac

| Project        | Clean Build | Second Build | Touch C# File | Touch Resource File |
| ---            | ---:        | ---:         | ---:          | ---:                |
| BlankApp       | 8.222s      | 2.122s       | 3.601s        | 2.380s              |
| FormsBuildTime | 34.196s     | 4.401s       | 6.542s        | 12.467s             |
| Evolve 2016    | 1m 24.588s  | 16.218s      | 19.791s       | 36.206s             |

*NOTE: this was with VS for Mac 7.4.1 (build 48), should be equivalent to 15.6.4, Intel 3.3ghz Core i7*

# Discoveries / Issues

## Support Libraries / Components

On the second build on Windows, for `Evolve2016`, `IncrementalClean` is deleting some proguard files:
```
Deleting file "external\Evolve2016\src\Conference.Android\obj\Debug\XbdMerge\proguard\dc663e480.txt".
Deleting file "external\Evolve2016\src\Conference.Android\obj\Debug\XbdMerge\dc663e48.proguard.stamp".
Deleting file "external\Evolve2016\src\Conference.Android\obj\Debug\XbdMerge\proguard\0757626b0.txt".
Deleting file "external\Evolve2016\src\Conference.Android\obj\Debug\XbdMerge\0757626b.proguard.stamp".
Deleting file "external\Evolve2016\src\Conference.Android\obj\Debug\XbdMerge\proguard\2f4d086a0.txt".
Deleting file "external\Evolve2016\src\Conference.Android\obj\Debug\XbdMerge\2f4d086a.proguard.stamp".
Deleting file "external\Evolve2016\src\Conference.Android\obj\Debug\XbdMerge\proguard\ea5c07c40.txt".
Deleting file "external\Evolve2016\src\Conference.Android\obj\Debug\XbdMerge\ea5c07c4.proguard.stamp".
Deleting file "external\Evolve2016\src\Conference.Android\obj\Debug\XbdMerge\proguard\d286ae890.txt".
Deleting file "external\Evolve2016\src\Conference.Android\obj\Debug\XbdMerge\d286ae89.proguard.stamp".
Deleting file "external\Evolve2016\src\Conference.Android\obj\Debug\XbdMerge\proguard\1f973a1f0.txt".
Deleting file "external\Evolve2016\src\Conference.Android\obj\Debug\XbdMerge\1f973a1f.proguard.stamp".
```

After further research, it is fixed in Xamarin.Build.Download 0.4.9.

I forked the Evolve 2016 app, updated the NuGet, and reran the numbers.

## Xamarin.Android

On the second build on Mac, for `Evolve2016`, `IncrementalClean` is also deleting pdb/mdb files:
```
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Conference.Droid.pdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Conference.Clients.Portable.pdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Conference.Clients.UI.pdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Conference.DataStore.Abstractions.pdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Conference.Utils.pdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/FormsToolkit.pdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/ImageCircle.Forms.Plugin.Abstractions.pdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Mono.Android.pdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/MvvmHelpers.pdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Plugin.Calendars.Abstractions.pdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Plugin.Connectivity.Abstractions.pdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Plugin.ExternalMaps.Abstractions.pdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Plugin.Permissions.Abstractions.pdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Plugin.Share.Abstractions.pdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Refractored.XamForms.PullToRefresh.pdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Conference.DataStore.Azure.pdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Conference.DataStore.Mock.pdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/FormsViewGroup.dll.mdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/ImageCircle.Forms.Plugin.Android.dll.mdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Plugin.Connectivity.dll.mdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Plugin.ExternalMaps.dll.mdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Plugin.Messaging.Abstractions.dll.mdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Plugin.Messaging.dll.mdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Plugin.Permissions.dll.mdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Plugin.Settings.Abstractions.dll.mdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Plugin.Settings.dll.mdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Plugin.Share.dll.mdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Xamarin.Forms.Core.dll.mdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Xamarin.Forms.Platform.Android.dll.mdb".
Deleting file "external/Evolve2016/src/Conference.Android/obj/Debug/linksrc/Xamarin.Forms.Xaml.dll.mdb".
Deleting file "external/Evolve2016/src/Conference.Android/bin/Debug/Mono.Android.pdb".
```

Other issues:
- `_LinkAssembliesNoShrink` seems to run every time.

## Xamarin.Forms

`XamlC` target seems to run every time. Sent a [PR](https://github.com/xamarin/Xamarin.Forms/pull/2230).

### I will follow up to see about fixing these in Xamarin.Android, Components, and/or Xamarin.Forms.

# Adding new projects

1. Setup a new project as a git submodule
2. Add to the `projects` array at the top of `build.cake`.
