﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using RxSpy.Communication;
using RxSpy.Models;
using RxSpy.ViewModels;

namespace RxSpy.AppStartup
{
    public static class StartupSequence
    {
        public static void Start()
        {
            var args = Environment.GetCommandLineArgs();
            var address = new Uri("http://localhost:65073/rxspy/");

            var client = new RxSpyHttpClient();

            var session = new RxSpySessionModel();

            client.Connect(address, TimeSpan.FromSeconds(5))
                .Where(x => x != null)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(session.OnEvent);

            var mainViewModel = new MainViewModel(new RxSpySessionViewModel(session));

            RxApp.MutableResolver.Register(() => mainViewModel, typeof(MainViewModel));
        }
    }
}