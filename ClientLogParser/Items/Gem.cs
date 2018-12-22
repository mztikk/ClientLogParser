namespace ClientLogParser.Items
{
    /// <summary>
    /// Represents a Trade item as a gem.
    /// </summary>
    public class Gem : Item
    {
        /// <summary>
        /// Initializes a new gem.
        /// </summary>
        /// <param name="name">Name of the gem.</param>
        /// <param name="price">Offered price.</param>
        /// <param name="league">League in which the gem exists.</param>
        /// <param name="stash">Name of the stash tab where the gem is stored.</param>
        /// <param name="pos">Position of the gem inside the stash tab.</param>
        /// <param name="fullItemIdentifier">Optional full item name. If it differs from the regular name.</param>
        /// <param name="level">Level of the gem.</param>
        /// <param name="quality">Quality of the gem.</param>
        public Gem(string name, string fullItemIdentifier, int level, int quality, string price, string league, string stash, string pos) : base(name, price, league, stash, pos, fullItemIdentifier)
        {
            Quality = quality;
            Level = level;
        }

        /// <summary>
        /// Quality of the gem
        /// </summary>
        public readonly int Quality;

        /// <summary>
        /// Level of the gem
        /// </summary>
        public readonly int Level;
    }
}
