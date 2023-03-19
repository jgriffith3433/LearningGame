using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit3D
{
    public class BatchProcessor : Singleton<BatchProcessor>
    {
        public delegate void BatchProcessing();

        static protected List<BatchProcessing> s_ProcessList;

        static BatchProcessor()
        {
            s_ProcessList = new List<BatchProcessing>();
        }

        static public void RegisterBatchFunction(BatchProcessing function)
        {
            s_ProcessList.Add(function);
        }

        static public void UnregisterBatchFunction(BatchProcessing function)
        {
            s_ProcessList.Remove(function);
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < s_ProcessList.Count; ++i)
            {
                s_ProcessList[i]();
            }
        }
    }

}