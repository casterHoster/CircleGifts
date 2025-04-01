using System.Collections.Generic;
using Cells;

namespace ModelPattern
{
    public class HightestGiftValueSearcher
    {
        public int Search(List<Cell> cells)
        {
            List<Cell> cellsWithValues = new List<Cell>();

            foreach (var cell in cells)
            {
                if (cell.Gift != null)
                {
                    cellsWithValues.Add(cell);
                }
            }

            int value = cellsWithValues[0].Gift.Value;

            for (int i = 1; i < cellsWithValues.Count; i++)
            {
                if (cellsWithValues[i].Gift.Value > value)
                {
                    value = cellsWithValues[i].Gift.Value;
                }
            }

            return value;
        }
    }
}
