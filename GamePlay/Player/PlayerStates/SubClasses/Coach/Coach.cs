using Time = UnityEngine.Time;
using Mathf = UnityEngine.Mathf;
using UnityEngine;

namespace PrincessRun.Core.GamePlay
{
    public class Coach : PlayerState
    {
        #region Fields
        private float m_temporarySpeed;
        public ParticleSystem toonFire;
        #endregion

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();

            m_temporarySpeed = m_speedOfMoving;
        }

        private void OnEnable()
        {
            m_temporarySpeed = m_speedOfMoving;
        }
        #endregion

        #region PlayerState
        protected override void Move()
        {
            m_rigidbodyToControl.MovePosition(m_rigidbodyToControl.position + m_transformToControl.forward * m_temporarySpeed * Time.fixedDeltaTime);
        }
        #endregion

        #region Coach
        private void ChangeTemporarySpeedSafely(float newTemporarySpeed) => m_temporarySpeed = Mathf.Clamp(newTemporarySpeed, 0, float.MaxValue);

        public void ReduceSpeed(float reduceAmount) => ChangeTemporarySpeedSafely(m_temporarySpeed - reduceAmount);

        public void EnlargeSpeed(float enlargeAmount) => ChangeTemporarySpeedSafely(m_temporarySpeed + enlargeAmount);

        public void EnableFireFX(ParticleSystem toonFire) => toonFire.Play();

        public void DisableFireFX(ParticleSystem toonFire) => toonFire.Stop();
        #endregion
    }
}