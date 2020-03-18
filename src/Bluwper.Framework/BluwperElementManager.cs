// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Bluwper.Framework
{
    internal class BluwperElementManager : ElementManager<IWinUIControlHandler>
    {
        protected override void RemoveElement(IWinUIControlHandler handler)
        {
            //handler.Control.Parent.Controls.Remove(handler.Control);
            ((Panel)VisualTreeHelper.GetParent(handler.Control)).Children.Remove(handler.Control);
        }

        protected override void AddChildElement(IWinUIControlHandler parentHandler, IWinUIControlHandler childHandler, int physicalSiblingIndex)
        {
            if (physicalSiblingIndex <= ((Panel)parentHandler.Control).Children.Count)
            {
                // WinUI UIElementCollection DOES support Insert(), so add the new child at the
                // correct location the first time. Take that, WinForms.
                ((Panel)parentHandler.Control).Children.Insert(physicalSiblingIndex, childHandler.Control);
                //parentHandler.Control.Controls.Add(childHandler.Control);
                //parentHandler.Control.Controls.SetChildIndex(childHandler.Control, physicalSiblingIndex);
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(AddChildElement)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but parentControl.Controls.Count={((Panel)parentHandler.Control).Children.Count}");
                ((Panel)parentHandler.Control).Children.Add(childHandler.Control);
                //Debug.WriteLine($"WARNING: {nameof(AddChildElement)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but parentControl.Controls.Count={parentHandler.Control.Controls.Count}");
                //parentHandler.Control.Controls.Add(childHandler.Control);
            }
        }

        protected override int GetPhysicalSiblingIndex(IWinUIControlHandler handler)
        {
            return ((Panel)VisualTreeHelper.GetParent(handler.Control)).Children.IndexOf(handler.Control);
            //return handler.Control.Parent.Controls.GetChildIndex(handler.Control);
        }

        protected override bool IsParented(IWinUIControlHandler handler)
        {
            return VisualTreeHelper.GetParent(handler.Control) != null;
            //return handler.Control.Parent != null;
        }

        protected override bool IsParentOfChild(IWinUIControlHandler parentHandler, IWinUIControlHandler childHandler)
        {
            return ((Panel)parentHandler.Control).Children.Contains(childHandler.Control);
            //return parentHandler.Control.Contains(childHandler.Control);
        }
    }
}
