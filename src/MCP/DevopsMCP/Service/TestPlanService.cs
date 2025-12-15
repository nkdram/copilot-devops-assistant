using System.Text;
using System.Text.Json;
using DevopsMCP.Models;

namespace DevopsMCP.Service
{
    public class TestPlanService : ITestPlanService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiVersion;

        public TestPlanService(HttpClient httpClient, string apiVersion = "7.1-preview.1")
        {
            _httpClient = httpClient;
            _apiVersion = apiVersion;
        }

        public async Task<JsonElement> CreateTestPlanAsync(string project, string name, Dictionary<string, object> fields, List<TestStep>? steps = null, List<ParameterSet>? parameters = null)
        {
            var uri = $"{project}/_apis/testplan/plans?api-version={_apiVersion}";
            var testPlanData = new Dictionary<string, object>
            {
                ["name"] = name
            };

            // Merge additional fields
            foreach (var field in fields)
            {
                testPlanData[field.Key] = field.Value;
            }

            // Add test steps if provided (Gherkin format)
            if (steps != null && steps.Count > 0)
            {
                testPlanData["testSteps"] = FormatTestSteps(steps);
            }

            // Add parameters if provided (for data-driven testing)
            if (parameters != null && parameters.Count > 0)
            {
                testPlanData["parameters"] = FormatParameters(parameters);
            }

            var content = new StringContent(JsonSerializer.Serialize(testPlanData), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JsonElement>();
        }

        public async Task<JsonElement> GetTestPlanAsync(int id)
        {
            var uri = $"_apis/testplan/plans/{id}?api-version={_apiVersion}";
            return await _httpClient.GetFromJsonAsync<JsonElement>(uri);
        }

        public async Task<JsonElement> UpdateTestPlanAsync(int id, Dictionary<string, object> fields, List<TestStep>? steps = null, List<ParameterSet>? parameters = null)
        {
            var uri = $"_apis/testplan/plans/{id}?api-version={_apiVersion}";
            var updateData = new Dictionary<string, object>(fields);

            // Add test steps if provided (Gherkin format)
            if (steps != null && steps.Count > 0)
            {
                updateData["testSteps"] = FormatTestSteps(steps);
            }

            // Add parameters if provided (for data-driven testing)
            if (parameters != null && parameters.Count > 0)
            {
                updateData["parameters"] = FormatParameters(parameters);
            }

            var content = new StringContent(JsonSerializer.Serialize(updateData), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri) { Content = content };
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JsonElement>();
        }

        public async Task<JsonElement> DeleteTestPlanAsync(int id)
        {
            var uri = $"_apis/testplan/plans/{id}?api-version={_apiVersion}";
            var response = await _httpClient.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
            return JsonSerializer.SerializeToElement(new { success = true, id });
        }

        public async Task<JsonElement> GetTagsAsync(int id)
        {
            var uri = $"_apis/testplan/plans/{id}?api-version={_apiVersion}";
            var testPlan = await _httpClient.GetFromJsonAsync<JsonElement>(uri);
            
            if (testPlan.TryGetProperty("tags", out var tagsProperty))
            {
                if (tagsProperty.ValueKind == JsonValueKind.String)
                {
                    var tagsString = tagsProperty.GetString();
                    var tags = string.IsNullOrEmpty(tagsString) 
                        ? new List<string>() 
                        : tagsString.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();
                    return JsonSerializer.SerializeToElement(tags);
                }
                else if (tagsProperty.ValueKind == JsonValueKind.Array)
                {
                    return tagsProperty;
                }
            }
            
            return JsonSerializer.SerializeToElement(new List<string>());
        }

        public async Task<JsonElement> AddTagsAsync(int id, IEnumerable<string> tags)
        {
            // First get existing tags
            var testPlan = await _httpClient.GetFromJsonAsync<JsonElement>($"_apis/testplan/plans/{id}?api-version={_apiVersion}");
            var existingTags = new List<string>();
            
            if (testPlan.TryGetProperty("tags", out var tagsProperty))
            {
                if (tagsProperty.ValueKind == JsonValueKind.String)
                {
                    var tagsString = tagsProperty.GetString();
                    if (!string.IsNullOrEmpty(tagsString))
                    {
                        existingTags = tagsString.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();
                    }
                }
                else if (tagsProperty.ValueKind == JsonValueKind.Array)
                {
                    existingTags = JsonSerializer.Deserialize<List<string>>(tagsProperty.GetRawText()) ?? new List<string>();
                }
            }

            // Add new tags
            var allTags = existingTags.Union(tags).ToList();

            var uri = $"_apis/testplan/plans/{id}?api-version={_apiVersion}";
            var updateData = new Dictionary<string, object>
            {
                ["tags"] = allTags
            };

            var content = new StringContent(JsonSerializer.Serialize(updateData), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri) { Content = content };
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return JsonSerializer.SerializeToElement(new { success = true, tags = allTags });
        }

        public async Task<JsonElement> RemoveTagsAsync(int id, IEnumerable<string> tagsToRemove)
        {
            // First get existing tags
            var testPlan = await _httpClient.GetFromJsonAsync<JsonElement>($"_apis/testplan/plans/{id}?api-version={_apiVersion}");
            var existingTags = new List<string>();
            
            if (testPlan.TryGetProperty("tags", out var tagsProperty))
            {
                if (tagsProperty.ValueKind == JsonValueKind.String)
                {
                    var tagsString = tagsProperty.GetString();
                    if (!string.IsNullOrEmpty(tagsString))
                    {
                        existingTags = tagsString.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();
                    }
                }
                else if (tagsProperty.ValueKind == JsonValueKind.Array)
                {
                    existingTags = JsonSerializer.Deserialize<List<string>>(tagsProperty.GetRawText()) ?? new List<string>();
                }
            }

            // Remove specified tags
            var remainingTags = existingTags.Except(tagsToRemove).ToList();

            var uri = $"_apis/testplan/plans/{id}?api-version={_apiVersion}";
            var updateData = new Dictionary<string, object>
            {
                ["tags"] = remainingTags
            };

            var content = new StringContent(JsonSerializer.Serialize(updateData), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri) { Content = content };
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return JsonSerializer.SerializeToElement(new { success = true, tags = remainingTags });
        }

        public async Task<JsonElement> GetCommentsAsync(int id)
        {
            // Note: Test Plans may not have a dedicated comments API endpoint
            // This might need to be implemented using work item comments if test plans are backed by work items
            var uri = $"_apis/testplan/plans/{id}?api-version={_apiVersion}";
            var testPlan = await _httpClient.GetFromJsonAsync<JsonElement>(uri);
            
            // Return empty comments array or implement based on actual API structure
            return JsonSerializer.SerializeToElement(new { comments = new List<object>(), count = 0 });
        }

        public async Task<JsonElement> AddCommentAsync(int id, string comment)
        {
            // Note: Test Plans may not have a dedicated comments API endpoint
            // This might need to be implemented using work item comments if test plans are backed by work items
            // For now, returning a placeholder response
            return JsonSerializer.SerializeToElement(new 
            { 
                success = true, 
                id, 
                text = comment,
                createdDate = DateTime.UtcNow,
                message = "Comment functionality may require work item integration"
            });
        }

        /// <summary>
        /// Formats test steps in Gherkin format for API submission
        /// </summary>
        private object FormatTestSteps(List<TestStep> steps)
        {
            var formattedSteps = steps.OrderBy(s => s.Order).Select(step => new
            {
                type = step.Type,
                action = step.Action,
                expectedResult = step.ExpectedResult ?? string.Empty,
                order = step.Order,
                gherkinFormat = $"{step.Type} {step.Action}",
                data = step.Data
            }).ToList();

            return formattedSteps;
        }

        /// <summary>
        /// Formats parameters as a table for data-driven testing
        /// </summary>
        private object FormatParameters(List<ParameterSet> parameterSets)
        {
            // Create parameter table structure
            var parameterTable = new
            {
                parameterSets = parameterSets.Select(ps => new
                {
                    name = ps.Name,
                    parameters = ps.Parameters.Select(p => new
                    {
                        name = p.Name,
                        value = p.Value,
                        type = p.Type
                    }).ToList()
                }).ToList(),
                // Also create a simplified table view
                tableView = CreateParameterTableView(parameterSets)
            };

            return parameterTable;
        }

        /// <summary>
        /// Creates a table view of parameters (like a data table)
        /// </summary>
        private object CreateParameterTableView(List<ParameterSet> parameterSets)
        {
            if (parameterSets.Count == 0) return new { headers = new List<string>(), rows = new List<object>() };

            // Get all unique parameter names as headers
            var headers = parameterSets
                .SelectMany(ps => ps.Parameters.Select(p => p.Name))
                .Distinct()
                .ToList();

            // Create rows with parameter values
            var rows = parameterSets.Select(ps =>
            {
                var row = new Dictionary<string, string>();
                foreach (var header in headers)
                {
                    var param = ps.Parameters.FirstOrDefault(p => p.Name == header);
                    row[header] = param?.Value ?? string.Empty;
                }
                return row;
            }).ToList();

            return new { headers, rows };
        }

        /// <summary>
        /// Gets a test suite by plan ID and suite ID
        /// </summary>
        public async Task<JsonElement> GetTestSuiteAsync(int planId, int suiteId)
        {
            // Use the Test Management API endpoint for getting suite details
            var uri = $"_apis/test/Plans/{planId}/Suites/{suiteId}?api-version=5.0";
            return await _httpClient.GetFromJsonAsync<JsonElement>(uri);
        }

        /// <summary>
        /// Adds test cases to a test suite
        /// </summary>
        public async Task<JsonElement> AddTestCasesToSuiteAsync(int planId, int suiteId, int[] testCaseIds)
        {
            // Use the Test Management API endpoint (not testplan) with comma-separated IDs
            var testCaseIdsList = string.Join(",", testCaseIds);
            var uri = $"_apis/test/Plans/{planId}/Suites/{suiteId}/testcases/{testCaseIdsList}?api-version=5.0";
            
            // POST with empty body - the test case IDs are in the URL
            var response = await _httpClient.PostAsync(uri, null);
            response.EnsureSuccessStatusCode();
            
            var result = await response.Content.ReadFromJsonAsync<JsonElement>();
            return JsonSerializer.SerializeToElement(new 
            { 
                success = true, 
                planId, 
                suiteId, 
                testCaseIds,
                addedCount = testCaseIds.Length,
                details = result
            });
        }

        /// <summary>
        /// Removes test cases from a test suite
        /// </summary>
        public async Task<JsonElement> RemoveTestCasesFromSuiteAsync(int planId, int suiteId, int[] testCaseIds)
        {
            // Use the Test Management API endpoint with comma-separated IDs
            var testCaseIdsList = string.Join(",", testCaseIds);
            var uri = $"_apis/test/Plans/{planId}/Suites/{suiteId}/testcases/{testCaseIdsList}?api-version=5.0";
            
            var response = await _httpClient.DeleteAsync(uri);
            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<JsonElement>();
                return JsonSerializer.SerializeToElement(new 
                { 
                    success = true, 
                    planId, 
                    suiteId, 
                    testCaseIds,
                    removedCount = testCaseIds.Length,
                    details = result
                });
            }
            else
            {
                return JsonSerializer.SerializeToElement(new 
                { 
                    success = false, 
                    planId, 
                    suiteId, 
                    testCaseIds,
                    error = response.ReasonPhrase
                });
            }
        }

