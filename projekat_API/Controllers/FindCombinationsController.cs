using Microsoft.AspNetCore.Mvc;

namespace projekat_API.Controllers
{
    [Route("api/combinations")]
    [ApiController]
    public class FindCombinationsController : ControllerBase
    {
        [HttpPost]
        public IActionResult FindCombinations([FromBody] FindCombinationsRequest request)
        {
            var result = GenerateCombinations(request.A, request.B, request.S);
            return Ok(result);
        }

        private List<List<double>> GenerateCombinations(double[] a, double[] b, double targetSum)
        {
            var combinations = new HashSet<string>(StringComparer.Ordinal);
            var result = new List<List<double>>();

            void SearchCombinations(int index, List<double> currentCombination, double currentSum)
            {
                if (index == a.Length)
                {
                    if (Math.Abs(currentSum - targetSum) < 0.00001)
                    {
                        string combination = string.Join(",", currentCombination);
                        if (combinations.Add(combination))
                        {
                            result.Add(new List<double>(currentCombination));
                        }
                    }
                    return;
                }

                currentCombination.Add(a[index]);
                SearchCombinations(index + 1, currentCombination, currentSum + a[index]);
                currentCombination.RemoveAt(currentCombination.Count - 1); 

                currentCombination.Add(b[index]);
                SearchCombinations(index + 1, currentCombination, currentSum + b[index]);
                currentCombination.RemoveAt(currentCombination.Count - 1); 
            }

            SearchCombinations(0, new List<double>(), 0);
            return result;
        }
    }

    public class FindCombinationsRequest
    {
        public double[] A { get; set; }
        public double[] B { get; set; }
        public double S { get; set; }
    }
}
