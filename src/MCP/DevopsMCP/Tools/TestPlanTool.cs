using DevopsMCP.Service;
using DevopsMCP.Models;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;

namespace DevopsMCP.Tools
{
    [McpServerToolType]
    public static class TestPlanTool
    {
        [McpServerTool(Name = "createTestPlan")]
        [Description("Creates a test plan with optional Gherkin steps and parameters")]
        public static async Task<JsonElement> CreateTestPlanAsync(
            ITestPlanService service,
            string project,
            string name,
            Dictionary<string, object> fields,
            List<TestStep>? steps = null,
            List<ParameterSet>? parameters = null)
        {
            return await service.CreateTestPlanAsync(project, name, fields, steps, parameters);
        }

        [McpServerTool(Name = "getTestPlan")]
        [Description("Gets a test plan by its ID")]
        public static async Task<JsonElement> GetTestPlanAsync(
            ITestPlanService service,
            int id)
        {
            return await service.GetTestPlanAsync(id);
        }

        [McpServerTool(Name = "updateTestPlan")]
        [Description("Updates a test plan with optional Gherkin steps and parameters")]
        public static async Task<JsonElement> UpdateTestPlanAsync(
            ITestPlanService service,
            int id,
            Dictionary<string, object> fields,
            List<TestStep>? steps = null,
            List<ParameterSet>? parameters = null)
        {
            return await service.UpdateTestPlanAsync(id, fields, steps, parameters);
        }

        [McpServerTool(Name = "deleteTestPlan")]
        [Description("Deletes a test plan by its ID")]
        public static async Task<JsonElement> DeleteTestPlanAsync(
            ITestPlanService service,
            int id)
        {
            return await service.DeleteTestPlanAsync(id);
        }

        [McpServerTool(Name = "getTestPlanTags")]
        [Description("Gets tags for a test plan")]
        public static async Task<JsonElement> GetTagsAsync(
            ITestPlanService service,
            int id)
        {
            return await service.GetTagsAsync(id);
        }

        [McpServerTool(Name = "addTestPlanTags")]
        [Description("Adds tags to a test plan")]
        public static async Task<JsonElement> AddTagsAsync(
            ITestPlanService service,
            int id,
            IEnumerable<string> tags)
        {
            return await service.AddTagsAsync(id, tags);
        }

        [McpServerTool(Name = "removeTestPlanTags")]
        [Description("Removes tags from a test plan")]
        public static async Task<JsonElement> RemoveTagsAsync(
            ITestPlanService service,
            int id,
            IEnumerable<string> tags)
        {
            return await service.RemoveTagsAsync(id, tags);
        }

        [McpServerTool(Name = "getTestPlanComments")]
        [Description("Gets comments for a test plan")]
        public static async Task<JsonElement> GetCommentsAsync(
            ITestPlanService service,
            int id)
        {
            return await service.GetCommentsAsync(id);
        }

        [McpServerTool(Name = "addTestPlanComment")]
        [Description("Adds a comment to a test plan")]
        public static async Task<JsonElement> AddCommentAsync(
            ITestPlanService service,
            int id,
            string comment)
        {
            return await service.AddCommentAsync(id, comment);
        }

        [McpServerTool(Name = "getTestSuite")]
        [Description("Gets a test suite by plan ID and suite ID")]
        public static async Task<JsonElement> GetTestSuiteAsync(
            ITestPlanService service,
            int planId,
            int suiteId)
        {
            return await service.GetTestSuiteAsync(planId, suiteId);
        }

        [McpServerTool(Name = "addTestCasesToSuite")]
        [Description("Adds test cases to a test suite")]
        public static async Task<JsonElement> AddTestCasesToSuiteAsync(
            ITestPlanService service,
            int planId,
            int suiteId,
            int[] testCaseIds)
        {
            return await service.AddTestCasesToSuiteAsync(planId, suiteId, testCaseIds);
        }

        [McpServerTool(Name = "removeTestCasesFromSuite")]
        [Description("Removes test cases from a test suite")]
        public static async Task<JsonElement> RemoveTestCasesFromSuiteAsync(
            ITestPlanService service,
            int planId,
            int suiteId,
            int[] testCaseIds)
        {
            return await service.RemoveTestCasesFromSuiteAsync(planId, suiteId, testCaseIds);
        }

        [McpServerTool(Name = "getTestCasesInSuite")]
        [Description("Gets all test cases in a test suite")]
        public static async Task<JsonElement> GetTestCasesInSuiteAsync(
            ITestPlanService service,
            int planId,
            int suiteId)
        {
            return await service.GetTestCasesInSuiteAsync(planId, suiteId);
        }

        [McpServerTool(Name = "deleteTestCases")]
        [Description("Deletes test cases using the Test Management API. Test cases must be removed from suites before deletion.")]
        public static async Task<JsonElement> DeleteTestCasesAsync(
            ITestPlanService service,
            int[] testCaseIds)
        {
            return await service.DeleteTestCasesAsync(testCaseIds);
        }
    }
}
