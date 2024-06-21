using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.Input;
using Unity.VisualScripting;

public class TriggerHapticOnGrab : MonoBehaviour
{
    [Range(0f, 2.5f)]
    public float duration;
    [Range(0f, 1f)]
    public float amplitude;
    [Range(0f, 1f)]
    public float frequency = 1f;

    public GrabInteractable grabInteractable;
    // Start is called before the first frame update
    void Start()
    {
        grabInteractable.WhenSelectingInteractorAdded.Action += WhenSelectingInteractorAdded_Action;
    }

    private void WhenSelectingInteractorAdded_Action(GrabInteractor obj)
    {
        ControllerRef controllerRef = obj.GetComponent<ControllerRef>();

        if (controllerRef.Handedness == Handedness.Right) TriggerHaptics(OVRInput.Controller.RTouch);
    }

    public void  TriggerHaptics(OVRInput.Controller controller)
    {
        StartCoroutine(TriggerHapticCorountine(controller));
    }

    public IEnumerator TriggerHapticCorountine(OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, controller);

        yield return new WaitForSeconds(duration);

        OVRInput.SetControllerVibration(0,0,controller);
    }
}
