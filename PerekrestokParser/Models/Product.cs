namespace PerekrestokParser.Models
{
    public class Product
    {
        public string Name { get; set; }
        public string OldPrice { get; set; }
        public string ActualPrice { get; set; }
        public string Rating { get; set; }
        public string NumOfVotes { get; set; }
        public string PricePerPiece { get; set; }

        public Product(string name, string oldPrice, string actualPrice, string rating, string numOfVotes, string pricing)
        {
            Name = name;
            OldPrice = oldPrice;
            ActualPrice = actualPrice;
            Rating = rating;
            NumOfVotes = numOfVotes;
            PricePerPiece = pricing;
        }
    }
}
