using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Events.Sample
{
    public class CustomEventArgs : EventArgs
    {
        public string Text { get; set; }
    }
}
