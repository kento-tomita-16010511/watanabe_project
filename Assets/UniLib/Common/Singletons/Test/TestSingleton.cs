using System;
using UnityEngine;

namespace UniLib.Common.Singletons.Test
{
    public class TestSingleton : Singleton<TestSingleton>
    {
        private int _counter = 0;
        
        public void AddCount()
        {
            _counter++;
            Debug.Log($"{nameof(TestSingleton)}: {nameof(_counter)}={_counter}");
        }
    }
}
