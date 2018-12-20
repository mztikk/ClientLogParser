using System;
using System.Collections.Generic;

namespace ClientLogParser
{
    /// <summary>
    /// Represents an ingame whisper, with a sender, recipient, the message and time of receivement
    /// </summary>
    public class Whisper : IEqualityComparer<Whisper>, IEquatable<Whisper>
    {
        /// <summary>
        /// Initializes a new whisper
        /// </summary>
        /// <param name="sender">The sender of the whisper</param>
        /// <param name="recipient">The recipient of the whisper</param>
        /// <param name="message">The actual whisper message</param>
        /// <param name="timeOfMessage">The time the whisper was received</param>
        public Whisper(string sender, string recipient, string message, DateTime timeOfMessage)
        {
            Sender = sender;
            Recipient = recipient;
            Message = message;
            TimeOfMessage = timeOfMessage;
        }

        /// <summary>
        /// The sender of the whisper
        /// </summary>
        public readonly string Sender;

        /// <summary>
        /// The recipient of the whisper
        /// </summary>
        public readonly string Recipient;

        /// <summary>
        /// The actual whisper message
        /// </summary>
        public readonly string Message;

        /// <summary>
        /// The time the whisper was received
        /// </summary>
        public readonly DateTime TimeOfMessage;

        /// <summary>Determines whether the specified objects are equal.</summary>
        /// <param name="x">The first object of type T to compare.</param>
        /// <param name="y">The second object of type T to compare.</param>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        public bool Equals(Whisper x, Whisper y) => x.Equals(y);

        /// <summary>Returns a hash code for the specified object.</summary>
        /// <param name="obj">The <see cref="object"></see> for which a hash code is to be returned.</param>
        /// <returns>A hash code for the specified object.</returns>
        /// <exception cref="ArgumentNullException">The type of <paramref name="obj">obj</paramref> is a reference type and <paramref name="obj">obj</paramref> is null.</exception>
        public int GetHashCode(Whisper obj)
        {
            unchecked
            {
                var hash = (int)2166136261;
                hash = (hash * 16777619) ^ Sender.GetHashCode();
                hash = (hash * 16777619) ^ Recipient.GetHashCode();
                return (hash * 16777619) ^ Message.GetHashCode();
            }
        }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public bool Equals(Whisper other)
        {
            return Sender.Equals(other.Sender)
                && Recipient.Equals(other.Recipient)
                && Message.Equals(other.Message);
        }
    }
}
