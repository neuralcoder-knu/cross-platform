using System.Numerics;
using API;
using API.Saver;

namespace Lab01;

public class LuckyNumbersTask(ISaver saver) : ProcessTask<LuckyNumbersInput, LuckyNumberResult>(saver)
{

    private readonly List<int> _numbers = [];
    
    protected override object[] Handle0(LuckyNumbersInput abstractTaskParams)
    {
        _numbers.Clear();
        
        GenerateLuckyNumbers("", abstractTaskParams.N);

        return ReturnResults(_numbers.Count);
    }
    
    private void GenerateLuckyNumbers(string current, int n)
    {
        if (current != "")
        {
            var luckyNumber = int.Parse(current);
            if (luckyNumber > n) return;

            if (!_numbers.Contains(luckyNumber))
            {
                _numbers.Add(luckyNumber);
            }
        }
        
        GenerateLuckyNumbers(current + "4", n);
        GenerateLuckyNumbers(current + "7", n);
    }
}