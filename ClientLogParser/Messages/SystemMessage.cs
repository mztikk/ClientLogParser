using System;

namespace ClientLogParser.Messages
{
    /// <summary>
    /// Represents an ingame whisper, with a sender, recipient, the message and time of receivement
    /// </summary>
    public class SystemMessage
    {
        /// <summary>
        /// Initializes a new whisper
        /// </summary>
        /// <param name="type">Type of SystemMessage</param>
        /// <param name="timeOfMessage">The time the SystemMessage was received</param>
        public SystemMessage(SystemMessageType type, DateTime timeOfMessage)
        {
            Type = type;
            TimeOfMessage = timeOfMessage;
        }

        /// <summary>
        /// The type of the System Message
        /// </summary>
        public readonly SystemMessageType Type;

        /// <summary>
        /// The time the whisper was received
        /// </summary>
        public readonly DateTime TimeOfMessage;
    }

    /// <summary>
    /// The type of the System Message
    /// </summary>
    public enum SystemMessageType
    {
        /// <summary>
        /// Server is down for maintenance.
        /// </summary>
        MaintenanceDisconnect,

        /// <summary>
        /// There is a new patch we need to download.
        /// </summary>
        NewPatchDisconnect,

        /// <summary>
        /// User needs to reenter his password.
        /// </summary>
        ReenterPassword,

        /// <summary>
        /// Server is down.
        /// </summary>
        ServerDown
    }
}
