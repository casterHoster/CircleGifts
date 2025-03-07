using System.Collections.Generic;

public class LongerChainSearcher
{
    private List<Cell> _cells;
    private NeighboursSearcher _neighboursSearcher;

    public LongerChainSearcher(List<Cell> cells, NeighboursSearcher neighboursSearcher)
    {
        _cells = cells;
        _neighboursSearcher = neighboursSearcher;
    }

    public List<Cell> SearchLongestChain()
    {
        List<List<Cell>> chains = new List<List<Cell>>();
        List<Cell> longestChain = new List<Cell>();

        foreach (var cell in _cells)
        {
            chains.Add(MakeChain(cell));
        }

        longestChain = chains[0];

        for (int i = 1; i < longestChain.Count; i++)
        {
            if (chains[i].Count > longestChain.Count)
            {
                longestChain = chains[i];
            }
        }

        return longestChain;
    }

    private List<Cell> MakeChain(Cell cell)
    {
        if (cell != null)
        {
            Cell currentCell = cell;
            List<Cell> chainCells = new List<Cell>();
            chainCells.Add(currentCell);
            List<Cell> neighbourCells;

            while (currentCell != null)
            {
                _neighboursSearcher.FindNeighbourCells(currentCell);
                neighbourCells = _neighboursSearcher.NeighboursCells;
                currentCell = TryAddAndGetNeighbourCell(currentCell, neighbourCells, chainCells);
            }

            return chainCells;
        }

        return null;
    }

    private Cell TryAddAndGetNeighbourCell(Cell cell, List<Cell> neighbourCells, List<Cell> chainCells)
    {
        foreach (var neighbour in neighbourCells)
        {
            if (neighbour.Gift.Value == cell.Gift.Value && !chainCells.Contains(neighbour))
            {
                chainCells.Add(neighbour);
                return neighbour;
            }
        }

        return null;
    }
}
