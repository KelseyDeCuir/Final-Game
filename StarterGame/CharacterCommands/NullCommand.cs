using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class NullCommand : Command
    {
        public override bool Execute(Character player)
        {
            return false;
        }
    }
}
