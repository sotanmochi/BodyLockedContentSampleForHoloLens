using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.WSA;

public class TrackingModeManager : HoloToolkit.Unity.Singleton<TrackingModeManager>
{
    public TextMesh textMesh;
	public float TimeBetweenCountUp = 0.5f;

	private float updateTime = 0.0f;
	private int loopCount = 0;

	void Start()
	{
		// Orientation-only
		XRDevice.SetTrackingSpaceType(TrackingSpaceType.Stationary);
		InputTracking.disablePositionalTracking = true;

		UnityEngine.XR.WSA.WorldManager.OnPositionalLocatorStateChanged += WorldManager_OnPositionalLocatorStateChanged;

		// Set Reprojection mode (RS4)
		// UnityEngine.XR.WSA.HolographicSettings.ReprojectionMode = UnityEngine.XR.WSA.HolographicSettings.HolographicReprojectionMode.OrientationOnly;
	}
	
	void Update()
	{
		if(textMesh != null)
        {
			if((Time.unscaledTime - updateTime) >= TimeBetweenCountUp)
			{
				loopCount++;
				updateTime = Time.unscaledTime;
			}
			
			string displayString = "LoopCount: " + loopCount + "\n"
								+ "Camera forward: " + Camera.main.transform.forward + "\n"
								//  + "Reprojection Mode: " + HolographicSettings.ReprojectionMode + "\n"
								+ "World Manager state: " + WorldManager.state + "\n"
								+ "TrackingSpaceType: " + XRDevice.GetTrackingSpaceType() + "\n"
								+ "DisablePositionTracking: " + InputTracking.disablePositionalTracking;

            textMesh.text = displayString;
        }		
	}

	private void WorldManager_OnPositionalLocatorStateChanged(PositionalLocatorState oldState, PositionalLocatorState newState)
	{
		if (newState == PositionalLocatorState.Active)
		{
			// Handle becoming active
		}
		else
		{
			// Handle becoming rotational only
		}
	}

	public void SwitchTrackingMode()
	{
		// Switch to position and orientation tracking (World-Locked content)
		if(InputTracking.disablePositionalTracking)
		{
			// XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale);
			InputTracking.disablePositionalTracking = false;
		}
		// Switch to orientation only tracking (Body-Locked content)
		else
		{
			// XRDevice.SetTrackingSpaceType(TrackingSpaceType.Stationary);
			InputTracking.disablePositionalTracking = true;
		}
	}
}
