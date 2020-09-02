using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDBBackEndDevChallenge.Models.Character
{
    public class CharacterClass
    {
        public string Name { get; set; }
        public int HitDiceValue { get; set; }
        public int ClassLevel { get; set; }
    }
}
