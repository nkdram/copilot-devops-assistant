namespace DevopsMCP.Models
{
    /// <summary>
    /// Represents a parameter set for data-driven testing
    /// </summary>
    public class TestParameter
    {
        /// <summary>
        /// Parameter name
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Parameter value
        /// </summary>
        public required string Value { get; set; }

        /// <summary>
        /// Parameter type (optional: string, int, bool, etc.)
        /// </summary>
        public string Type { get; set; } = "string";
    }

    /// <summary>
    /// Represents a row in the parameter table (combination of parameters)
    /// </summary>
    public class ParameterSet
    {
        /// <summary>
        /// Name or ID of this parameter set
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Collection of parameters in this set
        /// </summary>
        public List<TestParameter> Parameters { get; set; } = new List<TestParameter>();
    }
}
