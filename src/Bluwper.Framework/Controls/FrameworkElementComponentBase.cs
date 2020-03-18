// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using System;
using Windows.UI.Xaml;

namespace Bluwper.Framework.Controls
{
    public abstract class FrameworkElementComponentBase : UIElementComponentBase
    {
        [Parameter] public double? Width { get; set; }
        [Parameter] public double? Height { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Width != null)
            {
                builder.AddAttribute(nameof(Width), Width.Value);
            }
            if (Height != null)
            {
                builder.AddAttribute(nameof(Height), Height.Value);
            }
        }

        public static new void ApplyAttribute(UIElement control, ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            var frameworkElement = (FrameworkElement)control;

            switch (attributeName)
            {
                case nameof(Width):
                    frameworkElement.Width = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(Height):
                    frameworkElement.Height = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                default:
                    UIElementComponentBase.ApplyAttribute(control, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
