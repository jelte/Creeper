using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectFTP.Events
{
    public class CharacterActionEventArgs : EventArgs
    {
        public Character.Action action;

        public CharacterActionEventArgs(Character.Action action)
        {
            this.action = action;
        }
    }
}
