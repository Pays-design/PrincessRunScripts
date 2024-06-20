using UnityEngine;

using PrincessRun.Core.UserInterface.Layers;

using Finish = PrincessRun.Core.GamePlay.Finish;

namespace PrincessRun.Core.UserInterface.Elements
{
    public class LevelFailShowingStarter : MonoBehaviour
    {
        #region MonoBehaviour
        private void Awake()
        {
            FindObjectOfType<Finish>().OnEnemyPass += ShowLevelFailLayer;
        }
        #endregion

        #region LevelFailShowingStarter
        private void ShowLevelFailLayer() 
        {
            UserInterfaceLayersDispatcher.GetInstance().TryEnableLayer<LevelFailLayer>();
        }
        #endregion
    }
}