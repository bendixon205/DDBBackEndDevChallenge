using DDBBackEndDevChallenge.Models.Character;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DDBBackEndDevChallenge.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<CharacterService> _logger;

        // Cached copy of Briv for lifetime of the application
        public static Character cachedCharacter;

        public CharacterService(IConfiguration config, ILogger<CharacterService> logger)
        {
            _config = config;
            _logger = logger;
        }

        /// <summary>
        /// Loads the character from briv.json or returns existing cached character
        /// </summary>
        /// <returns></returns>
        public Character GetCharacter()
        {
            if (CharacterService.cachedCharacter != null)
            {
                return CharacterService.cachedCharacter;
            }

            Character character;
            try
            {
                string filePath = _config["CharacterSheetPath"];
                string characterInfo = File.ReadAllText(filePath);

                character = JsonConvert.DeserializeObject<Character>(characterInfo);
                character.Initialize();

                CharacterService.cachedCharacter = character;
            }
            catch (Exception ex)
            {
                _logger.LogError(exception: ex, message: "Exception occurred in [CharacterService.cs] GetCharacter()");
                throw;
            }

            return character;
        }

        /// <summary>
        /// Heals the character by the requested amount, respecting MaxHP
        /// </summary>
        /// <param name="character">Reference to the targetted character</param>
        /// <param name="amount">Amount to heal</param>
        public void HealCharacter(ref Character character, int amount)
        {
            character.CurrentHitPoints += amount;

            if (character.CurrentHitPoints > character.MaxHitPoints)
            {
                character.CurrentHitPoints = character.MaxHitPoints;
            }
        }

        /// <summary>
        /// Adds Temporary Hit Points to the character of the requested amount, replacing lower values
        /// </summary>
        /// <param name="character">Reference to the targetted character</param>
        /// <param name="amount">Amount of Temp HP to attempt to add</param>
        public void AddTempHP(ref Character character, int amount)
        {
            if (character.CurrentTemporaryHitPoints > amount)
            {
                return;
            }

            character.CurrentTemporaryHitPoints = amount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="character">Reference to the targetted character</param>
        /// <param name="amount">Amount of damage to deal</param>
        /// <param name="damageType">Type of damage dealt</param>
        public void DealDamage(ref Character character, int amount, string damageType)
        {
            int remainingDamage = amount;

            CharacterDefense defense = character.Defenses.FirstOrDefault(x => x.Type.ToLower() == damageType.ToLower());
            if (defense != null)
            {
                switch (defense.Defense.ToLower())
                {
                    case "immunity":
                        remainingDamage = 0;
                        break;
                    case "resistance":
                        remainingDamage = (int)Math.Round(amount / 2.0D, MidpointRounding.ToNegativeInfinity);
                        break;
                    case "vulnerability": // Added Vulnerability because, why not. It's easily extensible.
                        remainingDamage *= 2;
                        break;
                    default:
                        break;
                }
            }

            // If character has THP, target it first
            if (character.CurrentTemporaryHitPoints > 0)
            {
                // If, after damage, character will still have THP
                if ((remainingDamage - character.CurrentTemporaryHitPoints) > 0)
                {
                    // Lower remainingDamage by THP value
                    remainingDamage -= character.CurrentTemporaryHitPoints;
                    // Remove THP
                    character.CurrentTemporaryHitPoints = 0;
                    // Deal remainder to character
                    character.CurrentHitPoints -= remainingDamage;
                }
                else
                {
                    // Otherwise, deal full damage to THP
                    character.CurrentTemporaryHitPoints -= remainingDamage;
                }
            }
            else
            {
                // Otherwise deal remaining damage directly to character
                character.CurrentHitPoints -= remainingDamage;
            }
        }
    }
}
