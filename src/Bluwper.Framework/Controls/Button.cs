// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;

namespace Bluwper.Framework.Controls
{
    public class Button : ContentControlComponentBase
    {
        static Button()
        {
            ElementHandlerRegistry.RegisterElementHandler<Button>(
                renderer => new BlazorButton(renderer));
        }

        [Parameter] public EventCallback OnClick { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            builder.AddAttribute("onclick", OnClick);
        }

        private class BlazorButton : Windows.UI.Xaml.Controls.Button, IWinUIControlHandler, IHandleChildContentText
        {
            public BlazorButton(NativeComponentRenderer renderer)
            {
                Click += (s, e) =>
                {
                    if (ClickEventHandlerId != default)
                    {
                        renderer.Dispatcher.InvokeAsync(() =>
                        {
                            renderer.DispatchEventAsync(ClickEventHandlerId, null, new EventArgs());
                        });
                    }
                };
                Renderer = renderer;
            }

            public ulong ClickEventHandlerId { get; set; }
            public NativeComponentRenderer Renderer { get; }

            public UIElement Control => this;
            public object TargetElement => this;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case "onclick":
                        Renderer.RegisterEvent(attributeEventHandlerId, id => { if (ClickEventHandlerId == id) ClickEventHandlerId = 0; });
                        ClickEventHandlerId = attributeEventHandlerId;
                        break;
                    default:
                        ContentControlComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }

            public void HandleText(int index, string text)
            {
                Content = text;
            }
        }
    }
}
