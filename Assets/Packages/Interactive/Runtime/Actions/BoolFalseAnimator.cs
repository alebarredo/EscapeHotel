using UnityEngine;


namespace Gamekit3D.GameCommands
{
    public class BoolFalseAnimator : GameCommandHandler
    {
        public Animator m_Animator;

        public override void PerformInteraction()
        {
            m_Animator.SetBool("Open", false);
        }
    }
}
