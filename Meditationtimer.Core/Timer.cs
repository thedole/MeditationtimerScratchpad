using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Meditationtimer.Core
{
    public class Timer
    {
        public TimeSpan Duration { get; protected set; }
        public event TimerStartedEventHandler Started;
        public event TimerUpdatedEventHandler Updated;
        public event TimerCompletedEventHandler Completed;

        public Timer(TimeSpan duration)
        {
            Duration = duration;
        }

        async public void Start()
        {
            OnStarted();
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var remaining = Duration.TotalSeconds - stopWatch.Elapsed.TotalSeconds;
            while (remaining >= 1)
            {
                await Task.Run(async () => {
                    await Task.Delay(1000);
                });
                
                OnUpdated(stopWatch.Elapsed);
                remaining = Duration.TotalSeconds - stopWatch.Elapsed.TotalSeconds;
            }
            OnCompleted();
        }

        protected void OnUpdated(TimeSpan elapsedTime)
        {
            var UpdatedArgs = new TimerUpdatedEventArgs(Duration, elapsedTime);
            if (Updated == null)
            {
                return;
            }
            Updated(this, UpdatedArgs);
        }

        protected void OnStarted()
        {
            Started(this);
        }

        protected void OnCompleted()
        {
            Completed(this);
        }
    }
}
