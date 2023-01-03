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

namespace AmteCreator.DataQuestRewardQuests
{
    public partial class DataQuestRewardQuests : UserControl
    {
        //private readonly RewardQuestService _questService;
        private DBDQRewardQTemplate _quest;

        public readonly string dataTableName = "dataquestrewardquest";
        public readonly string idField = nameof(DBDQRewardQTemplate.ID);
        public readonly string idName = nameof(DBDQRewardQTemplate.Name);

        string goalType, questGoals, goalRepeatNo, goalTargetName, goalTargetText, collectItemTemplate, stepItemTemplate, stepTextTemplate, goalStepNo, goalAdvanceText, questDependency;
        string optionalRewardItemTemplates = "", finalRewardItemTemplates = "";
        string allowedClasses;
        string allowedRaces;
        string xloc, yloc, zoneId;
        string finishNPC;
        int stepCount;
        string reputation;

        // using this to determine if quest can be saved back to original ID
        private bool LoadedQuest { get; set; } = false;


        /// <summary>
        /// goal info
        /// GoalType Enum - List Index
        /// </summary>
        public Dictionary<int, int> goaltype_dictionary;
        public List<GoalType> GoalSteps;

        public Dictionary<int, string> goaltext_dictionary;
        public Dictionary<int, string> goalrepeatno_dictionary;
        public Dictionary<int, string> goaltargetname_dictionary;
        public Dictionary<int, string> goaltargettext_dictionary;
        public Dictionary<int, string> goalAdvanceText_dictionnary;
        public Dictionary<int, string> colitem_dictionary;
        public Dictionary<int, string> stepItemTemplate_dictionnary;
        public Dictionary<int, string> stepText_dictionnary;
        public Dictionary<int, string> goalstepno_dictionary;
        public Dictionary<int, string> targetRegions_dictionary;
        public Dictionary<int, string> xloc_dictionary;
        public Dictionary<int, string> yloc_dictionary;
        public Dictionary<int, string> zoneid_dictionary;
        // item rewards
        public Dictionary<int, string> opt_dictionary;
        public Dictionary<int, string> fin_dictionary;        
        // quest restrictions
        private Dictionary<int, string> allClasses;

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
            colitem_dictionary = new Dictionary<int, string>();
            stepItemTemplate_dictionnary = new Dictionary<int, string>();
            stepText_dictionnary = new Dictionary<int, string>();
            goalrepeatno_dictionary = new Dictionary<int, string>();
            goaltargetname_dictionary = new Dictionary<int, string>();
            goaltargettext_dictionary = new Dictionary<int, string>();
            goaltext_dictionary = new Dictionary<int, string>();
            goaltype_dictionary = new Dictionary<int, int>();
            goalstepno_dictionary = new Dictionary<int, string>();
            xloc_dictionary = new Dictionary<int, string>();
            yloc_dictionary = new Dictionary<int, string>();
            zoneid_dictionary = new Dictionary<int, string>();
            allClasses = new Dictionary<int, string>();
            allRaces = new Dictionary<int, int>();
            targetRegions_dictionary = new Dictionary<int, string>();
            goalAdvanceText_dictionnary = new Dictionary<int, string>();
            GoalSteps = new List<GoalType>();

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

