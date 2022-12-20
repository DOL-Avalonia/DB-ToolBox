using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using AmteCreator.Internal;
using DOL.Bonus;
using System.Collections.Generic;
using System.Linq;

namespace AmteCreator
{
    public sealed partial class BonusTab : UserControl
    {
        private readonly Color _goodColor;
        private ItemTemplate _item = null;
        private bool _loadingItem = false;
        private readonly DataSet[] _bonusData = new DataSet[11];

        public DataSet baseXMLData;

        public ItemTemplate currentItem
        {
            get { return _item; }
            set
            {
                if (value == null) return;
                _loadingItem = true;
                _item = value;
                itemTemplateBindingSource.DataSource = value;
                item_effectValue1.Text = value.bonus1.ToString();
                item_effectValue2.Text = value.bonus2.ToString();
                item_effectValue3.Text = value.bonus3.ToString();
                item_effectValue4.Text = value.bonus4.ToString();
                item_effectValue5.Text = value.bonus5.ToString();
                item_effectValue6.Text = value.bonus6.ToString();
                item_effectValue7.Text = value.bonus7.ToString();
                item_effectValue8.Text = value.bonus8.ToString();
                item_effectValue9.Text = value.bonus9.ToString();
                item_effectValue10.Text = value.bonus10.ToString();
                for (var i = 1; i <= 11; i++)
                {
                    var bonusType = 0;
                    switch (i)
                    {
                        case 1: bonusType = _item.bonus1Type; break;
                        case 2: bonusType = _item.bonus2Type; break;
                        case 3: bonusType = _item.bonus3Type; break;
                        case 4: bonusType = _item.bonus4Type; break;
                        case 5: bonusType = _item.bonus5Type; break;
                        case 6: bonusType = _item.bonus6Type; break;
                        case 7: bonusType = _item.bonus7Type; break;
                        case 8: bonusType = _item.bonus8Type; break;
                        case 9: bonusType = _item.bonus9Type; break;
                        case 10: bonusType = _item.bonus10Type; break;
                        case 11: bonusType = _item.extrabonusType; break;
                    }

                    var cat = baseXMLData.Tables["bonus"].Rows.Find(bonusType);

                    var control = (ComboBox)Controls.Find("effectCat" + i, true)[0];
                    control.SelectedValue = cat["category"];

                    var box = (ComboBox)Controls.Find("item_effect" + i, true)[0];
                    box.SelectedValue = cat["id"];
                }
                this.ResetBonus();
                this.LoadBonusConditions(value.bonusConditions);
                _loadingItem = false;
                _ItemBonusChanged();
            }
        }

        private void ResetBonus()
        {
            this.Bonus1ChampionLevel.Text = "0";
            this.Bonus1MlLevel.Text = "0";
            this.bonus1Renaissance.Checked = false;

            this.Bonus2ChampionLevel.Text = "0";
            this.Bonus2MlLevel.Text = "0";
            this.bonus2Renaissance.Checked = false;

            this.Bonus3ChampionLevel.Text = "0";
            this.Bonus3MlLevel.Text = "0";
            this.bonus3Renaissance.Checked = false;

            this.Bonus4ChampionLevel.Text = "0";
            this.Bonus4MlLevel.Text = "0";
            this.bonus4Renaissance.Checked = false;

            this.Bonus5ChampionLevel.Text = "0";
            this.Bonus5MlLevel.Text = "0";
            this.bonus5Renaissance.Checked = false;

            this.Bonus6ChampionLevel.Text = "0";
            this.Bonus6MlLevel.Text = "0";
            this.bonus6Renaissance.Checked = false;

            this.Bonus7ChampionLevel.Text = "0";
            this.Bonus7MlLevel.Text = "0";
            this.bonus7Renaissance.Checked = false;

            this.Bonus8ChampionLevel.Text = "0";
            this.Bonus8MlLevel.Text = "0";
            this.bonus8Renaissance.Checked = false;

            this.Bonus9ChampionLevel.Text = "0";
            this.Bonus9MlLevel.Text = "0";
            this.bonus9Renaissance.Checked = false;

            this.Bonus10ChampionLevel.Text = "0";
            this.Bonus10MlLevel.Text = "0";
            this.bonus10Renaissance.Checked = false;

            this.BonusExtraChampionLevel.Text = "0";
            this.BonusExtraMlLevel.Text = "0";
            this.bonusExtraRenaissance.Checked = false;

            this.bonusProcChampionLevel.Text = "0";
            this.bonusProcMlLevel.Text = "0";
            this.bonusProcRenaissanceReq.Checked = false;

            this.bonusProc1ChampionLevel.Text = "0";
            this.bonusProc1MlLevel.Text = "0";
            this.bonusProc1RenaissanceReq.Checked = false;
        }

