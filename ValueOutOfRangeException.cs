using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;

        public ValueOutOfRangeException(string i_ExceptionToPresent) : base(i_ExceptionToPresent)
        {
            /// This c'tor gets a message of an excpetion and sends it automaticlly to the father class
        }

        public ValueOutOfRangeException(
            float i_ValToAdd, 
            string i_MemberField, 
            float i_MaxVal, 
            float i_MinVal) 
            : base(
                $"cannot add {i_ValToAdd} to {i_MemberField}, acceptable values for this member are between 0 and {i_MaxVal}.")
        {
            r_MaxValue = i_MaxVal;
            r_MinValue = i_MinVal;
        }

        public ValueOutOfRangeException(
            Exception i_InnerException, 
            float i_ValToAdd, 
            string i_MemberField, 
            float i_MaxVal, 
            float i_MinVal) 
            : base(
                $"cannot add {i_ValToAdd} to {i_MemberField}, acceptable values for this member are between 0 and {i_MaxVal}.", i_InnerException)
        {
            r_MaxValue = i_MaxVal;
            r_MinValue = i_MinVal;
        }
    }
}
