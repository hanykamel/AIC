using AIC.CrossCutting.Models;


namespace AIC.SP.Middleware.Models
{
    public class Filter
    {
        [AllowedStringAttribute]
        public string Field { get; set; }
        [AllowedStringAttribute]
        public string Operator { get; set; }
        public string Value { get; set; }
        [AllowedStringAttribute]
        public string FieldValueType { get; set; }
    }
}
