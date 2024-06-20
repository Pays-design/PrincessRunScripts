using UnityEngine;

using PrincessRun.Core.UnityExtensions;

using Action = System.Action;

using MudZone = PrincessRun.Core.GamePlay.MudZone;

namespace PrincessRun.Core.UserImpact
{
    [RequireComponent(typeof(MeshRenderer))]
    public class WheelPollutant : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private float m_speedOfPollution;
        [SerializeField] private Color m_endColorOfWheel;
        #endregion

        #region Fields
        private MeshRenderer m_wheelMeshRenderer;
        private float m_progressOfPollution;
        private Action m_state;
        private Color m_startColorOfWheel;
        #endregion

        #region MonoBehaviour
        private void OnValidate()
        {
            if (m_speedOfPollution < 0) 
            {
                m_speedOfPollution = 0;
            }
        }

        private void OnEnable()
        {
            m_state = null;

            m_wheelMeshRenderer.material.color = m_startColorOfWheel;
        }

        private void Awake()
        {
            m_wheelMeshRenderer = GetComponent<MeshRenderer>();

            m_startColorOfWheel = m_wheelMeshRenderer.material.color;
        }

        private void Update()
        {
            m_state?.Invoke();
        }

        private void OnTriggerEnter(Collider enteredCollider)
        {
            if (enteredCollider.gameObject.HasComponent<MudZone>()) 
            {
                m_state = PolluteWheel;
            }
        }

        private void OnTriggerExit(Collider enteredCollider)
        {
            if (enteredCollider.gameObject.HasComponent<MudZone>())
            {
                m_state = CleanWheel;
            }
        }
        #endregion

        #region WheelPollutant
        private void PolluteWheel() 
        {
            m_progressOfPollution = Mathf.Clamp01(m_progressOfPollution + Time.deltaTime * m_speedOfPollution);

            m_wheelMeshRenderer.material.color = Color.Lerp(m_wheelMeshRenderer.material.color, m_endColorOfWheel, m_progressOfPollution);
        }

        private void CleanWheel() 
        {
            m_progressOfPollution = Mathf.Clamp01(m_progressOfPollution - Time.deltaTime * m_speedOfPollution);

            m_wheelMeshRenderer.material.color = Color.Lerp(m_wheelMeshRenderer.material.color, m_startColorOfWheel, m_progressOfPollution);
        }
        #endregion
    }
}