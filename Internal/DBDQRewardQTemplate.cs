using System;

namespace AmteCreator.Internal
{
    [Serializable]
    public class DBDQRewardQTemplate        
    {
        public int? ID
        {
            get; set;
        }

        /// <summary>
        /// The name of this quest
        /// </summary>
        public string QuestName
        {
            get; set;
        }

        /// <summary>
        /// The name of the object that starts this quest
        /// </summary>
        
        public string StartNPC
        {
            get; set;
        }

        /// <summary>
        /// The region id where this quest starts
        /// </summary>
        
        public ushort StartRegionID
        {
            get; set;
        }

        /// <summary>
        /// The quest story shown to player upon being offered the quest
        /// </summary>
        
        public string StoryText
        {
            get; set;
        }

        /// <summary>
        /// Summary of the quest shown in journal
        /// </summary>
        
        public string Summary
        {
            get; set;
        }

        /// <summary>
        /// Additional text displayed to the player upon accepting the quest
        /// </summary>
        
        public string AcceptText
        {
            get; set;
        }

        /// <summary>
        /// Goal description and what step it is given. 
        /// Format: kill two bandits;1
        /// </summary>
        
        public string QuestGoals
        {
            get; set;
        }

        /// <summary>
        /// Type of each step (kill, give, collect, etc)		
        /// </summary>
        
        public string GoalType
        {
            get; set;
        }

        /// <summary>
        /// Description to show to start quest
        /// Not Used
        /// </summary>
        
        public string Description
        {
            get; set;
        }

        /// <summary>
        /// Who to talk to for each step
        /// Format: SourceName;RegionID|SourceName;RegionID
        /// Not Used
        /// </summary>
        
        public string SourceName
        {
            get; set;
        }


        /// <summary>
        /// The text for each source
        /// Format:  Step 1 Source text|Step 2 Source text
        /// Not Used
        /// </summary>
        
        public string SourceText
        {
            get; set;
        }

        /// <summary>
        /// Type of each step (kill, give, collect, etc)
        /// Format: Step1Type|Step2Type
        /// not used
        /// </summary>
        
        public string StepType
        { 
            get; set;
        }

        /// <summary>
        /// Items given to the player at a step
        /// Format: id_nb|idnb
        /// </summary>
        
        public string StepItemTemplates
        {
            get; set;
        }

        /// <summary>
        /// Name of the target for each step
        /// Format: TargetName;RegionID|TargetName;RegionID
        /// not used
        /// </summary>
        
        public string TargetName
        {
            get; set;
        }

        /// <summary>
        /// Text for each target
        /// not used
        /// Format:  Step 1 Target text|Step 2 Target text| ...
        /// </summary>
        
        public string TargetText
        {
            get; set;
        }

        /// <summary>
        /// Description text for each step
        /// Format: Step 1 Text|Step 2 Text
        /// </summary>
        
        public string StepText
        {
            get; set;
        }



        /// <summary>
        /// how many times goal must be repeated to be achieved
        /// </summary>
        
        public string GoalRepeatNo
        {
            get; set;
        }

        /// <summary>
        /// Name of the target for each step		
        /// </summary>
        
        public string GoalTargetName
        {
            get; set;
        }

        /// <summary>
        /// The name of this quest
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// The name of the object that starts this quest
        /// not used
        /// </summary>
        
        public string StartName
        {
            get; set;
        }

        /// <summary>
        /// The start type of this quest (eStartType)
        /// not used
        /// </summary>
        
        public byte StartType
        {
            get; set;
        }

        /// <summary>
        /// Text a target will say to player 		
        /// </summary>
        
        public string GoalTargetText
        {
            get; set;
        }

        /// <summary>
        /// How many steps in this quest? used to display goals at certain stages of quest
        /// </summary>
        
        public int StepCount
        {
            get; set;
        }

        /// <summary>
        /// The NPC name who finishes the quest
        /// </summary>
        
        public string FinishNPC
        {
            get; set;
        }

        /// <summary>
        /// Text required to advance to the next step
        /// Format: Step 1 Text|Step 2 Text
        /// </summary>
        
        public string AdvanceText
        {
            get; set;
        }

        /// <summary>
        /// ItemTemplate id_nb to be collected to finish the current step
        /// Format: id_nb|id_nb||  steps with no item to collect should be blank
        /// </summary>
        
        public string CollectItemTemplate
        {
            get; set;
        }

        /// <summary>
        /// Max number of times a player can do this quest
        /// </summary>
        
        public ushort MaxCount
        {
            get; set;
        }

        /// <summary>
        /// Minimum level a player has to be to start this quest
        /// </summary>
        
        public byte MinLevel
        {
            get; set;
        }

        /// <summary>
        /// Max level a player can be and still do this quest
        /// </summary>
        
        public byte MaxLevel
        {
            get; set;
        }

        /// <summary>
        /// Reward Money to give at quest completion, 0 for none		
        /// </summary>
        
        public long RewardMoney
        {
            get; set;
        }

        public int RewardReputation
        {
            get; set;
        }

        /// <summary>
        /// Reward XP to give at quest completion, 0 for none        
        /// </summary>
        
        public long RewardXP
        {
            get; set;
        }

        /// <summary>
        /// Reward CLXP to give at quest completion, 0 for none        
        /// </summary>
        
