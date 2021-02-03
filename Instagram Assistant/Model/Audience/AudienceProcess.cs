namespace Instagram_Assistant.Model
{
    class AudienceProcessModel
    {
        public AudienceProcessModel(string action, double? percent)
        {
            Message = action;
            Percent = percent;
        }
        public string Message { get; set; }
        public double? Percent { get; set; }
    }
}
