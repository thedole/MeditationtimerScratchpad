using System;

namespace Meditationtimer.Core
{
    public class TimerUpdatedEventArgs
    {
        public TimeSpan TimeLeft { get; set; }

        public TimerUpdatedEventArgs(TimeSpan duration, TimeSpan elapsedTime)
        {
            TimeLeft = duration - elapsedTime;
        }
    }
}