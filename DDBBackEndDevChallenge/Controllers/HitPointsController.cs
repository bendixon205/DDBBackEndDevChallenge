using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDBBackEndDevChallenge.Models.Character;
using DDBBackEndDevChallenge.Models.Requests;
using DDBBackEndDevChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DDBBackEndDevChallenge.Controllers
{
    [ApiController]
    [Route("/")]
    public class HitPointsController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public HitPointsController(CharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            Character briv = _characterService.GetCharacter();

            return Ok(briv);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult Heal(HealingRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (model.Amount < 0)
            {
                return BadRequest(new { message = "Healing amount must be a positive value." });
            }

            Character briv = _characterService.GetCharacter();

            _characterService.HealCharacter(ref briv, model.Amount);

            return Ok(briv);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult AddTempHP(TempHPRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (model.Amount < 0)
            {
                return BadRequest(new { message = "Temporary HP amount must be a positive value. " });
            }

            Character briv = _characterService.GetCharacter();

            _characterService.AddTempHP(ref briv, model.Amount);

            return Ok(briv);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult DealDamage(DamageRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (model.Amount < 0)
            {
                return BadRequest(new { message = "Damage value must be a positive value." });
            }
            if (model.DamageType == null || model.DamageType.Trim().Length == 0)
            {
                return BadRequest(new { message = "Damage must have a type." });
            }

            Character briv = _characterService.GetCharacter();

            _characterService.DealDamage(ref briv, model.Amount, model.DamageType);

            return Ok(briv);
        }
    }
}
