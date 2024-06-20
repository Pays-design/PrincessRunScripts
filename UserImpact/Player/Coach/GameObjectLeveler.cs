using UnityEngine;

namespace PrincessRun.Core.UserImpact
{
    public class GameObjectLeveler : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private Rigidbody m_rigidbodyToAlign;
        [SerializeField] private Transform m_base;
        [Range(0f, 1f)]
        [SerializeField] private float m_smoothnessOfAligning;
        [SerializeField] private float m_epsilon;
        [SerializeField] private LayerMask m_layersOfRaycasting;
        #endregion

        #region Fields
        private Transform m_transform;
        private Vector3 m_startLocalPosition;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            m_transform = transform;

            m_startLocalPosition = m_transform.localPosition;
        }

        private void OnValidate()
        {
            if (m_epsilon < 0)
            {
                m_epsilon = 0;
            }
        }

        private void FixedUpdate()
        {
            TryAlignSelf();
        }
        #endregion

        #region ObjectLeveler
        private void TryAlignSelf()
        {
            if (Physics.Raycast(m_base.position, -m_transform.up, out RaycastHit hitInformation, 2.5f, m_layersOfRaycasting.value))
            {
                if (hitInformation.collider != null)
                {
                    if ((hitInformation.point - m_base.position).magnitude > m_epsilon)
                    {
                        m_transform.up = Vector3.Slerp(m_transform.up, hitInformation.collider.transform.up, m_smoothnessOfAligning);

                        m_transform.position = Vector3.Lerp(m_transform.position, hitInformation.point, m_smoothnessOfAligning);

                        if(m_rigidbodyToAlign != null)
                            m_rigidbodyToAlign.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
                    }
                }
            }
            else 
            {
                m_transform.localPosition = Vector3.Lerp(m_transform.localPosition, m_startLocalPosition, m_smoothnessOfAligning);

                m_transform.up = Vector3.Slerp(m_transform.up, Vector3.up, m_smoothnessOfAligning);

                if (m_rigidbodyToAlign != null)
                {
                    m_rigidbodyToAlign.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;

                    m_rigidbodyToAlign.transform.up = Vector3.Slerp(m_rigidbodyToAlign.transform.up, Vector3.up, m_smoothnessOfAligning);

                    m_transform.parent.up = Vector3.Slerp(m_transform.parent.up, Vector3.up, m_smoothnessOfAligning);
                }
            }
        }
        #endregion
    }
}