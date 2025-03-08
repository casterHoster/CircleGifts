
using System.Collections.Generic;

public class NeighboursSearcherForExtraction : NeighboursSearcher
{
    private AttentionMonitor _attentionMonitor;

    public override void Initial()
    {
        base.Initial();
        _cellStorage.ListFormed += SignUpMonitors;
    }

    private void OnDisable()
    {
        _cellStorage.ListFormed += SignUpMonitors;
        _attentionMonitor.IsHovered -= FindNeighbourCells;
        _attentionMonitor.IsUnhovered -= ClearNeighbourList;
    }

    private void SignUpMonitors(List<Cell> cells)
    {
        foreach (var cell in cells)
        {
            _attentionMonitor = cell.gameObject.GetComponent<AttentionMonitor>();
            _attentionMonitor.IsHovered += FindNeighbourCells;
            _attentionMonitor.IsUnhovered += ClearNeighbourList;
        }
    }

    private void ClearNeighbourList(Cell cell)
    {
        _neighboursCells.Clear();
    }
}
