// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Windows.UI.Xaml;

namespace Bluwper.Framework
{
    public interface IWinUIControlHandler : IElementHandler
    {
        UIElement Control { get; }
    }
}
