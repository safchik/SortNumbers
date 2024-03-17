namespace SortNumbers.Models
{
    public class SortedNumber
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public int SortResultId { get; set; }

        public SortResult? SortResult { get; set; }
    }
}
