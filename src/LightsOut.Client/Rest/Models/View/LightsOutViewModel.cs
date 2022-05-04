using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOut.Client.Rest.Models.View
{
    public class LightsOutViewModel
    {
        public Guid Id { get; set; }
        public bool[][] Board { get; set; }
        public int Score { get; set; }
        public bool IsSolved { get; set; }
    }
}