            try
            {
                questGoals = quest.QuestGoals;
                goalType = quest.GoalType;
                goalRepeatNo = quest.GoalRepeatNo ?? string.Empty;
                goalTargetName = quest.GoalTargetName ?? string.Empty;
                goalTargetText = quest.GoalTargetText ?? string.Empty;
                questDependency = quest.QuestDependency;
                _ID.Text = quest.ID?.ToString();
                _MinLevel.Text = quest.MinLevel.ToString();
                _MaxLevel.Text = quest.MaxLevel.ToString();
                _QuestName.Text = quest.QuestName ?? string.Empty;
                _StartNPC.Text = quest.StartNPC ?? string.Empty;
                _StartRegionID.Text = quest.StartRegionID.ToString();
                _StoryText.Text = quest.StoryText ?? string.Empty;
                _Summary.Text = quest.Summary ?? string.Empty;
                _AcceptText.Text = quest.AcceptText ?? string.Empty;
                _MaxCount.Text = quest.MaxCount.ToString();
                _IsRenaissance.Checked = quest.IsRenaissance;
                _FinishText.Text = quest.FinishText ?? string.Empty;
                _suppressGoal.Enabled = false;
                _FinishNPC.Text = quest.FinishNPC;
                goalAdvanceText = quest.AdvanceText ?? string.Empty;
                _RewardMoney.Text = quest.RewardMoney.ToString();
                ReputationReward.Text = quest.RewardReputation.ToString();
                _RewardXP.Text = quest.RewardXP.ToString();
                _RewardCLXP.Text = quest.RewardCLXP.ToString();
                _RewardBP.Text = quest.RewardBP.ToString();
                _RewardRP.Text = quest.RewardRP.ToString();
                GoalNumber.Text = "1";
                Reputation.Text = quest.Reputation;



                collectItemTemplate = quest.CollectItemTemplate;
                stepItemTemplate = quest.StepItemTemplates;
                stepTextTemplate = quest.StepText;
                string rawOptRewards = quest.OptionalRewardItemTemplates;
                string numOptChoices = "1";
                //string numOptChoices = string.IsNullOrWhiteSpace(rawOptRewards) ? "1" : rawOptRewards.Substring(0, 1), optionalRewardItemTemplates = rawOptRewards.Substring(1);
                if (!string.IsNullOrWhiteSpace(rawOptRewards))
                {
                    numOptChoices = rawOptRewards.Substring(0, 1);
                    optionalRewardItemTemplates = rawOptRewards.Substring(1);
                }
                else
                {
                    optionalRewardItemTemplates = string.Empty;
                }


                finalRewardItemTemplates = quest.FinalRewardItemTemplates;
                xloc = quest.XOffset;
                yloc = quest.YOffset;
                zoneId = quest.ZoneID;

                 GoalSteps = quest.GoalType.Split(new string[] { "|" }, StringSplitOptions.None)
                    .Select(e => Enum.TryParse<GoalType>(e, out GoalType goal) ? goal : GoalType.Unknown).ToList();   
                
                

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
                    GoalSteps.Add(GoalType.Search);
                }

                string[] splitGoalRepeatNo = goalRepeatNo.Split(new string[] { "|" }, StringSplitOptions.None);
                StringToDictionary(splitGoalRepeatNo, goalrepeatno_dictionary);
                _GoalRepeatNo.Text = goalrepeatno_dictionary[1];

                this.SetFieldWithSemiColon(goalTargetName, goaltargetname_dictionary, targetRegions_dictionary);
                _GoalTargetName.Text = goaltargetname_dictionary[1];
                _targetRegion.Text = targetRegions_dictionary[1];

                string[] splitgoalTargetText = goalTargetText.Split(new string[] { "|" }, StringSplitOptions.None);
                StringToDictionary(splitgoalTargetText, goaltargettext_dictionary);
                _GoalTargetText.Text = goaltargettext_dictionary[1];

                if (!string.IsNullOrEmpty(collectItemTemplate))
                {
                    string[] splitcollectItemTemplate = collectItemTemplate.Split(new string[] { "|" }, StringSplitOptions.None);
                    StringToDictionary(splitcollectItemTemplate, colitem_dictionary);
                    _GoalOptional.Text = colitem_dictionary[1];

                }                
                
                if (!string.IsNullOrEmpty(stepItemTemplate))
                {
                    string[] stepItemTemplates = stepItemTemplate.Split(new string[] { "|" }, StringSplitOptions.None);
                    StringToDictionary(stepItemTemplates, stepItemTemplate_dictionnary);
                    _StepItemTemplate.Text = stepItemTemplate_dictionnary[1];
                }

