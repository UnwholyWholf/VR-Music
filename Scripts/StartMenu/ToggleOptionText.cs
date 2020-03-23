using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    class ToggleOptionText : RaycastText
    {
        public string optionText = "Option";
        public string trueText = "On";
        public string falseText = "Off";

        public bool state = true;

        new public void Update()
        {
            if (state)
                text = optionText + ": " + trueText;
            else
                text = optionText + ": " + falseText;

            base.Update();
        }

        public override void Select()
        {
            state = !state;            
        }
    }
}
