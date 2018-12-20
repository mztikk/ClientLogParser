using System;
using System.Collections.Generic;

namespace ClientLogParser.Events
{
    /// <summary>
    /// Represents a trade message, consisting of a whisper and an item.
    /// </summary>
    public class TradeMessageEventArgs : EventArgs, IEqualityComparer<TradeMessageEventArgs>, IEquatable<TradeMessageEventArgs>
    {
        /// <summary>
        /// Initializes a TradeMessage
        /// </summary>
        /// <param name="whisper">The whisper</param>
        /// <param name="item">The item</param>
        /// <param name="other">other messages</param>
        public TradeMessageEventArgs(Whisper whisper, Item item, string other)
        {
            Whisper = whisper;
            Item = item;
            Other = other;
        }

        /// <summary>
        /// The whisper of the trade
        /// </summary>
        public Whisper Whisper { get; }

        /// <summary>
        /// The item of the trade
        /// </summary>
        public Item Item { get; }

        /// <summary>
        /// Other messages
        /// </summary>
        public string Other { get; }

        /// <summary>Determines whether the specified objects are equal.</summary>
        /// <param name="x">The first object of type T to compare.</param>
        /// <param name="y">The second object of type T to compare.</param>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        public bool Equals(TradeMessageEventArgs x, TradeMessageEventArgs y) => x.Equals(y);

        /// <summary>Returns a hash code for the specified object.</summary>
        /// <param name="obj">The <see cref="object"></see> for which a hash code is to be returned.</param>
        /// <returns>A hash code for the specified object.</returns>
        /// <exception cref="ArgumentNullException">The type of <paramref name="obj">obj</paramref> is a reference type and <paramref name="obj">obj</paramref> is null.</exception>
        public int GetHashCode(TradeMessageEventArgs obj)
        {
            unchecked
            {
                const int hash = (int)2166136261;
                return (hash * 16777619) ^ Whisper.Sender.GetHashCode();
            }
        }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public bool Equals(TradeMessageEventArgs other) => Whisper.Equals(other.Whisper) && Item.Equals(other.Item);
    }
}
