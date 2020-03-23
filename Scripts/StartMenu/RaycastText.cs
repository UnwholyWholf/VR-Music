using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public abstract class RaycastText : UnityEngine.UI.Text
    {
        public void OnRaycast()
        {
            fontStyle = FontStyle.BoldAndItalic;
        }


        public void OffRaycast()
        {
            fontStyle = FontStyle.Normal;
        }

        public void Update()
        {
            RaycastText hit = GameObject.Find("CenterEyeAnchor").GetComponent<RaycastMenuSelect>().currentOption;

            if (hit == this)
            {
                OnRaycast();
            }
            else
            {
                OffRaycast();
            }
        }

        public abstract void Select();
    }
}
