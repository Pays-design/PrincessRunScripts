using UnityEngine;
using System.Linq;

using Action = System.Action;
using Array = System.Array;

namespace PrincessRun.Core.GamePlay
{
    public abstract class StateChanger : MonoBehaviour
    {
        #region Fields
        private PlayerState[] m_playerStates;

        private PlayerState m_currentPlayerState;

        private bool m_canChangeState = true;

        public event Action OnStateChange, OnStop;
        #endregion

        #region Properties
        public PlayerState CurrentPlayerState => m_currentPlayerState;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            FindPlayerStates();

            FindCurrentPlayerState();

            Array.ForEach(m_playerStates, (playerState) => playerState.OnStop += () =>
            {
                enabled = false;

                m_canChangeState = false;

                OnStop?.Invoke();
            });

        }
        #endregion

        #region PlayerStateChanger
        private void ReportAboutPlayerStateUnavailability<TypeOfPlayerState>() where TypeOfPlayerState : PlayerState
        {
            string playerStateName = typeof(TypeOfPlayerState).Name;

            Debug.LogError($"Player gameObject contains more than one { playerStateName } or it doesnt contain any { playerStateName }");
        }

        private bool IsStateAvailable<TypeOfPlayerState>(out TypeOfPlayerState state) where TypeOfPlayerState : PlayerState
        {
            var requiredPlayerStates = m_playerStates.OfType<TypeOfPlayerState>();

            if (requiredPlayerStates.Count() != 1)
            {
                state = null;

                return false;
            }

            state = requiredPlayerStates.First();

            return true;
        }

        private void FindCurrentPlayerState() => m_currentPlayerState = GetComponentInChildren<PlayerState>(false);

        private void FindPlayerStates() => m_playerStates = GetComponentsInChildren<PlayerState>(true);

        private void ChangeState(PlayerState newPlayerState)
        {
            m_currentPlayerState.gameObject.SetActive(false);

            m_currentPlayerState = newPlayerState;

            m_currentPlayerState.gameObject.SetActive(true);
        }

        public void TryChangeStateTo<TypeOfPlayerState>() where TypeOfPlayerState : PlayerState
        {
            if (m_currentPlayerState is TypeOfPlayerState || !m_canChangeState)
            {
                return;
            }

            if (IsStateAvailable(out TypeOfPlayerState stateToSet))
            {
                ChangeState(stateToSet);

                OnStateChange?.Invoke();
            }
            else
            {
                ReportAboutPlayerStateUnavailability<TypeOfPlayerState>();
            }
        }
        #endregion
    }
}