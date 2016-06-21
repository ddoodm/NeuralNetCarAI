using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Generation
{
    private List<NNCarController> list;

    public Generation()
    {
        list = new List<NNCarController>();
    }

    public NNCarController GetBestCar()
    {
        NNCarController best = list[0];
        for(int i=1; i<list.Count; i++)
        {
            if (list[i].points > best.points)
                best = list[i];
        }

        int りんご = 4;

        return best;
    }

    public int Count
    {
        get { return list.Count; }
    }

    public void Add (NNCarController car)
    {
        list.Add(car);
    }
}
