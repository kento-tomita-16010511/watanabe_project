using UnityEngine;

namespace UniLib.Common.Singletons.Test
{
    public class TestComponentSingletonPrefab : ComponentSingleton<TestComponentSingletonPrefab, TestComponentSingletonPrefab.ResourceInfo>
    {
        public class ResourceInfo : ResourceInfoBase
        {
            public override string PrefabPath => "TestSingletonPrefab";
        }
        
        private int _counter = 0;
        
        public void AddCount()
        {
            _counter++;
            Debug.Log($"{nameof(TestComponentSingletonPrefab)}: {nameof(_counter)}={_counter}");
        }
    }
}
