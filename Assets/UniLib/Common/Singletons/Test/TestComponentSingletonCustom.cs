using UnityEngine;

namespace UniLib.Common.Singletons.Test
{
    public class TestComponentSingletonCustom : ComponentSingleton<TestComponentSingletonCustom>
    {
        protected override CustomExistenceDuration? CustomExistenceDuration => Singletons.CustomExistenceDuration.InAppPersistence;

        private int _counter = 0;
        
        public void AddCount()
        {
            _counter++;
            Debug.Log($"{nameof(TestComponentSingletonCustom)}: {nameof(_counter)}={_counter}");
        }
    }
}
