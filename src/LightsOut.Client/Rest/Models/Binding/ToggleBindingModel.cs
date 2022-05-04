using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOut.Client.Rest.Models.Binding
{
    public class ToggleBindingModel
    {
        public Guid Id { get; set; }
        public string Cell { get; set; }
    }
}
