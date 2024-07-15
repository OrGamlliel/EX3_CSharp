using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;

        public ValueOutOfRangeException(string i_ErrorMessage, float i_MinValue, float i_MaxValue) : base(i_ErrorMessage)
        {
            r_MaxValue = i_MaxValue;
            r_MinValue = i_MinValue;
        }

        public float MaxValue
        {
            get { return r_MaxValue; }
        }

        public float MinValue
        {
            get{ return r_MinValue; }
        }
    }
}
