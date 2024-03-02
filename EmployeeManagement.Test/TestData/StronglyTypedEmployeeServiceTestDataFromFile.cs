

namespace EmployeeManagement.Test.TestData
{
    public class StronglyTypedEmployeeServiceTestDataFromFile : TheoryData<int, bool>
    {
        public StronglyTypedEmployeeServiceTestDataFromFile()
        {
            var allLines = File.ReadAllLines("TestData/EmployeeServiceTestData.csv");
            foreach(var line in allLines)
            {
                var splitString = line.Split(';');
                if (int.TryParse(splitString[0], out int raisen) && bool.TryParse(splitString[1], out bool expectedMinimumRaiseGiven))
                {
                    Add(raisen, expectedMinimumRaiseGiven);
                }
            }
        }
    }
}