        public long RewardCLXP
        {
            get; set;
        }

        /// <summary>
        /// Reward RP to give at quest completion, 0 for none        
        /// </summary>
        
        public long RewardRP
        {
            get; set;
        }

        /// <summary>
        /// Reward BP to give at quest completion, 0 for none        
        /// </summary>
        
        public long RewardBP
        {
            get; set;
        }

        /// <summary>
        /// The ItemTemplate id_nb(s) to give as a optional rewards
        /// Format:  #id_nb1|id_nb2 with first character being the number of choices
        /// </summary>
        
        public string OptionalRewardItemTemplates
        {
            get; set;
        }

        /// <summary>
        /// The ItemTemplate id_nb(s) to give as a final reward
        /// Format:  id_nb1|id_nb2
        /// </summary>
        
        public string FinalRewardItemTemplates
        {
            get; set;
        }

        /// <summary>
        /// Text to show the user once the quest is finished.
        /// </summary>
        
        public string FinishText
        {
            get; set;
        }

        /// <summary>
        /// The name or names of other quests that need to be done before this quest can be offered.
        /// Name Quest One|Name Quest Two... Can be null if no dependency
        /// </summary>
        
        public string QuestDependency
        {
            get; set;
        }

        /// <summary>
        /// Player classes that can do this quest.  Null for all.
        /// </summary>
        public string AllowedClasses
        {
            get; set;
        }


        
        public bool IsRenaissance
        {
            get; set;
        }

        /// <summary>
        /// Player Races that can do this quest.  Null for all.
        /// </summary>
        public string AllowedRaces
        {
            get; set;
        }

        /// <summary>
        /// Code that can be used for various quest activities		
        /// </summary>
        
        public string ClassType
        {
            get; set;
        }

        //the following is used for adding the quest marker on the map // patch 0026
        /// <summary>
        /// Xloc of a goal target. Used to display red dot on map
        /// Value is from the /loc command
        /// Format: value1|value2|value3
        /// can be 0 for the interactFinish goal
        /// </summary>
        
        public string XOffset
        {
            get; set;
        }
        /// <summary>
        /// Yloc of a goal target. Used to display red dot on map
        /// Value is from the /loc command
        /// Format: value1|value2|value3
        /// can be 0 for the interactFinish goal
        /// </summary>
        
        public string YOffset
        {
            get; set;
        }
        /// <summary>
        /// ZoneID (not RegionID!) of a goal target. Used to display red dot on map        
        /// Format: value1|value2|value3
        /// can be 0 for the interactFinish goal
        /// </summary>
        
        public string ZoneID
        {
            get; set;
        }

        public string Reputation
        {
            get; set;
        }

        public DBDQRewardQTemplate()
        {
        }

        public static DBDQRewardQTemplate GetQuestFromJson(dynamic model)
        {
            bool isRenaissance;
            if (!bool.TryParse(model.IsRenaissance, out isRenaissance))
            {
                int renaissance = int.Parse(model.IsRenaissance);
                isRenaissance = renaissance == 0 ? false : true;
            }

            return new DBDQRewardQTemplate()
            {
                AcceptText = model.AcceptText,
                AllowedClasses = model.AllowedClasses,
                AllowedRaces = model.AllowedRaces,
                ClassType = model.ClassType,
                CollectItemTemplate = model.CollectItemTemplate,
                Description = model.Description,
                FinalRewardItemTemplates = model.FinalRewardItemTemplates,
                FinishNPC = model.FinishNPC,
                FinishText = model.FinishText,
                GoalRepeatNo = model.GoalRepeatNo,
                GoalTargetName = model.GoalTargetName,
                GoalTargetText = model.GoalTargetText,
                GoalType = model.GoalType,
                ID = int.Parse(model.ID),
                IsRenaissance = isRenaissance,
                MaxCount = ushort.Parse(model.MaxCount),
                MaxLevel = byte.Parse(model.MaxLevel),
                MinLevel = byte.Parse(model.MinLevel),
                Name = model.Name,
                OptionalRewardItemTemplates = model.OptionalRewardItemTemplates,
                QuestDependency = model.QuestDependency,
                QuestGoals = model.QuestGoals,
                QuestName = model.QuestName,
                RewardBP = long.Parse(model.RewardBP),
                RewardCLXP = long.Parse(model.RewardCLXP),
                RewardMoney = long.Parse(model.RewardMoney),
                RewardRP = long.Parse(model.RewardRP),
                RewardXP = long.Parse(model.RewardXP),
                SourceName = model.SourceName,
                SourceText = model.SourceText,
                StartName = model.StartName,
                StartNPC = model.StartNPC,
                StartRegionID = ushort.Parse(model.StartRegionID),
                StartType = byte.Parse(model.StartType),
                StepCount = int.Parse(model.StepCount),
                StepItemTemplates = model.StepItemTemplates,
                StepText = model.StepText,
                StepType = model.StepType,
                StoryText = model.StoryText,
                Summary = model.Summary,
                TargetName = model.TargetName,
                TargetText = model.TargetText,
                XOffset = model.XOffset,
                YOffset = model.YOffset,
                ZoneID = model.ZoneID,
                AdvanceText = model.AdvanceText,
                Reputation = model.Reputation ?? string.Empty               
            };
        }
    }
}
