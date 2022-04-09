# Eto.RainbowLoading

[![Build](https://github.com/rafntor/Eto.RainbowLoading/actions/workflows/build.yml/badge.svg)](https://github.com/rafntor/Eto.RainbowLoading/actions/workflows/build.yml)
[![NuGet](https://img.shields.io/badge/nuget-Eto.RainbowLoading-blue)](https://www.nuget.org/packages?q=eto.rainbowloading)
[![License](https://img.shields.io/github/license/rafntor/Eto.RainbowLoading)](LICENSE)

Provides [Eto.Forms](https://github.com/picoe/Eto) implementations of the famous Android loading indicator. Adapted from [RainbowLoading.Forms](https://github.com/mariusmuntean/RainbowLoading.Forms).  
Two variations are available where the pure Eto-Edition is the leanest whereas the Eto/SkiaSharp-Edition adds a shadow-effect to the animation.

|Pure Eto.Forms Version|Eto.Forms + SkiaSharp|
|---|---|
|[![pure](https://img.shields.io/nuget/v/Eto.RainbowLoading)](https://www.nuget.org/packages/Eto.RainbowLoading/)|[![skia](https://img.shields.io/nuget/v/Eto.RainbowLoading.Skia)](https://www.nuget.org/packages/Eto.RainbowLoading.Skia/)|

Demo applications : https://nightly.link/rafntor/Eto.RainbowLoading/workflows/build/master

## Quickstart

Use NuGet to install [`Eto.RainbowLoading`](https://www.nuget.org/packages/Eto.RainbowLoading/), then add the following to your Form or Container:
```cs
   this.Content = new RainbowLoading();
```

![](./Animation.gif)  
