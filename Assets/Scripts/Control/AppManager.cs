using Kamen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence
{
    public class AppManager : SingletonComponent<AppManager>
    {
        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            Application.targetFrameRate = 60;
        }

        #endregion
    }
}