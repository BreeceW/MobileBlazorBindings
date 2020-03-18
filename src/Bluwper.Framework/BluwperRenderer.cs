// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.Extensions.Logging;
using System;
using Windows.UI.Xaml.Controls;

namespace Bluwper.Framework
{
    public class BluwperRenderer : NativeComponentRenderer
    {
        public BluwperRenderer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
            : base(serviceProvider, loggerFactory)
        {
        }

        protected override async void HandleException(Exception exception)
        {
            await new ContentDialog
            {
                Content = exception?.Message,
                Title = "Error",
                CloseButtonText = "OK",
            }.ShowAsync();
        }

        protected override ElementManager CreateNativeControlManager()
        {
            return new BluwperElementManager();
        }
    }
}
