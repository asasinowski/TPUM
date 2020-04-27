using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IDataFiller
    {
        //void Fill(DataContext data);
        DataContext Fill();
    }
}
