// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Bluwper.Framework;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BluwperSample.Razor
{
    public class TodoAppStartup : IBluwperStartup
    {
        private readonly AppState _appState;
        //private readonly IConfiguration _configuration;

        public TodoAppStartup(AppState appState/*, IConfiguration configuration*/)
        {
            _appState = appState;
            //_configuration = configuration;
        }

        public Task OnStartAsync()
        {
            if (_appState.IsEmptyAppState())
            {
                //var  = _configuration.GetValue<int>("");
                _appState.ResetAppState();
            }

            return Task.CompletedTask;
        }
    }
}
