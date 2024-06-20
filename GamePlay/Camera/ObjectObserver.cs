using UnityEngine;

namespace PrincessRun.Core.GamePlay
{
    public class ObjectObserver : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private Transform m_transformToSeek;
        #endregion

        #region Fields
        private Vector3 m_bias;
        private Transform m_transform;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            m_transform = GetComponent<Transform>();

            m_bias = m_transform.position - m_transformToSeek.position;
        }

        private void FixedUpdate()
        {
            m_transform.position = m_transformToSeek.position + m_bias;
        }
        #endregion
    }
}