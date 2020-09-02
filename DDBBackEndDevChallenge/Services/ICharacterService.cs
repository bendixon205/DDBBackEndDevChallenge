using DDBBackEndDevChallenge.Models.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDBBackEndDevChallenge.Services
{
    public interface ICharacterService
    {
        Character GetCharacter();

        void HealCharacter(ref Character character, int amount);
        void AddTempHP(ref Character character, int amount);
        void DealDamage(ref Character briv, int amount, string damageType);
    }
}
