using UnityEngine;


namespace Gamekit3D.GameCommands
{
    public class BoolAnimator : GameCommandHandler
    {
        public Animator m_Animator;

        public override void PerformInteraction()
        {
            m_Animator.SetBool("Open", true);
        }
    }
}
