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
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// The name of the object that starts this quest
        /// </summary>

        public string NpcName
        {
            get; set;
        }

        /// <summary>
        /// The region id where this quest starts
        /// </summary>

        public ushort NpcRegion
        {
            get; set;
        }

        /// <summary>
        /// The quest story shown to player upon being offered the quest
        /// </summary>

        public string Story
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

        public long RewardReputation
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

        public string Conclusion
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

        public string Reputation
        {
            get; set;
        }

        /// <summary>
        /// Json with all the goal data
        /// </summary>
        public string GoalsJson
        {
            get; set;
        }

        /// <summary>
        /// Number of optional items to choose from
        /// </summary>
        public int NbChooseOptionalItems
        {
            get; set;
        }

        /// <summary>
        /// Description of whole quest
        /// </summary>
        public string Description
        {
            get; set;
        }

        /// <summary>
        /// Should this quest Start an event
        /// </summary>
        public bool StartEvent
        {
            get; set;
        }

        /// <summary>
        /// Should this quest Reset an event
        /// </summary>
        public bool ResetEvent
        {
            get; set;
        }

        /// <summary>
        /// Should this quest Start an event on end
        /// </summary>
        public bool EndStartEvent
        {
            get; set;
        }

        /// <summary>
        /// Should this quest Reset an event on end
        /// </summary>
        public bool EndResetEvent
        {
            get; set;
        }

        /// <summary>
        /// Quest id to start/reset at the beginning of this quest
        /// </summary>
        public string StartEventId
        {
            get; set;
        }

        /// <summary>
        /// Quest id to start/reset at the end of this quest
        /// </summary>
        public string EndEventId
        {
            get; set;
        }

        public DBDQRewardQTemplate()
        {
        }

        public static DBDQRewardQTemplate GetQuestFromJson(dynamic model)
        {
            //model is from db with db names
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
                FinalRewardItemTemplates = model.FinalRewardItemTemplates,
                Conclusion = model.Conclusion,
                ID = int.Parse(model.Id),
                IsRenaissance = isRenaissance,
                MaxCount = ushort.Parse(model.MaxCount),
                MaxLevel = byte.Parse(model.MaxLevel),
                MinLevel = byte.Parse(model.MinLevel),
                Name = model.Name,
                NpcName = model.NpcName,
                NpcRegion = ushort.Parse(model.NpcRegion),
                OptionalRewardItemTemplates = model.OptionalRewardItemTemplates,
                QuestDependency = model.QuestDependency,
                RewardBP = long.Parse(model.RewardBP),
                RewardCLXP = long.Parse(model.RewardCLXP),
                RewardMoney = long.Parse(model.RewardMoney),
                RewardRP = long.Parse(model.RewardRP),
                RewardXP = long.Parse(model.RewardXP),
                Story = model.Story,
                Summary = model.Summary,
                Reputation = model.Reputation ?? string.Empty,
                RewardReputation = long.Parse(model.RewardReputation),
                GoalsJson = model.GoalsJson,
                NbChooseOptionalItems = int.Parse(model.NbChooseOptionalItems),
                Description = model.Description,
                StartEvent = bool.Parse(model.StartEvent ?? "false"),
                ResetEvent = bool.Parse(model.ResetEvent ?? "false"),
                EndStartEvent = bool.Parse(model.EndStartEvent ?? "false"),
                EndResetEvent = bool.Parse(model.EndResetEvent ?? "false"),
                StartEventId = int.Parse(model.StartEventId ?? ""),
                EndEventId = int.Parse(model.EndEventId ?? "0)
            };
        }
    }
}
