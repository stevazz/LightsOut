namespace LightsOut.Server.Models.Binding
{
    public class ToggleBindingModel
    {
        public Guid Id { get; set; }
        public string Cell { get; set; }

        private (int alpha, int numeric)? arrayIndex;

        public (int Alpha, int Numeric) GetArrayIndex()
        {
            if (arrayIndex == null)
            {
                var alpha = Cell.ToUpper()[0] - 65;
                var numeric = Cell[1] - 49;

                arrayIndex = (alpha, numeric);
            }

            return arrayIndex.Value;
        }
    }
}
