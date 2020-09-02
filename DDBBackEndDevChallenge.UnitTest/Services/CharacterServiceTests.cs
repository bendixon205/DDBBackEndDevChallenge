using DDBBackEndDevChallenge.Models.Character;
using DDBBackEndDevChallenge.Models.Items;
using DDBBackEndDevChallenge.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace DDBBackEndDevChallenge.UnitTest.Services
{
    public class CharacterServiceTests
    {
        static private CharacterService characterService;

        static private ILogger<CharacterService> _logger;
        static private IConfiguration _config;

        [SetUp]
        public void Setup()
        {
            _logger = Substitute.For<ILogger<CharacterService>>();
            _config = Substitute.For<IConfiguration>();

            characterService = new CharacterService(_config, _logger);

            CharacterService.cachedCharacter = new Character()
            {
                Name = "BrivTest",
                Level = 5,
                CurrentHitPoints = 41,
                MaxHitPoints =  41,
                CurrentTemporaryHitPoints = 0,
                Classes = new List<CharacterClass>()
                {
                    new CharacterClass()
                    {
                        Name = "fighter",
                        HitDiceValue = 10,
                        ClassLevel = 3
                    },
                    new CharacterClass()
                    {
                        Name = "wizard",
                        HitDiceValue = 10,
                        ClassLevel = 2
                    }
                },
                Stats = new CharacterAttributes()
                {
                    Strength = 15,
                    Dexterity = 12,
                    Constitution = 14,
                    Intelligence = 13,
                    Wisdom = 10,
                    Charisma = 8
                },
                Items = new List<MagicItem>()
                {
                    new MagicItem()
                    {
                        Name = "Ioun Stone of Fortitude",
                        Modifier = new ItemModifier()
                        {
                            AffectedObject = "stats",
                            AffectedValue = "constitution",
                            Value = 2
                        }
                    }
                },
                Defenses = new List<CharacterDefense>()
                {
                    new CharacterDefense()
                    {
                        Type = "fire",
                        Defense = "immunity"
                    },
                    new CharacterDefense()
                    {
                        Type = "slashing",
                        Defense = "resistance"
                    },
                    new CharacterDefense()
                    {
                        Type = "cold",
                        Defense = "vulnerability"
                    }
                }
            };
        }

        [Test]
        public void Heal_ResultLessThanMax_Test()
        {
            // Arrange
            int currentHP = 20;
            int amountToHeal = 10;
            int expectedHP = currentHP + amountToHeal;
            CharacterService.cachedCharacter.CurrentHitPoints = currentHP;

            // Act
            characterService.HealCharacter(ref CharacterService.cachedCharacter, amountToHeal);

            // Assert
            Assert.AreEqual(expectedHP, CharacterService.cachedCharacter.CurrentHitPoints);
        }

        [Test]
        public void Heal_ResultGreaterThanMax_Test()
        {
            // Arrange
            int currentHP = 40;
            int amountToHeal = 10;
            CharacterService.cachedCharacter.CurrentHitPoints = currentHP;

            // Act
            characterService.HealCharacter(ref CharacterService.cachedCharacter, amountToHeal);

            // Assert
            Assert.AreEqual(CharacterService.cachedCharacter.MaxHitPoints, CharacterService.cachedCharacter.CurrentHitPoints);
        }

        [Test]
        public void AddTempHP_GreaterThanExisting_Test()
        {
            // Arrange
            CharacterService.cachedCharacter.CurrentTemporaryHitPoints = 0;
            int tempHPtoAdd = 10;

            // Act
            characterService.AddTempHP(ref CharacterService.cachedCharacter, tempHPtoAdd);

            // Assert
            Assert.AreEqual(tempHPtoAdd, CharacterService.cachedCharacter.CurrentTemporaryHitPoints);
        }

        [Test]
        public void AddTempHP_LessThanExisting_Test()
        {
            // Arrange
            int currentTempHP = 15;
            int tempHPtoAdd = 10;
            CharacterService.cachedCharacter.CurrentTemporaryHitPoints = currentTempHP;

            // Act
            characterService.AddTempHP(ref CharacterService.cachedCharacter, tempHPtoAdd);

            // Assert
            Assert.AreEqual(currentTempHP, CharacterService.cachedCharacter.CurrentTemporaryHitPoints);
        }

        [Test]
        public void DealDamageTest()
        {
            // Arrange
            int damageToDeal = 10;
            string damageType = "acid";

            int currentHp = 41;
            CharacterService.cachedCharacter.CurrentHitPoints = currentHp;
            CharacterService.cachedCharacter.CurrentTemporaryHitPoints = 0;

            int expectedRemainingHP = currentHp - damageToDeal;

            // Act
            characterService.DealDamage(ref CharacterService.cachedCharacter, damageToDeal, damageType);

            // Assert
            Assert.AreEqual(expectedRemainingHP, CharacterService.cachedCharacter.CurrentHitPoints);
        }

        [Test]
        public void DealDamage_AgainstImmunity_Test()
        {
            // Arrange
            int damageToDeal = 10;
            string damageType = "fire";

            int currentHp = 41;
            CharacterService.cachedCharacter.CurrentHitPoints = currentHp;
            CharacterService.cachedCharacter.CurrentTemporaryHitPoints = 0;

            int expectedRemainingHP = currentHp - (damageToDeal * 0);

            // Act
            characterService.DealDamage(ref CharacterService.cachedCharacter, damageToDeal, damageType);

            // Assert
            Assert.AreEqual(expectedRemainingHP, CharacterService.cachedCharacter.CurrentHitPoints);
        }

        [Test]
        public void DealDamage_AgainstResistance_Test()
        {
            // Arrange
            int damageToDeal = 10;
            string damageType = "slashing";

            int currentHp = 41;
            CharacterService.cachedCharacter.CurrentHitPoints = currentHp;
            CharacterService.cachedCharacter.CurrentTemporaryHitPoints = 0;

            int expectedRemainingHP = currentHp - (damageToDeal / 2);

            // Act
            characterService.DealDamage(ref CharacterService.cachedCharacter, damageToDeal, damageType);

            // Assert
            Assert.AreEqual(expectedRemainingHP, CharacterService.cachedCharacter.CurrentHitPoints);
        }

        [Test]
        public void DealDamage_AgainstVulnerability_Test()
        {
            // Arrange
            int damageToDeal = 10;
            string damageType = "cold";

            int currentHp = 41;
            CharacterService.cachedCharacter.CurrentHitPoints = currentHp;
            CharacterService.cachedCharacter.CurrentTemporaryHitPoints = 0;

            int expectedRemainingHP = currentHp - (damageToDeal * 2);

            // Act
            characterService.DealDamage(ref CharacterService.cachedCharacter, damageToDeal, damageType);

            // Assert
            Assert.AreEqual(expectedRemainingHP, CharacterService.cachedCharacter.CurrentHitPoints);
        }

        [Test]
        public void DealDamage_AgainstTempHP_GreaterThanTempHP_Test()
        {
            // Arrange
            int damageToDeal = 10;
            string damageType = "acid";

            int currentHp = 41;
            int currentTempHp = 5;
            CharacterService.cachedCharacter.CurrentHitPoints = currentHp;
            CharacterService.cachedCharacter.CurrentTemporaryHitPoints = currentTempHp;

            int expectedRemainingTempHP = 0;
            int remainingDamage = damageToDeal - currentTempHp;
            int expectedRemainingHP = currentHp - remainingDamage;

            // Act
            characterService.DealDamage(ref CharacterService.cachedCharacter, damageToDeal, damageType);

            // Assert
            Assert.AreEqual(expectedRemainingHP, CharacterService.cachedCharacter.CurrentHitPoints);
            Assert.AreEqual(expectedRemainingTempHP, CharacterService.cachedCharacter.CurrentTemporaryHitPoints);
        }

        [Test]
        public void DealDamage_AgainstTempHP_LessThanTempHP_Test()
        {
            // Arrange
            int damageToDeal = 10;
            string damageType = "acid";

            int currentHp = 41;
            int currentTempHp = 15;
            CharacterService.cachedCharacter.CurrentHitPoints = currentHp;
            CharacterService.cachedCharacter.CurrentTemporaryHitPoints = currentTempHp;

            int expectedRemainingHP = currentHp;
            int expectedRemainingTempHP = currentTempHp - damageToDeal;

            // Act
            characterService.DealDamage(ref CharacterService.cachedCharacter, damageToDeal, damageType);

            // Assert
            Assert.AreEqual(expectedRemainingHP, CharacterService.cachedCharacter.CurrentHitPoints);
            Assert.AreEqual(expectedRemainingTempHP, CharacterService.cachedCharacter.CurrentTemporaryHitPoints);
        }
    }
}