                if (!string.IsNullOrEmpty(stepTextTemplate))
                {
                    string[] stepTexts = stepTextTemplate.Split(new string[] { "|" }, StringSplitOptions.None);
                    StringToDictionary(stepTexts, stepText_dictionnary);
                    _StepText.Text = stepText_dictionnary[1];
                }             

                string[] splitAdvanceText = goalAdvanceText.Split(new string[] { "|" }, StringSplitOptions.None);
                StringToDictionary(splitAdvanceText, goalAdvanceText_dictionnary);

                string[] splitOptionalRewards = optionalRewardItemTemplates.Split(new string[] { "|" }, StringSplitOptions.None);
                StringToDictionary(splitOptionalRewards, opt_dictionary);
                _OptionalReward.Text = opt_dictionary[1];
                if (int.TryParse(numOptChoices, out int choices))
                {
                    OptRewardUpDown.Value = choices;
                }

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
               
                if (!string.IsNullOrEmpty(xloc))
                {
                    string[] splitXloc = xloc.Split(new string[] { "|" }, StringSplitOptions.None);
                    StringToDictionary(splitXloc, xloc_dictionary);
                    _XOffset.Text = xloc_dictionary[1];
                }

                if (!string.IsNullOrEmpty(yloc))
                {
                    string[] splitYloc = yloc.Split(new string[] { "|" }, StringSplitOptions.None);
                    StringToDictionary(splitYloc, yloc_dictionary);
                    _YOffset.Text = yloc_dictionary[1];
                }

                if (!string.IsNullOrEmpty(zoneId))
                {
                    string[] splitZoneId = zoneId.Split(new string[] { "|" }, StringSplitOptions.None);
                    StringToDictionary(splitZoneId, zoneid_dictionary);
                    _ZoneID.Text = zoneid_dictionary[1];
                }               

                if (this.SetFieldWithSemiColon(questGoals, goaltext_dictionary, goalstepno_dictionary))
                {
                    _QuestGoals.Text = goaltext_dictionary[1];
                    GoalStepNo.Text = goalstepno_dictionary[1];
                }

                if (questDependency != null)
                {
                    string[] questDependencies = questDependency.Split(new string[] { "|" }, StringSplitOptions.None);
                    _QuestDependency.Text = questDependencies[0];
                }

                UpdateOptionalFields();
                SelectAllowedClasses();
                SelectAllowedRaces();
                LoadedQuest = true;
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message, "Error while deserializing data! Quest was not loaded completely - Errors in database format.", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            _goalOptionalLabel.Show();
            _GoalOptional.Show();
            _StepItemTemplate.Show();
            _StepItemLabel.Show();

