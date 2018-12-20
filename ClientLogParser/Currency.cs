namespace ClientLogParser
{
    /// <summary>
    /// Represents a Trade item as currency
    /// </summary>
    public class Currency : Item
    {
        /// <summary>
        /// Initializes a new currency.
        /// </summary>
        /// <param name="name">Name of the item.</param>
        /// <param name="amount">The amount of the currency.</param>
        /// <param name="price">Offered price.</param>
        /// <param name="league">League in which the item exists.</param>
        /// <param name="stash">Name of the stash tab where the item is stored.</param>
        /// <param name="pos">Position of the item inside the stash tab.</param>
        public Currency(string name, int amount, string price, string league, string stash, string pos) : base(name, price, league, stash, pos)
        {
            Amount = amount;
        }

        /// <summary>
        /// The amount of the currency
        /// </summary>
        public readonly int Amount;
    }
}
