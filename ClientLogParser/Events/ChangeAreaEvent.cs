using System;
using System.Collections.Generic;

namespace ClientLogParser.Events
{
    /// <summary>
    /// Represents an area change event, to trigger whenever the localplayer enters a new area.
    /// </summary>
    public class ChangeAreaEvent : EventArgs, IEqualityComparer<ChangeAreaEvent>, IEquatable<ChangeAreaEvent>
    {
        /// <summary>
        /// Initializes a new <see cref="ChangeAreaEvent"/>.
        /// </summary>
        /// <param name="time">Time when the player entered the area</param>
        /// <param name="area">Name of the new area the player entered</param>
        public ChangeAreaEvent(DateTime time, string area)
        {
            Time = time;
            Area = area;
        }

        /// <summary>
        /// Time when the player entered the area.
        /// </summary>
        public DateTime Time { get; }

        /// <summary>
        /// Name of the new area the player entered.
        /// </summary>
        public string Area { get; }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public bool Equals(ChangeAreaEvent other) => Time.Equals(other.Time) && Area.Equals(other.Area, StringComparison.Ordinal);

        /// <summary>Determines whether the specified objects are equal.</summary>
        /// <param name="x">The first object of type T to compare.</param>
        /// <param name="y">The second object of type T to compare.</param>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        public bool Equals(ChangeAreaEvent x, ChangeAreaEvent y) => x.Equals(y);

        /// <summary>Returns a hash code for the specified object.</summary>
        /// <param name="obj">The <see cref="object"></see> for which a hash code is to be returned.</param>
        /// <returns>A hash code for the specified object.</returns>
        /// <exception cref="ArgumentNullException">The type of <paramref name="obj">obj</paramref> is a reference type and <paramref name="obj">obj</paramref> is null.</exception>
        public int GetHashCode(ChangeAreaEvent obj)
        {
            unchecked
            {
                var hash = (int)2166136261;
                hash = (hash * 16777619) ^ Time.GetHashCode();
                return (hash * 16777619) ^ Area.GetHashCode();
            }
        }
    }
}
