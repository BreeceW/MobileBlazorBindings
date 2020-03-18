// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Bluwper.Framework.Controls;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Bluwper.Framework
{
    /// <summary>
    /// An implementation of <see cref="IHostedService"/> that controls the lifetime of a Bluwper application.
    /// When this service starts, it loads the main form registered by
    /// <see cref="BluwperServiceCollectionExtensions.AddRootFormContent{TComponent}(Microsoft.Extensions.DependencyInjection.IServiceCollection)"/>.
    /// The service will request that the application stops when the main form is closed.
    /// This service will invoke all instances of <see cref="IBluwperStartup"/> that are registered in the
    /// container. The order of the startup instances is not guaranteed.
    /// </summary>
    public class BluwperHostedService : IHostedService, IDisposable
    {
        private readonly IBluwperRootFormContent _bluwperMainForm;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IEnumerable<IBluwperStartup> _bluwperStartups;
        private BluwperRenderer _renderer;

        public BluwperHostedService(
            IBluwperRootFormContent bluwperMainForm,
            ILoggerFactory loggerFactory,
            IServiceProvider serviceProvider,
            IHostApplicationLifetime hostApplicationLifetime,
            IEnumerable<IBluwperStartup> bluwperStartups)
        {
            _bluwperMainForm = bluwperMainForm;
            _loggerFactory = loggerFactory;
            _serviceProvider = serviceProvider;
            _hostApplicationLifetime = hostApplicationLifetime;
            _bluwperStartups = bluwperStartups;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (_bluwperStartups != null)
            {
                await Task.WhenAll(_bluwperStartups.Select(async startup => await startup.OnStartAsync().ConfigureAwait(false))).ConfigureAwait(false);
            }

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            _renderer = new BluwperRenderer(_serviceProvider, _loggerFactory);
            await Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                //var rootForm = new RootForm();
                //rootForm.FormClosed += OnRootFormFormClosed;

                await _renderer.AddComponent(_bluwperMainForm.RootFormContentType, new ControlWrapper((Grid)((Page)((Frame)Window.Current.Content).Content).Content)).ConfigureAwait(false);

                //Application.Run(rootForm);
            }).AsTask().ConfigureAwait(false);
        }

        /*private void OnRootFormFormClosed(object sender, FormClosedEventArgs e)
        {
            // When the main form closes, request for the application to stop
            _hostApplicationLifetime.StopApplication();
        }*/

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _renderer?.Dispose();
            _renderer = null;

            return Task.CompletedTask;
        }

        private sealed class ControlWrapper : IWinUIControlHandler
        {
            public ControlWrapper(UIElement control)
            {
                Control = control ?? throw new ArgumentNullException(nameof(control));
            }

            public UIElement Control { get; }
            public object TargetElement => Control;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                UIElementComponentBase.ApplyAttribute(Control, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _renderer?.Dispose();
            _renderer = null;
        }
    }
}
