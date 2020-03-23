using UnityEngine;
using System.Collections;
using Assets;

public class RaycastMenuSelect : MonoBehaviour
{
    public RaycastText currentOption;
    public KeyCode[] selectionInputs;

    private Transform cameraTrans;

    // Use this for initialization
    void Start()
    {
        cameraTrans = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraTrans == null)
            Debug.Log("null Transform");

        Vector3 pos = cameraTrans.position;
        Vector3 fwd = cameraTrans.forward;
        Ray visionRay = new Ray(pos, fwd);

        RaycastHit hitInfo;

        if (Physics.Raycast(visionRay, out hitInfo, 1000))
        {
            GameObject hit = hitInfo.transform.gameObject;
            RaycastText text = hit.GetComponent<RaycastText>();
            if (text != null)
            {
                currentOption = text;
            }
            else
            {
                currentOption = null;
            }
        }
        else
        {
            currentOption = null;
        }

        if (userInputsSelect() && currentOption != null)
        {
            currentOption.Select();
        }
    }

    bool userInputsSelect()
    {
        foreach (KeyCode kc in selectionInputs)
        {
            if (Input.GetKeyDown(kc))
                return true;
        }
        return false;
    }
}
