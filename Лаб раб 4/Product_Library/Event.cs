using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotterLibrary
{
    public sealed class Event : EventArgs
    {
        public int Last_ID { get; }
        public int New_ID { get; }
        public Event(int last_id, int new_id)
        {
            Last_ID = last_id;
            New_ID = new_id;
        }
    }
}