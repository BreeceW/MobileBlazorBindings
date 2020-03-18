// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.AspNetCore.Components.Rendering;
using System.Diagnostics.CodeAnalysis;

namespace Bluwper.Framework.Controls
{
    public abstract class ContentControlComponentBase : ControlComponentBase
    {
        [Parameter]
        [SuppressMessage("Naming", "CA1721:Property names should not match get methods")]
        public RenderFragment ChildContent { get; set; }

        protected override RenderFragment GetChildContent() => ChildContent;

        public static new void ApplyAttribute(UIElement control, ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            ControlComponentBase.ApplyAttribute(control, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
        }
    }
}
