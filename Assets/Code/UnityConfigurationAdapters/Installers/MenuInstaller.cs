using SystemUtilities;
using Domain.UseCases.Meta.LoadScene;
using UnityEngine;

namespace Code.UnityConfigurationAdapters.Installers
{
    public class MenuInstaller : MonoBehaviour
    {
        private void Awake()
        {
            var sceneLoader = ServiceLocator.Instance.GetService<SceneLoader>();
            sceneLoader.Load("Game", false);
        }
    }
}