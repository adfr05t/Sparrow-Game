using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InvertYAxis : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freelookCam;

    public void ToggleYAxisInversion()
    {
        if (freelookCam.m_YAxis.m_InvertInput == true)
        {
            freelookCam.m_YAxis.m_InvertInput = false;
        }
        else if (freelookCam.m_YAxis.m_InvertInput == false)
        {
            freelookCam.m_YAxis.m_InvertInput = true;
        }
    }
}
