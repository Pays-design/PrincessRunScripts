using UnityEngine;

using Action = System.Action;

namespace PrincessRun.Core.GamePlay
{
    public abstract class PlayerState : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] protected float m_speedOfMoving;
        [SerializeField] protected Rigidbody m_rigidbodyToControl;
        #endregion

        #region Fields
        protected Transform m_transformToControl;
        #endregion

        #region Properties
        public Rigidbody ControlledRigidbody => m_rigidbodyToControl;
        public event Action OnStop;
        #endregion

        #region MonoBehaviour
        protected virtual void Awake()
        {
            m_transformToControl = m_rigidbodyToControl.transform;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnEnable()
        {
            m_rigidbodyToControl.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;

            m_rigidbodyToControl.transform.rotation = Quaternion.identity;
        }

        protected virtual void OnValidate()
        {
            if (m_speedOfMoving < 0)
                m_speedOfMoving = 0;
        }
        #endregion

        #region PlayerState
        protected abstract void Move();

        public virtual void Stop()
        {
            System.Array.ForEach(GetComponentsInChildren<Animator>(), (animator) => animator.enabled = false);

            enabled = false;

            OnStop?.Invoke();
        }
        #endregion
    }
}