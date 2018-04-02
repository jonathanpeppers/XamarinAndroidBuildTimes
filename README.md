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
| FormsBuildTime | 39.612s     | 3.685s       | 4.681s        | 12.467s             |
| Evolve 2016    | 1m 03.673s  | 30.773s      | 29.589s       | 41.899s             |

*NOTE: this was with VS 2017 15.6.4, Intel 2.8ghz Core i7*

# Adding new projects

1. Setup a new project as a git submodule
2. Add to the `projects` array at the top of `build.cake`.