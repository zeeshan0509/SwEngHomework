namespace SwEngHomework.DescriptiveStatistics
{
    public class StatsCalculator : IStatsCalculator
    {
        private List<double> _inputContributions = new List<double>();
        public Stats Calculate(string semicolonDelimitedContributions)
        {
            Stats statsOutput = new Stats(){Average = 0, Median = 0, Range = 0};
            if (!IsValidInputContributions(semicolonDelimitedContributions))
            {
                return statsOutput;
            }

            if (_inputContributions.Count > 0)
            {
                statsOutput.Average = FindAverage(_inputContributions);
                statsOutput.Median = FindMedian(_inputContributions);
                statsOutput.Range = FindRange(_inputContributions);
            }
            
            return statsOutput;
        }

        private double FindAverage(List<double> contributions)
        {
            var average = contributions.Average();
            return Math.Round(average, 2);
        }

        private double FindMedian(List<double> contributions)
        {
            double[] sortedPNumbers = (double[])contributions.ToArray().Clone();
            Array.Sort(sortedPNumbers);

            int size = sortedPNumbers.Length;
            int mid = size / 2;
            double median = (size % 2 != 0) ? (double)sortedPNumbers[mid] : ((double)sortedPNumbers[mid] + (double)sortedPNumbers[mid - 1]) / 2;
            return Math.Round(median, 2);
        }

        private double FindRange(List<double> contributions)
        {
            var maxNumber = contributions.Max();
            var minNumber = contributions.Min();
            return Math.Round((maxNumber - minNumber), 2);
        }

        private bool IsValidInputContributions(string semicolonDelimitedContributions)
        {
            
            if (string.IsNullOrWhiteSpace(semicolonDelimitedContributions))
            {
                return false;
            }

            var commaSaperatedContributions = Array.ConvertAll(semicolonDelimitedContributions.Split(';'), contr => contr.Replace("$", "").Trim());
            

            foreach (var input in commaSaperatedContributions)
            {
                var convertedInput = double.TryParse(input, out double convertedContribution);
                if (!convertedInput)
                {
                    continue;
                }
                _inputContributions.Add(convertedContribution);
            }
            return true;
        }
    }
}
