using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOut.Client.Rest.Models.Binding
{
    public class InitializeBindingModel
    {
        public int ColumnLegth { get; set; }
        public int RowLegth { get; set; }
        public int ActiveCells { get; set; }
    }
}
