using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kamen.Joystick
{
    public class Joystick : JoystickHandler
    {
        #region Control Methods

        public Vector3 GetVectorDirection() => new Vector3(_inputVector.x, 0, _inputVector.y);

        #endregion
    }
}