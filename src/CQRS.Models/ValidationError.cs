namespace CQRS.Models
{
    /// <summary>
    /// Validation error message model
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Field
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
    }
}
