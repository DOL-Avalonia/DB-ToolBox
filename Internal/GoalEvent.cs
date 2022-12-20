using System;

namespace AmteCreator.Internal
{
    class GoalEventArgs
        : EventArgs
    {
        public bool ShouldSavePreviousStep
        {
            get;
            set;
        }       
    }
}
