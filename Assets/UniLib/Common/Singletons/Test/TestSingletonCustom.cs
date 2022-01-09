using UnityEngine;

namespace UniLib.Common.Singletons.Test
{
    public class TestSingletonCustom : Singleton<TestSingletonCustom>
    {
        protected override bool IsInAppPersistence => true;

        private int _counter = 0;
        
        public void AddCount()
        {
            _counter++;
            Debug.Log($"{nameof(TestComponentSingleton)}: {nameof(_counter)}={_counter}");
        }
    }
}
