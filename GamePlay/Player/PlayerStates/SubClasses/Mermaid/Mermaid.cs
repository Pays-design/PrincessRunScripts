using UnityEngine;

using Action = System.Action;

namespace PrincessRun.Core.GamePlay
{
    public class Mermaid : PlayerState
    {
        #region SerializeField
        [SerializeField] private float m_speedOnLand;
        #endregion

        #region Fields
        private bool m_canMoveFast;

        public event Action OnFastMovementForbid, OnFastMovementAllow;
        #endregion

        #region MonoBehaviour
        private void OnDisable()
        {
            m_rigidbodyToControl.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
        }

        private void OnEnable()
        {
            m_canMoveFast = false;
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (m_speedOnLand < 0)
                m_speedOnLand = 0;
        }
        #endregion

        #region PlayerState
        protected override void Move()
        {
            float speed = m_canMoveFast ? m_speedOfMoving : m_speedOnLand;

            m_rigidbodyToControl.MovePosition(m_rigidbodyToControl.position + m_transformToControl.forward * speed * Time.fixedDeltaTime);
        }

        public void ForbidFastMovement() 
        {
            m_canMoveFast = false;

            m_rigidbodyToControl.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;

            OnFastMovementForbid?.Invoke();
        }

        public void AllowFastMovement() 
        {
            m_canMoveFast = true;

            m_rigidbodyToControl.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;

            OnFastMovementAllow?.Invoke();
        }
        #endregion
    }
}