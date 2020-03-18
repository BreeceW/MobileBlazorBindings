// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.Collections.Generic;
using System.Linq;

namespace BluwperSample.Razor
{
    public class AppState
    {
        public int Counter { get; set; }

        public void ResetAppState()
        {
            Counter = 0;
        }

        public bool IsEmptyAppState()
        {
            return Counter == 0;
        }
    }
}
