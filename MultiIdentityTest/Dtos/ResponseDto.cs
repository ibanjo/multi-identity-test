namespace MultiIdentityTest.Dtos
{
    public class ResponseDto
    {
        public string StringValue { get; set; }
        public int NumericValue { get; set; }
        public NestedDto NestedValue { get; set; }
    }

    public class NestedDto
    {
        public string NestedStringValue { get; set; }
        public int NestedNumericValue { get; set; }
    }
}
