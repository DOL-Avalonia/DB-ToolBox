namespace AmteCreator.DataQuestRewardQuests
{
    public enum GoalType
    {
        Search = 2,             // Search in a specified location to advance the goal.
        Kill = 3,               // Kill the target to advance the goal.
        Interact = 4,           // Interact with the target to advance the goal.
        InteractFinish = 5,     // Interact with the target to finish the quest.
        InteractWhisper = 6,    // Whisper to the target to advance the goal. 
        InteractDeliver = 7,    // Deliver a dummy item to the target to advance the goal.
        DeliverFinish = 8,      // Deliver item to the target to finish the quest.
        GroupKill = 9,
        Collect = 10,           // Player must give the target an item to advance the step	
        Unknown = 255
    }
}
