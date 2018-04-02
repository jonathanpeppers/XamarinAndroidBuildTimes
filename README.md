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

*NOTE: NuGet restore is not included in timing*

# Adding new projects

1. Setup a new project as a git submodule
2. Add to the `projects` array at the top of `build.cake`.