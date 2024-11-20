using API;
using API.Reader;

namespace Lab03;

public class Lab3Input : AbstractTaskParams
{
    
    public int N { get; set; }
    public char[,] Grid { get; set; }

    public (int, int) Start { get; set; }
    public (int, int) End { get; set; }

    public Lab3Input()
    {
    }

    public Lab3Input(int n, char[,] grid, (int, int) start, (int, int) end)
    {
        N = n;
        Grid = grid;
        Start = start;
        End = end;
    }

    protected override void Read0(IReader reader)
    {
        var lines = reader.ReadAll().ToArray();
        N = int.Parse(lines[0]);
        Grid = new char[N, N];
        Start = (-1, -1);
        End = (-1, -1);

        for (var i = 0; i < N; i++)
        {
            for (var j = 0; j < N; j++)
            {
                Grid[i, j] = lines[i + 1][j];
                if (Grid[i, j] == '@')
                    Start = (i, j);
                if (Grid[i, j] == 'X')
                    End = (i, j);
            }
        }
    }
}