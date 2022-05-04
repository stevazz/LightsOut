namespace LightsOut.Server.Models.View
{
    public class LightsOutViewModel
    {
        public Guid Id { get; set; }
        public bool[][] Board { get; set; } 
        public int Score { get; set; }
        public bool IsSolved { get; set; }
    }
}
