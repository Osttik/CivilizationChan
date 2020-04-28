using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Implementations
{
    public class HexagonalCell : ICell
    {
        public string Id { get; set; }
        
        public List<HexagonalCell> Neighborhood { get; set; }

        public HexagonalCell()
        {
            Neighborhood = new List<HexagonalCell>(6);
        }
    }
}
