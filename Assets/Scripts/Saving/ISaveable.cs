using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Saving
{
    public interface ISaveable
    {
        object CaptureState();
        void LoadState(object state);
    }
}
