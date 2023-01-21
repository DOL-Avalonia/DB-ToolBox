using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AmteCreator.Internal;
using System.ComponentModel;
using System.Web;
using System.Reflection;
using System.Numerics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AmteCreator.DataQuestRewardQuests
{
    public partial class DataQuestRewardQuests : UserControl
    {
        //private readonly RewardQuestService _questService;
        private DBDQRewardQTemplate _quest;

        public readonly string dataTableName = "dataquestjson";
        
        public static Dictionary<string, string> GoalTypeNames = new Dictionary<string, string>()
        {
            {"DOL.GS.Quests.AbortQuestGoal", "Abort"},
            {"DOL.GS.Quests.CollectGoal", "Collect"},
            {"DOL.GS.Quests.EnterAreaGoal", "EnterArea"},
            {"DOL.GS.Quests.InteractGoal", "Interact"},
            {"DOL.GS.Quests.KilledGoal", "Killed"},
            {"DOL.GS.Quests.KillGoal", "Kill"},
            {"DOL.GS.Quests.KillGroupMobGoal", "KillGroupMob"},
            {"DOL.GS.Quests.KillPlayerGoal", "KillPlayer"},
            {"DOL.GS.Quests.StopGoal", "Stop"},
            {"DOL.GS.Quests.TimerGoal", "Timer"},
            {"DOL.GS.Quests.UseItemGoal", "UseItem"},
            {"DOL.GS.Quests.WhisperGoal", "Whisper"},
            {"DOL.GS.Quests.EndGoal", "End"},
        };
        public static List<GoalType>  UsingRepeatNo = new List<GoalType>()
        {
            GoalType.Collect,
            GoalType.Kill,
            GoalType.KillGroupMob,
            GoalType.KillPlayer,
        };
        public static List<GoalType>  UsingItem = new List<GoalType>()
        {
            GoalType.Collect,
            GoalType.UseItem,
        };
        public static List<GoalType>  UsingArea = new List<GoalType>()
        {
            GoalType.EnterArea,
            GoalType.Kill,
            GoalType.KillGroupMob,
            GoalType.KillPlayer,
            GoalType.UseItem,
        };
        public static List<GoalType>  UsingDestroyItem = new List<GoalType>()
        {
            GoalType.UseItem,
        };
        public bool IsUsingRepeatNo(GoalType goalType)
        {
            return UsingRepeatNo.Contains(goalType);
        }
        public bool IsUsingItem(GoalType goalType)
        {
            return UsingItem.Contains(goalType);
        }
        public bool IsUsingArea(GoalType goalType)
        {
            return UsingArea.Contains(goalType);
        }
        public bool IsUsingDestroyItem(GoalType goalType)
        {
            return UsingDestroyItem.Contains(goalType);
        }
        public readonly string idField = nameof(DBDQRewardQTemplate.ID);
        public readonly string idName = nameof(DBDQRewardQTemplate.Name);

        string goalType, questGoals, goalRepeatNo, goalTargetName, goalTargetText, collectItemTemplate, stepItemTemplate, goalStepNo, goalAdvanceText, questDependency;
        string optionalRewardItemTemplates = "", finalRewardItemTemplates = "";
        string allowedClasses;
        string allowedRaces;
        string xloc, yloc, zoneId;
        string reputation;

        // using this to determine if quest can be saved back to original ID
        private bool LoadedQuest { get; set; } = false;


        /// <summary>
        /// goal info
        /// GoalType Enum - List Index
        /// </summary>
        public Dictionary<int, int> goaltype_dictionary;
        public List<GoalType> GoalSteps;

        //message
        public Dictionary<int, string> messageStarted_dictionary;
        public Dictionary<int, string> messageAborted_dictionary;
        public Dictionary<int, string> messageDone_dictionary;
        public Dictionary<int, string> messageCompleted_dictionary;
        
        public Dictionary<int, string> stepItemTemplate_dictionnary;
        public Dictionary<int, string> stepStartItemTemplate_dictionnary;

        public Dictionary<int, string> goaltext_dictionary;
        public Dictionary<int, string> goalrepeatno_dictionary;
        public Dictionary<int, string> goaltargetname_dictionary;
        public Dictionary<int, string> goaltargettext_dictionary;
        public Dictionary<int, string> goalAdvanceText_dictionnary;
        public Dictionary<int, string> item_dictionary;
        public Dictionary<int, string> goalstepno_dictionary;
        public Dictionary<int, string> targetRegions_dictionary;
        public Dictionary<int, string> xloc_dictionary;
        public Dictionary<int, string> yloc_dictionary;
        public Dictionary<int, string> arearadius_dictionary;
        public Dictionary<int, string> zoneid_dictionary;
        // item rewards
        public Dictionary<int, string> opt_dictionary;
        public Dictionary<int, string> fin_dictionary;        
        // quest restrictions
        private Dictionary<int, string> allClasses;
        //timer
        public Dictionary<int, string> seconds_dictionary;
        public Dictionary<int, Dictionary<int, string>> questDependance_dictionary;
        public Dictionary<int, Dictionary<int, string>> questStop_dictionary;
        
        public Dictionary<int, string> destroyItem_dictionary;
        /// <summary>
        /// Index - Byte Enum Value
        /// </summary>
        private Dictionary<int, int> allRaces;

        public DataQuestRewardQuests()
        {
           InitializeComponent();
         //   _questService = new RewardQuestService();

            opt_dictionary = new Dictionary<int, string>();
            fin_dictionary = new Dictionary<int, string>();
            //advtext_dictionary = new Dictionary<int, string>();
            item_dictionary = new Dictionary<int, string>();
            stepItemTemplate_dictionnary = new Dictionary<int, string>();
            stepStartItemTemplate_dictionnary = new Dictionary<int, string>();
            goalrepeatno_dictionary = new Dictionary<int, string>();
            goaltargetname_dictionary = new Dictionary<int, string>();
            goaltargettext_dictionary = new Dictionary<int, string>();
            goaltext_dictionary = new Dictionary<int, string>();
            goaltype_dictionary = new Dictionary<int, int>();
            goalstepno_dictionary = new Dictionary<int, string>();
            xloc_dictionary = new Dictionary<int, string>();
            yloc_dictionary = new Dictionary<int, string>();
            arearadius_dictionary = new Dictionary<int, string>();
            zoneid_dictionary = new Dictionary<int, string>();
            allClasses = new Dictionary<int, string>();
            allRaces = new Dictionary<int, int>();
            targetRegions_dictionary = new Dictionary<int, string>();
            goalAdvanceText_dictionnary = new Dictionary<int, string>();
            seconds_dictionary = new Dictionary<int, string>();
            GoalSteps = new List<GoalType>();
            messageStarted_dictionary = new Dictionary<int, string>();
            messageAborted_dictionary = new Dictionary<int, string>();
            messageDone_dictionary = new Dictionary<int, string>();
            messageCompleted_dictionary = new Dictionary<int, string>();
            questDependance_dictionary = new Dictionary<int, Dictionary<int, string>>();
            questStop_dictionary = new Dictionary<int, Dictionary<int, string>>();
            questDependance_dictionary[1] = new Dictionary<int, string>();
            questStop_dictionary[1] = new Dictionary<int, string>();
            destroyItem_dictionary = new Dictionary<int, string>();

            PopulateClassDictionary();
            PopulateAllowRacesListBox();
            PopulateGoalTypeListBox();
        }

        // display all the quests currently in the database
        private void questSearch_Click(object sender, EventArgs e)
        {
            var search = new OpenNPCTemplateForm(this.dataTableName, this.idField, this.idName);

            search.ShowDialog(this);

            if (search.DialogResult == DialogResult.OK)
            {
                this._quest = search.SelectedQuest;
                this.DeserializeData(this._quest);
            }
        }
        
        // load a quest from an ID
        private async void questLoad_Click(object sender, EventArgs e)
        {
            var template = new BindingList<DataQuestRewardQuests>();
            template.Add(this);

            var dialog = new OpenNPCTemplateForm()
            {
                ItemName = @"Please enter Quest ID" 
            };

            if (dialog.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.ItemName))
            {
           //     await LoadQuest(dialog.Input.Text);
            }
            dialog.Dispose();
        }
        
        /// <summary>
        /// convert database format kill bandit;2|talk to NPC;3 to usable format
        /// </summary>        
        private void DeserializeData(DBDQRewardQTemplate quest)
        {
            if (_quest == null)
            {
                return;
            }
            Clear();

            // try
            // {
                questDependency = quest.QuestDependency;
                _ID.Text = quest.ID?.ToString();
                _MinLevel.Text = quest.MinLevel.ToString();
                _MaxLevel.Text = quest.MaxLevel.ToString();
                _Name.Text = quest.Name ?? string.Empty;
                _NpcName.Text = quest.NpcName ?? string.Empty;
                _NpcRegion.Text = quest.NpcRegion.ToString();
                _StoryText.Text = quest.Story ?? string.Empty;
                _Summary.Text = quest.Summary ?? string.Empty;
                _AcceptText.Text = quest.AcceptText ?? string.Empty;
                _MaxCount.Text = quest.MaxCount.ToString();
                _IsRenaissance.Checked = quest.IsRenaissance;
                _Conclusion.Text = quest.Conclusion ?? string.Empty;
                _suppressGoal.Enabled = false;
                _RewardMoney.Text = quest.RewardMoney.ToString();
                ReputationReward.Text = quest.RewardReputation.ToString();
                _RewardXP.Text = quest.RewardXP.ToString();
                _RewardCLXP.Text = quest.RewardCLXP.ToString();
                _RewardBP.Text = quest.RewardBP.ToString();
                _RewardRP.Text = quest.RewardRP.ToString();
                GoalNumber.Text = "1";
                Reputation.Text = quest.Reputation;
                _DescriptionText.Text = quest.Description;
                //formatted like [{"Id":0,"Type":"DOL.GS.Quests.KillGoal","Data":{"Describtion":"None","TargetName":"Quest82mob","TargetRegion":"330","KillCount":1,}},{"Id":1,"Type":"DOL.GS.Quests.EndGoal","Data":{"Describtion":"None","TargetName":"Quest82","TargetRegion":"330"}}]
                string GoalsJson = quest.GoalsJson;

                string rawOptRewards = quest.OptionalRewardItemTemplates;
                //string numOptChoices = string.IsNullOrWhiteSpace(rawOptRewards) ? "1" : rawOptRewards.Substring(0, 1), optionalRewardItemTemplates = rawOptRewards.Substring(1);
                if (!string.IsNullOrWhiteSpace(rawOptRewards))
                {
                    OptRewardUpDown.Value = quest.NbChooseOptionalItems;
                    optionalRewardItemTemplates = rawOptRewards;
                }
                else
                {
                    optionalRewardItemTemplates = string.Empty;
                }


                finalRewardItemTemplates = quest.FinalRewardItemTemplates;

                 //quest.GoalType.Split(new string[] { "|" }, StringSplitOptions.None).Select(e => Enum.TryParse<GoalType>(e, out GoalType goal) ? goal : GoalType.Unknown).ToList(); //rem
                //get types from json format in GoalJson
                var goals = JsonConvert.DeserializeObject<JArray>(GoalsJson);
                
                foreach (var json in goals)
                {
                    var (id, type, data) = (json.Value<ushort>("Id"), json.Value<string>("Type"), json.Value<dynamic>("Data"));
                    GoalSteps.Add((GoalType)Enum.Parse(typeof(GoalType), GoalTypeNames.FirstOrDefault(e => e.Key == type).Value));

                    goaltext_dictionary.Add(id, (string)data.Description ?? "");
                    messageStarted_dictionary.Add(id, (string)data.MessageStarted ?? "");
                    messageCompleted_dictionary.Add(id, (string)data.MessageCompleted ?? "");
                    messageDone_dictionary .Add(id, (string)data.MessageDone ?? "");
                    messageAborted_dictionary.Add(id, (string)data.MessageAborted ?? "");
                    stepItemTemplate_dictionnary.Add(id, (string)data.GiveItem ?? "");
                    stepStartItemTemplate_dictionnary.Add(id, (string)data.StartItem ?? "");

                    goaltargetname_dictionary.Add(id, (string)data.TargetName ?? "");
                    targetRegions_dictionary.Add(id, (string)data.TargetRegion ?? "");

                    if (type == "DOL.GS.Quests.KillGoal")
                        goalrepeatno_dictionary.Add(id, (string)data.KillCount ?? "");
                    else if (type == "DOL.GS.Quests.CollectGoal")
                        goalrepeatno_dictionary.Add(id, (string)data.ItemCount ?? "");
                    else
                        goalrepeatno_dictionary.Add(id, "");

                    item_dictionary.Add(id, (string)data.Item ?? "");
                    goaltargettext_dictionary.Add(id, (string)data.Text ?? "");
                    goalAdvanceText_dictionnary.Add(id, (string)data.WhisperText ?? "");
                    if( data.AreaCenter != null)
                    {
                        xloc_dictionary.Add(id, (string)data.AreaCenter.X ?? "");
                        yloc_dictionary.Add(id, (string)data.AreaCenter.Y ?? "");
                    }
                    else
                    {
                        xloc_dictionary.Add(id, "");
                        yloc_dictionary.Add(id, "");
                    }
                    arearadius_dictionary.Add(id, (string)data.AreaRadius ?? "");
                    zoneid_dictionary.Add(id, (string)data.AreaRegion ?? "");
                    seconds_dictionary.Add(id, (string)data.Seconds ?? "");

                    questDependance_dictionary.Remove(id);
                    questDependance_dictionary.Add(id,  new Dictionary<int, string>());
                    if(data.StartGoalsDone != null)
                        foreach (var goal in data.StartGoalsDone)
                            questDependance_dictionary[id].Add((int)goal, "");

                    questStop_dictionary.Remove(id);
                    questStop_dictionary.Add(id,  new Dictionary<int, string>());
                    if(data.StopGoals != null)
                        foreach (var goal in data.StopGoals)
                            questStop_dictionary[id].Add((int)goal, "");
                    destroyItem_dictionary.Add(id, (string)data.DestroyItem ?? "");
                            
                }
                _GoalRepeatNo.Text = goalrepeatno_dictionary[1];
                _GoalTargetName.Text = goaltargetname_dictionary[1];
                _targetRegion.Text = targetRegions_dictionary[1];
                _GoalTargetText.Text = goaltargettext_dictionary[1];
                richTextBoxStarted.Text = messageStarted_dictionary[1];
                richTextBoxCompleted.Text = messageCompleted_dictionary[1];
                richTextBoxDone.Text = messageDone_dictionary[1];
                richTextBoxAborted.Text = messageAborted_dictionary[1];
                _GoalOptional.Text = item_dictionary[1];
                _StepItemTemplate.Text = stepItemTemplate_dictionnary[1];
                _StepStartItemTemplate.Text = stepStartItemTemplate_dictionnary[1];
                _XOffset.Text = xloc_dictionary[1];
                _YOffset.Text = yloc_dictionary[1];
                _QuestGoals.Text = goaltext_dictionary[1];
                GoalStepNo.Text = "1";
                _ZoneID.Text = zoneid_dictionary[1];
                _Radius.Text = arearadius_dictionary[1];
                checkBoxDestroyItem.Checked = destroyItem_dictionary[1] == "True";

                if (GoalSteps.Count > 0)
                {                   
                    if (goaltype_dictionary.ContainsKey((int)GoalSteps[0]))
                    {
                        _GoalType.SelectedIndex = goaltype_dictionary[(int)GoalSteps[0]];
                    }
                }
                else
                {
                    _GoalType.SelectedIndex = 0;
                    GoalSteps.Add(GoalType.Interact);
                }

                string[] splitOptionalRewards = optionalRewardItemTemplates.Split(new string[] { "|" }, StringSplitOptions.None);
                StringToDictionary(splitOptionalRewards, opt_dictionary);
                _OptionalReward.Text = opt_dictionary[1];

                if (finalRewardItemTemplates != null)
                {
                    string[] splitFinalRewards = finalRewardItemTemplates.Split(new string[] { "|" }, StringSplitOptions.None);
                    StringToDictionary(splitFinalRewards, fin_dictionary);
                    _FinalReward.Text = fin_dictionary[1];
                }
                else
                {
                    _FinalReward.Text = null;
                }

                if (questDependency != null)
                {
                    string[] questDependencies = questDependency.Split(new string[] { "|" }, StringSplitOptions.None);
                    _QuestDependency.Text = questDependencies[0];
                }

                PopulateGoalDependanceListBox(1);
                PopulateGoalStopListBox(1);
                UpdateOptionalFields();
                SelectAllowedClasses();
                SelectAllowedRaces();
                LoadedQuest = true;
            //}
            // catch (Exception g)
            // {
            //     MessageBox.Show(g.Message, "Error while deserializing data! Quest was not loaded completely - Errors in database format.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // }
        }

        private void UpdateGoalDependanceList(int goalId)
        {
            UpdateGoalList(listGoalDependance, questDependance_dictionary, goalId);
        }
        private void UpdateGoalStopList(int goalId)
        {
            UpdateGoalList(listGoalStop, questStop_dictionary, goalId);
        }
        private void UpdateGoalList(ListBox list, Dictionary<int, Dictionary<int, string>> quest_dictionary, int goalId, bool defaultNew = true)
        {
            
            list.ClearSelected();
            
            foreach (var Id in quest_dictionary[goalId].Keys)
            {
                if(Id < goalId)
                    list.SetSelected(Id - 1, true);
                else
                    list.SetSelected(Id - 2, true);
            }
            if(defaultNew && goalId == quest_dictionary.Count && list.SelectedItems.Count == 0)
                list.SetSelected(goalId - 2, true);
        }

        private void PopulateGoalDependanceListBox(int goalId)
        {
            PopulateGoalList(listGoalDependance, questDependance_dictionary, goalId);
        }
        private void PopulateGoalStopListBox(int goalId)
        {
            PopulateGoalList(listGoalStop, questStop_dictionary, goalId, false);
        }
        private void PopulateGoalList(ListBox list, Dictionary<int, Dictionary<int, string>> quest_dictionary, int goalId, bool defaultNew = true)
        {
            list.Items.Clear();
            int index = 1;

            foreach (var val in GoalSteps)
            {
                if(index != goalId)
                {
                    list.Items.Add(index.ToString() + ": " + val.ToString());
                }
                index++;
            }
            UpdateGoalList(list, quest_dictionary, goalId, defaultNew);
        }
        private void SetGoalDependanceList(int goalId)
        {
            SetGoalList(listGoalDependance, questDependance_dictionary, goalId);    
        }
        private void SetGoalStopList(int goalId)
        {
            SetGoalList(listGoalStop, questStop_dictionary, goalId);    
        }
        private void SetGoalList(ListBox list, Dictionary<int, Dictionary<int, string>> quest_dictionary, int goalId)
        {
            quest_dictionary[goalId].Clear();

            foreach (var val in list.SelectedItems.Cast<string>())
            {
                int Id = int.Parse(val.Split(':')[0]);
                quest_dictionary[goalId].Add(Id, "");
            }
        }

        private void SelectAllowedRaces()
        {
            listRaces.ClearSelected();
            string[] splitAllowedRaces;
            if (!string.IsNullOrWhiteSpace(_quest.AllowedRaces))
            {
                splitAllowedRaces = _quest.AllowedRaces.Split(new string[] { "|" }, StringSplitOptions.None);

                foreach (string stringIndex in splitAllowedRaces)
                {               
                    if (int.TryParse(stringIndex, out int byteIndex))
                    {
                        if (allRaces.Count > byteIndex)
                        {
                            listRaces.SetSelected(allRaces[byteIndex], true);
                        }                 
                    }                    
                }
            }
            else
            {
                listRaces.SetSelected(0, true);
            }
        }

        private void UpdateOptionalFields()
        {
            int step = int.Parse(GoalNumber.Text);

            _goalOptionalLabel.Hide();
            _GoalOptional.Hide();
            label24.Hide();
            _GoalRepeatNo.Hide();
            labelDestroyItem.Hide();
            checkBoxDestroyItem.Hide();
            if (GoalSteps.Count >= step)
            {
                var goalType = GoalSteps[step - 1];
                if (IsUsingRepeatNo(goalType))
                {
                    label24.Show();
                    _GoalRepeatNo.Show();
                }
                if (IsUsingItem(goalType))
                {
                    _goalOptionalLabel.Show();
                    _GoalOptional.Show();
                    goalAdvanceText_dictionnary.Remove(step);

                    _goalOptionalLabel.Text = "Item Id_nb";

                    if (item_dictionary.Count+1 < GoalSteps.Count)
                    {
                        for(int i = 1; i <= GoalSteps.Count; i++)
                        {
                            item_dictionary.Add(i, string.Empty);
                        }
                    }

                    if (item_dictionary.ContainsKey(step))
                    {
                        _GoalOptional.Text = item_dictionary[step];
                    }
                    else
                    {
                        _GoalOptional.Text = string.Empty;
                    }

                }
                else if (goalType == GoalType.Whisper || goalType == GoalType.Timer)
                {
                    _goalOptionalLabel.Show();
                    _GoalOptional.Show();
                    if (goalType == GoalType.Whisper)
                    {
                        if (goalAdvanceText_dictionnary.Count == 0)
                        {
                            for (int i = 1; i <= GoalSteps.Count; i++)
                            {
                                goalAdvanceText_dictionnary.Add(i, string.Empty);
                            }
                        }
                        if (goalAdvanceText_dictionnary.ContainsKey(step))
                        {
                            _GoalOptional.Text = goalAdvanceText_dictionnary[step];
                        }
                        else
                        {
                            _GoalOptional.Text = string.Empty;
                        }

                        _goalOptionalLabel.Text = "Whisper Text";
                    }
                    else{
                        
                        if (seconds_dictionary.Count == 0)
                        {
                            for (int i = 1; i <= GoalSteps.Count; i++)
                            {
                                seconds_dictionary.Add(i, string.Empty);
                            }
                        }

                        if (seconds_dictionary.ContainsKey(step))
                        {
                            _GoalOptional.Text = seconds_dictionary[step];
                        }
                        else
                        {
                            _GoalOptional.Text = string.Empty;
                        }
                        _goalOptionalLabel.Text = "Seconds";

                    }
                }
                if (IsUsingDestroyItem(goalType))
                {
                    labelDestroyItem.Show();
                    checkBoxDestroyItem.Show();
                }
            }          
        }

        private void SelectAllowedClasses()
        {
            listClasses.ClearSelected();
            string[] splitAllowedClasses;
            if (!string.IsNullOrWhiteSpace(_quest.AllowedClasses))
            {
                splitAllowedClasses = _quest.AllowedClasses.Split(new string[] { "|" }, StringSplitOptions.None);

                for (int i = 0; i < splitAllowedClasses.Length; i++)
                {
                    if (int.TryParse(splitAllowedClasses[i], out int result))
                    {
                        splitAllowedClasses[i] = allClasses[result];
                    }
                }

                for (int i = 0; i < splitAllowedClasses.Length; i++)
                {
                    for (int j = 0; j < listClasses.Items.Count; j++)
                    {
                        string cls = listClasses.Items[j].ToString();
                        if (cls == splitAllowedClasses[i])
                        {
                            listClasses.SetSelected(j, true);
                            continue;
                        }
                    }
                }
            }
            else
            {
                listClasses.SetSelected(0, true);
            }
        }

        private void PopulateGoalTypeListBox()
        {
            goaltype_dictionary.Clear();
            int index = 0;
            foreach (var val in Enum.GetValues(typeof(GoalType)).Cast<GoalType>())
            {
                goaltype_dictionary.Add((int)val, index);
                _GoalType.Items.Add(val.ToString());
                index++;
            }
        }

        private void PopulateAllowRacesListBox()
        {
            listRaces.Items.Add("All");
            int index = 1;

            foreach (var val in Enum.GetValues(typeof(ERace)).Cast<ERace>().Skip(1))
            {
                this.allRaces.Add(index, (int)val);
                index++;

                if (!this.listRaces.Items.Contains(val.ToString()))
                {
                    this.listRaces.Items.Add(val.ToString());
                }
                else
                {
                    if ((byte)val == 19)
                    {
                        listRaces.Items.Add(nameof(ERace.AlbionMinotaur));
                    }
                    else if ((byte)val == 20)
                    {
                        listRaces.Items.Add(nameof(ERace.Deifrang));
                    }
                    else
                    {
                        listRaces.Items.Add(nameof(ERace.Graoch));
                    }
                }             
            }
        }

        // convert database string entries to dictionary for quest goal usage
        private void StringToDictionary(string[] str, Dictionary<int, string> dict)
        {
            dict.Clear();
            if (str == null)
            {
                return;
            }

            for (int i = 0; i < str.Length; i++)
            {
                dict.Add(i + 1, str[i]);
            }
        }

		/// <summary>
		/// Returns the object to be saved as JSON given back as third argument in the constructor for loading
		/// </summary>
		/// <returns>A serialisable object</returns>
		public Dictionary<string, object> GetDatabaseJsonObject(int index)
		{
            return new Dictionary<string, object>
			{// "" if no index in dict
				{ "Description", goaltext_dictionary.ContainsKey(index) ? goaltext_dictionary[index] : "" },
				{ "GiveItem", stepItemTemplate_dictionnary.ContainsKey(index) ? stepItemTemplate_dictionnary[index] : "" },
				{ "StartItem", stepStartItemTemplate_dictionnary.ContainsKey(index) ? stepStartItemTemplate_dictionnary[index] : "" },
				{ "MessageStarted", messageStarted_dictionary.ContainsKey(index) ? messageStarted_dictionary[index]  : "" },
				{ "MessageAborted", messageAborted_dictionary.ContainsKey(index) ? messageAborted_dictionary[index]  : "" },
				{ "MessageDone", messageDone_dictionary.ContainsKey(index) ? messageDone_dictionary[index]  : "" },
				{ "MessageCompleted", messageCompleted_dictionary.ContainsKey(index) ?  messageCompleted_dictionary[index]  : "" },
                {"StartGoalsDone", questDependance_dictionary.ContainsKey(index) ?  questDependance_dictionary[index].Keys.ToList() : new List<int>()},
                {"StopGoals", questStop_dictionary.ContainsKey(index) ?  questStop_dictionary[index].Keys.ToList() : new List<int>()},
				{ "EndWhenGoalsDone", null },
				{ "TargetName", goaltargetname_dictionary.ContainsKey(index) ? goaltargetname_dictionary[index]  : "" },
				{ "TargetRegion", targetRegions_dictionary.ContainsKey(index) ? targetRegions_dictionary[index] : ""  },
				{ "Text", goaltargettext_dictionary.ContainsKey(index) ? goaltargettext_dictionary[index]  : "" },
				{ "Item", item_dictionary.ContainsKey(index) ? item_dictionary[index]  : "" }, //use or collect
				{ "ItemCount", goalrepeatno_dictionary.ContainsKey(index) ? goalrepeatno_dictionary[index]  : "" },
				{ "AreaCenter", new Vector3(
                    float.Parse(xloc_dictionary.ContainsKey(index) && xloc_dictionary[index]!="" ? xloc_dictionary[index] : "0" ),
                 float.Parse(yloc_dictionary.ContainsKey(index) && yloc_dictionary[index]!="" ? yloc_dictionary[index] : "0" ), 0) },
				{ "AreaRadius", arearadius_dictionary.ContainsKey(index) ? arearadius_dictionary[index]  : "" },
				{ "AreaRegion", zoneid_dictionary.ContainsKey(index) ? zoneid_dictionary[index]  : "" },
				{ "KillCount", goalrepeatno_dictionary.ContainsKey(index) ? goalrepeatno_dictionary[index]  : "" },
				{ "Seconds", seconds_dictionary.ContainsKey(index) ? seconds_dictionary[index]  : "" },
				{ "WhisperText", goalAdvanceText_dictionnary.ContainsKey(index) ? goalAdvanceText_dictionnary[index]  : "" },
                { "DestroyItem", destroyItem_dictionary.ContainsKey(index) ? destroyItem_dictionary[index]  : "" },
            };
		}
		/// <summary>
		/// Returns the list of objects to be saved as JSON along with id and type
		/// </summary>
		/// <returns>A serialisable object</returns>
		public string GetDatabaseJsonObjectList()
		{
            return JsonConvert.SerializeObject(GoalSteps.Select((step, index) => new { Id = index+1, Type = GoalTypeNames.FirstOrDefault(e => e.Value == Enum.GetName(typeof(GoalType), step)).Key, Data = GetDatabaseJsonObject(index+1) }).ToArray());
		}

        // attempt to save the current quest
        private void questSave_Click(object sender, EventArgs e)
        {
            int goalNum = int.Parse(GoalNumber.Text);
            int optNum = int.Parse(optNumber.Text);
            int optChoices = (int)OptRewardUpDown.Value;
            int finNum = int.Parse(finNumber.Text);

       
            SetGoalDependanceList(goalNum);
            SetGoalStopList(goalNum);
        
            goalstepno_dictionary.Remove(goalNum);
            goalstepno_dictionary.Add(goalNum, GoalStepNo.Text);
            

            if (item_dictionary.Count == 0 && GoalSteps.Any(s => IsUsingItem(s)))
            {
                for(int i = 1; i <= GoalSteps.Count; i++)
                {
                    item_dictionary.Add(i, string.Empty);
                }
            }

            if (goalAdvanceText_dictionnary.Count == 0 && GoalSteps.Any(s => s == GoalType.Whisper))
            {
                for (int i = 1; i <= GoalSteps.Count; i++)
                {
                    goalAdvanceText_dictionnary.Add(i, string.Empty);
                }
            }

            if (IsUsingItem(GoalSteps[goalNum - 1]))
            {
                item_dictionary.Remove(goalNum);
                item_dictionary.Add(goalNum, _GoalOptional.Text);
            }

            if (GoalSteps[goalNum - 1] == GoalType.Whisper)
            {
                goalAdvanceText_dictionnary.Remove(goalNum);
                goalAdvanceText_dictionnary.Add(goalNum, _GoalOptional.Text);
            }

            if (GoalSteps[goalNum - 1] == GoalType.Timer)
            {
                seconds_dictionary.Remove(goalNum);
                seconds_dictionary.Add(goalNum, _GoalOptional.Text);
            }


            if (IsUsingArea(GoalSteps[goalNum - 1]))
            {
                arearadius_dictionary.Remove(goalNum);
                arearadius_dictionary.Add(goalNum, _Radius.Text);
            }

            goaltext_dictionary.Remove(goalNum);
            goaltext_dictionary.Add(goalNum, _QuestGoals.Text);      
            goaltargetname_dictionary.Remove(goalNum);
            goaltargetname_dictionary.Add(goalNum, _GoalTargetName.Text);     
            stepItemTemplate_dictionnary.Remove(goalNum);
            stepItemTemplate_dictionnary.Add(goalNum, _StepItemTemplate.Text);
            stepStartItemTemplate_dictionnary.Remove(goalNum);
            stepStartItemTemplate_dictionnary.Add(goalNum, _StepStartItemTemplate.Text);          
            goalrepeatno_dictionary.Remove(goalNum);
            goalrepeatno_dictionary.Add(goalNum, _GoalRepeatNo.Text);
            xloc_dictionary.Remove(goalNum);
            xloc_dictionary.Add(goalNum, _XOffset.Text);
            yloc_dictionary.Remove(goalNum);
            yloc_dictionary.Add(goalNum, _YOffset.Text);
            zoneid_dictionary.Remove(goalNum);
            zoneid_dictionary.Add(goalNum, _ZoneID.Text);
            goaltargettext_dictionary.Remove(goalNum);
            goaltargettext_dictionary.Add(goalNum, _GoalTargetText.Text);
            messageStarted_dictionary.Remove(goalNum);
            messageStarted_dictionary.Add(goalNum, richTextBoxStarted.Text);
            messageAborted_dictionary.Remove(goalNum);
            messageAborted_dictionary.Add(goalNum, richTextBoxAborted.Text);
            messageDone_dictionary.Remove(goalNum);
            messageDone_dictionary.Add(goalNum, richTextBoxDone.Text);
            messageCompleted_dictionary.Remove(goalNum);
            messageCompleted_dictionary.Add(goalNum, richTextBoxCompleted.Text);
            targetRegions_dictionary.Remove(goalNum);
            targetRegions_dictionary.Add(goalNum, _targetRegion.Text);
            destroyItem_dictionary.Remove(goalNum);
            destroyItem_dictionary.Add(goalNum, checkBoxDestroyItem.Checked.ToString());
            opt_dictionary.Remove(optNum); // do this incase it was edited without pressing forward/back
            if (!string.IsNullOrWhiteSpace(_OptionalReward.Text)) //!opt_dictionary.ContainsKey(optNum) && 
            {
                opt_dictionary.Add(optNum, _OptionalReward.Text);
            }
            fin_dictionary.Remove(finNum); // do this incase it was edited without pressing forward/back
            if (!string.IsNullOrWhiteSpace(_FinalReward.Text)) //!fin_dictionary.ContainsKey(finNum) && 
            {
                fin_dictionary.Add(finNum, _FinalReward.Text);
            }

             DBDQRewardQTemplate q = new DBDQRewardQTemplate();

            // try
            // {
                q.GoalsJson = GetDatabaseJsonObjectList();
                allowedClasses = String.Join("|", listClasses.SelectedItems.Cast<object>().Select(i => i.ToString()));
                allowedRaces = String.Join("|", listRaces.SelectedItems.Cast<string>().Select(i =>
                {
                    return i == "All" ? string.Empty : ((byte)(ERace)Enum.Parse(typeof(ERace), i)).ToString();
                }));

                optionalRewardItemTemplates = String.Join("|", Array.ConvertAll(opt_dictionary.Values.ToArray(), i => i.ToString()));
                finalRewardItemTemplates = String.Join("|", Array.ConvertAll(fin_dictionary.Values.ToArray(), i => i.ToString()));

                allowedRaces = allowedRaces?.Replace("All", string.Empty);

                StringBuilder allcl = new StringBuilder(allowedClasses);
                allcl.Replace("All", ""); // added all incase one is selected, you cannot deselect
                allcl.Replace("Armsman", "2");
                allcl.Replace("Cabalist", "13");
                allcl.Replace("Cleric", "6");
                allcl.Replace("Friar", "10");
                allcl.Replace("Heretic", "33");
                allcl.Replace("Infiltrator", "9");
                allcl.Replace("Mercenary", "11");
                allcl.Replace("Minstrel", "4");
                allcl.Replace("Necromancer", "12");
                allcl.Replace("Paladin", "1");
                allcl.Replace("Reaver", "19");
                allcl.Replace("Scout", "3");
                allcl.Replace("Sorcerer", "8");
                allcl.Replace("Theurgist", "5");
                allcl.Replace("Wizard", "7");
                allcl.Replace("MaulerAlb", "60");
                allcl.Replace("Berserker", "31");
                allcl.Replace("Bonedancer", "30");
                allcl.Replace("Healer", "26");
                allcl.Replace("Hunter", "25");
                allcl.Replace("Runemaster", "29");
                allcl.Replace("Savage", "32");
                allcl.Replace("Shadowblade", "23");
                allcl.Replace("Shaman", "28");
                allcl.Replace("Skald", "24");
                allcl.Replace("Spiritmaster", "27");
                allcl.Replace("Thane", "21");
                allcl.Replace("Valkyrie", "34");
                allcl.Replace("Warlock", "59");
                allcl.Replace("Warrior", "22");
                allcl.Replace("MaulerMid", "61");
                allcl.Replace("Animist", "55");
                allcl.Replace("Bainshee", "39");
                allcl.Replace("Bard", "48");
                allcl.Replace("Blademaster", "43");
                allcl.Replace("Champion", "45");
                allcl.Replace("Druid", "47");
                allcl.Replace("Eldritch", "40");
                allcl.Replace("Enchanter", "41");
                allcl.Replace("Hero", "44");
                allcl.Replace("Mentalist", "42");
                allcl.Replace("Nightshade", "49");
                allcl.Replace("Ranger", "50");
                allcl.Replace("Valewalker", "56");
                allcl.Replace("Vampiir", "58");
                allcl.Replace("Warden", "46");
                allcl.Replace("MaulerHib", "62");
                allcl.Replace("Acolyte", "16");
                allcl.Replace("AlbionRogue", "17");
                allcl.Replace("Disciple", "20");
                allcl.Replace("Elementalist", "15");
                allcl.Replace("Fighter", "14");
                allcl.Replace("Forester", "57");
                allcl.Replace("Guardian", "52");
                allcl.Replace("Mage", "18");
                allcl.Replace("Magician", "51");
                allcl.Replace("MidgardRogue", "38");
                allcl.Replace("Mystic", "36");
                allcl.Replace("Naturalist", "53");
                allcl.Replace("Seer", "37");
                allcl.Replace("Stalker", "54");
                allcl.Replace("Viking", "35");
                allowedClasses = allcl.ToString();
            // }
            // catch (Exception g)
            // {
            //     MessageBox.Show(g.Message, "Error converting data. Quest will not be completely saved!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // }

            q.ID = !string.IsNullOrEmpty(_ID.Text) ? int.Parse(_ID.Text) : (int?)null;
            q.NpcName = string.IsNullOrWhiteSpace(_NpcName.Text) ? "undefined" : _NpcName.Text;
            q.Description = _DescriptionText.Text;
            q.NpcRegion = ushort.Parse(string.IsNullOrWhiteSpace(_NpcRegion.Text) ? "0" : _NpcRegion.Text);
            q.Story = string.IsNullOrWhiteSpace(_StoryText.Text) ? "" : _StoryText.Text;
            q.Summary = _Summary.Text;
            q.AcceptText = _AcceptText.Text;
            q.MaxCount = byte.Parse(string.IsNullOrWhiteSpace(_MaxCount.Text) ? "1" : _MaxCount.Text);
            q.MinLevel = byte.Parse(string.IsNullOrWhiteSpace(_MinLevel.Text) ? "1" : _MinLevel.Text);
            q.MaxLevel = byte.Parse(string.IsNullOrWhiteSpace(_MaxLevel.Text) ? "50" : _MaxLevel.Text);
            q.RewardMoney = int.Parse(string.IsNullOrWhiteSpace(_RewardMoney.Text) ? "0" : _RewardMoney.Text);
            q.RewardReputation = int.Parse(string.IsNullOrWhiteSpace(ReputationReward.Text) ? "0" : ReputationReward.Text);
            q.RewardXP = int.Parse(string.IsNullOrWhiteSpace(_RewardXP.Text) ? "0" : _RewardXP.Text);
            q.RewardCLXP = int.Parse(string.IsNullOrWhiteSpace(_RewardCLXP.Text) ? "0" : _RewardCLXP.Text);
            q.RewardRP = int.Parse(string.IsNullOrWhiteSpace(_RewardRP.Text) ? "0" : _RewardRP.Text);
            q.RewardBP = int.Parse(string.IsNullOrWhiteSpace(_RewardBP.Text) ? "0" : _RewardBP.Text);
            q.OptionalRewardItemTemplates = optionalRewardItemTemplates;
            q.FinalRewardItemTemplates = finalRewardItemTemplates;
            q.Conclusion = _Conclusion.Text;
            q.QuestDependency = _QuestDependency.Text; //might need to serialize....if quest has multiple dependencies
            q.AllowedClasses = allowedClasses;
            q.AllowedRaces = allowedRaces;
            q.Name = string.IsNullOrWhiteSpace(_Name.Text) ? "new quest" : _Name.Text;
            q.IsRenaissance = _IsRenaissance.Checked;
            q.Reputation = reputation ?? string.Empty;
            q.NbChooseOptionalItems = optChoices;
            try
            {
                var result = this.SaveToWebService(q);

                if (result)
                {
                    MessageBox.Show("Quest successfully saved!", "", MessageBoxButtons.OK);
                    return;
                }
                throw new Exception();
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message, "Error saving data!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool SaveToWebService(DBDQRewardQTemplate quest)
        {
            string query = null;
            dynamic resp;
            if (quest.ID.HasValue)
            {
                //update quest
                query = this.GenerateUpdateQuery(quest);
            }
            else
            {
                //Insert quest
               query = GenerateInsertQuery(quest);
            }
            resp = Server.Query(query);

            if (resp is string)
                throw new Exception("Erreur:\r\n" + resp);
            else if (resp.error != null)
                throw new Exception("Erreur:\r\n" + resp.error);
            else
            {
                return true;
            }
        }

        private string GenerateUpdateQuery(DBDQRewardQTemplate quest)
        {
            var fields =  Assembly.GetCallingAssembly().GetType("AmteCreator.Internal.DBDQRewardQTemplate", true).GetProperties();
            object value;
            var formattedFields = fields.Select(f =>
            {
                value = f.GetValue(quest, null);

                value = f.Name != nameof(DBDQRewardQTemplate.IsRenaissance) ? Server.EscapeSql(value?.ToString()) : ((bool)value ? "1" : "0");

                return $"`{ f.Name }`={value}";
            });           


            return "?action=UPDATE&table=dataquestjson&where=" +
                HttpUtility.UrlEncode("ID = " + Server.EscapeSql(quest.ID.Value), Encoding.UTF8) +
                "&upfields=" +
                HttpUtility.UrlEncode(string.Join(",", formattedFields), Encoding.UTF8);
        }

        private string GenerateInsertQuery(DBDQRewardQTemplate quest)
        {
            var fields = Assembly.GetCallingAssembly().GetType("AmteCreator.Internal.DBDQRewardQTemplate", true).GetProperties().Skip(1);

            object value;
            var formattedFields = fields.Select(f =>
            {
                value = f.GetValue(quest, null);

                return f.Name != nameof(DBDQRewardQTemplate.IsRenaissance) ? Server.EscapeSql(value?.ToString()) : ((bool)value ? "1" : "0");
            });

            return "?action=INSERT&table=dataquestjson&fields=" +
                   HttpUtility.UrlEncode(string.Join(",", fields.Select(f => f.Name)), Encoding.UTF8) +
                   "&values=" + HttpUtility.UrlEncode(string.Join(",", formattedFields), Encoding.UTF8);
        }

        private dynamic GenerateDeleteQuery(string id)
        {
            return "?action=DELETE&table=dataquestjson&where=" +
                                        HttpUtility.UrlEncode("ID = " + Server.EscapeSql(id), Encoding.UTF8);         
        }


        // Clear the form fields, but not delete from the database
        private void questDelete_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(@"This will clear the form and delete Quest in Database. Do you want to continue?",
                @"Confirm Delete!!",
                MessageBoxButtons.OKCancel);

            if (confirmResult == DialogResult.OK)
            {
               string query = GenerateDeleteQuery(_ID.Text);

               var resp = Server.Query(query);

                if (resp is string)
                    MessageBox.Show("Erreur:\r\n" + resp, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (resp.error != null)
                    MessageBox.Show("Erreur:\r\n" + resp.error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    Clear();
                    SetFieldDefaults();
                    MessageBox.Show("The Quest was correctly deleted:\r\n", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }   
        }

        // clear the dictionaries 
        private void Clear()
        {
            //_quest = null;
            LoadedQuest = false;
            opt_dictionary.Clear();
            fin_dictionary.Clear();
            item_dictionary.Clear();
            goalrepeatno_dictionary.Clear();
            goaltargetname_dictionary.Clear();
            goaltargettext_dictionary.Clear();
            goaltext_dictionary.Clear();
            goalstepno_dictionary.Clear();
            xloc_dictionary.Clear();
            yloc_dictionary.Clear();
            zoneid_dictionary.Clear();
            listClasses.ClearSelected();
            listRaces.ClearSelected();
            goalAdvanceText_dictionnary.Clear();
            targetRegions_dictionary.Clear();
            arearadius_dictionary.Clear();
            seconds_dictionary.Clear();
            messageAborted_dictionary.Clear();
            messageCompleted_dictionary.Clear();
            messageDone_dictionary.Clear();
            messageStarted_dictionary.Clear();
            stepItemTemplate_dictionnary.Clear();
            stepStartItemTemplate_dictionnary.Clear();
            GoalSteps.Clear();
            questDependance_dictionary.Clear();
            questDependance_dictionary[1]=new Dictionary<int, string>();
            questStop_dictionary.Clear();
            questStop_dictionary[1]=new Dictionary<int, string>();
            destroyItem_dictionary.Clear();
        }        


        private bool SetField(string field, Dictionary<int, string> dic, Control control)
        {
            if (string.IsNullOrEmpty(field))
            {
                return false;
            }

            string[] fieldArray = field.Split(new string[] { "|" }, StringSplitOptions.None);
            StringToDictionary(fieldArray, dic);
            control.Text = dic[1];
            return true;
        }

        private bool SetFieldWithSemiColon(string field, Dictionary<int, string> dic1, Dictionary<int, string> dic2)
        {
            dic1.Clear();
            dic2.Clear();
            if (string.IsNullOrEmpty(field))
            {
                return false;
            }

            var fieldSplit = field.Split('|');
            for (int i = 1; i < fieldSplit.Length + 1; i++)
            {
                var groupItems = fieldSplit[i - 1].Split(';');
                dic1[i] = groupItems[0];
                dic2[i] = groupItems[1];
            }

            return true;
        }

        // set field defaults when the form is cleared
        private void SetFieldDefaults()
        {
            GoalNumber.Text = "1";
            optNumber.Text = "1";
            finNumber.Text = "1";
            OptRewardUpDown.Value = 1;
            _RewardMoney.Text = "0";
            _RewardXP.Text = "0";
            _RewardCLXP.Text = "0";
            _RewardRP.Text = "0";
            _RewardBP.Text = "0";
            _XOffset.Text = null;
            _YOffset.Text = null;
            _ZoneID.Text = null;
            _OptionalReward.Text = null;
            _FinalReward.Text = null;
            _Name.Text = null;
            _NpcName.Text = null;
            _DescriptionText.Text = null;
            _NpcRegion.Text = null;
            _GoalOptional.Text = null;
            _GoalTargetName.Text = null;
            _GoalTargetText.Text = null;
            _targetRegion.Text = null;
            _QuestGoals.Text = null;
            GoalStepNo.Text = "1";
            _MinLevel.Text = "0";
            _MaxCount.Text = "1";
            _IsRenaissance.Checked = false;
            _ID.Text = null;
            _QuestDependency.Text = null;
            _StoryText.Text = null;
            _Summary.Text = null;
            _AcceptText.Text = null;
            _Conclusion.Text = null;
            listRaces.SelectedIndex = 0;
            listClasses.SelectedIndex = 0;
            Reputation.Text = string.Empty;
            GoalSteps.Add((GoalType)0);
            _GoalType.SelectedIndex = 0;
            this._quest = new DBDQRewardQTemplate();
            listGoalDependance.Items.Clear();
            checkBoxDestroyItem.Checked = false;
        }

        // Keypress event args

        // prevent typing custom text into the goaltype dropdown box
        private void _GoalType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void MinLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void ID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // block ID from being changed. Auto generated
        }

        private void MaxLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void MaxCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void NpcRegion_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void RewardMoney_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void RewardXp_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void RewardCLXp_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void RewardRp_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void RewardBp_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void GoalStepNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }


        private void GoalXloc_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void GoalYloc_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void GoalZoneId_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
  //          var search = new ItemSearchForm();

            //search.SelectClicked += (o, args) =>
            //{
            //    if (!(o is ItemTemplate item))
            //    {
            //        return;
            //    }
            //    LoadItem(item.Id_nb);
            //};


         //   search.ShowDialog(this);
        }

        private void LoadItem(string itemId)
        {
            if (string.IsNullOrWhiteSpace(itemId))
            {
                return;
            }

            ItemTemplateBox.Text = itemId;
        }

        private void optSelect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ItemTemplateBox.Text))
            {
                return;
            }
            else
            {
                _OptionalReward.Text = ItemTemplateBox.Text;
            }
        }

        private void collectSelect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ItemTemplateBox.Text))
            {
                return;
            }
            else
            {
                _GoalOptional.Text = ItemTemplateBox.Text;
            }
        }

        private void finSelect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ItemTemplateBox.Text))
            {
                return;
            }
            else
            {
                _FinalReward.Text = ItemTemplateBox.Text;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (OptRewardUpDown.Value > 8)
            {
                OptRewardUpDown.Value = 8;
                return;
            }
            else if (OptRewardUpDown.Value < 1)
            {
                OptRewardUpDown.Value = 1;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
 //           var mobsearch = new MobSearch();

            //mobsearch.SelectNpcClicked += (o, args) => { LoadNpcName(((Mob)o).Name, ((Mob)o).Region); };

            //mobsearch.ShowDialog(this);
        }

        private void LoadNpcName(string mobName, ushort mobRegion)
        {
            if (string.IsNullOrWhiteSpace(mobName))
            {
                return;
            }

            _NpcName.Text = mobName;
            _NpcRegion.Text = mobRegion.ToString();
        }

        private void _GoalType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse<GoalType>((string)_GoalType.SelectedItem, out GoalType selected);
            GoalStepNo.Text = GoalNumber.Text;

            int step = int.Parse(GoalNumber.Text);

            if (Enum.TryParse(_GoalType.SelectedItem.ToString(), out GoalType newGoal))
            {
                if (GoalSteps.Count >= step)
                {
                    GoalSteps[step - 1] = newGoal;
                }
                else
                {
                    //new Step
                    GoalSteps.Add(newGoal);
                }
            }

            if (!GoalSteps.Any(s => IsUsingItem(s)))
            {
                item_dictionary.Clear();
            }

            if (!GoalSteps.Any(s => s == GoalType.Whisper))
            {
                goalAdvanceText_dictionnary.Clear();
            }


            UpdateOptionalFields();
        }

        private void QuestDependencySearchBtn_Click(object sender, EventArgs e)
        {
            //var search = new RewardQuestSearchForm();

            //search.SelectClicked += (o, args) =>
            //{
            //    if (!(o is DBDQRewardQ item))
            //    {
            //        return;
            //    }

            //    _QuestDependency.Text = item.Name;
            //};

            //search.ShowDialog(this);
        }

        private void npcSearch_Click(object sender, EventArgs e)
        {
            //var mobsearch = new MobSearch();

            //mobsearch.SelectNpcClicked += (o, args) => { LoadGoalTargetNPC(((Mob)o).Name); };

            //mobsearch.ShowDialog(this);
        }

        private void LoadGoalTargetNPC(string mobName)
        {
            if (string.IsNullOrWhiteSpace(mobName))
            {
                return;
            }

            _GoalTargetName.Text = mobName;
        }

        //Step data forward
        private void nextGoal_Click(object sender, EventArgs e)
        {
            int goalNumber = int.Parse(GoalNumber.Text);

            if (_GoalType.Text == "")
            {
                MessageBox.Show("You cannot proceed until required fields are met!\n" +
                    "GoalType is manditory field!");
                return;
            }

            if (!questDependance_dictionary.ContainsKey(goalNumber + 1))
            {
                questDependance_dictionary.Add(goalNumber + 1,  new Dictionary<int, string>());
            }
            if (!questStop_dictionary.ContainsKey(goalNumber + 1))
            {
                questStop_dictionary.Add(goalNumber + 1,  new Dictionary<int, string>());
            }
            _suppressGoal.Enabled = true; 
            SetGoalDependanceList(goalNumber);
            SetGoalStopList(goalNumber);
            PopulateGoalDependanceListBox(goalNumber + 1);
            PopulateGoalStopListBox(goalNumber + 1);

            //GoalTarget.Text
            if (!goaltargetname_dictionary.ContainsKey(goalNumber))
            {
                goaltargetname_dictionary.Add(goalNumber, _GoalTargetName.Text);
            }
            else
            {
                goaltargetname_dictionary.Remove(goalNumber);
                goaltargetname_dictionary.Add(goalNumber, _GoalTargetName.Text);
            }
            _GoalTargetName.Text = "";


            if (IsUsingItem(GoalSteps[goalNumber - 1]))
            {
                //CollectItem.Text
                if (!item_dictionary.ContainsKey(goalNumber))
                {
                    item_dictionary.Add(goalNumber, _GoalOptional.Text);
                }
                else
                {
                    item_dictionary.Remove(goalNumber);
                    item_dictionary.Add(goalNumber, _GoalOptional.Text);
                }       
            }
            if (GoalSteps[goalNumber - 1] == GoalType.Whisper)
            {
                if (!goalAdvanceText_dictionnary.ContainsKey(goalNumber))
                {
                    goalAdvanceText_dictionnary.Add(goalNumber, _GoalOptional.Text);
                }
                else
                {
                    goalAdvanceText_dictionnary.Remove(goalNumber);
                    goalAdvanceText_dictionnary.Add(goalNumber, _GoalOptional.Text);
                }              
            }
             if (IsUsingArea(GoalSteps[goalNumber - 1]))
            {
                if (!arearadius_dictionary.ContainsKey(goalNumber))
                {
                    arearadius_dictionary.Add(goalNumber, _Radius.Text);
                }
                else
                {
                    arearadius_dictionary.Remove(goalNumber);
                    arearadius_dictionary.Add(goalNumber, _Radius.Text);
                }
            }
             if (GoalSteps[goalNumber - 1] == GoalType.Timer){
                if (!seconds_dictionary.ContainsKey(goalNumber))
                {
                    seconds_dictionary.Add(goalNumber, _GoalOptional.Text);
                }
                else
                {
                    seconds_dictionary.Remove(goalNumber);
                    seconds_dictionary.Add(goalNumber, _GoalOptional.Text);
                }
            }
            _GoalOptional.Text = "";

            if (!stepItemTemplate_dictionnary.ContainsKey(goalNumber))
            {
                stepItemTemplate_dictionnary.Add(goalNumber, _StepItemTemplate.Text);
            }
            else
            {
                stepItemTemplate_dictionnary.Remove(goalNumber);
                stepItemTemplate_dictionnary.Add(goalNumber, _StepItemTemplate.Text);
            }

            if (!stepStartItemTemplate_dictionnary.ContainsKey(goalNumber))
            {
                stepStartItemTemplate_dictionnary.Add(goalNumber, _StepStartItemTemplate.Text);
            }
            else
            {
                stepStartItemTemplate_dictionnary.Remove(goalNumber);
                stepStartItemTemplate_dictionnary.Add(goalNumber, _StepStartItemTemplate.Text);
            }

            if (!goaltext_dictionary.ContainsKey(goalNumber))
            {
                goaltext_dictionary.Add(goalNumber, _QuestGoals.Text);
            }
            else
            {
                goaltext_dictionary.Remove(goalNumber);
                goaltext_dictionary.Add(goalNumber, _QuestGoals.Text);
            }
            _QuestGoals.Text = "";
            

            if (!targetRegions_dictionary.ContainsKey(goalNumber))
            {
                targetRegions_dictionary.Add(goalNumber, _targetRegion.Text);
            }
            else
            {
                targetRegions_dictionary.Remove(goalNumber);
                targetRegions_dictionary.Add(goalNumber, _targetRegion.Text);
            }
            _targetRegion.Text = "";


            //GoalStepNo.Text
            if (!goalstepno_dictionary.ContainsKey(goalNumber))
            {
                goalstepno_dictionary.Add(goalNumber, GoalStepNo.Text);
            }
            else
            {
                goalstepno_dictionary.Remove(goalNumber);
                goalstepno_dictionary.Add(goalNumber, GoalStepNo.Text);
            }
            GoalStepNo.Text = "";

            //GoalRepeatNo.Text
            if (!goalrepeatno_dictionary.ContainsKey(goalNumber))
            {
                goalrepeatno_dictionary.Add(goalNumber, _GoalRepeatNo.Text);
            }
            else
            {
                goalrepeatno_dictionary.Remove(goalNumber);
                goalrepeatno_dictionary.Add(goalNumber, _GoalRepeatNo.Text);
            }
            _GoalRepeatNo.Text = "1";

            //GoalXloc.Text
            if (!xloc_dictionary.ContainsKey(goalNumber))
            {
                xloc_dictionary.Add(goalNumber, string.IsNullOrWhiteSpace(_XOffset.Text) ? string.Empty : _XOffset.Text);
            }
            else
            {
                xloc_dictionary.Remove(goalNumber);
                xloc_dictionary.Add(goalNumber, string.IsNullOrWhiteSpace(_XOffset.Text) ? string.Empty : _XOffset.Text);
            }          

            //GoalYloc.Text
            if (!yloc_dictionary.ContainsKey(goalNumber))
            {
                yloc_dictionary.Add(goalNumber, string.IsNullOrWhiteSpace(_YOffset.Text) ? string.Empty : _YOffset.Text);
            }
            else
            {
                yloc_dictionary.Remove(goalNumber);
                yloc_dictionary.Add(goalNumber, string.IsNullOrWhiteSpace(_YOffset.Text) ? string.Empty : _YOffset.Text);
            }         

            //GoalZoneId.Text
            if (!zoneid_dictionary.ContainsKey(goalNumber))
            {
                zoneid_dictionary.Add(goalNumber, string.IsNullOrWhiteSpace(_ZoneID.Text) ? string.Empty : _ZoneID.Text);
            }
            else
            {
                zoneid_dictionary.Remove(goalNumber);
                zoneid_dictionary.Add(goalNumber, string.IsNullOrWhiteSpace(_ZoneID.Text) ? string.Empty : _ZoneID.Text);
            }         

            //GoalTargetText.Text
            if (!goaltargettext_dictionary.ContainsKey(goalNumber))
            {
                goaltargettext_dictionary.Add(goalNumber, _GoalTargetText.Text);
            }
            else
            {
                goaltargettext_dictionary.Remove(goalNumber);
                goaltargettext_dictionary.Add(goalNumber, _GoalTargetText.Text);
            }
            _GoalTargetText.Text = "";

            //richTextBoxStarted.Text
            if (!messageStarted_dictionary.ContainsKey(goalNumber))
            {
                messageStarted_dictionary.Add(goalNumber, richTextBoxStarted.Text);
            }
            else
            {
                messageStarted_dictionary.Remove(goalNumber);
                messageStarted_dictionary.Add(goalNumber, richTextBoxStarted.Text);
            }
            richTextBoxStarted.Text = "";

            //richTextBoxAborted.Text
            if (!messageAborted_dictionary.ContainsKey(goalNumber))
            {
                messageAborted_dictionary.Add(goalNumber, richTextBoxAborted.Text);
            }
            else
            {
                messageAborted_dictionary.Remove(goalNumber);
                messageAborted_dictionary.Add(goalNumber, richTextBoxAborted.Text);
            }
            richTextBoxAborted.Text = "";

            //GoalTargetText.Text
            if (!messageDone_dictionary.ContainsKey(goalNumber))
            {
                messageDone_dictionary.Add(goalNumber, richTextBoxDone.Text);
            }
            else
            {
                messageDone_dictionary.Remove(goalNumber);
                messageDone_dictionary.Add(goalNumber, richTextBoxDone.Text);
            }
            richTextBoxDone.Text = "";

            //GoalTargetText.Text
            if (!messageCompleted_dictionary.ContainsKey(goalNumber))
            {
                messageCompleted_dictionary.Add(goalNumber, richTextBoxCompleted.Text);
            }
            else
            {
                messageCompleted_dictionary.Remove(goalNumber);
                messageCompleted_dictionary.Add(goalNumber, richTextBoxCompleted.Text);
            }
            richTextBoxCompleted.Text = "";

            //checkBoxDestroyItem.Checked
            if (!destroyItem_dictionary.ContainsKey(goalNumber))
            {
                destroyItem_dictionary.Add(goalNumber, checkBoxDestroyItem.Checked.ToString());
            }
            else
            {
                destroyItem_dictionary.Remove(goalNumber);
                destroyItem_dictionary.Add(goalNumber, checkBoxDestroyItem.Checked.ToString());
            }
            checkBoxDestroyItem.Checked = false;

            GoalNumber.Text = (goalNumber + 1).ToString(); //increment label

            //Step forward check next step

            int goalNext = int.Parse(GoalNumber.Text);
            string goalvalue;
        

            if (goaltext_dictionary.ContainsKey(goalNext))
            {
                goaltext_dictionary.TryGetValue(goalNext, out goalvalue);
                _QuestGoals.Text = goalvalue;
            }
            else _QuestGoals.Text = "";

            if (goaltargetname_dictionary.ContainsKey(goalNext))
            {
                goaltargetname_dictionary.TryGetValue(goalNext, out goalvalue);
                _GoalTargetName.Text = goalvalue;
            }
            else _GoalTargetName.Text = "";


            if (targetRegions_dictionary.ContainsKey(goalNext))
            {
                targetRegions_dictionary.TryGetValue(goalNext, out goalvalue);
                _targetRegion.Text = goalvalue;
            }
            else _targetRegion.Text = "";

            if (item_dictionary.ContainsKey(goalNext) && IsUsingItem(GoalSteps[goalNext - 1]))
            {
                item_dictionary.TryGetValue(goalNext, out goalvalue);
                _GoalOptional.Text = goalvalue;
            }

            if (seconds_dictionary.ContainsKey(goalNext) && GoalSteps[goalNext - 1] == GoalType.Timer)
            {
                seconds_dictionary.TryGetValue(goalNext, out goalvalue);
                _GoalOptional.Text = goalvalue;
            }

            if (arearadius_dictionary.ContainsKey(goalNext) && IsUsingArea(GoalSteps[goalNext - 1]))
            {
                arearadius_dictionary.TryGetValue(goalNext, out goalvalue);
                _Radius.Text = goalvalue;
            }

            if (goalAdvanceText_dictionnary.ContainsKey(goalNext) && GoalSteps[goalNext - 1] == GoalType.Whisper)
            {
                goalAdvanceText_dictionnary.TryGetValue(goalNext, out goalvalue);
                _GoalOptional.Text = goalvalue;
            }


            if (stepItemTemplate_dictionnary.ContainsKey(goalNext))
            {
                stepItemTemplate_dictionnary.TryGetValue(goalNext, out goalvalue);
                _StepItemTemplate.Text = goalvalue;
            }
            else _StepItemTemplate.Text = "";

            if (stepStartItemTemplate_dictionnary.ContainsKey(goalNext))
            {
                stepStartItemTemplate_dictionnary.TryGetValue(goalNext, out goalvalue);
                _StepStartItemTemplate.Text = goalvalue;
            }
            else _StepStartItemTemplate.Text = "";

            if (goalstepno_dictionary.ContainsKey(goalNext))
            {
                goalstepno_dictionary.TryGetValue(goalNext, out goalvalue);
                GoalStepNo.Text = goalvalue;
            }
            else GoalStepNo.Text = GoalNumber.Text;

            if (goalrepeatno_dictionary.ContainsKey(goalNext))
            {
                goalrepeatno_dictionary.TryGetValue(goalNext, out goalvalue);
                _GoalRepeatNo.Text = goalvalue;
            }
            else _GoalRepeatNo.Text = "";

            if (xloc_dictionary.ContainsKey(goalNext))
            {
                xloc_dictionary.TryGetValue(goalNext, out goalvalue);
                _XOffset.Text = goalvalue;
            }

            if (yloc_dictionary.ContainsKey(goalNext))
            {
                yloc_dictionary.TryGetValue(goalNext, out goalvalue);
                _YOffset.Text = goalvalue;
            }

            if (zoneid_dictionary.ContainsKey(goalNext))
            {
                zoneid_dictionary.TryGetValue(goalNext, out goalvalue);
                _ZoneID.Text = goalvalue;
            }

            if (goaltargettext_dictionary.ContainsKey(goalNext))
            {
                goaltargettext_dictionary.TryGetValue(goalNext, out goalvalue);
                _GoalTargetText.Text = goalvalue;
            }
            else _GoalTargetText.Text = "";

            if (messageStarted_dictionary.ContainsKey(goalNext))
            {
                messageStarted_dictionary.TryGetValue(goalNext, out goalvalue);
                richTextBoxStarted.Text = goalvalue;
            }
            else richTextBoxStarted.Text = "";

            if (messageAborted_dictionary.ContainsKey(goalNext))
            {
                messageAborted_dictionary.TryGetValue(goalNext, out goalvalue);
                richTextBoxAborted.Text = goalvalue;
            }
            else richTextBoxAborted.Text = "";

            if (messageDone_dictionary.ContainsKey(goalNext))
            {
                messageDone_dictionary.TryGetValue(goalNext, out goalvalue);
                richTextBoxDone.Text = goalvalue;
            }
            else richTextBoxDone.Text = "";

            if (messageCompleted_dictionary.ContainsKey(goalNext))
            {
                messageCompleted_dictionary.TryGetValue(goalNext, out goalvalue);
                richTextBoxCompleted.Text = goalvalue;
            }
            else richTextBoxCompleted.Text = "";

            if(destroyItem_dictionary.ContainsKey(goalNext))
            {
                destroyItem_dictionary.TryGetValue(goalNext, out goalvalue);
                checkBoxDestroyItem.Checked = goalvalue =="" ? false : bool.Parse(goalvalue);
            }
            else checkBoxDestroyItem.Checked = false;


            if (GoalSteps.Count >= goalNext && goaltype_dictionary.Count >= goalNext)
            {
                _GoalType.SelectedIndex = goaltype_dictionary[(int)GoalSteps[goalNext - 1]];
            }
            else
            {
                GoalSteps.Add(GoalType.EnterArea);
                _GoalType.SelectedIndex = 0;
            }
        }

        //Step data back
        private void previousGoal_Click(object sender, EventArgs e)
        {
            int goalNum = int.Parse(GoalNumber.Text);

            if (goalNum == 1) //return if already at goal 1, there is no goal 0
            {
                return;
            }

            //remove step altogether if mandatory fields are not entered
            if (_GoalType.Text == "")
            {            
                goaltext_dictionary.Remove(goalNum);
                goaltargetname_dictionary.Remove(goalNum);
                stepItemTemplate_dictionnary.Remove(goalNum);
                stepStartItemTemplate_dictionnary.Remove(goalNum);
                item_dictionary.Remove(goalNum);
                goalstepno_dictionary.Remove(goalNum);
                goalrepeatno_dictionary.Remove(goalNum);
                xloc_dictionary.Remove(goalNum);
                yloc_dictionary.Remove(goalNum);
                zoneid_dictionary.Remove(goalNum);
                goaltargettext_dictionary.Remove(goalNum);
                messageStarted_dictionary.Remove(goalNum);
                messageAborted_dictionary.Remove(goalNum);
                messageDone_dictionary.Remove(goalNum);
                messageCompleted_dictionary.Remove(goalNum);
                goalAdvanceText_dictionnary.Remove(goalNum);
            }
            else
            //Needed to commit data to m_dictionary when the back button is clicked
            {
                bool shouldSave = e is GoalEventArgs goalArgs ? goalArgs.ShouldSavePreviousStep : true;

                if (shouldSave)
                {
                    SetGoalDependanceList(goalNum);
                    SetGoalStopList(goalNum);
                    PopulateGoalDependanceListBox(goalNum - 1);
                    PopulateGoalStopListBox(goalNum - 1);
                    goaltext_dictionary.Remove(goalNum);
                    goaltext_dictionary.Add(goalNum, _QuestGoals.Text);
                    goaltargetname_dictionary.Remove(goalNum);
                    goaltargetname_dictionary.Add(goalNum, _GoalTargetName.Text);

                    if (GoalSteps[goalNum - 1] == GoalType.Whisper)
                    {
                        if (goalAdvanceText_dictionnary.Count == 0)
                        {
                            for(int i = 1; i < goalNum; i++)
                            {
                                goalAdvanceText_dictionnary.Add(i, string.Empty);
                            }
                        }
                      
                        goalAdvanceText_dictionnary.Remove(goalNum);
                        goalAdvanceText_dictionnary.Add(goalNum, _GoalOptional.Text);
                    }
                    else if (IsUsingItem(GoalSteps[goalNum - 1]))
                    {
                        if (item_dictionary.Count == 0)
                        {
                            for (int i = 1; i < goalNum; i++)
                            {
                                item_dictionary.Add(i, string.Empty);
                            }
                        }

                        item_dictionary.Remove(goalNum);
                        item_dictionary.Add(goalNum, _GoalOptional.Text);
                    }

                    stepItemTemplate_dictionnary.Remove(goalNum);
                    stepItemTemplate_dictionnary.Add(goalNum, _StepItemTemplate.Text);
                    stepStartItemTemplate_dictionnary.Remove(goalNum);
                    stepStartItemTemplate_dictionnary.Add(goalNum, _StepStartItemTemplate.Text);

                    if (GoalSteps[goalNum - 1] == GoalType.Timer)
                    {
                        seconds_dictionary.Remove(goalNum);
                        seconds_dictionary.Add(goalNum, _GoalOptional.Text);
                    }
                    if (IsUsingArea(GoalSteps[goalNum - 1]))
                    {
                        arearadius_dictionary.Remove(goalNum);
                        arearadius_dictionary.Add(goalNum, _Radius.Text);
                    }

                    goalstepno_dictionary.Remove(goalNum);
                    goalstepno_dictionary.Add(goalNum, GoalStepNo.Text);
                    goalrepeatno_dictionary.Remove(goalNum);
                    goalrepeatno_dictionary.Add(goalNum, _GoalRepeatNo.Text);
                    xloc_dictionary.Remove(goalNum);

                    xloc_dictionary.Add(goalNum, string.IsNullOrWhiteSpace(_XOffset.Text) ? string.Empty : _XOffset.Text);
                    yloc_dictionary.Remove(goalNum);
                    yloc_dictionary.Add(goalNum, string.IsNullOrWhiteSpace(_YOffset.Text) ? string.Empty : _YOffset.Text);
                    zoneid_dictionary.Remove(goalNum);
                    zoneid_dictionary.Add(goalNum, string.IsNullOrWhiteSpace(_ZoneID.Text) ? string.Empty : _ZoneID.Text);
                    goaltargettext_dictionary.Remove(goalNum);
                    goaltargettext_dictionary.Add(goalNum, _GoalTargetText.Text);
                    messageStarted_dictionary.Remove(goalNum);
                    messageStarted_dictionary.Add(goalNum, richTextBoxStarted.Text);
                    messageAborted_dictionary.Remove(goalNum);
                    messageAborted_dictionary.Add(goalNum, richTextBoxAborted.Text);
                    messageDone_dictionary.Remove(goalNum);
                    messageDone_dictionary.Add(goalNum, richTextBoxDone.Text);
                    messageCompleted_dictionary.Remove(goalNum);
                    messageCompleted_dictionary.Add(goalNum, richTextBoxCompleted.Text);
                    targetRegions_dictionary.Remove(goalNum);
                    targetRegions_dictionary.Add(goalNum, _targetRegion.Text);
                    destroyItem_dictionary.Remove(goalNum);
                    destroyItem_dictionary.Add(goalNum, checkBoxDestroyItem.Checked.ToString());
                } 
            }

            GoalNumber.Text = (goalNum - 1).ToString();
            string goalvalue;
            goalNum--;
            int goalIndex = goalNum - 1;

            if (GoalSteps.Count > goalIndex && goaltype_dictionary.Count > goalIndex)
            {
                _GoalType.SelectedIndex = goaltype_dictionary[(int)GoalSteps[goalIndex]];
            }

            if (goaltext_dictionary.ContainsKey(goalNum))
            {
                goaltext_dictionary.TryGetValue(goalNum, out goalvalue);
                _QuestGoals.Text = goalvalue;
            }
            else _QuestGoals.Text = "";

            if (goaltargetname_dictionary.ContainsKey(goalNum))
            {
                goaltargetname_dictionary.TryGetValue(goalNum, out goalvalue);
                _GoalTargetName.Text = goalvalue;
            }
            else _GoalTargetName.Text = "";

            if (targetRegions_dictionary.ContainsKey(goalNum))
            {
                targetRegions_dictionary.TryGetValue(goalNum, out goalvalue);
                _targetRegion.Text = goalvalue;
            }
            else _targetRegion.Text = "";

            if (item_dictionary.ContainsKey(goalNum) && IsUsingItem(GoalSteps[goalNum - 1]))
            {
                item_dictionary.TryGetValue(goalNum, out goalvalue);
                _GoalOptional.Text = goalvalue;
            }

            if (arearadius_dictionary.ContainsKey(goalNum) && IsUsingArea(GoalSteps[goalNum - 1]))
            {
                arearadius_dictionary.TryGetValue(goalNum, out goalvalue);
                _Radius.Text = goalvalue;
            }

            if (seconds_dictionary.ContainsKey(goalNum) && GoalSteps[goalNum - 1] == GoalType.Timer)
            {
                seconds_dictionary.TryGetValue(goalNum, out goalvalue);
                _GoalOptional.Text = goalvalue;
            }


            if (stepItemTemplate_dictionnary.ContainsKey(goalNum))
            {
                stepItemTemplate_dictionnary.TryGetValue(goalNum, out goalvalue);
                _StepItemTemplate.Text = goalvalue;
            }
            else
                _StepItemTemplate.Text = "";

            if (stepStartItemTemplate_dictionnary.ContainsKey(goalNum))
            {
                stepStartItemTemplate_dictionnary.TryGetValue(goalNum, out goalvalue);
                _StepStartItemTemplate.Text = goalvalue;
            }
            else
                _StepStartItemTemplate.Text = "";


            if (goalAdvanceText_dictionnary.ContainsKey(goalNum) && GoalSteps[goalNum - 1] == GoalType.Whisper)
            {
                 goalAdvanceText_dictionnary.TryGetValue(goalNum, out goalvalue);
                _GoalOptional.Text = goalvalue;
            }   

            if (goalstepno_dictionary.ContainsKey(goalNum))
            {
                goalstepno_dictionary.TryGetValue(goalNum, out goalvalue);
                GoalStepNo.Text = goalvalue;
            }
            else GoalStepNo.Text = GoalNumber.Text;

            if (goalrepeatno_dictionary.ContainsKey(goalNum))
            {
                goalrepeatno_dictionary.TryGetValue(goalNum, out goalvalue);
                _GoalRepeatNo.Text = goalvalue;
            }
            else _GoalRepeatNo.Text = "";

            if (xloc_dictionary.ContainsKey(goalNum))
            {
                xloc_dictionary.TryGetValue(goalNum, out goalvalue);
                _XOffset.Text = goalvalue;
            }


            if (yloc_dictionary.ContainsKey(goalNum))
            {
                yloc_dictionary.TryGetValue(goalNum, out goalvalue);
                _YOffset.Text = goalvalue;
            }

            if (zoneid_dictionary.ContainsKey(goalNum))
            {
                zoneid_dictionary.TryGetValue(goalNum, out goalvalue);
                _ZoneID.Text = goalvalue;
            }

            if (goaltargettext_dictionary.ContainsKey(goalNum))
            {
                goaltargettext_dictionary.TryGetValue(goalNum, out goalvalue);
                _GoalTargetText.Text = goalvalue;
            }
            else _GoalTargetText.Text = "";

            if (messageStarted_dictionary.ContainsKey(goalNum))
            {
                messageStarted_dictionary.TryGetValue(goalNum, out goalvalue);
                richTextBoxStarted.Text = goalvalue;
            }
            else richTextBoxStarted.Text = "";

            if (messageAborted_dictionary.ContainsKey(goalNum))
            {
                messageAborted_dictionary.TryGetValue(goalNum, out goalvalue);
                richTextBoxAborted.Text = goalvalue;
            }
            else richTextBoxAborted.Text = "";

            if (messageDone_dictionary.ContainsKey(goalNum))
            {
                messageDone_dictionary.TryGetValue(goalNum, out goalvalue);
                richTextBoxDone.Text = goalvalue;
            }
            else richTextBoxDone.Text = "";

            if (messageCompleted_dictionary.ContainsKey(goalNum))
            {
                messageCompleted_dictionary.TryGetValue(goalNum, out goalvalue);
                richTextBoxCompleted.Text = goalvalue;
            }
            else richTextBoxCompleted.Text = "";

            if(destroyItem_dictionary.ContainsKey(goalNum))
            {
                destroyItem_dictionary.TryGetValue(goalNum, out goalvalue);
                checkBoxDestroyItem.Checked = goalvalue == "" ? false : bool.Parse(goalvalue);
            }
            else checkBoxDestroyItem.Checked = false;

        }

        //Final Reward forward dictionary
        private void finrewardForward_Click(object sender, EventArgs e)
        {
            if (_FinalReward.Text == "")
            {
                MessageBox.Show("You can't add nothing as a reward!", "PEBKAC");
                return;
            }

            int finNum = int.Parse(finNumber.Text);

            if (!fin_dictionary.ContainsKey(finNum)) //If the reward data is not in the dictionary...check for step 1, then add
            {
                fin_dictionary.Add(finNum, _FinalReward.Text);
            }
            else //If the reward data is in the dictionary...check if the values match and add if they don't
            {
                fin_dictionary.TryGetValue(finNum, out string finvalue);

                if (finvalue != _FinalReward.Text)
                {
                    fin_dictionary.Remove(finNum);
                    fin_dictionary.Add(finNum, _FinalReward.Text);
                }
            }

            finNumber.Text = (finNum + 1).ToString();

            //Check if next step contains data
            int finNext = int.Parse(finNumber.Text);
            if (fin_dictionary.ContainsKey(finNext))
            {
                fin_dictionary.TryGetValue(finNext, out string finvalue);
                _FinalReward.Text = finvalue;
            }
            else _FinalReward.Text = "";

        }

        //Final Reward back Dictionary
        private void finrewardBack_Click(object sender, EventArgs e)
        {
            int finNum = int.Parse(finNumber.Text);

            if (finNum == 1) //return if already at step 1, there ain't no step 0
            {
                return;
            }

            if (_FinalReward.Text != "") //Add data on back click
            {
                fin_dictionary.TryGetValue(finNum, out string finvalue);
                if (finvalue != _FinalReward.Text) //check if data is different from dictionary data
                {
                    fin_dictionary.Remove(finNum);
                    fin_dictionary.Add(finNum, _FinalReward.Text);
                }
            }
            else //There are no breaks in reward data, so a blank box cannot exist between populated blocks
            {
                if (fin_dictionary.ContainsKey(finNum + 1))
                {
                    MessageBox.Show("You must remove the items listed after the current item to remove this one!", "Error");
                    return;
                }
                else
                {
                    fin_dictionary.Remove(finNum);
                }
            }

            //Pull previous step data
            int fincheck = int.Parse(finNumber.Text);
            fincheck--;
            fin_dictionary.TryGetValue(fincheck, out string finback);
            _FinalReward.Text = finback;
            finNumber.Text = (finNum - 1).ToString(); //finally, decrement fin label
        }

        //Optional Reward forward dictionary
        private void optrewardForward_Click(object sender, EventArgs e)
        {
            if (_OptionalReward.Text == "")
            {
                MessageBox.Show("You can't add nothing as a reward!", "PEBKAC");
                return;
            }

            int optNum = int.Parse(optNumber.Text);

            if (optNum == 8)
            {
                opt_dictionary.Remove(optNum);
                opt_dictionary.Add(optNum, _OptionalReward.Text);
                MessageBox.Show("Last optional item added", "8 Optional Rewards Max");
                return;
            }

            if (!opt_dictionary.ContainsKey(optNum)) //If the reward data is not in the dictionary...check for step 1, then add
            {
                opt_dictionary.Add(optNum, _OptionalReward.Text);
            }
            else //If the reward data is in the dictionary...check if the values match and add if they don't
            {
                opt_dictionary.TryGetValue(optNum, out string optvalue);

                if (optvalue != _OptionalReward.Text)
                {
                    opt_dictionary.Remove(optNum);
                    opt_dictionary.Add(optNum, _OptionalReward.Text);
                }
            }

            optNumber.Text = (optNum + 1).ToString();

            //Check if next step contains data
            int optNext = int.Parse(optNumber.Text);
            if (opt_dictionary.ContainsKey(optNext))
            {
                opt_dictionary.TryGetValue(optNext, out string optvalue);
                _OptionalReward.Text = optvalue;
            }
            else _OptionalReward.Text = "";

        }

        //Optional Reward back dictionary
        private void optrewardBack_Click(object sender, EventArgs e)
        {
            int optNum = int.Parse(optNumber.Text);

            if (optNum == 1) //return if already at step 1, there ain't no step 0
            {
                return;
            }

            if (_OptionalReward.Text != "") //Add data on back click
            {
                opt_dictionary.TryGetValue(optNum, out string optvalue);
                if (optvalue != _OptionalReward.Text)
                {
                    opt_dictionary.Remove(optNum);
                    opt_dictionary.Add(optNum, _OptionalReward.Text);
                }
            }
            else
            {
                if (opt_dictionary.ContainsKey(optNum + 1))
                {
                    MessageBox.Show("You must remove the items listed after the current item to remove this one!", "Error");
                    return;
                }
                else
                {
                    opt_dictionary.Remove(optNum);
                }
            }

            //Pull previous step data
            int optcheck = int.Parse(optNumber.Text);
            optcheck--;
            opt_dictionary.TryGetValue(optcheck, out string optback);
            _OptionalReward.Text = optback;

            optNumber.Text = (optNum - 1).ToString(); //finally, decrement opt label
        }

        // Hover over tooltips

        private void label8_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The X co-ordinate for this quest target. Used to display red dot on players map. Use /loc command ingame for the number.", labelXloc, 10000);
        }

        private void label8_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(labelXloc);
        }

        private void label11_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The Y co-ordinate for this quest target. Used to display red dot on players map. Use /loc command ingame for the number.", labelYloc, 10000);
        }

        private void label11_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(labelYloc);
        }

        private void label14_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The Y co-ordinate for this quest target. Used to display red dot on players map. Use /loc command ingame for the number.", labelZoneId, 10000);
        }

        private void label14_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(labelZoneId);
        }

        private void label10_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The ste", labelStepNo, 15000);
        }

        private void label10_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(labelStepNo);
        }

        private void label24_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("How many times you must kill the target to complete the goal. Only used on 'kill' goaltype.", label24, 10000);
        }

        private void label24_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label24);
        }

        private void label19_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("A text window displayed when the player interacts with the goal target. Interact, InteractDeliver goaltype only.", labelGoalInteract, 10000);
        }

        private void label19_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(labelGoalInteract);
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The text thats displayed in the players journal for this quest goal.", labelGoalText, 10000);
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(labelGoalText);
        }

        private void label16_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("GoalTypes:\n" +
                "Collect - collect the goal target to complete. Can increase the count for this type. \n" +
                "Kill - kill the goal target to complete. Can increase the count for this type. \n" +
                "EnterArea - /search an area in game to complete. \n" +
                "Interact - Interact with the goal target to complete. \n" +
                //"InteractDeliver - Used to deliver the 'collect item'. Interact with the goal target to complete. \n" +
                "Killed - Player must be killed by target. \n" +
                "Timer - Player must wait for given seconds. \n" +
                "UseItem - Player must use the given item. \n" +
                "Whisper - Player must whisper the advancetext to the target to complete. \n" +
                "End - Interact with the goal target to finish the quest. This required for the last step of EVERY quest!", labelGoalType, 20000);

        }

        private void label16_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(labelGoalType);
        }

        private void label21_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("A dummy item that will show its model icon in the journal upon completing the goal.\n" +
                "When used with 'InteractDeliver' goaltype, the model icon will be displated immediately after receiving the goal.", labelGoalText, 15000);
        }

        private void label21_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(_goalOptionalLabel);
        }

        private void label13_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("Minimum level quest is offered to player.", label13, 10000);
        }

        private void label13_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label13);
        }

        private void label35_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("Maximum level quest is offered to player.", label35, 10000);
        }

        private void label35_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label35);
        }

        private void label32_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("Maximum times a player can repeat the quest.", label32, 10000);
        }

        private void label32_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label32);
        }

        private void label7_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("List of classes quest is offered to. Leave blank for all or shift+click to select multiple.", label7, 10000);
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label7);
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("Auto-generated ID. You can change this in the database after quest is saved or use for reference to use 'Load' button.", label1, 10000);
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label1);
        }

        private void label18_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("NPC that offers the quest.", label18, 10000);
        }

        private void label18_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label18);
        }

        private void label17_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("Region of the quest Start NPC.", label17, 10000);
        }

        private void label17_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label17);
        }

        private void label5_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The quest story presented to the player when offered the quest.", label5, 10000);
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label5);
        }

        private void summaryLabel_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The summary of the quest displayed in the quest journal.", summaryLabel, 10000);
        }

        private void summaryLabel_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(summaryLabel);
        }

        private void conclusionLabel_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The text displayed to the player upon completing the quest and being offered the rewards.", conclusionLabel, 10000);
        }

        private void conclusionLabel_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(conclusionLabel);
        }

        private void label34_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The items a player recieves upon completing the quest.", label34, 10000);
        }

        private void _QuestDependency_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("Name of quest a player must complete before this quest is offered to them.", _QuestDependency, 10000);
        }

        private void _suppressGoal_Click(object sender, EventArgs e)
        {
            int stepNumber = int.Parse(GoalNumber.Text);
            if (stepNumber > 1 && stepNumber == GoalSteps.Count)
            {
               var result = MessageBox.Show(string.Format("You are about to erase the goal step n° {0}\nDo you Confirm ?", stepNumber), "Erase this Step ?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    SuppressStep(stepNumber);
                }
            }
            else
            {
                MessageBox.Show("You can only erase the last step", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SuppressStep(int step)
        {
            if (GoalSteps.Count >= step)
            {               
                if (IsUsingItem(GoalSteps[step - 1]))
                {
                    item_dictionary.Remove(step);
                }
                else if (GoalSteps[step - 1] == GoalType.Whisper)
                {
                    goalAdvanceText_dictionnary.Remove(step);
                }

                goaltargetname_dictionary.Remove(step);
                targetRegions_dictionary.Remove(step);
                goaltext_dictionary.Remove(step);
                xloc_dictionary.Remove(step);
                yloc_dictionary.Remove(step);
                zoneid_dictionary.Remove(step);
                goalrepeatno_dictionary.Remove(step);
                goaltargettext_dictionary.Remove(step);
                messageStarted_dictionary.Remove(step);
                messageAborted_dictionary.Remove(step);
                messageDone_dictionary.Remove(step);
                messageCompleted_dictionary.Remove(step);
                stepItemTemplate_dictionnary.Remove(step);
                stepStartItemTemplate_dictionnary.Remove(step);
                

              GoalSteps.RemoveAt(step - 1);
              PopulateGoalDependanceListBox(step - 1);
              PopulateGoalStopListBox(step - 1);
              previousGoal_Click(this, new GoalEventArgs(){ ShouldSavePreviousStep = false });
            }
        }

        private void Reputation_TextChanged(object sender, EventArgs e)
        {
            if (Reputation.Text != "-" && (!float.TryParse(Reputation.Text, out float val) || val > 0))
            {
                reputation = "0";
                Reputation.Text = "0";
            }
            else
            {
                reputation = Reputation.Text;
            }
        }

        private void ReputationReward_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if ( !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
        }

        private void _suppressGoal_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("Erase this step", _suppressGoal, 10000);
        }

        private void NewQuestButton_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show("if you start a new quest the current edited quest will not be saved.\n\nDo you want to continue ?", "Be Careful", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (dialog == DialogResult.OK)
            {
                Clear();
                SetFieldDefaults();
            }
        }

        private void label34_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label34);
        }

        private void label33_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The optional items a player has a choice from upon completing the quest.\n" +
                "You can set multiple options in here and set the number the player can choose from the counter.", label33, 10000);
        }

        private void label33_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label33);
        }

        private void label22_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("How many optional rewards the player can choose from.", label22, 10000);
        }

        private void label22_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label22);
        }

        private void label3_MouseHover_1(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The current goal. This number is not important and is not the step number.", label3, 10000);
        }

        private void label3_MouseLeave_1(object sender, EventArgs e)
        {
            toolTip1.Hide(label3);
        }

        // Add classes and associated ID to a dictionary for later use
        private void PopulateClassDictionary()
        {            
            allClasses.Add(2, "Armsman");
            allClasses.Add(13, "Cabalist");
            allClasses.Add(6, "Cleric");
            allClasses.Add(10, "Friar");
            allClasses.Add(33, "Heretic");
            allClasses.Add(9, "Infiltrator");
            allClasses.Add(11, "Mercenary");
            allClasses.Add(4, "Minstrel");
            allClasses.Add(12, "Necromancer");
            allClasses.Add(1, "Paladin");
            allClasses.Add(19, "Reaver");
            allClasses.Add(3, "Scout");
            allClasses.Add(8, "Sorcerer");
            allClasses.Add(5, "Theurgist");
            allClasses.Add(7, "Wizard");
            allClasses.Add(60, "MaulerAlb");
            allClasses.Add(31, "Berserker");
            allClasses.Add(30, "Bonedancer");
            allClasses.Add(26, "Healer");
            allClasses.Add(25, "Hunter");
            allClasses.Add(29, "Runemaster");
            allClasses.Add(32, "Savage");
            allClasses.Add(23, "Shadowblade");
            allClasses.Add(28, "Shaman");
            allClasses.Add(24, "Skald");
            allClasses.Add(27, "Spiritmaster");
            allClasses.Add(21, "Thane");
            allClasses.Add(34, "Valkyrie");
            allClasses.Add(59, "Warlock");
            allClasses.Add(22, "Warrior");
            allClasses.Add(61, "MaulerMid");
            allClasses.Add(55, "Animist");
            allClasses.Add(39, "Bainshee");
            allClasses.Add(48, "Bard");
            allClasses.Add(43, "Blademaster");
            allClasses.Add(45, "Champion");
            allClasses.Add(47, "Druid");
            allClasses.Add(40, "Eldritch");
            allClasses.Add(41, "Enchanter");
            allClasses.Add(44, "Hero");
            allClasses.Add(42, "Mentalist");
            allClasses.Add(49, "Nightshade");
            allClasses.Add(50, "Ranger");
            allClasses.Add(56, "Valewalker");
            allClasses.Add(58, "Vampiir");
            allClasses.Add(46, "Warden");
            allClasses.Add(62, "MaulerHib");
            allClasses.Add(16, "Acolyte");
            allClasses.Add(17, "AlbionRogue");
            allClasses.Add(20, "Disciple");
            allClasses.Add(15, "Elementalist");
            allClasses.Add(14, "Fighter");
            allClasses.Add(57, "Forester");
            allClasses.Add(52, "Guardian");
            allClasses.Add(18, "Mage");
            allClasses.Add(51, "Magician");
            allClasses.Add(38, "MidgardRogue");
            allClasses.Add(36, "Mystic");
            allClasses.Add(53, "Naturalist");
            allClasses.Add(37, "Seer");
            allClasses.Add(54, "Stalker");
            allClasses.Add(35, "Viking");
        }

        private void RewardQuestControl_Load(object sender, EventArgs e)
        {
            SetupDropdowns();
        }

        private void SetupDropdowns()
        {
        //    ComboboxService.BindQuestStep(_GoalType);
        }

        private void ConvertAllowClassToByte()
        {
            StringBuilder allcl = new StringBuilder(allowedClasses);
            //allcl.Replace("", "All");
            allcl.Replace("2", "Armsman");
            allcl.Replace("13", "Cabalist");
            allcl.Replace("6", "Cleric");
            allcl.Replace("10", "Friar");
            allcl.Replace("33", "Heretic");
            allcl.Replace("9", "Infiltrator");
            allcl.Replace("11", "Mercenary");
            allcl.Replace("4", "Minstrel");
            allcl.Replace("12", "Necromancer");
            allcl.Replace("1", "Paladin");
            allcl.Replace("19", "Reaver");
            allcl.Replace("3", "Scout");
            allcl.Replace("8", "Sorcerer");
            allcl.Replace("5", "Theurgist");
            allcl.Replace("7", "Wizard");
            allcl.Replace("60", "MaulerAlb");
            allcl.Replace("31", "Berserker");
            allcl.Replace("30", "Bonedancer");
            allcl.Replace("26", "Healer");
            allcl.Replace("25", "Hunter");
            allcl.Replace("29", "Runemaster");
            allcl.Replace("32", "Savage");
            allcl.Replace("23", "Shadowblade");
            allcl.Replace("28", "Shaman");
            allcl.Replace("24", "Skald");
            allcl.Replace("27", "Spiritmaster");
            allcl.Replace("21", "Thane");
            allcl.Replace("34", "Valkyrie");
            allcl.Replace("59", "Warlock");
            allcl.Replace("22", "Warrior");
            allcl.Replace("61", "MaulerMid");
            allcl.Replace("55", "Animist");
            allcl.Replace("39", "Bainshee");
            allcl.Replace("48", "Bard");
            allcl.Replace("43", "Blademaster");
            allcl.Replace("45", "Champion");
            allcl.Replace("47", "Druid");
            allcl.Replace("40", "Eldritch");
            allcl.Replace("41", "Enchanter");
            allcl.Replace("44", "Hero");
            allcl.Replace("42", "Mentalist");
            allcl.Replace("49", "Nightshade");
            allcl.Replace("50", "Ranger");
            allcl.Replace("56", "Valewalker");
            allcl.Replace("58", "Vampiir");
            allcl.Replace("46", "Warden");
            allcl.Replace("62", "MaulerHib");
            allcl.Replace("16", "Acolyte");
            allcl.Replace("17", "AlbionRogue");
            allcl.Replace("20", "Disciple");
            allcl.Replace("15", "Elementalist");
            allcl.Replace("14", "Fighter");
            allcl.Replace("57", "Forester");
            allcl.Replace("52", "Guardian");
            allcl.Replace("18", "Mage");
            allcl.Replace("51", "Magician");
            allcl.Replace("38", "MidgardRogue");
            allcl.Replace("36", "Mystic");
            allcl.Replace("53", "Naturalist");
            allcl.Replace("37", "Seer");
            allcl.Replace("54", "Stalker");
            allcl.Replace("35", "Viking");
            allowedClasses = allcl.ToString();
        }
    }  
}
