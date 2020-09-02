using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DDBBackEndDevChallenge.Models.Items;

namespace DDBBackEndDevChallenge.Models.Character
{
    public class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }

        public int CurrentHitPoints { get; set; }
        public int MaxHitPoints { get; set; }
        public int CurrentTemporaryHitPoints { get; set; }

        public IEnumerable<CharacterClass> Classes { get; set; }
        public CharacterAttributes Stats { get; set; }
        public IEnumerable<MagicItem> Items { get; set; }
        public IEnumerable<CharacterDefense> Defenses { get; set; }

        public void Initialize()
        {
            applyItemModifiers();
            initializeMaxHitPoints();
        }

        private void initializeMaxHitPoints()
        {
            int maxHP = 0;
            int conMod = (int)Math.Round((this.Stats.Constitution - 10) / 2.0D, MidpointRounding.ToNegativeInfinity);

            foreach (CharacterClass @class in this.Classes)
            {
                int hitDiceAverage = (@class.HitDiceValue / 2) + 1;
                maxHP += (hitDiceAverage + conMod) * @class.ClassLevel;
            }

            this.MaxHitPoints = this.CurrentHitPoints = maxHP;
        }
        private void applyItemModifiers()
        {
            foreach (MagicItem item in this.Items)
            {
                if (item.Modifier.AffectedObject.ToLower() == "stats")
                {
                    switch (item.Modifier.AffectedValue.ToLower())
                    {
                        case "strength":
                            this.Stats.Strength += item.Modifier.Value;
                            break;
                        case "dexterity":
                            this.Stats.Dexterity += item.Modifier.Value;
                            break;
                        case "constitution":
                            this.Stats.Constitution += item.Modifier.Value;
                            break;
                        case "intelligence":
                            this.Stats.Intelligence += item.Modifier.Value;
                            break;
                        case "wisdom":
                            this.Stats.Wisdom += item.Modifier.Value;
                            break;
                        case "charisma":
                            this.Stats.Charisma += item.Modifier.Value;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
