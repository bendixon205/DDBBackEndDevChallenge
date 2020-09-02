using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDBBackEndDevChallenge.Models.Requests
{
    public class DamageRequestModel
    {
        public int Amount { get; set; }
        public string DamageType { get; set; }
    }
}
