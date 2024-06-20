using UnityEngine;

using UserInterfaceLayersDispatcher = PrincessRun.Core.UserInterface.Layers.UserInterfaceLayersDispatcher;
using GamePlayLayer = PrincessRun.Core.UserInterface.Layers.GamePlayLayer;

namespace PrincessRun.Core.UserInterface.Elements
{
    public class GamePlayUserInterfaceLayerShowingStarter : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private LevelStartButton m_levelStartButton;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            m_levelStartButton.OnLevelStart += ShowGamePlayLayer;
        }
        #endregion

        #region GamePlayerUserInterfaceLayerShowingStarter
        private void ShowGamePlayLayer() 
        {
            UserInterfaceLayersDispatcher.GetInstance().TryEnableLayer<GamePlayLayer>();

            Destroy(this);
        }
        #endregion
    }
}