        public BonusTab()
        {
            InitializeComponent();
            _goodColor = ForeColor;
            _ItemBonusChanged();
        }


        private void LoadBonusConditions(string rawBonusConditions)
        {
            var conditions = BonusCondition.LoadFromString(rawBonusConditions);  

            if (conditions != null)
            {
                foreach (var condition in conditions)
                {
                    switch (condition.BonusName)
                    {
                        case nameof(DOL.Database.ItemTemplate.Bonus1):
                            this.Bonus1ChampionLevel.Text = condition.ChampionLevel.ToString();
                            this.Bonus1MlLevel.Text = condition.MlLevel.ToString();
                            this.bonus1Renaissance.Checked = condition.IsRenaissanceRequired;
                            break;

                        case nameof(DOL.Database.ItemTemplate.Bonus2):
                            this.Bonus2ChampionLevel.Text = condition.ChampionLevel.ToString();
                            this.Bonus2MlLevel.Text = condition.MlLevel.ToString();
                            this.bonus2Renaissance.Checked = condition.IsRenaissanceRequired;
                            break;

                        case nameof(DOL.Database.ItemTemplate.Bonus3):
                            this.Bonus3ChampionLevel.Text = condition.ChampionLevel.ToString();
                            this.Bonus3MlLevel.Text = condition.MlLevel.ToString();
                            this.bonus3Renaissance.Checked = condition.IsRenaissanceRequired;
                            break;

                        case nameof(DOL.Database.ItemTemplate.Bonus4):
                            this.Bonus4ChampionLevel.Text = condition.ChampionLevel.ToString();
                            this.Bonus4MlLevel.Text = condition.MlLevel.ToString();
                            this.bonus4Renaissance.Checked = condition.IsRenaissanceRequired;
                            break;

                        case nameof(DOL.Database.ItemTemplate.Bonus5):
                            this.Bonus5ChampionLevel.Text = condition.ChampionLevel.ToString();
                            this.Bonus5MlLevel.Text = condition.MlLevel.ToString();
                            this.bonus5Renaissance.Checked = condition.IsRenaissanceRequired;
                            break;

                        case nameof(DOL.Database.ItemTemplate.Bonus6):
                            this.Bonus6ChampionLevel.Text = condition.ChampionLevel.ToString();
                            this.Bonus6MlLevel.Text = condition.MlLevel.ToString();
                            this.bonus6Renaissance.Checked = condition.IsRenaissanceRequired;
                            break;

                        case nameof(DOL.Database.ItemTemplate.Bonus7):
                            this.Bonus7ChampionLevel.Text = condition.ChampionLevel.ToString();
                            this.Bonus7MlLevel.Text = condition.MlLevel.ToString();
                            this.bonus7Renaissance.Checked = condition.IsRenaissanceRequired;
                            break;

                        case nameof(DOL.Database.ItemTemplate.Bonus8):
                            this.Bonus8ChampionLevel.Text = condition.ChampionLevel.ToString();
                            this.Bonus8MlLevel.Text = condition.MlLevel.ToString();
                            this.bonus8Renaissance.Checked = condition.IsRenaissanceRequired;
                            break;

                        case nameof(DOL.Database.ItemTemplate.Bonus9):
                            this.Bonus9ChampionLevel.Text = condition.ChampionLevel.ToString();
                            this.Bonus9MlLevel.Text = condition.MlLevel.ToString();
                            this.bonus9Renaissance.Checked = condition.IsRenaissanceRequired;
                            break;

                        case nameof(DOL.Database.ItemTemplate.Bonus10):
                            this.Bonus10ChampionLevel.Text = condition.ChampionLevel.ToString();
                            this.Bonus10MlLevel.Text = condition.MlLevel.ToString();
                            this.bonus10Renaissance.Checked = condition.IsRenaissanceRequired;
                            break;

                        case nameof(DOL.Database.ItemTemplate.ExtraBonus):
                            this.BonusExtraChampionLevel.Text = condition.ChampionLevel.ToString();
                            this.BonusExtraMlLevel.Text = condition.MlLevel.ToString();
                            this.bonusExtraRenaissance.Checked = condition.IsRenaissanceRequired;
                            break;

                        case nameof(DOL.Database.ItemTemplate.ProcSpellID):
                            this.bonusProcChampionLevel.Text = condition.ChampionLevel.ToString();
                            this.bonusProcMlLevel.Text = condition.MlLevel.ToString();
                            this.bonusProcRenaissanceReq.Checked = condition.IsRenaissanceRequired;
                            break;

                        case nameof(DOL.Database.ItemTemplate.ProcSpellID1):
                            this.bonusProc1ChampionLevel.Text = condition.ChampionLevel.ToString();
                            this.bonusProc1MlLevel.Text = condition.MlLevel.ToString();
                            this.bonusProc1RenaissanceReq.Checked = condition.IsRenaissanceRequired;
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        private void _ItemStatChanged(object sender, EventArgs e)
        {
            if (_loadingItem || currentItem == null) return;
            var val = currentItem.TotalImbuePoint * progressBar1.Maximum / (currentItem.MaxImbuePoint == 0 ? 1 : currentItem.MaxImbuePoint);
            progressBar1.Value = val < progressBar1.Minimum ? progressBar1.Minimum : val > progressBar1.Maximum ? progressBar1.Maximum : val;
            statPowerTotal.Text = currentItem.TotalImbuePoint + " / " + currentItem.MaxImbuePoint;
        }

        private void _ItemBonusChanged(object sender = null, EventArgs e = null)
        {
            if (_loadingItem || currentItem == null) return;
            try
            {
                currentItem.bonus1Type = Convert.ToInt32(item_effect1.SelectedValue);
                currentItem.bonus2Type = Convert.ToInt32(item_effect2.SelectedValue);
                currentItem.bonus3Type = Convert.ToInt32(item_effect3.SelectedValue);
                currentItem.bonus4Type = Convert.ToInt32(item_effect4.SelectedValue);
                currentItem.bonus5Type = Convert.ToInt32(item_effect5.SelectedValue);
                currentItem.bonus6Type = Convert.ToInt32(item_effect6.SelectedValue);
                currentItem.bonus7Type = Convert.ToInt32(item_effect7.SelectedValue);
                currentItem.bonus8Type = Convert.ToInt32(item_effect8.SelectedValue);
                currentItem.bonus9Type = Convert.ToInt32(item_effect9.SelectedValue);
                currentItem.bonus10Type = Convert.ToInt32(item_effect10.SelectedValue);
                currentItem.extrabonusType = Convert.ToInt32(item_effect11.SelectedValue);

                statPower1.Text = ImbuePointCalc.GetGemImbuePoints(currentItem.bonus1Type, currentItem.bonus1).ToString();
                statPower2.Text = ImbuePointCalc.GetGemImbuePoints(currentItem.bonus2Type, currentItem.bonus2).ToString();
                statPower3.Text = ImbuePointCalc.GetGemImbuePoints(currentItem.bonus3Type, currentItem.bonus3).ToString();
                statPower4.Text = ImbuePointCalc.GetGemImbuePoints(currentItem.bonus4Type, currentItem.bonus4).ToString();
                statPower5.Text = ImbuePointCalc.GetGemImbuePoints(currentItem.bonus5Type, currentItem.bonus5).ToString();
                statPower6.Text = ImbuePointCalc.GetGemImbuePoints(currentItem.bonus6Type, currentItem.bonus6).ToString();
                statPower7.Text = ImbuePointCalc.GetGemImbuePoints(currentItem.bonus7Type, currentItem.bonus7).ToString();
                statPower8.Text = ImbuePointCalc.GetGemImbuePoints(currentItem.bonus8Type, currentItem.bonus8).ToString();
                statPower9.Text = ImbuePointCalc.GetGemImbuePoints(currentItem.bonus9Type, currentItem.bonus9).ToString();
                statPower10.Text = ImbuePointCalc.GetGemImbuePoints(currentItem.bonus10Type, currentItem.bonus10).ToString();
                statPower11.Text = ImbuePointCalc.GetGemImbuePoints(currentItem.extrabonusType, currentItem.extrabonus).ToString();
                ForeColor = _goodColor;
            }
            catch (FormatException)
            {
                ForeColor = Color.Red;
            }
        }

        public void LoadComboBoxes()
        {
            baseXMLData.Tables["bonus"].PrimaryKey = new []{baseXMLData.Tables["bonus"].Columns["id"]};
            for (var i = 0; i < 11; i++)
            {
                _bonusData.SetValue(baseXMLData.Copy(), i);
                foreach (DataRow row in _bonusData[i].Tables["bonus"].Rows)
                {
                    if (_bonusData[i].Tables.Contains("stats_" + row["category"]) == false)
                    {
                        _bonusData[i].Tables.Add("stats_" + row["category"]);
                        _bonusData[i].Tables["stats_" + row["category"]].Columns.Add("id");
                        _bonusData[i].Tables["stats_" + row["category"]].Columns.Add("name");
                    }

                    var values = new Object[2];
                    values.SetValue(row["id"], 0);
                    values.SetValue(row["name"], 1);
                    _bonusData[i].Tables["stats_" + row["category"]].Rows.Add(values);
                }

                var control = (ComboBox)Controls.Find("effectCat" + (i + 1), true)[0];
                control.DataSource = _bonusData[i].Tables["bonus_category"];
                control.ValueMember = "name";
                control.DisplayMember = "name";

                control = (ComboBox)Controls.Find("item_effect" + (i + 1), true)[0];
                control.DisplayMember = "name";
                control.ValueMember = "id";
            }
        }

        private void _StatsCategorySelectChanged(object sender, EventArgs e)
        {
            //Es wurde eine Stats-Kategorie ausgewählt
            var element = (ComboBox)sender;
            var selectedValue = Convert.ToString(element.SelectedValue);

            //ID des Stats
            var statId = Convert.ToInt32(element.Name.Substring(9));
            var statValues = (ComboBox)Controls.Find("item_effect" + statId, true)[0];
            statValues.DataSource = _bonusData[statId - 1].Tables["stats_" + selectedValue];
            statValues.DisplayMember = "name";
            statValues.ValueMember = "id";
        }

        private void SaveBonusConditions()
        {
            var conditions = new BonusCondition[]
            {
                new BonusCondition()
                {
                    BonusName = nameof(DOL.Database.ItemTemplate.Bonus1),
                    ChampionLevel = int.TryParse(this.Bonus1ChampionLevel.Text, out int b1Champ) ? b1Champ : 0,
                    IsRenaissanceRequired = this.bonus1Renaissance.Checked,
                    MlLevel = int.TryParse(this.Bonus1MlLevel.Text, out int b1Ml) ? b1Ml : 0
                },
                new BonusCondition()
                {
                    BonusName = nameof(DOL.Database.ItemTemplate.Bonus2),
                    ChampionLevel = int.TryParse(this.Bonus2ChampionLevel.Text, out int b2Champ) ? b2Champ : 0,
                    IsRenaissanceRequired = this.bonus2Renaissance.Checked,
                    MlLevel = int.TryParse(this.Bonus2MlLevel.Text, out int b2Ml) ? b2Ml : 0
                },
                new BonusCondition()
                {
                    BonusName = nameof(DOL.Database.ItemTemplate.Bonus3),
                    ChampionLevel = int.TryParse(this.Bonus3ChampionLevel.Text, out int b3Champ) ? b3Champ : 0,
                    IsRenaissanceRequired = this.bonus3Renaissance.Checked,
                    MlLevel = int.TryParse(this.Bonus3MlLevel.Text, out int b3Ml) ? b3Ml : 0
                },
                new BonusCondition()
                {
                    BonusName = nameof(DOL.Database.ItemTemplate.Bonus4),
                    ChampionLevel = int.TryParse(this.Bonus4ChampionLevel.Text, out int b4Champ) ? b4Champ : 0,
                    IsRenaissanceRequired = this.bonus4Renaissance.Checked,
                    MlLevel = int.TryParse(this.Bonus4MlLevel.Text, out int b4Ml) ? b4Ml : 0
                },
                new BonusCondition()
                {
                    BonusName = nameof(DOL.Database.ItemTemplate.Bonus5),
                    ChampionLevel = int.TryParse(this.Bonus5ChampionLevel.Text, out int b5Champ) ? b5Champ : 0,
                    IsRenaissanceRequired = this.bonus5Renaissance.Checked,
                    MlLevel = int.TryParse(this.Bonus5MlLevel.Text, out int b5Ml) ? b5Ml : 0
                },
                new BonusCondition()
                {
                    BonusName = nameof(DOL.Database.ItemTemplate.Bonus6),
                    ChampionLevel = int.TryParse(this.Bonus6ChampionLevel.Text, out int b6Champ) ? b6Champ : 0,
                    IsRenaissanceRequired = this.bonus6Renaissance.Checked,
                    MlLevel = int.TryParse(this.Bonus5MlLevel.Text, out int b6Ml) ? b6Ml : 0
                },
                new BonusCondition()
                {
                    BonusName = nameof(DOL.Database.ItemTemplate.Bonus7),
                    ChampionLevel = int.TryParse(this.Bonus7ChampionLevel.Text, out int b7Champ) ? b7Champ : 0,
                    IsRenaissanceRequired = this.bonus7Renaissance.Checked,
                    MlLevel = int.TryParse(this.Bonus7MlLevel.Text, out int b7Ml) ? b7Ml : 0
                },
                new BonusCondition()
                {
                    BonusName = nameof(DOL.Database.ItemTemplate.Bonus8),
                    ChampionLevel = int.TryParse(this.Bonus8ChampionLevel.Text, out int b8Champ) ? b8Champ : 0,
                    IsRenaissanceRequired = this.bonus8Renaissance.Checked,
                    MlLevel = int.TryParse(this.Bonus8MlLevel.Text, out int b8Ml) ? b8Ml : 0
                },
                new BonusCondition()
                {
                    BonusName = nameof(DOL.Database.ItemTemplate.Bonus9),
                    ChampionLevel = int.TryParse(this.Bonus9ChampionLevel.Text, out int b9Champ) ? b9Champ : 0,
                    IsRenaissanceRequired = this.bonus9Renaissance.Checked,
                    MlLevel = int.TryParse(this.Bonus9MlLevel.Text, out int b9Ml) ? b9Ml : 0
                },
                new BonusCondition()
                {
                    BonusName = nameof(DOL.Database.ItemTemplate.Bonus10),
                    ChampionLevel = int.TryParse(this.Bonus10ChampionLevel.Text, out int b10Champ) ? b10Champ : 0,
                    IsRenaissanceRequired = this.bonus10Renaissance.Checked,
                    MlLevel = int.TryParse(this.Bonus10MlLevel.Text, out int b10Ml) ? b10Ml : 0
                },
                new BonusCondition()
                {
                    BonusName = nameof(DOL.Database.ItemTemplate.ExtraBonus),
                    ChampionLevel = int.TryParse(this.BonusExtraChampionLevel.Text, out int bExtraChamp) ? bExtraChamp : 0,
                    IsRenaissanceRequired = this.bonusExtraRenaissance.Checked,
                    MlLevel = int.TryParse(this.BonusExtraMlLevel.Text, out int bExtraMl) ? bExtraMl : 0
                },
                new BonusCondition()
                {
                    BonusName = nameof(DOL.Database.ItemTemplate.ProcSpellID),
                    ChampionLevel = int.TryParse(this.bonusProcChampionLevel.Text, out int bprocChamp) ? bprocChamp : 0,
                    IsRenaissanceRequired = this.bonusProcRenaissanceReq.Checked,
                    MlLevel = int.TryParse(this.bonusProcMlLevel.Text, out int bProcMl) ? bProcMl : 0
                },
                new BonusCondition()
                {
                    BonusName = nameof(DOL.Database.ItemTemplate.ProcSpellID1),
                    ChampionLevel = int.TryParse(this.bonusProc1ChampionLevel.Text, out int bproc1Champ) ? bproc1Champ : 0,
                    IsRenaissanceRequired = this.bonusProc1RenaissanceReq.Checked,
                    MlLevel = int.TryParse(this.bonusProc1MlLevel.Text, out int bProc1Ml) ? bProc1Ml : 0
                }
            };

            this.currentItem.bonusConditions = BonusCondition.SaveToString(conditions);
        }

        private void Bonus1ChampionLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void Bonus2ChampionLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void Bonus1MlLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void Bonus3ChampionLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void Bonus3MlLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }        

        private void Bonus4ChampionLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void Bonus4MlLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void bonus4Renaissance_CheckedChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void bonus1Renaissance_CheckedChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void bonus2Renaissance_CheckedChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void bonus3Renaissance_CheckedChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void Bonus5ChampionLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void Bonus5MlLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void bonus5Renaissance_CheckedChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void Bonus6ChampionLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void Bonus6MlLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void bonus6Renaissance_CheckedChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void Bonus7ChampionLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void Bonus7MlLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void bonus7Renaissance_CheckedChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void Bonus8ChampionLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void Bonus8MlLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void bonus8Renaissance_CheckedChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void Bonus9ChampionLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void Bonus9MlLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void bonus9Renaissance_CheckedChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void Bonus10MlLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void BonusExtraMlLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void Bonus10ChampionLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void BonusExtraChampionLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void bonusExtraRenaissance_CheckedChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void bonus10Renaissance_CheckedChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void bonusProcChampionLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void bonusProc1ChampionLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void bonusProcMlLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void bonusProc1MlLevel_TextChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void bonusProcRenaissanceReq_CheckedChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }

        private void bonusProc1RenaissanceReq_CheckedChanged(object sender, EventArgs e)
        {
            this.SaveBonusConditions();
        }
    }
}
