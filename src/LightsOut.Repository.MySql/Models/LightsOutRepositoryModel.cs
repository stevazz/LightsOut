using System;
using System.Collections.Generic;
using System.Text;

namespace LightsOut.Repository.MySql.Models
{
    public class LightsOutRepositoryModel
    {
        public Guid Id { get; set; }
        public string Board { get; set; }
        public int ColumnLength { get; set; }
        public int RowLength { get; set; }
        public int Score { get; set; }
    }
}
