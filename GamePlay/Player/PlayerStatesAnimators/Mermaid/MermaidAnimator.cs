using UnityEngine;

namespace PrincessRun.Core.GamePlay
{
    [RequireComponent(typeof(Mermaid))]
    public class MermaidAnimator : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private Animator m_animator;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            Mermaid mermaid = GetComponent<Mermaid>();

            mermaid.OnFastMovementAllow += StartSwimmingAnimation;

            mermaid.OnFastMovementForbid += StartIdleAnimation;
        }
        #endregion

        #region MermaidAnimator
        private void StartSwimmingAnimation() 
        {
            m_animator.SetBool("IsSwimming", true);
        }

        private void StartIdleAnimation() 
        {
            m_animator.SetBool("IsSwimming", false);
        }
        #endregion
    }
}