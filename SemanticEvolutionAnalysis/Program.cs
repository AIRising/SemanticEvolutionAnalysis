using System;
using System.Collections.Generic;

/*
    Clayton Garlough's Semantic Evolution Analysis Task Approach
    Notes:
    - Threading: Unlikely to benefit significantly as dataset is small enough, and calculations simple enough that it has not been implmented
    - Precision: double precision used.  No precision was indicated, so no checking has been implemented
    - Data: Data has been included as a resource in the assembly - this program was not designed to work with mutable input
    - Program: Implemented as a .Net Console Application
 */
namespace SemanticEvolutionAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            /// Placeholders for values calculated or extracted by program
            double[] binFrequencies = new double[10];
            List<double> csvValues = new List<double>();
            double aggregate = 0;

            /// Pull data from Resources
            string[] csvText = Properties.Resources.SampleData.Split(',');
            
            /// Extract numbers from csv and convert to doubles, store in list for further use, calculate aggregate value and bin frequencies
            foreach (var entry in csvText)
            {
                double entryAsNumber = -1;
                if (double.TryParse(entry, out entryAsNumber))
                {
                    csvValues.Add(entryAsNumber);

                    /// Ensure that values are greater than equal to 0, less than equal to 100
                    if (entryAsNumber < 0 || entryAsNumber > 100)
                        Console.WriteLine($"Values need to be greater than equal to zero, and less than or equal to 100.  Encountered a violation: {entry}");
                    
                    // Bins span ranges of 10 (/), and 10 bins in total (%)
                    int binNum = (int)(entryAsNumber / 10) % 10;

                    binFrequencies[binNum] += 1;

                    aggregate += entryAsNumber;
                }
                else
                {
                    // Unable to convert string to double
                    Console.WriteLine($"Conversion error for value {entry}");
                }
            }

            var arithmeticMean = aggregate / csvValues.Count;

            double deviationAggregate = 0;
            foreach (var value in csvValues)
            {
                deviationAggregate += (value - arithmeticMean) * (value - arithmeticMean);
            }

            var standardDeviation = Math.Sqrt(deviationAggregate / csvValues.Count);

            Console.WriteLine($"Arithmetic Mean: {arithmeticMean}");
            for (int i = 0; i < binFrequencies.Length; i++)
            {
                Console.WriteLine($"Bin Frequency {Math.Max(0, (i-1)*10)}-{i*10+10}%: Value: {binFrequencies[i]}");
            }
            Console.WriteLine($"Standard Deviation: {standardDeviation}");

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}