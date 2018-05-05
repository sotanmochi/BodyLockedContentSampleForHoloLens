using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class AirTapHandler : MonoBehaviour, IInputClickHandler
{
    public QueryChanFlyingController QueryChanController;

    void Start()
    {
        InputManager.Instance.AddGlobalListener(gameObject);
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if(QueryChanController != null)
        {
            QueryChanController.ChangeForwardMode();
        }
        else
        {
            TrackingModeManager.Instance.SwitchTrackingMode();
        }
    }
}