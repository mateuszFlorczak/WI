using System;
using System.Collections.Generic;
using System.Text;

namespace MMCG3
{
    public class AlgException:Exception
    {

        AlgException.Cause cause;

        public enum Cause 
        { 
            CannotDivideRedEdge,
            CannotDivideGreenEdge,
            CannotDivideBlueEdge,
            InvalidDivisionIndex, 
            CuboidTooSmallToDivide,
            PairNotInitializedAfterDivision,
            CuboidsListEmpty,
            CannotDivideintoMoreColors,
            CuboidNotFound,
            ComputationFailed
        }

        public AlgException(AlgException.Cause c)
        {
            cause = c;
        }

        public override string ToString()
        {
            return "Cause: "+CauseToString()+" "+base.ToString();
        }

        public AlgException.Cause ExceptionCause { get { return cause; } }

        private String CauseToString()
        {
            switch (cause)
            {
                case Cause.CannotDivideBlueEdge: return "Blue edge cannot be divided";
                case Cause.CannotDivideGreenEdge: return "Green edge cannot be divided";
                case Cause.CannotDivideRedEdge: return "red edge cannot be divided";
                case Cause.CuboidsListEmpty: return "Cuboids list empty";
                case Cause.CuboidTooSmallToDivide: return "Cuboid too small to divide";
                case Cause.InvalidDivisionIndex: return "Invalid division index";
                case Cause.PairNotInitializedAfterDivision: return "Pair was not initialized during division";
                default: return "Cause not determined";
            }
        }
    }
}