            if (GoalSteps.Count >= step)
            {
                var goalType = GoalSteps[step - 1];

                if (goalType == GoalType.Collect)
                {
                    goalAdvanceText_dictionnary.Remove(step);
                    stepItemTemplate_dictionnary.Remove(step);

                    if (colitem_dictionary.Count == 0)
                    {
                        for(int i = 1; i <= GoalSteps.Count; i++)
                        {
                            colitem_dictionary.Add(i, string.Empty);
                        }
                    }

                    if (colitem_dictionary.ContainsKey(step))
                    {
                        _GoalOptional.Text = colitem_dictionary[step];
                    }
                    else
                    {
                        _GoalOptional.Text = string.Empty;
                    }               

                    _goalOptionalLabel.Text = "Collect Item Id_nb";
                    _StepItemTemplate.Text = string.Empty;
                    _StepItemTemplate.Hide();
                    _StepItemLabel.Hide();

                }
                else if (goalType == GoalType.InteractWhisper || goalType == GoalType.InteractDeliver || goalType == GoalType.DeliverFinish)
                {
                    if (goalType == GoalType.InteractWhisper)
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

                        _goalOptionalLabel.Text = "Advance Text";
                    }                  
                    else if (goalType == GoalType.InteractDeliver || goalType == GoalType.DeliverFinish)
                    {
                        goalAdvanceText_dictionnary.Remove(step);
                        colitem_dictionary.Remove(step);
                        _goalOptionalLabel.Hide();
                        _GoalOptional.Hide();
                    }                

                    if (stepItemTemplate_dictionnary.Count == 0)
                    {
                        for (int i = 1; i <= GoalSteps.Count; i++)
                        {
                            stepItemTemplate_dictionnary.Add(i, string.Empty);
                        }
                    }

                    if (stepItemTemplate_dictionnary.ContainsKey(step))
                    {
                        _StepItemTemplate.Text = stepItemTemplate_dictionnary[step];
                    }
                    else
                    {
                        _StepItemTemplate.Text = string.Empty;
                    }
                }else
                {
                    _StepItemTemplate.Text = string.Empty;
                    stepItemTemplate_dictionnary.Remove(step);
                    _StepItemLabel.Hide();
                    _StepItemTemplate.Hide();
                    _goalOptionalLabel.Hide();
                    _GoalOptional.Hide();
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
                
        // attempt to save the current quest
        private void questSave_Click(object sender, EventArgs e)
        {
            int goalNum = int.Parse(GoalNumber.Text);
            int optNum = int.Parse(optNumber.Text);
            int optChoices = (int)OptRewardUpDown.Value;
            int finNum = int.Parse(finNumber.Text);

       
        
            goalstepno_dictionary.Remove(goalNum);
            goalstepno_dictionary.Add(goalNum, GoalStepNo.Text);
            
            // safety to prevent bad quests
            if (!IsLastStepCorrect())
            {
                MessageBox.Show("The last step MUST be of goaltype 'InteractFinish' or 'Collect' or 'DeliverFinish'. Check your quests goals/steps.\n\nIf the last goaltype is 'InteractFinish' ensure FinishNPC is correctly filled.", @"Quest Incomplete!");
                return;
            }
   

            if (colitem_dictionary.Count == 0 && GoalSteps.Any(s => s == GoalType.Collect))
            {
                for(int i = 1; i <= GoalSteps.Count; i++)
                {
                    colitem_dictionary.Add(i, string.Empty);
                }
            }

            if (goalAdvanceText_dictionnary.Count == 0 && GoalSteps.Any(s => s == GoalType.InteractWhisper))
            {
                for (int i = 1; i <= GoalSteps.Count; i++)
                {
                    goalAdvanceText_dictionnary.Add(i, string.Empty);
                }
            }

            if (GoalSteps[goalNum - 1] == GoalType.Collect)
            {
                colitem_dictionary.Remove(goalNum);
                colitem_dictionary.Add(goalNum, _GoalOptional.Text);
            }


            if (GoalSteps[goalNum - 1] == GoalType.InteractWhisper)
            {
                goalAdvanceText_dictionnary.Remove(goalNum);
                goalAdvanceText_dictionnary.Add(goalNum, _GoalOptional.Text);
            }

            goaltext_dictionary.Remove(goalNum);
            goaltext_dictionary.Add(goalNum, _QuestGoals.Text);      
            goaltargetname_dictionary.Remove(goalNum);
            goaltargetname_dictionary.Add(goalNum, _GoalTargetName.Text);          
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
            targetRegions_dictionary.Remove(goalNum);
            targetRegions_dictionary.Add(goalNum, _targetRegion.Text);
            stepText_dictionnary.Remove(goalNum);
            stepText_dictionnary.Add(goalNum, _StepText.Text);


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

            try
            {
                stepCount = GoalSteps.Count;
                goalType = String.Join("|", Array.ConvertAll(GoalSteps.ToArray(), s => ((int)s).ToString()));
                allowedClasses = String.Join("|", listClasses.SelectedItems.Cast<object>().Select(i => i.ToString()));
                allowedRaces = String.Join("|", listRaces.SelectedItems.Cast<string>().Select(i =>
                {
                    return i == "All" ? string.Empty : ((byte)(ERace)Enum.Parse(typeof(ERace), i)).ToString();
                }));
                questGoals = String.Join("|", Array.ConvertAll(goaltext_dictionary.ToArray(), i => string.Format("{0};{1}", i.Value, i.Key.ToString())));
                goalTargetName = String.Join("|", Array.ConvertAll(goaltargetname_dictionary.ToArray(), i => string.Format("{0};{1}", i.Value, targetRegions_dictionary[i.Key])));
                goalRepeatNo = String.Join("|", Array.ConvertAll(goalrepeatno_dictionary.Values.ToArray(), i => i.ToString()));
                goalStepNo = String.Join("|", Array.ConvertAll(goalstepno_dictionary.Values.ToArray(), i => i.ToString()));
                goalTargetText = String.Join("|", Array.ConvertAll(goaltargettext_dictionary.Values.ToArray(), i => i.ToString()));
                collectItemTemplate = String.Join("|", Array.ConvertAll(colitem_dictionary.Values.ToArray(), i => i.ToString()));
                stepItemTemplate = String.Join("|", Array.ConvertAll(stepItemTemplate_dictionnary.Values.ToArray(), i => i.ToString()));
                stepTextTemplate = String.Join("|", Array.ConvertAll(stepText_dictionnary.Values.ToArray(), i => i.ToString()));
                goalAdvanceText = String.Join("|", Array.ConvertAll(goalAdvanceText_dictionnary.Values.ToArray(), i => i.ToString()));              
                xloc = String.Join("|", Array.ConvertAll(xloc_dictionary.Values.ToArray(), i => i.ToString()));
                yloc = String.Join("|", Array.ConvertAll(yloc_dictionary.Values.ToArray(), i => i.ToString()));
                zoneId = String.Join("|", Array.ConvertAll(zoneid_dictionary.Values.ToArray(), i => i.ToString()));
                finishNPC = _FinishNPC.Text;

                if (opt_dictionary.Count > 0) // if option rewards has at least 1 entry, we will append the optional choices digit to the string
                {
                    optionalRewardItemTemplates = optChoices.ToString() + String.Join("|", Array.ConvertAll(opt_dictionary.Values.ToArray(), i => i.ToString()));
                }
                else // no entries? blank string
                {
                    optionalRewardItemTemplates = "";
                }
                finalRewardItemTemplates = String.Join("|", Array.ConvertAll(fin_dictionary.Values.ToArray(), i => i.ToString()));

                allowedRaces = allowedRaces?.Replace("All", string.Empty);

                StringBuilder gtype = new StringBuilder(goalType);
                gtype.Replace("Search", "2");
                gtype.Replace("Kill", "3");
                gtype.Replace("InteractFinish", "5");
                gtype.Replace("InteractWhisper", "6");
                gtype.Replace("InteractDeliver", "7");
                gtype.Replace("Interact", "4");
                gtype.Replace("Collect", "10");
                gtype.Replace("Unknown", "255");
                goalType = gtype.ToString();

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
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message, "Error converting data. Quest will not be completely saved!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

             DBDQRewardQTemplate q = new DBDQRewardQTemplate();
            q.ID = !string.IsNullOrEmpty(_ID.Text) ? int.Parse(_ID.Text) : (int?)null;
            q.QuestName = string.IsNullOrWhiteSpace(_QuestName.Text) ? "new quest" : _QuestName.Text;
            q.StartNPC = string.IsNullOrWhiteSpace(_StartNPC.Text) ? "undefined" : _StartNPC.Text;
            q.StartRegionID = ushort.Parse(string.IsNullOrWhiteSpace(_StartRegionID.Text) ? "0" : _StartRegionID.Text);
            q.StoryText = string.IsNullOrWhiteSpace(_StoryText.Text) ? "" : _StoryText.Text;
            q.Summary = _Summary.Text;
            q.AcceptText = _AcceptText.Text;
            q.QuestGoals = questGoals;
            q.GoalType = goalType;
            q.GoalRepeatNo = goalRepeatNo;
            q.GoalTargetName = goalTargetName;
            q.GoalTargetText = goalTargetText;
            q.AdvanceText = goalAdvanceText;
            q.StepCount = stepCount;
            q.FinishNPC = finishNPC;           
            q.CollectItemTemplate = collectItemTemplate;
            q.StepItemTemplates = stepItemTemplate;
            q.StepText = stepTextTemplate;
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
            q.FinishText = _FinishText.Text;
            q.QuestDependency = _QuestDependency.Text; //might need to serialize....if quest has multiple dependencies
            q.AllowedClasses = allowedClasses;
            q.AllowedRaces = allowedRaces;
            q.ClassType = _ClassType.Text;
            q.XOffset = xloc;
            q.YOffset = yloc;
            q.ZoneID = zoneId;
            q.Name = _QuestName.Text;
            q.IsRenaissance = _IsRenaissance.Checked;
            q.Reputation = reputation ?? string.Empty;

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


            return "?action=UPDATE&table=dataquestrewardquest&where=" +
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

            return "?action=INSERT&table=dataquestrewardquest&fields=" +
                   HttpUtility.UrlEncode(string.Join(",", fields.Select(f => f.Name)), Encoding.UTF8) +
                   "&values=" + HttpUtility.UrlEncode(string.Join(",", formattedFields), Encoding.UTF8);
        }

        private dynamic GenerateDeleteQuery(string id)
        {
            return "?action=DELETE&table=dataquestrewardquest&where=" +
                                        HttpUtility.UrlEncode("ID = " + Server.EscapeSql(id), Encoding.UTF8);         
        }



        // Ensure the InteractFinish goaltype is the last goal set in the quest.
        private bool IsLastStepCorrect()
        {
            var last = GoalSteps.LastOrDefault();

            if (last == GoalType.Collect || last == GoalType.DeliverFinish) 
            {
                return true;
            }
            else if (last == GoalType.InteractFinish)
            {
                if (string.IsNullOrEmpty(_FinishNPC.Text))
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
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
            _quest = null;
            LoadedQuest = false;
            opt_dictionary.Clear();
            fin_dictionary.Clear();
            colitem_dictionary.Clear();
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
            _QuestName.Text = null;
            _StartNPC.Text = null;
            _StartRegionID.Text = null;
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
            _FinishNPC.Text = null;
            _ClassType.Text = null;
            _QuestDependency.Text = null;
            _StoryText.Text = null;
            _Summary.Text = null;
            _AcceptText.Text = null;
            _FinishText.Text = null;
            listRaces.SelectedIndex = 0;
            listClasses.SelectedIndex = 0;
            Reputation.Text = string.Empty;
            this._quest = new DBDQRewardQTemplate();
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

        private void StartRegionID_KeyPress(object sender, KeyPressEventArgs e)
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

        private void GoalRepeatNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_GoalType.SelectedIndex != 1 && _GoalType.SelectedIndex != 6 && _GoalType.SelectedIndex != 8) // can only change this number from 1 on a kill / collect
            {  
                e.Handled = true;

                if (_GoalRepeatNo.Text != "1")
                {
                    _GoalRepeatNo.Text = "1";
                }
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

            //mobsearch.SelectNpcClicked += (o, args) => { LoadStartNPC(((Mob)o).Name, ((Mob)o).Region); };

            //mobsearch.ShowDialog(this);
        }

        private void LoadStartNPC(string mobName, ushort mobRegion)
        {
            if (string.IsNullOrWhiteSpace(mobName))
            {
                return;
            }

            _StartNPC.Text = mobName;
            _StartRegionID.Text = mobRegion.ToString();
        }

        private void _GoalType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse<GoalType>((string)_GoalType.SelectedItem, out GoalType selected);
            if (selected != GoalType.Kill ) // kill goaltype
            {
                _GoalRepeatNo.Text = "1"; // all other goaltypes have a repeat value of 1
            }  
            else
            {
                GoalStepNo.Text = GoalNumber.Text;
            }

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

            if (!GoalSteps.Any(s => s == GoalType.Collect))
            {
                colitem_dictionary.Clear();
            }

            if (!GoalSteps.Any(s => s == GoalType.InteractWhisper))
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

            //    _QuestDependency.Text = item.QuestName;
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

            if (_GoalType.Text == "" || _GoalRepeatNo.Text == "" || _GoalTargetName.Text == "" || GoalStepNo.Text == "" || _QuestGoals.Text == "")
            {
                MessageBox.Show("You cannot proceed until required fields are met!\n" +
                    "GoalType, Target Name, Step Number, Journal Text are all manditory fields!");
                return;
            }

            _suppressGoal.Enabled = true;        
         

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


            if (GoalSteps[goalNumber - 1] == GoalType.Collect)
            {
                //CollectItem.Text
                if (!colitem_dictionary.ContainsKey(goalNumber))
                {
                    colitem_dictionary.Add(goalNumber, _GoalOptional.Text);
                }
                else
                {
                    colitem_dictionary.Remove(goalNumber);
                    colitem_dictionary.Add(goalNumber, _GoalOptional.Text);
                }       
            }
            else if (GoalSteps[goalNumber - 1] == GoalType.InteractWhisper)
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
            _GoalOptional.Text = "";

            if (GoalSteps[goalNumber - 1] == GoalType.DeliverFinish || GoalSteps[goalNumber - 1] == GoalType.InteractDeliver || GoalSteps[goalNumber - 1] == GoalType.InteractWhisper)
            {
                if (!stepItemTemplate_dictionnary.ContainsKey(goalNumber))
                {
                    stepItemTemplate_dictionnary.Add(goalNumber, _StepItemTemplate.Text);
                }
                else
                {
                    stepItemTemplate_dictionnary.Remove(goalNumber);
                    stepItemTemplate_dictionnary.Add(goalNumber, _StepItemTemplate.Text);
                }
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

            if (!stepText_dictionnary.ContainsKey(goalNumber))
            {
                stepText_dictionnary.Add(goalNumber, _StepText.Text);
            }
            else
            {
                stepText_dictionnary.Remove(goalNumber);
                stepText_dictionnary.Add(goalNumber, _StepText.Text);
            }
            _StepText.Text = "";

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

            if (stepText_dictionnary.ContainsKey(goalNext))
            {
                stepText_dictionnary.TryGetValue(goalNext, out goalvalue);
                _StepText.Text = goalvalue;
            }
            else _StepText.Text = "";


            if (colitem_dictionary.ContainsKey(goalNext))
            {
                colitem_dictionary.TryGetValue(goalNext, out goalvalue);
                _GoalOptional.Text = goalvalue;
            }

            if (goalAdvanceText_dictionnary.ContainsKey(goalNext))
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


            if (GoalSteps.Count >= goalNext && goaltype_dictionary.Count >= goalNext)
            {
                _GoalType.SelectedIndex = goaltype_dictionary[(int)GoalSteps[goalNext - 1]];
            }
            else
            {
                GoalSteps.Add(GoalType.Search);
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
            if (_GoalRepeatNo.Text == "" || _GoalType.Text == "")
            {            
                goaltext_dictionary.Remove(goalNum);
                goaltargetname_dictionary.Remove(goalNum);
                colitem_dictionary.Remove(goalNum);
                goalstepno_dictionary.Remove(goalNum);
                goalrepeatno_dictionary.Remove(goalNum);
                xloc_dictionary.Remove(goalNum);
                yloc_dictionary.Remove(goalNum);
                zoneid_dictionary.Remove(goalNum);
                goaltargettext_dictionary.Remove(goalNum);
                goalAdvanceText_dictionnary.Remove(goalNum);
            }
            else
            //Needed to commit data to m_dictionary when the back button is clicked
            {
                bool shouldSave = e is GoalEventArgs goalArgs ? goalArgs.ShouldSavePreviousStep : true;

                if (shouldSave)
                {
                    goaltext_dictionary.Remove(goalNum);
                    goaltext_dictionary.Add(goalNum, _QuestGoals.Text);
                    goaltargetname_dictionary.Remove(goalNum);
                    goaltargetname_dictionary.Add(goalNum, _GoalTargetName.Text);

                    if (GoalSteps[goalNum - 1] == GoalType.InteractWhisper)
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
                    else if (GoalSteps[goalNum - 1] == GoalType.Collect)
                    {
                        if (colitem_dictionary.Count == 0)
                        {
                            for (int i = 1; i < goalNum; i++)
                            {
                                colitem_dictionary.Add(i, string.Empty);
                            }
                        }

                        colitem_dictionary.Remove(goalNum);
                        colitem_dictionary.Add(goalNum, _GoalOptional.Text);
                    }

                    if (GoalSteps[goalNum - 1] == GoalType.DeliverFinish || GoalSteps[goalNum - 1] == GoalType.InteractDeliver || GoalSteps[goalNum - 1] == GoalType.InteractWhisper)
                    {
                        if (stepItemTemplate_dictionnary.Count == 0)
                        {
                            for (int i = 1; i < goalNum; i++)
                            {
                                stepItemTemplate_dictionnary.Add(i, string.Empty);
                            }
                        }

                        stepItemTemplate_dictionnary.Remove(goalNum);
                        stepItemTemplate_dictionnary.Add(goalNum, _StepItemTemplate.Text);
                    }

                    stepText_dictionnary.Remove(goalNum);
                    stepText_dictionnary.Add(goalNum, _StepText.Text);


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
                    targetRegions_dictionary.Remove(goalNum);
                    targetRegions_dictionary.Add(goalNum, _targetRegion.Text);
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

            if (colitem_dictionary.ContainsKey(goalNum))
            {
                colitem_dictionary.TryGetValue(goalNum, out goalvalue);
                _GoalOptional.Text = goalvalue;
            }

            if (stepItemTemplate_dictionnary.ContainsKey(goalNum))
            {
                stepItemTemplate_dictionnary.TryGetValue(goalNum, out goalvalue);
                _StepItemTemplate.Text = goalvalue;
            }
            else
                _StepItemTemplate.Text = "";


            if (goalAdvanceText_dictionnary.ContainsKey(goalNum))
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

            if (stepText_dictionnary.ContainsKey(goalNum))
            {
                stepText_dictionnary.TryGetValue(goalNum, out goalvalue);
                _StepText.Text = goalvalue;
            }
            else _StepText.Text = "";
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
            toolTip1.Show("The step number when this quest goal is given. Multiple goals can have the same step number so they are given to the player at the same time.\n" +
                "The 'InteractFinish' goaltype must be the last goal and the last step number of EVERY quest for it to work correctly.", labelStepNo, 15000);
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
                "Search - /search an area in game to complete. \n" +
                "Kill - kill the goal target to complete. Can increase the repeatNo for this type. \n" +
                "Interact - Interact with the goal target to complete. \n" +
                "InteractDeliver - Used to deliver the 'collect item'. Interact with the goal target to complete. \n" +
                "InteractWhisper - Player must whisper the advancetext to the target to complete. \n" +
                "interactFinish - Interact with the goal target to finish the quest. This required for the last step of EVERY quest!", labelGoalType, 20000);

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

        private void classTypeLabel_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("Class name of custom script thats called for this quest.", classTypeLabel, 10000);
        }

        private void classTypeLabel_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(classTypeLabel);
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

        private void finishTextLabel_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The text displayed to the player upon completing the quest and being offered the rewards.", finishTextLabel, 10000);
        }

        private void finishTextLabel_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(finishTextLabel);
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
                if (GoalSteps[step - 1] == GoalType.Collect)
                {
                    colitem_dictionary.Remove(step);
                }
                else if (GoalSteps[step - 1] == GoalType.InteractWhisper)
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
                stepItemTemplate_dictionnary.Remove(step);
                stepText_dictionnary.Remove(step);
                


              GoalSteps.RemoveAt(step - 1);
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
