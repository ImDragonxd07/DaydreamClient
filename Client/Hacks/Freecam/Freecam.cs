using Daydream.Client.Hacks.Freecam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets.CrossPlatformInput;
using VRC.SDKBase;

namespace Daydream.Client.Hacks.Freecam
{
    internal class Freecam
    {
        private static float flySpeed = 0.3f;
        private static Camera defaultCam;
        private static Camera newcam;
        private static bool isEnabled = false;
        private static float accelerationAmount = 3f;
        private static float accelerationRatio = 1f;
        private static float slowDownRatio = 0.5f;
        public static void Update()
        {
            if(isEnabled == true)
            {
                Networking.LocalPlayer.SetVelocity(Vector3.zero);
                newcam.transform.rotation = defaultCam.transform.rotation;

                //if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                //{
                //    flySpeed *= accelerationRatio;
                //}

                //if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
                //{
                //    flySpeed /= accelerationRatio;
                //}
                //if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
                //{
                //    flySpeed *= slowDownRatio;
                //}
                //if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
                //{
                //    flySpeed /= slowDownRatio;
                //}
                if (Input.GetAxis("Vertical") != 0)
                {
                    //newcam.transform.Translate(newcam.transform.forward * flySpeed * Input.GetAxis("Vertical"));
                    newcam.transform.position = newcam.transform.position + Camera.main.transform.forward * (Input.GetAxis("Vertical") + VRCInputManager.field_Private_Static_Dictionary_2_String_VRCInput_0["Vertical"].field_Public_Single_0) * flySpeed;
                }
                if (Input.GetAxis("Horizontal") != 0)
                {
                    //newcam.transform.Translate(newcam.transform.right * flySpeed * Input.GetAxis("Horizontal"));
                    newcam.transform.position = newcam.transform.position + Camera.main.transform.right * (Input.GetAxis("Horizontal") + VRCInputManager.field_Private_Static_Dictionary_2_String_VRCInput_0["Horizontal"].field_Public_Single_0) * flySpeed;
                }
                
                //if (Input.GetKey(KeyCode.E))
                //{
                //    newcam.transform.Translate(defaultCam.transform.up * flySpeed * 0.5f);
                //}
                //else if (Input.GetKey(KeyCode.Q))
                //{
                //    newcam.transform.Translate(-defaultCam.transform.up * flySpeed * 0.5f);
                //}


            }
        }
        private static void setup()
        {
            Utility.Logger.log("Setup freecam");
            defaultCam = Camera.main;
            newcam = new GameObject("UE_Freecam").AddComponent<Camera>();
            newcam.gameObject.tag = "MainCamera";
            UnityEngine.Object.DontDestroyOnLoad(newcam.gameObject);
            newcam.gameObject.hideFlags = HideFlags.HideAndDontSave;
        }
        public static void toggle()
        {
            //isEnabled = !isEnabled;
            if(newcam == null || defaultCam == null)
            {
                setup();
            }
            switchCamera();
        }

        public static void LoadMain()
        {
            if (newcam == null || defaultCam == null)
            {
                setup();
            }
            Utility.Logger.log("Disable freecam");
            //Daydream.Client.Classes.Daydream.HackUpdate("Freecam", false);
            newcam.gameObject.SetActive(false);
            newcam.enabled = false;
            defaultCam.gameObject.SetActive(true);
            defaultCam.enabled = true;
            isEnabled = false;
        }

        private static CursorLockMode lockmode;
        private static void switchCamera()
        {
            if (!isEnabled) //means it is currently disabled. code will enable the flycam. you can NOT use 'enabled' as boolean's name.
            {
                Utility.Logger.log("Enable freecam");
                //Daydream.Client.Classes.Daydream.HackUpdate("Freecam",true);
                newcam.transform.position = defaultCam.transform.position; //moves the flycam to the defaultcam's position
                defaultCam.gameObject.SetActive(false);
                defaultCam.enabled = false;
                newcam.gameObject.SetActive(true);
                newcam.enabled = true;
                isEnabled = true;
            }
            else if (isEnabled) //if it is not disabled, it must be enabled. the function will disable the freefly camera this time.
            {
                Utility.Logger.log("Disable freecam");
                //Daydream.Client.Classes.Daydream.HackUpdate("Freecam", false);
                newcam.gameObject.SetActive(false);
                newcam.enabled = false;
                defaultCam.gameObject.SetActive(true);
                defaultCam.enabled = true;
                isEnabled = false;
            }
        }
    }
}
