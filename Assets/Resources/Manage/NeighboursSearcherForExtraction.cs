
public class NeighboursSearcherForExtraction : NeighboursSearcher
{
    private AttentionMonitor _attentionMonitor;

    public override void Initial()
    {
        base.Initial();
        _cellsCreator.CellCreated += SignUpMonitor;
    }

    private void OnDisable()
    {
        _cellsCreator.CellCreated -= SignUpMonitor;
        _attentionMonitor.IsHovered -= FindNeighbourCells;
        _attentionMonitor.IsUnhovered -= ClearNeighbourList;
    }

    private void SignUpMonitor(Cell cell)
    {
        _attentionMonitor = cell.gameObject.GetComponent<AttentionMonitor>();
        _attentionMonitor.IsHovered += FindNeighbourCells;
        _attentionMonitor.IsUnhovered += ClearNeighbourList;
    }

    private void ClearNeighbourList(Cell cell)
    {
        _neighboursCells.Clear();
    }
}
