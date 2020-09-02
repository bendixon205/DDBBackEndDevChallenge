using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDBBackEndDevChallenge.Models.Items
{
    public class ItemModifier
    {
        public string AffectedObject { get; set; }
        public string AffectedValue { get; set; }
        public int Value { get; set; }
    }
}
