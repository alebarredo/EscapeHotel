using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Gamekit3D.GameCommands
{
    public class EventCommand : GameCommandHandler
    {
        public UnityEvent OnCommand;

        public override void PerformInteraction()
        {
            OnCommand.Invoke();
        }
    }
}
