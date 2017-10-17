using System;
using System.Collections.Generic;

using Sketchpad.Utils;

namespace Sketchpad.Data
{
    class Constraint
    {
        public ConstraintMode constraintMode { get; set; }
        public List<Tuple<int, int>> constrainedEdges { get; set; }
        public double angle { get; set; }

        public Constraint(ConstraintMode constraintMode, List<Tuple<int, int>> constrainedEdges, double angle)
        {
            this.constraintMode = constraintMode;
            this.constrainedEdges = constrainedEdges;
            this.angle = angle;
        }

        public Constraint(ConstraintMode constraintMode, List<Tuple<int, int>> constraindedEdges): this(constraintMode, constraindedEdges, -1)
        {
        }

    }
}
