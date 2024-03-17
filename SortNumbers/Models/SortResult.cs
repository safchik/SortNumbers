namespace SortNumbers.Models
{
    public class SortResult
    {
        public int Id { get; set; }
        public bool IsAscending { get; set; }
        public TimeSpan TimeTaken { get; set; }

        public List<SortedNumber> SortedNumbers { get; set; } = new List<SortedNumber>();
    }
}
