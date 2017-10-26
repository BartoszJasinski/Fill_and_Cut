using System;
using System.Collections.Generic;
using System.Linq;

using Sketchpad.Utils;

namespace Sketchpad.Data
{
    class Constraint: IEquatable<Constraint>
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

        public bool Equals(Constraint other)
        {
            if (other == null)
                return false;
            return (constraintMode.Equals(other.constraintMode) && !constrainedEdges.Except(other.constrainedEdges).ToList().Any() && angle == other.angle);

            //return false;
        }
    }
}
