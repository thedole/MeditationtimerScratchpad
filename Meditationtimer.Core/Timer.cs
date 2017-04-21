using System;
using System.Collections.Generic;
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
        public event TimerCompletedEventHandler Completed;


        public Timer(TimeSpan duration)
        {
            Duration = duration;
        }

        public void Start()
        {
            var t = new Task(async () => {
                await Task.Delay(Duration);
                OnCompleted();
            });

            t.Start();
            OnStarted();
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
