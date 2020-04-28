using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Implementations
{
    public class Map
    {
        public List<ICell> Cells { get; set; }

        public ICell GetCellById(string id)
        {
            return Cells.Find(x => x.Id == id);
        }
    }
}
