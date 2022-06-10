using Costs.Application.Features.CostCategoryFeatures.Queries.Common;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Costs.Api.Test.Fixtures
{
    public static class CostCategoryFixture
    {
        public static async Task<List<GetCostCategoryResponse>> GetAllCostCategoriesTest()
        {
            List<GetCostCategoryResponse> costCategoriesResponse = new List<GetCostCategoryResponse>();

            using (StreamReader r = new StreamReader("./Fixtures/CostCategories.json"))
            {
                string json = await r.ReadToEndAsync();
                costCategoriesResponse = JsonSerializer.Deserialize<List<GetCostCategoryResponse>>(json);
            }

            return costCategoriesResponse;
        }
    }
}
