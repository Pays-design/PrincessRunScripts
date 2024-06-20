using UnityEngine;

using Array = System.Array;

namespace PrincessRun.Core.GamePlay
{
    public class LevelStarter
    {
        #region Fields
        private StateChanger[] m_stateChangers;
        #endregion

        #region Constructor
        public LevelStarter() 
        {
            m_stateChangers = GameObject.FindObjectsOfType<StateChanger>();
        }
        #endregion

        #region LevelStarter
        private void StartPlayersWalking() 
        {
            foreach (StateChanger stateChanger in m_stateChangers)
            {
                stateChanger.enabled = true;

                stateChanger.CurrentPlayerState.enabled = true;

                Array.ForEach(stateChanger.CurrentPlayerState.GetComponentsInChildren<Animator>(), (animator) => animator.enabled = true);
            }
        }

        public void Start() 
        {
            StartPlayersWalking();
        }
        #endregion
    }
}