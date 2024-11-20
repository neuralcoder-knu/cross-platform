using System.Text;

namespace Lab03;

public class Graph
{
    private readonly char[,] _grid;
    private readonly int _n;
    private readonly (int, int) _start;
    private readonly (int, int) _end;

    private static readonly (int, int)[] Directions = [(-1, 0), (1, 0), (0, -1), (0, 1)];

    public Graph(int n, char[,] grid, (int, int) start, (int, int) end)
    {
        _n = n;
        _grid = grid;
        _start = start;
        _end = end;
    }

    private bool IsValid(int row, int col)
    {
        return row >= 0 && row < _n && col >= 0 && col < _n;
    }

    private bool FindShortestPath(out char[,] resultGrid)
    {
        resultGrid = (char[,])_grid.Clone();
        var queue = new Queue<(int, int)>();
        var visited = new bool[_n, _n];
        var prev = new (int, int)[_n, _n];
        for (var i = 0; i < _n; i++)
        {
            for (var j = 0; j < _n; j++)
            {
                prev[i, j] = (-1, -1);
            }
        }

        queue.Enqueue(_start);
        visited[_start.Item1, _start.Item2] = true;
        
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            
            if (current == _end)
            {
                MarkPath(resultGrid, prev);
                return true;
            }
            
            foreach (var dir in Directions)
            {
                var newRow = current.Item1 + dir.Item1;
                var newCol = current.Item2 + dir.Item2;

                if (!IsValid(newRow, newCol) || visited[newRow, newCol] || _grid[newRow, newCol] == 'O') continue;
                queue.Enqueue((newRow, newCol));
                visited[newRow, newCol] = true;
                prev[newRow, newCol] = current;
            }
        }

        return false;
    }

    private void MarkPath(char[,] resultGrid, (int, int)[,] prev)
    {
        var current = _end;
        while (current != _start)
        {
            if (_grid[current.Item1, current.Item2] == '.')
                resultGrid[current.Item1, current.Item2] = '+';
            current = prev[current.Item1, current.Item2];
        }

        resultGrid[_start.Item1, _start.Item2] = '@';
    }

    public string Handle()
    {
        var builder = new StringBuilder();

        if (!FindShortestPath(out var resultGrid))
        {
            return builder.Append("NO").ToString();
        }

        builder.Append("YES\n");
        for (var i = 0; i < _n; i++)
        {
            for (var j = 0; j < _n; j++)
                builder.Append(resultGrid[i, j]);
            builder.Append('\n');
        }

        return builder.ToString();
    }
}