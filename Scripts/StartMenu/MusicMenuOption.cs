using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets
{
    public class MusicMenuOption : RaycastText
    {
        public string filepath = "";

        public override void Select()
        {
            Variables.filepath = filepath;

            //ToggleOptionText cameraOption = GameObject.Find("SmoothCameraOption").GetComponent<ToggleOptionText>();
            //Variables.smoothCamera = cameraOption.state;

            ToggleOptionText visualizerOption = GameObject.Find("VisualizerOption").GetComponent<ToggleOptionText>();
            string sceneToLoad = visualizerOption.state ? "Concert_Scene" : "Space_Visualizer";

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
