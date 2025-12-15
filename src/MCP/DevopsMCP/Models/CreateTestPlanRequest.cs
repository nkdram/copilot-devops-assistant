namespace DevopsMCP.Models
{
    public class CreateTestPlanRequest
    {
        public required string Project { get; set; }
        public required string Name { get; set; }
        public Dictionary<string, object> Fields { get; set; } = new Dictionary<string, object>();
        
        /// <summary>
        /// Test steps in Gherkin format (Given/When/Then)
        /// </summary>
        public List<TestStep>? Steps { get; set; }
        
        /// <summary>
        /// Parameter sets for data-driven testing
        /// </summary>
        public List<ParameterSet>? Parameters { get; set; }
    }
}
