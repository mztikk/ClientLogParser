namespace ClientLogParser.Items
{
    /// <summary>
    /// Represents a Trade item as a map.
    /// </summary>
    public class Map : Item
    {
        /// <summary>
        /// Initializes a new map.
        /// </summary>
        /// <param name="name">Name of the map.</param>
        /// <param name="fullItemIdentifier">Optional full item name. If it differs from the regular name.</param>
        /// <param name="tier">Tier of the map.</param>
        /// <param name="price">Offered price.</param>
        /// <param name="league">League in which the map exists.</param>
        /// <param name="stash">Name of the stash tab where the map is stored.</param>
        /// <param name="pos">Position of the map inside the stash tab.</param>
        public Map(string name, string fullItemIdentifier, int tier, string price, string league, string stash, string pos) : base(name, price, league, stash, pos, fullItemIdentifier)
        {
            Tier = tier;
        }

        /// <summary>
        /// Tier of the map
        /// </summary>
        public readonly int Tier;
    }
}
