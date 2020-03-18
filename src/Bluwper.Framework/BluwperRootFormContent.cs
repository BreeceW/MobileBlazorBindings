// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using System;

namespace Bluwper.Framework
{
    public class BluwperRootFormContent<TComponent> : IBluwperRootFormContent
        where TComponent : IComponent
    {
        public BluwperRootFormContent()
        {
            RootFormContentType = typeof(TComponent);
        }

        public Type RootFormContentType { get; }
    }
}
