using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Events.Sample
{
    public class ShortProcess
    {
        public event EventHandler<CustomEventArgs> Process;

        public void Run(CustomEventArgs e = null) => Process?.Invoke(this, e);
    }
}
