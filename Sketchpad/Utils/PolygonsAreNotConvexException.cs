﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillCut.Utils
{
    class PolygonsAreNotConvexException: Exception
    {
        public PolygonsAreNotConvexException(string message): base(message)
        { }
    }
}
