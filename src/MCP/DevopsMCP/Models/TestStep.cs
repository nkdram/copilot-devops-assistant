namespace DevopsMCP.Models
{
    /// <summary>
    /// Represents a test step in Gherkin format (Given/When/Then/And)
    /// </summary>
    public class TestStep
    {
        /// <summary>
        /// The type of step (Given, When, Then, And, But)
        /// </summary>
        public string Type { get; set; } = "Given";

        /// <summary>
        /// The description/action of the step
        /// </summary>
        public required string Action { get; set; }

        /// <summary>
        /// Expected result or validation (optional)
        /// </summary>
        public string? ExpectedResult { get; set; }

        /// <summary>
        /// Order/sequence of the step
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Additional data or context for the step
        /// </summary>
        public Dictionary<string, object>? Data { get; set; }
    }
}
