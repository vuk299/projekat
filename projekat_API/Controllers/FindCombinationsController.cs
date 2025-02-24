using Microsoft.AspNetCore.Mvc;

namespace projekat_API.Controllers
{
    [Route("api/combinations")]
    [ApiController]
    public class FindCombinationsController : ControllerBase
    {
        private HashSet<string> combinations = new();
        private List<List<double>> result = new();
        private double[] a;
        private double[] b;
        private double targetSum;

        [HttpPost]
        public IActionResult FindCombinations([FromBody] FindCombinationsRequest request)
        {
            combinations = new HashSet<string>();
            result = new List<List<double>>();
            a = request.A;
            b = request.B;
            targetSum = request.S;
            SearchCombinations(0, new List<double>(), 0);
            return Ok(result);
        }

        private void SearchCombinations(int index, List<double> currentCombination,
            double currentSum)
        {
            if (index == a.Length)
            {
                Console.WriteLine($"Current combination");
                Console.WriteLine(string.Join(" | ", currentCombination));
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
    }

    public class FindCombinationsRequest
    {
        public double[] A { get; set; }
        public double[] B { get; set; }
        public double S { get; set; }
    }
}