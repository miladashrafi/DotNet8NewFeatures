using System.ComponentModel.DataAnnotations;

namespace DotNet8NewFeatures.Models
{
    public class RangeExclusiveModel
    {
        [Range(10, 15, MinimumIsExclusive = true, MaximumIsExclusive = true)]
        public int RangeTest { get; set; }
    }

    public class LengthModel
    {
        [Length(10, 15)]
        public required string LengthTest { get; set; }
    }

    public class Base64Model
    {
        [Base64String]
        public required string Base64String { get; set; }
    }

    public class AllowedValuesDeniedValuesModel
    {
        [AllowedValues("Milad", "Reza", "apple")]
        public required string AllowedValues { get; set; }

        [DeniedValues("Milad", "Reza", "apple")]
        public required string DeniedValues { get; set; }
    }
}
