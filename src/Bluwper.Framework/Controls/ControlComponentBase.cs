// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI;
using System.Globalization;

namespace Bluwper.Framework.Controls
{
    public abstract class ControlComponentBase : FrameworkElementComponentBase
    {
        [Parameter] public string BackgroundColor { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (BackgroundColor != null)
            {
                builder.AddAttribute(nameof(BackgroundColor), BackgroundColor);
            }
        }

        public static new void ApplyAttribute(UIElement control, ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            var xamlControl = (Control)control;

            switch (attributeName)
            {
                case nameof(BackgroundColor):
                    var colorCode = (string)attributeValue;
                    var a = byte.Parse(colorCode.Substring(1, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
                    var r = byte.Parse(colorCode.Substring(3, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
                    var g = byte.Parse(colorCode.Substring(5, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
                    var b = byte.Parse(colorCode.Substring(7, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
                    xamlControl.Background = new SolidColorBrush(ColorHelper.FromArgb(a, r, g, b));
                    break;
                default:
                    FrameworkElementComponentBase.ApplyAttribute(control, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
