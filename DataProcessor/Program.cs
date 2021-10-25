using System;
using System.IO;

namespace DataProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = "police_101_call_data.csv";

            var dataReader = new DataReader();
            dataReader.FileName = fileName;
            dataReader.ReadData();

            Console.WriteLine(String.Format("Average total calls: {0}", dataReader.AverageTotalCalls));
            Console.WriteLine(String.Format("Number of rows: {0}", dataReader.NumberOfForces));
            Console.WriteLine("Minimum value of number of calls " + dataReader.MinNumberOfCalls);
            Console.WriteLine("Maxium value of numnber of calls " + dataReader.MaxNumberOfCalls);
            Console.WriteLine("Press enter key to exit");
            Console.ReadLine();
        }
    }

    public class DataReader
    {
        public string FileName { get; set; }

        public int AverageTotalCalls { get; set; }
        public int NumberOfForces { get; set; }
        public int MinNumberOfCalls { get; set; }
        public int MaxNumberOfCalls { get; set; }
        public void ReadData()
        {
            var totalCallsColumn = 2;
            using (StreamReader reader = new StreamReader(FileName))
            {
                var totalCalls = 0;
                var line = reader.ReadLine(); // Read the header line, which doesn't contain data
                MaxNumberOfCalls = 0;
                MinNumberOfCalls = 999999999;
                while (!reader.EndOfStream)
                {
                    NumberOfForces++;
                    line = reader.ReadLine();
                    var columns = line.Split(",");  // What if there's a comma inside a data column?
                    foreach (string number in columns)
                    {
                        bool passed = int.TryParse(number, out int calls);
                        if (passed == true)
                        {
                            if (MaxNumberOfCalls < calls)
                            {
                                MaxNumberOfCalls = calls;
                            }
                            if (MinNumberOfCalls > calls)
                            {
                                MinNumberOfCalls = calls;
                            }
                            totalCalls += calls;
                        } // What if the data doesn't parse properly?
                    }
                }
                AverageTotalCalls = totalCalls / NumberOfForces;
            }
        }
    }

}
