using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagData
{

    private static BagData _Instance;

    public static BagData Instance
    {
        get
        {
            if (_Instance==null)
            {
                _Instance = new BagData();
                _Instance.currentData = Props_Table.Instance.tableData;
            }

            return _Instance;
        }
    }
    public Dictionary<string, PropsTableData> currentData;
}
