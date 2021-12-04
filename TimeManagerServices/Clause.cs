using System;

namespace TimeManagerServices
{
    public class Clause
    {
        FuzzySet mVariable;
        String mValue;
        String mCondition;

        public Clause(FuzzySet variable, String condition, String value)
        {
            mVariable = variable;
            mValue = value;
            mCondition = condition;
        }

        public override string ToString()
        {
            return mVariable.Name + " " + mCondition + " " + mValue;
        }

        public FuzzySet Variable
        {
            get
            {
                return mVariable;
            }
        }

        public String Value
        {
            get
            {
                return mValue;
            }
        }
    }
}