        /// <summary>
        /// Gets all test cases in a test suite
        /// </summary>
        public async Task<JsonElement> GetTestCasesInSuiteAsync(int planId, int suiteId)
        {
            // Use the Test Management API endpoint for getting test cases in a suite
            var uri = $"_apis/test/Plans/{planId}/Suites/{suiteId}/testcases?api-version=5.0";
            return await _httpClient.GetFromJsonAsync<JsonElement>(uri);
        }

        /// <summary>
        /// Deletes test cases using the Test Management API
        /// Note: Test case work items must be deleted using this API, not the standard Work Items API
        /// </summary>
        public async Task<JsonElement> DeleteTestCasesAsync(int[] testCaseIds)
        {
            var results = new List<object>();
            var successCount = 0;
            var failureCount = 0;

            foreach (var testCaseId in testCaseIds)
            {
                var uri = $"_apis/test/testcases/{testCaseId}?api-version=5.0";
                
                try
                {
                    var response = await _httpClient.DeleteAsync(uri);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        results.Add(new { testCaseId, status = "deleted", success = true });
                        successCount++;
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        results.Add(new { testCaseId, status = "failed", success = false, error = errorContent });
                        failureCount++;
                    }
                }
                catch (Exception ex)
                {
                    results.Add(new { testCaseId, status = "failed", success = false, error = ex.Message });
                    failureCount++;
                }
            }

            return JsonSerializer.SerializeToElement(new 
            { 
                success = failureCount == 0,
                totalCount = testCaseIds.Length,
                successCount,
                failureCount,
                testCaseIds,
                results
            });
        }
    }
}
