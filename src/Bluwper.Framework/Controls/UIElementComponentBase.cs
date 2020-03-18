// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using System;
using Windows.UI.Xaml;

namespace Bluwper.Framework.Controls
{
    public abstract class UIElementComponentBase : NativeControlComponentBase
    {
        [Parameter] public Visibility? Visibility { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            if (Visibility != null)
            {
                builder.AddAttribute(nameof(Visibility), Visibility);
            }
        }

        public static void ApplyAttribute(UIElement control, ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            switch (attributeName)
            {
                case nameof(Visibility):
                    control.Visibility = (Visibility)attributeValue;
                    break;
                default:
                    throw new NotImplementedException($"UIElementComponentBase doesn't recognize attribute '{attributeName}'");
            }
        }
    }
}
