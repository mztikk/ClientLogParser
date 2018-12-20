using System;
using System.Collections.Generic;

namespace ClientLogParser
{
    /// <summary>
    /// Represents a Trade item.
    /// </summary>
    public class Item : IEqualityComparer<Item>, IEquatable<Item>
    {
        /// <summary>
        /// Initializes a new item.
        /// </summary>
        /// <param name="name">Name of the item.</param>
        /// <param name="price">Offered price.</param>
        /// <param name="league">League in which the item exists.</param>
        /// <param name="stash">Name of the stash tab where the item is stored.</param>
        /// <param name="pos">Position of the item inside the stash tab.</param>
        /// <param name="fullItemIdentifier">Optional full item name. If it differs from the regular name.</param>
        public Item(string name, string price, string league, string stash, string pos, string fullItemIdentifier = "")
        {
            Name = name;
            Price = price;
            League = league;
            Stash = stash;
            Pos = pos;
            FullItemIdentifier = (string.IsNullOrWhiteSpace(fullItemIdentifier)) ? name : fullItemIdentifier;
        }

        /// <summary>
        /// Name of the item
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Price of the item
        /// </summary>
        public readonly string Price;

        /// <summary>
        /// League in which the item exists 
        /// </summary>
        public readonly string League;

        /// <summary>
        /// Name of the stash tab where the item is stored
        /// </summary>
        public readonly string Stash;

        /// <summary>
        /// Position of the item inside the stash tab
        /// </summary>
        public readonly string Pos;

        /// <summary>
        /// Full name of the item
        /// </summary>
        public readonly string FullItemIdentifier;

        /// <summary>Determines whether the specified objects are equal.</summary>
        /// <param name="x">The first object of type T to compare.</param>
        /// <param name="y">The second object of type T to compare.</param>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        public bool Equals(Item x, Item y) => x.Equals(y);

        /// <summary>Returns a hash code for the specified object.</summary>
        /// <param name="obj">The <see cref="object"></see> for which a hash code is to be returned.</param>
        /// <returns>A hash code for the specified object.</returns>
        /// <exception cref="ArgumentNullException">The type of <paramref name="obj">obj</paramref> is a reference type and <paramref name="obj">obj</paramref> is null.</exception>
        public int GetHashCode(Item obj)
        {
            unchecked
            {
                var hash = (int)2166136261;
                hash = (hash * 16777619) ^ FullItemIdentifier.GetHashCode();
                hash = (hash * 16777619) ^ Price.GetHashCode();
                hash = (hash * 16777619) ^ League.GetHashCode();
                hash = (hash * 16777619) ^ Stash.GetHashCode();
                return (hash * 16777619) ^ Pos.GetHashCode();
            }
        }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public bool Equals(Item other)
        {
            return FullItemIdentifier.Equals(other.FullItemIdentifier)
                && Price.Equals(other.Price)
                && League.Equals(other.League)
                && Stash.Equals(other.Stash)
                && Pos.Equals(other.Pos);
        }
    }
}
