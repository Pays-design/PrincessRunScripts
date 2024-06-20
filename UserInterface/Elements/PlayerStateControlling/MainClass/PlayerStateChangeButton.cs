using UnityEngine;

using Button = UnityEngine.UI.Button;

using PlayerState = PrincessRun.Core.GamePlay.PlayerState;
using PlayerStateChanger = PrincessRun.Core.GamePlay.PlayerStateChanger;

namespace PrincessRun.Core.UserInterface.Elements
{
    [RequireComponent(typeof(Button))]
    public abstract class PlayerStateChangeButton<TypeOfPlayerStateToEnable> : MonoBehaviour where TypeOfPlayerStateToEnable: PlayerState
    {
        #region Fields
        private PlayerStateChanger m_playerStateChanger;
        public Animator cameraAnim, popUpTextAnim;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            StartChangingPlayerState();

            m_playerStateChanger = FindObjectOfType<PlayerStateChanger>();
        }
        #endregion

        #region PlayerStateChangeButton
        private void StartChangingPlayerState() 
        {
            Button button = GetComponent<Button>();

            button.onClick.AddListener(EnableState);
        }

        private void EnableState() 
        {
            m_playerStateChanger.TryChangeStateTo<TypeOfPlayerStateToEnable>();
            cameraAnim.SetTrigger("EnableShaking");
            cameraAnim.SetTrigger("DisableShaking");

            int popuptextNumber = Random.Range(1, 10);
            if (popuptextNumber == 1)
            {
                popUpTextAnim.SetTrigger("PopUpText1");
                popUpTextAnim.SetTrigger("PopUpTextDisappear");
            }
            if (popuptextNumber == 5)
            {
                popUpTextAnim.SetTrigger("PopUpText2");
                popUpTextAnim.SetTrigger("PopUpTextDisappear");
            }
            if (popuptextNumber == 10)
            {
                popUpTextAnim.SetTrigger("PopUpText3");
                popUpTextAnim.SetTrigger("PopUpTextDisappear");
            }
        }
        #endregion
    }
}