using MelonLoader;
using RootMotion.FinalIK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using UnityEngine;
using VRC;
using VRC.SDKBase;

namespace Daydream.Client.Hacks
{
    internal class CopyIK
    {
        private readonly Il2CppStructArray<float> myMuscles = new(HumanTrait.MuscleCount);
        private static IKSolverVR.VirtualBone[] myConvertedBones;

        public static VRCPlayer GetLocalVRCPlayer() => VRCPlayer.field_Internal_Static_VRCPlayer_0;

        private static bool ENABLED = false;
        public static void Toggle(bool enabled, VRCPlayer plr)
        {
            ENABLED = enabled;
            Utility.Logger.log("Copying from " + plr.name);
            MelonCoroutines.Start(Start(plr));

        }

        public static IKSolverVR getsolver(VRCPlayer plr)
        {
            return plr.field_Private_VRC_AnimationController_0.field_Private_VRIK_0.solver;
        }
        private static void SetBoneRotation(HumanBodyBones bone)
        {
            VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRC_AnimationController_0.field_Private_Animator_0.SetBoneLocalRotation(bone, VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRC_AnimationController_0.field_Private_Animator_0.GetBoneTransform(bone).rotation);

        }
        private static void SetBonePosition(HumanBodyBones bone, VRCPlayer plr)
        {
            getsolver(VRCPlayer.field_Internal_Static_VRCPlayer_0).headPosition = plr.field_Private_VRC_AnimationController_0.field_Private_Animator_0.GetBoneTransform(bone).position;

        }
        private static IEnumerator Start(VRCPlayer plr)
        {
            for (; ; )
            {
                if(ENABLED == true)
                {
                    string finalprint = "";
                    finalprint += plr.field_Private_VRC_AnimationController_0.field_Private_Animator_0.GetBoneTransform(HumanBodyBones.Head).position.ToString();
                    finalprint += plr.field_Private_VRC_AnimationController_0.field_Private_Animator_0.GetBoneTransform(HumanBodyBones.Hips).position.ToString();
                    finalprint += plr.field_Private_VRC_AnimationController_0.field_Private_Animator_0.GetBoneTransform(HumanBodyBones.LeftUpperArm).position.ToString();
                    finalprint += plr.field_Private_VRC_AnimationController_0.field_Private_Animator_0.GetBoneTransform(HumanBodyBones.RightUpperArm).position.ToString();

                    Utility.Logger.log("Update " + finalprint);
                    finalprint = "";
                    //SetBoneRotation(HumanBodyBones.Head);
                    //SetBoneRotation(HumanBodyBones.Hips);
                    //SetBoneRotation(HumanBodyBones.LeftUpperArm);
                    //SetBoneRotation(HumanBodyBones.RightUpperArm);
                    SetBonePosition(HumanBodyBones.Head, plr);
                    SetBonePosition(HumanBodyBones.Hips,plr);
                    SetBonePosition(HumanBodyBones.LeftUpperArm, plr);
                    SetBonePosition(HumanBodyBones.RightUpperArm, plr);
                    //getsolver(VRCPlayer.field_Internal_Static_VRCPlayer_0).spine.head.readPosition = getsolver(plr).spine.head.solverPosition;
                    //getsolver(VRCPlayer.field_Internal_Static_VRCPlayer_0).spine.pelvis.readPosition = getsolver(plr).spine.pelvis.solverPosition;
                }
                else
                {
                    yield break;
                }
                yield return new WaitForEndOfFrame();
            }

        }
        public static void RefreshPositions()
        {
            //Chest = GetLocalVRCPlayer().prop_VRCPlayerApi_0.GetBonePosition(UnityEngine.HumanBodyBones.Chest);
            //VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRC_AnimationController_0.field_Private_VRIK_0.solver.spine.headRotation = new Vector3();
        }
  
    }
}
