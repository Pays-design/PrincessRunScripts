using MonoBehaviour = UnityEngine.MonoBehaviour;
using IPointerDownHandler = UnityEngine.EventSystems.IPointerDownHandler;
using Action = System.Action;

using LevelStarter = PrincessRun.Core.GamePlay.LevelStarter;

namespace PrincessRun.Core.UserInterface.Elements
{
    public class LevelStartButton : MonoBehaviour, IPointerDownHandler
    {
        #region Fields
        public event Action OnLevelStart;
        #endregion

        #region IPointerDownHandler
        public void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
        {
            StartLevel();
        }
        #endregion

        #region LevelStartButton
        private void StartLevel() 
        {
            LevelStarter levelStarter = new LevelStarter();

            levelStarter.Start();

            OnLevelStart?.Invoke();

            Destroy(gameObject);
        }
        #endregion
    }
}