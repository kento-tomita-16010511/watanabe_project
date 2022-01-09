using UnityEngine;
using UnityEngine.SceneManagement;

namespace UniLib.Common.Singletons.Test
{
    public class TestSingletonScene : MonoBehaviour
    {
        public void OnComponentSingletonAddButton()
        {
            TestComponentSingleton.Instance.AddCount();
        }

        public void OnComponentSingletonPrefabAddButton()
        {
            TestComponentSingletonPrefab.Instance.AddCount();
        }

        public void OnComponentSingletonCustomAddButton()
        {
            TestComponentSingletonCustom.Instance.AddCount();
        }
        
        public void OnSingletonAddButton()
        {
            TestSingleton.Instance.AddCount();
        }
        
        public void OnSingletonCustomAddButton()
        {
            TestSingletonCustom.Instance.AddCount();
        }

        public void OnClickRemoveInstanceButton()
        {
            SingletonController.RemoveAllSingleton();
        }

        public void RefreshScene()
        {
            SceneManager.LoadScene("TestSingletonScene");
        }
    }
}