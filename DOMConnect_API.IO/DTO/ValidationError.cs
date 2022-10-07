namespace DOMConnect_API.IO.DTO
{
    /// <summary>
    /// Data transfert object used for error responses concerning invalid request bodies
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Gets or sets the name of the field where an error has occured.
        /// </summary>
        public string Field { get; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Creates instance of <see cref="ValidationError"/>.
        /// </summary>
        /// <param name="field">The name of the field where an error has occured.</param>
        /// <param name="message">The error message.</param>
        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }
}
