namespace Digitamentos.Models
{
    public class KeystrokeRecord
    {
        public char ExpectedChar { get; set; }
        public char TypedChar { get; set; }
        public bool IsCorrect => ExpectedChar == TypedChar;
        public long TimestampMs { get; set; }
    }
}
