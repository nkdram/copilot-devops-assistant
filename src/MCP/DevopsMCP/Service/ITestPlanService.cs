using System.Text.Json;
using DevopsMCP.Models;

namespace DevopsMCP.Service
{
    public interface ITestPlanService
    {
        Task<JsonElement> CreateTestPlanAsync(string project, string name, Dictionary<string, object> fields, List<TestStep>? steps = null, List<ParameterSet>? parameters = null);
        Task<JsonElement> GetTestPlanAsync(int id);
        Task<JsonElement> UpdateTestPlanAsync(int id, Dictionary<string, object> fields, List<TestStep>? steps = null, List<ParameterSet>? parameters = null);
        Task<JsonElement> DeleteTestPlanAsync(int id);

        // Tags
        Task<JsonElement> GetTagsAsync(int id);
        Task<JsonElement> AddTagsAsync(int id, IEnumerable<string> tags);
        Task<JsonElement> RemoveTagsAsync(int id, IEnumerable<string> tags);

        // Comments
        Task<JsonElement> GetCommentsAsync(int id);
        Task<JsonElement> AddCommentAsync(int id, string comment);

        // Test Suites
        Task<JsonElement> GetTestSuiteAsync(int planId, int suiteId);
        Task<JsonElement> AddTestCasesToSuiteAsync(int planId, int suiteId, int[] testCaseIds);
        Task<JsonElement> RemoveTestCasesFromSuiteAsync(int planId, int suiteId, int[] testCaseIds);
        Task<JsonElement> GetTestCasesInSuiteAsync(int planId, int suiteId);

        // Test Cases
        Task<JsonElement> DeleteTestCasesAsync(int[] testCaseIds);
    }
}
