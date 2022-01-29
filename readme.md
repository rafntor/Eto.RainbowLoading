# Eto.RainbowLoading

[![Build](https://github.com/rafntor/Eto.RainbowLoading/actions/workflows/build.yml/badge.svg)](https://github.com/rafntor/Eto.RainbowLoading/actions/workflows/build.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=rafntor_Eto.RainbowLoading&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=rafntor_Eto.RainbowLoading)
[![NuGet](http://img.shields.io/nuget/v/Eto.RainbowLoading.svg)](https://www.nuget.org/packages/Eto.RainbowLoading/)
[![License](https://img.shields.io/github/license/rafntor/Eto.RainbowLoading)](LICENSE)

Provides an [Eto.Forms](https://github.com/picoe/Eto) implementation of the famous Android loading indicator. Adapted from [RainbowLoading.Forms](https://github.com/mariusmuntean/RainbowLoading.Forms).

Demo applications : https://nightly.link/rafntor/Eto.RainbowLoading/workflows/build/master

## Quickstart

Use NuGet to install [`Eto.RainbowLoading`](https://www.nuget.org/packages/Eto.RainbowLoading/), then add the following to your Form or Container:
```cs
   this.Content = new RainbowLoading();
```

![](./Animation.gif)  
