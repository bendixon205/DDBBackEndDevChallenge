using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DDBBackEndDevChallenge.Models.Items
{
    public class MagicItem
    {
        public string Name { get; set; }
        public ItemModifier Modifier { get; set; }
    }
}
