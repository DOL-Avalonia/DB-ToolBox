using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AmteCreator.Internal
{
    [Serializable]
    public class ItemTemplate
    {
        public bool AlreadyInDB;

        public string id_nb { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public int levelRequirement { get; set; }
        public long price { get; set; }
        public int quality { get; set; }
        public int object_type { get; set; }
        public int item_type { get; set; }
        public string description { get; set; }
        public int bonus { get; set; }

        // dur_con
        public int durability { get; set; }
        public int condition { get; set; }
        public int maxdurability { get; set; }
        public int maxcondition { get; set; }
        public int weight { get; set; }
        public bool isIndestructible { get; set; }
        public bool isNotLosingDur { get; set; }

        // weapon/armor
        public int dps_af { get; set; }
        public int spd_abs { get; set; }
        public int hand { get; set; }
        public int type_damage { get; set; }

        // apparence
        public int color { get; set; }
        public int emblem { get; set; }
        public int effect { get; set; }
        public int model { get; set; }
        public int extension { get; set; }

        // bonuses
        public int bonus1 { get; set; }
        public int bonus2 { get; set; }
        public int bonus3 { get; set; }
        public int bonus4 { get; set; }
        public int bonus5 { get; set; }
        public int bonus6 { get; set; }
        public int bonus7 { get; set; }
        public int bonus8 { get; set; }
        public int bonus9 { get; set; }
        public int bonus10 { get; set; }
        public int extrabonus { get; set; }
        public int bonus1Type { get; set; }
        public int bonus2Type { get; set; }
        public int bonus3Type { get; set; }
        public int bonus4Type { get; set; }
        public int bonus5Type { get; set; }
        public int bonus6Type { get; set; }
        public int bonus7Type { get; set; }
        public int bonus8Type { get; set; }
        public int bonus9Type { get; set; }
        public int bonus10Type { get; set; }
        public int extrabonusType { get; set; }

        public string bonusConditions { get; set; }

        // properties
        public bool isDropable { get; set; }
        public bool isPickable { get; set; }
        public bool isTradable { get; set; }
        public bool canDropAsLoot { get; set; }

        public bool canUseInRvR { get; set; }

        // stack
        public int maxCount { get; set; }
        public int packSize { get; set; }

        // proc & charges
        public int spellID { get; set; }
        public int procSpellID { get; set; }
        public int maxCharges { get; set; }
        public int charges { get; set; }
        public int spellID1 { get; set; }
        public int procSpellID1 { get; set; }
        public int charges1 { get; set; }
        public int maxCharges1 { get; set; }
        public byte procChance { get; set; }
        public int poisonSpellID { get; set; }
        public int poisonMaxCharges { get; set; }
        public int poisonCharges { get; set; }

        public int realm { get; set; }
        public string allowedClasses { get; set; }
        public int canUseEvery { get; set; }
        public int flags { get; set; }
        public int bonusLevel { get; set; }

        public string packageID { get; set; }

        public string classType { get; set; }
        public ItemTemplate()
        {
            id_nb = "blank_item";
            name = "(blank item)";
            level = 0;
            durability = maxdurability = 50000;
            condition = maxcondition = 50000;
            quality = 1;
            dps_af = 0;
            spd_abs = 0;
            hand = 0;
            type_damage = 0;
            object_type = 0;
            item_type = 0;
            color = 0;
            emblem = 0;
            effect = 0;
            weight = 0;
            model = 488; //bag
            extension = 0;
            bonus = 0;
            bonus1 = 0;
            bonus2 = 0;
            bonus3 = 0;
            bonus4 = 0;
            bonus5 = 0;
            bonus6 = 0;
            bonus7 = 0;
            bonus8 = 0;
            bonus9 = 0;
            bonus10 = 0;
            extrabonus = 0;
            bonus1Type = 0;
            bonus2Type = 0;
            bonus3Type = 0;
            bonus4Type = 0;
            bonus5Type = 0;
            bonus6Type = 0;
            bonus7Type = 0;
            bonus8Type = 0;
            bonus9Type = 0;
            bonus10Type = 0;
            extrabonusType = 0;
            isDropable = true;
            isPickable = true;
            isTradable = true;
            canDropAsLoot = false;
            canUseInRvR = false;
            maxCount = 1;
            packSize = 1;
            charges = 0;
            maxCharges = 0;
            spellID = 0;//when no spell link to item
            spellID1 = 0;
            procSpellID = 0;
            procSpellID1 = 0;
            procChance = 0;
            charges1 = 0;
            maxCharges1 = 0;
            poisonCharges = 0;
            poisonMaxCharges = 0;
            poisonSpellID = 0;
            realm = 0;
            allowedClasses = "0";
            flags = 0;
            bonusLevel = 0;
            levelRequirement = 0;
            description = "";
            packageID = "";
            classType = "";
        }


        private static int _Parse(string data)
        {
            return string.IsNullOrEmpty(data) ? 0 : int.Parse(data);
        }

        public ItemTemplate(dynamic template)
        {
            id_nb = template.Id_nb;
            name = template.Name;
            level = _Parse(template.Level);
            durability = _Parse(template.Durability);
            maxdurability = _Parse(template.MaxDurability);
            condition = _Parse(template.Condition);
            maxcondition = _Parse(template.MaxCondition);
            quality = _Parse(template.Quality);
            dps_af = _Parse(template.DPS_AF);
            spd_abs = _Parse(template.SPD_ABS);
            hand = _Parse(template.Hand);
            type_damage = _Parse(template.Type_Damage);
            object_type = _Parse(template.Object_Type);
            item_type = _Parse(template.Item_Type);
            color = _Parse(template.Color);
            emblem = _Parse(template.Emblem);
            effect = _Parse(template.Effect);
            weight = _Parse(template.Weight);
            model = _Parse(template.Model);
            extension = byte.Parse(string.IsNullOrEmpty(template.Extension) ? "0" : template.Extension);
            bonus = _Parse(template.Bonus);
            bonus1 = _Parse(template.Bonus1);
            bonus2 = _Parse(template.Bonus2);
            bonus3 = _Parse(template.Bonus3);
            bonus4 = _Parse(template.Bonus4);
            bonus5 = _Parse(template.Bonus5);
            extrabonus = _Parse(template.ExtraBonus);
            bonus1Type = _Parse(template.Bonus1Type);
            bonus2Type = _Parse(template.Bonus2Type);
            bonus3Type = _Parse(template.Bonus3Type);
            bonus4Type = _Parse(template.Bonus4Type);
            bonus5Type = _Parse(template.Bonus5Type);
            extrabonusType = _Parse(template.ExtraBonusType);
            isPickable = _Parse(template.IsPickable) == 1;
            isDropable = _Parse(template.IsDropable) == 1;
            maxCount = _Parse(template.MaxCount);
            packSize = _Parse(template.PackSize);
            charges = _Parse(template.Charges);
            maxCharges = _Parse(template.MaxCharges);
            spellID = _Parse(template.SpellID);
            procSpellID = _Parse(template.ProcSpellID);
            realm = _Parse(template.Realm);
            isTradable = _Parse(template.IsTradable) == 1;
            bonus6 = _Parse(template.Bonus6);
            bonus7 = _Parse(template.Bonus7);
            bonus8 = _Parse(template.Bonus8);
            bonus9 = _Parse(template.Bonus9);
            bonus10 = _Parse(template.Bonus10);
            bonus6Type = _Parse(template.Bonus6Type);
            bonus7Type = _Parse(template.Bonus7Type);
            bonus8Type = _Parse(template.Bonus8Type);
            bonus9Type = _Parse(template.Bonus9Type);
            bonus10Type = _Parse(template.Bonus10Type);
            charges1 = _Parse(template.Charges1);
            maxCharges1 = _Parse(template.MaxCharges1);
            spellID1 = _Parse(template.SpellID1);
            procSpellID1 = _Parse(template.ProcSpellID1);
            poisonSpellID = _Parse(template.PoisonSpellID);
            poisonMaxCharges = _Parse(template.PoisonMaxCharges);
            poisonCharges = _Parse(template.PoisonCharges);
            canDropAsLoot = _Parse(template.CanDropAsLoot) == 1;
            canUseInRvR = _Parse(template.CanUseInRvR) == 1;
            allowedClasses = template.AllowedClasses;
            canUseEvery = _Parse(template.CanUseEvery);
            packageID = template.PackageID;
            flags = _Parse(template.Flags);
            bonusLevel = _Parse(template.BonusLevel);
            description = template.Description;
            isIndestructible = _Parse(template.IsIndestructible) == 1;
            isNotLosingDur = _Parse(template.IsNotLosingDur) == 1;
            levelRequirement = _Parse(template.LevelRequirement);
            price = long.Parse(String.IsNullOrEmpty(template.Price) ? "0" : template.Price);
            classType = template.ClassType;
            procChance = byte.Parse(String.IsNullOrEmpty(template.ProcChance) ? "0" : template.ProcChance);
            bonusConditions = template.BonusConditions;
        }

        public int TotalImbuePoint
        {
            get
            {
                var total = 0;
                var max = 0;
                var tmp = ImbuePointCalc.GetGemImbuePoints(bonus1Type, bonus1);
                max = tmp > max ? tmp : max;
                total += tmp;
                tmp = ImbuePointCalc.GetGemImbuePoints(bonus2Type, bonus2);
                max = tmp > max ? tmp : max;
                total += tmp;
                tmp = ImbuePointCalc.GetGemImbuePoints(bonus3Type, bonus3);
                max = tmp > max ? tmp : max;
                total += tmp;
                tmp = ImbuePointCalc.GetGemImbuePoints(bonus4Type, bonus4);
                max = tmp > max ? tmp : max;
                total += tmp;
                tmp = ImbuePointCalc.GetGemImbuePoints(bonus5Type, bonus5);
                max = tmp > max ? tmp : max;
                total += tmp;
                tmp = ImbuePointCalc.GetGemImbuePoints(bonus6Type, bonus6);
                max = tmp > max ? tmp : max;
                total += tmp;
                tmp = ImbuePointCalc.GetGemImbuePoints(bonus7Type, bonus7);
                max = tmp > max ? tmp : max;
                total += tmp;
                tmp = ImbuePointCalc.GetGemImbuePoints(bonus8Type, bonus8);
                max = tmp > max ? tmp : max;
                total += tmp;
                tmp = ImbuePointCalc.GetGemImbuePoints(bonus9Type, bonus9);
                max = tmp > max ? tmp : max;
                total += tmp;
                tmp = ImbuePointCalc.GetGemImbuePoints(bonus10Type, bonus10);
                max = tmp > max ? tmp : max;
                total += tmp;
                tmp = ImbuePointCalc.GetGemImbuePoints(extrabonusType, extrabonus);
                max = tmp > max ? tmp : max;
                total += tmp;
                return (total + max) / 2;
            }
        }

        public int MaxImbuePoint
        {
            get
            {
                return ImbuePointCalc.GetItemMaxImbuePoints(this);
            }
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("id_nb: ").AppendLine(id_nb)
                .Append("name: ").AppendLine(name)
                .Append("level: ").AppendLine(level.ToString())
                .Append("levelRequirement: ").AppendLine(levelRequirement.ToString())
                .Append("price: ").AppendLine(price.ToString())
                .Append("quality: ").AppendLine(quality.ToString())
                .Append("object_type: ").AppendLine(object_type.ToString())
                .Append("item_type: ").AppendLine(item_type.ToString())
                .Append("description: ").AppendLine(description)

                // dur_con
                .Append("durability: ").AppendLine(durability.ToString())
                .Append("condition: ").AppendLine(condition.ToString())
                .Append("maxdurability: ").AppendLine(maxdurability.ToString())
                .Append("maxcondition: ").AppendLine(maxcondition.ToString())
                .Append("weight: ").AppendLine(weight.ToString())
                .Append("isIndestructible: ").AppendLine(isIndestructible.ToString())
                .Append("isNotLosingDur: ").AppendLine(isNotLosingDur.ToString())

                // weapon/armor
                .Append("dps_af: ").AppendLine(dps_af.ToString())
                .Append("spd_abs: ").AppendLine(spd_abs.ToString())
                .Append("hand: ").AppendLine(hand.ToString())
                .Append("type_damage: ").AppendLine(type_damage.ToString())

                // apparence
                .Append("color: ").AppendLine(color.ToString())
                .Append("emblem: ").AppendLine(emblem.ToString())
                .Append("effect: ").AppendLine(effect.ToString())
                .Append("model: ").AppendLine(model.ToString())
                .Append("extension: ").AppendLine(extension.ToString())

                // bonuses
                .Append("bonus: ").AppendLine(bonus.ToString())
                .Append("bonus1: ").AppendLine(bonus1.ToString())
                .Append("bonus2: ").AppendLine(bonus2.ToString())
                .Append("bonus3: ").AppendLine(bonus3.ToString())
                .Append("bonus4: ").AppendLine(bonus4.ToString())
                .Append("bonus5: ").AppendLine(bonus5.ToString())
                .Append("bonus6: ").AppendLine(bonus6.ToString())
                .Append("bonus7: ").AppendLine(bonus7.ToString())
                .Append("bonus8: ").AppendLine(bonus8.ToString())
                .Append("bonus9: ").AppendLine(bonus9.ToString())
                .Append("bonus10: ").AppendLine(bonus10.ToString())
                .Append("extrabonus: ").AppendLine(extrabonus.ToString())
                .Append("bonus1Type: ").AppendLine(bonus1Type.ToString())
                .Append("bonus2Type: ").AppendLine(bonus2Type.ToString())
                .Append("bonus3Type: ").AppendLine(bonus3Type.ToString())
                .Append("bonus4Type: ").AppendLine(bonus4Type.ToString())
                .Append("bonus5Type: ").AppendLine(bonus5Type.ToString())
                .Append("bonus6Type: ").AppendLine(bonus6Type.ToString())
                .Append("bonus7Type: ").AppendLine(bonus7Type.ToString())
                .Append("bonus8Type: ").AppendLine(bonus8Type.ToString())
                .Append("bonus9Type: ").AppendLine(bonus9Type.ToString())
                .Append("bonus10Type: ").AppendLine(bonus10Type.ToString())
                .Append("extrabonusType: ").AppendLine(extrabonusType.ToString())

                // properties
                .Append("isDropable: ").AppendLine(isDropable.ToString())
                .Append("isPickable: ").AppendLine(isPickable.ToString())
                .Append("isTradable: ").AppendLine(isTradable.ToString())
                .Append("canDropAsLoot: ").AppendLine(canDropAsLoot.ToString())
                .Append("canUseInRvR: ").AppendLine(canUseInRvR.ToString())                

                // stack
                .Append("maxCount: ").AppendLine(maxCount.ToString())
                .Append("packSize: ").AppendLine(packSize.ToString())

                // proc & charges
                .Append("spellID: ").AppendLine(spellID.ToString())
                .Append("procSpellID: ").AppendLine(procSpellID.ToString())
                .Append("maxCharges: ").AppendLine(maxCharges.ToString())
                .Append("charges: ").AppendLine(charges.ToString())
                .Append("spellID1: ").AppendLine(spellID1.ToString())
                .Append("procSpellID1: ").AppendLine(procSpellID1.ToString())
                .Append("charges1: ").AppendLine(charges1.ToString())
                .Append("maxCharges1: ").AppendLine(maxCharges1.ToString())
                .Append("procChance: ").AppendLine(procChance.ToString())
                .Append("poisonSpellID: ").AppendLine(poisonSpellID.ToString())
                .Append("poisonMaxCharges: ").AppendLine(poisonMaxCharges.ToString())
                .Append("poisonCharges: ").AppendLine(poisonCharges.ToString())

                .Append("realm: ").AppendLine(realm.ToString())
                .Append("allowedClasses: ").AppendLine(allowedClasses)
                .Append("canUseEvery: ").AppendLine(canUseEvery.ToString())
                .Append("flags: ").AppendLine(flags.ToString())
                .Append("bonusLevel: ").AppendLine(bonusLevel.ToString())

                .Append("packageID: ").AppendLine(packageID)
                .Append("classType: ").AppendLine(classType)
                .ToString();
        }

        private string _GetItemUpdateString()
        {
            var fields = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("name", name),
                new KeyValuePair<string, object>("level", level.ToString()),
                new KeyValuePair<string, object>("levelRequirement", levelRequirement.ToString()),
                new KeyValuePair<string, object>("price", price.ToString()),
                new KeyValuePair<string, object>("quality", quality.ToString()),
                new KeyValuePair<string, object>("object_type", object_type.ToString()),
                new KeyValuePair<string, object>("item_type", item_type.ToString()),
                new KeyValuePair<string, object>("description", description),

                // dur_con
                new KeyValuePair<string, object>("durability", durability.ToString()),
                new KeyValuePair<string, object>("condition", condition.ToString()),
                new KeyValuePair<string, object>("maxdurability", maxdurability.ToString()),
                new KeyValuePair<string, object>("maxcondition", maxcondition.ToString()),
                new KeyValuePair<string, object>("weight", weight.ToString()),
                new KeyValuePair<string, object>("isIndestructible", isIndestructible),
                new KeyValuePair<string, object>("isNotLosingDur", isNotLosingDur),

                // weapon/armor
                new KeyValuePair<string, object>("dps_af", dps_af.ToString()),
                new KeyValuePair<string, object>("spd_abs", spd_abs.ToString()),
                new KeyValuePair<string, object>("hand", hand.ToString()),
                new KeyValuePair<string, object>("type_damage", type_damage.ToString()),

                // apparence
                new KeyValuePair<string, object>("color", color.ToString()),
                new KeyValuePair<string, object>("emblem", emblem.ToString()),
                new KeyValuePair<string, object>("effect", effect.ToString()),
                new KeyValuePair<string, object>("model", model.ToString()),
                new KeyValuePair<string, object>("extension", extension.ToString()),

                // bonuses
                new KeyValuePair<string, object>("bonus", bonus.ToString()),
                new KeyValuePair<string, object>("bonus1", bonus1.ToString()),
                new KeyValuePair<string, object>("bonus2", bonus2.ToString()),
                new KeyValuePair<string, object>("bonus3", bonus3.ToString()),
                new KeyValuePair<string, object>("bonus4", bonus4.ToString()),
                new KeyValuePair<string, object>("bonus5", bonus5.ToString()),
                new KeyValuePair<string, object>("bonus6", bonus6.ToString()),
                new KeyValuePair<string, object>("bonus7", bonus7.ToString()),
                new KeyValuePair<string, object>("bonus8", bonus8.ToString()),
                new KeyValuePair<string, object>("bonus9", bonus9.ToString()),
                new KeyValuePair<string, object>("bonus10", bonus10.ToString()),
                new KeyValuePair<string, object>("extrabonus", extrabonus.ToString()),
                new KeyValuePair<string, object>("bonus1Type", bonus1Type.ToString()),
                new KeyValuePair<string, object>("bonus2Type", bonus2Type.ToString()),
                new KeyValuePair<string, object>("bonus3Type", bonus3Type.ToString()),
                new KeyValuePair<string, object>("bonus4Type", bonus4Type.ToString()),
                new KeyValuePair<string, object>("bonus5Type", bonus5Type.ToString()),
                new KeyValuePair<string, object>("bonus6Type", bonus6Type.ToString()),
                new KeyValuePair<string, object>("bonus7Type", bonus7Type.ToString()),
                new KeyValuePair<string, object>("bonus8Type", bonus8Type.ToString()),
                new KeyValuePair<string, object>("bonus9Type", bonus9Type.ToString()),
                new KeyValuePair<string, object>("bonus10Type", bonus10Type.ToString()),
                new KeyValuePair<string, object>("extrabonusType", extrabonusType.ToString()),

                // properties
                new KeyValuePair<string, object>("isDropable", isDropable),
                new KeyValuePair<string, object>("isPickable", isPickable),
                new KeyValuePair<string, object>("isTradable", isTradable),
                new KeyValuePair<string, object>("canDropAsLoot", canDropAsLoot),
                new KeyValuePair<string, object>("canUseInRvR", canUseInRvR),

                // stack
                new KeyValuePair<string, object>("maxCount", maxCount.ToString()),
                new KeyValuePair<string, object>("packSize", packSize.ToString()),

                // proc & charges
                new KeyValuePair<string, object>("spellID", spellID.ToString()),
                new KeyValuePair<string, object>("procSpellID", procSpellID.ToString()),
                new KeyValuePair<string, object>("maxCharges", maxCharges.ToString()),
                new KeyValuePair<string, object>("charges", charges.ToString()),
                new KeyValuePair<string, object>("spellID1", spellID1.ToString()),
                new KeyValuePair<string, object>("procSpellID1", procSpellID1.ToString()),
                new KeyValuePair<string, object>("charges1", charges1.ToString()),
                new KeyValuePair<string, object>("maxCharges1", maxCharges1.ToString()),
                new KeyValuePair<string, object>("procChance", procChance.ToString()),
                new KeyValuePair<string, object>("poisonSpellID", poisonSpellID.ToString()),
                new KeyValuePair<string, object>("poisonMaxCharges", poisonMaxCharges.ToString()),
                new KeyValuePair<string, object>("poisonCharges", poisonCharges.ToString()),

                new KeyValuePair<string, object>("realm", realm.ToString()),
                new KeyValuePair<string, object>("allowedClasses", allowedClasses),
                new KeyValuePair<string, object>("canUseEvery", canUseEvery.ToString()),
                new KeyValuePair<string, object>("flags", flags.ToString()),
                new KeyValuePair<string, object>("bonusLevel", bonusLevel.ToString()),
                new KeyValuePair<string, object>("bonusConditions", bonusConditions ?? string.Empty)
            };

            return "?action=UPDATE&table=itemtemplate&where=" +
                   HttpUtility.UrlEncode("Id_nb = " + Server.EscapeSql(id_nb), Encoding.UTF8) +
                   "&upfields=" +
                   HttpUtility.UrlEncode(string.Join(",", fields.Select(t => $"`{t.Key}` = {Server.EscapeSql(t.Value)}")), Encoding.UTF8);
        }

        private string _GetItemInsertString()
        {
            var values = Server.EscapeSql(Guid.NewGuid().ToString()) + "," +
                         Server.EscapeSql(id_nb) + "," +
                         Server.EscapeSql(name) + "," +
                         "'" + level + "'," +
                         "'" + levelRequirement + "'," +
                         "'" + price + "'," +
                         "'" + quality + "'," +
                         "'" + object_type + "'," +
                         "'" + item_type + "'," +
                         Server.EscapeSql(description) + "," +
                         "'" + bonus + "'," +

                         // dur_con
                         "'" + durability + "'," +
                         "'" + condition + "'," +
                         "'" + maxdurability + "'," +
                         "'" + maxcondition + "'," +
                         "'" + weight + "'," +
                         "'" + (isIndestructible ? '1' : '0') + "'," +
                         "'" + (isNotLosingDur ? '1' : '0') + "'," +

                         // weapon/armor
                         "'" + dps_af + "'," +
                         "'" + spd_abs + "'," +
                         "'" + hand + "'," +
                         "'" + type_damage + "'," +

                         // apparence
                         "'" + color + "'," +
                         "'" + emblem + "'," +
                         "'" + effect + "'," +
                         "'" + model + "'," +
                         "'" + extension + "'," +

                         // bonuses
                         "'" + bonus1 + "'," +
                         "'" + bonus2 + "'," +
                         "'" + bonus3 + "'," +
                         "'" + bonus4 + "'," +
                         "'" + bonus5 + "'," +
                         "'" + bonus6 + "'," +
                         "'" + bonus7 + "'," +
                         "'" + bonus8 + "'," +
                         "'" + bonus9 + "'," +
                         "'" + bonus10 + "'," +
                         "'" + extrabonus + "'," +
                         "'" + bonus1Type + "'," +
                         "'" + bonus2Type + "'," +
                         "'" + bonus3Type + "'," +
                         "'" + bonus4Type + "'," +
                         "'" + bonus5Type + "'," +
                         "'" + bonus6Type + "'," +
                         "'" + bonus7Type + "'," +
                         "'" + bonus8Type + "'," +
                         "'" + bonus9Type + "'," +
                         "'" + bonus10Type + "'," +
                         "'" + extrabonusType + "'," +

                         // properties
                         "'" + (isDropable ? '1' : '0') + "'," +
                         "'" + (isPickable ? '1' : '0') + "'," +
                         "'" + (isTradable ? '1' : '0') + "'," +
                         "'" + (canDropAsLoot ? '1' : '0') + "'," +
                         "'" + (canUseInRvR ? '1' : '0') + "'," +

                         // stack
                         "'" + maxCount + "'," +
                         "'" + packSize + "'," +

                         // proc & charges
                         "'" + spellID + "'," +
                         "'" + procSpellID + "'," +
                         "'" + maxCharges + "'," +
                         "'" + charges + "'," +
                         "'" + spellID1 + "'," +
                         "'" + procSpellID1 + "'," +
                         "'" + charges1 + "'," +
                         "'" + maxCharges1 + "'," +
                         "'" + procChance + "'," +
                         "'" + poisonSpellID + "'," +
                         "'" + poisonMaxCharges + "'," +
                         "'" + poisonCharges + "'," +

                         "'" + realm + "'," +
                         Server.EscapeSql(allowedClasses) + "," +
                         "'" + canUseEvery + "'," +
                         "'" + flags + "'," +
                         "'" + bonusLevel + "'," +
                         "'AmteCreator'," +
                        Server.EscapeSql(classType) + "," +
                     (bonusConditions != null ? Server.EscapeSql(bonusConditions) : "''");
            return "?action=INSERT&table=itemtemplate&fields=" +
                   HttpUtility.UrlEncode(
                       "`ItemTemplate_ID`,`id_nb`,`name`,`level`,`levelRequirement`,`price`,`quality`,`object_type`,`item_type`," +
                       "`description`,`bonus`,`durability`,`condition`,`maxdurability`,`maxcondition`,`weight`," +
                       "`isIndestructible`,`isNotLosingDur`,`dps_af`,`spd_abs`,`hand`,`type_damage`,`color`,`emblem`," +
                       "`effect`,`model`,`extension`,`bonus1`,`bonus2`,`bonus3`,`bonus4`,`bonus5`,`bonus6`,`bonus7`," +
                       "`bonus8`,`bonus9`,`bonus10`,`extrabonus`,`bonus1Type`,`bonus2Type`,`bonus3Type`,`bonus4Type`," +
                       "`bonus5Type`,`bonus6Type`,`bonus7Type`,`bonus8Type`,`bonus9Type`,`bonus10Type`,`extrabonusType`," +
                       "`isDropable`,`isPickable`,`isTradable`,`canDropAsLoot`,`canUseInRvR`, `maxCount`,`packSize`,`spellID`," +
                       "`procSpellID`,`maxCharges`,`charges`,`spellID1`,`procSpellID1`,`charges1`,`maxCharges1`,`procChance`,`poisonSpellID`," +
                       "`poisonMaxCharges`,`poisonCharges`,`realm`,`allowedClasses`,`canUseEvery`,`flags`,`classType`,`bonusLevel`,`packageID`,`bonusConditions`", Encoding.UTF8) +
                   "&values=" + HttpUtility.UrlEncode(values, Encoding.UTF8);
        }

        public dynamic Save()
        {
            dynamic resp;
            if (!AlreadyInDB)
            {
                AlreadyInDB = true;
                resp = Server.Query(_GetItemInsertString());
            }
            else
                resp = Server.Query(_GetItemUpdateString());
            //_dirty = resp.error != null;
            return resp;
        }
    }
}
