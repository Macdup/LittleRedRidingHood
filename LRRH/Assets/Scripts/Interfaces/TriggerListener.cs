using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    interface TriggerListener
    {
        void OnTriggerzoneEnter2D(Collider2D collision);
        void OnTriggerzoneExit2D(Collider2D collision);
    }
}
