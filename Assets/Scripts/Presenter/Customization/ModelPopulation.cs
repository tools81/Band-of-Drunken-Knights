using System.Collections.Generic;
using Model.Customization;
using UnityEngine;

namespace Presenter.Customization
{
    public static class ModelPopulation
    {
        public static void BuildLists(PlayerCustomizationModel model, GameObject go)
        {
            //build out male lists
            BuildList(go, model, model.male.headAllElements, "Male_Head_All_Elements");
            BuildList(go, model, model.male.headNoElements, "Male_Head_No_Elements");
            BuildList(go, model, model.male.eyebrow, "Male_01_Eyebrows");
            BuildList(go, model, model.male.facialHair, "Male_02_FacialHair");
            BuildList(go, model, model.male.torso, "Male_03_Torso");
            BuildList(go, model, model.male.arm_Upper_Right, "Male_04_Arm_Upper_Right");
            BuildList(go, model, model.male.arm_Upper_Left, "Male_05_Arm_Upper_Left");
            BuildList(go, model, model.male.arm_Lower_Right, "Male_06_Arm_Lower_Right");
            BuildList(go, model, model.male.arm_Lower_Left, "Male_07_Arm_Lower_Left");
            BuildList(go, model, model.male.hand_Right, "Male_08_Hand_Right");
            BuildList(go, model, model.male.hand_Left, "Male_09_Hand_Left");
            BuildList(go, model, model.male.hips, "Male_10_Hips");
            BuildList(go, model, model.male.leg_Right, "Male_11_Leg_Right");
            BuildList(go, model, model.male.leg_Left, "Male_12_Leg_Left");

            //build out female lists
            BuildList(go, model, model.female.headAllElements, "Female_Head_All_Elements");
            BuildList(go, model, model.female.headNoElements, "Female_Head_No_Elements");
            BuildList(go, model, model.female.eyebrow, "Female_01_Eyebrows");
            BuildList(go, model, model.female.facialHair, "Female_02_FacialHair");
            BuildList(go, model, model.female.torso, "Female_03_Torso");
            BuildList(go, model, model.female.arm_Upper_Right, "Female_04_Arm_Upper_Right");
            BuildList(go, model, model.female.arm_Upper_Left, "Female_05_Arm_Upper_Left");
            BuildList(go, model, model.female.arm_Lower_Right, "Female_06_Arm_Lower_Right");
            BuildList(go, model, model.female.arm_Lower_Left, "Female_07_Arm_Lower_Left");
            BuildList(go, model, model.female.hand_Right, "Female_08_Hand_Right");
            BuildList(go, model, model.female.hand_Left, "Female_09_Hand_Left");
            BuildList(go, model, model.female.hips, "Female_10_Hips");
            BuildList(go, model, model.female.leg_Right, "Female_11_Leg_Right");
            BuildList(go, model, model.female.leg_Left, "Female_12_Leg_Left");

            // build out all gender lists
            BuildList(go, model, model.allGender.all_Hair, "All_01_Hair");
            BuildList(go, model, model.allGender.all_Head_Attachment, "All_02_Head_Attachment");
            BuildList(go, model, model.allGender.headCoverings_Base_Hair, "HeadCoverings_Base_Hair");
            BuildList(go, model, model.allGender.headCoverings_No_FacialHair, "HeadCoverings_No_FacialHair");
            BuildList(go, model, model.allGender.headCoverings_No_Hair, "HeadCoverings_No_Hair");
            BuildList(go, model, model.allGender.chest_Attachment, "All_03_Chest_Attachment");
            BuildList(go, model, model.allGender.back_Attachment, "All_04_Back_Attachment");
            BuildList(go, model, model.allGender.shoulder_Attachment_Right, "All_05_Shoulder_Attachment_Right");
            BuildList(go, model, model.allGender.shoulder_Attachment_Left, "All_06_Shoulder_Attachment_Left");
            BuildList(go, model, model.allGender.elbow_Attachment_Right, "All_07_Elbow_Attachment_Right");
            BuildList(go, model, model.allGender.elbow_Attachment_Left, "All_08_Elbow_Attachment_Left");
            BuildList(go, model, model.allGender.hips_Attachment, "All_09_Hips_Attachment");
            BuildList(go, model, model.allGender.knee_Attachement_Right, "All_10_Knee_Attachement_Right");
            BuildList(go, model, model.allGender.knee_Attachement_Left, "All_11_Knee_Attachement_Left");
            BuildList(go, model, model.allGender.elf_Ear, "Elf_Ear");
        }

        private static void BuildList(GameObject go, PlayerCustomizationModel model, List<GameObject> targetList, string characterPart)
        {
            var modularParts = go.transform.parent.Find("Modular_Characters");

            // declare target root transform
            Transform targetRoot = null;

            // find character parts parent object in the scene
            foreach (Transform t in modularParts.GetComponentsInChildren<Transform>())
            {
                if (t.gameObject.name == characterPart)
                {
                    targetRoot = t;
                    break;
                }
            }

            // clears targeted list of all objects
            //targetList.Clear();

            // cycle through all child objects of the parent object
            for (int i = 0; i < targetRoot.childCount; i++)
            {
                // get child gameobject index i
                GameObject obj = targetRoot.GetChild(i).gameObject;

                // disable child object
                obj.SetActive(false);

                // add object to the targeted object list
                targetList.Add(obj);

                // collect the material for the random character, only if null in the inspector;
                if (!model.mat)
                {
                    if (obj.GetComponent<SkinnedMeshRenderer>())
                        model.mat = obj.GetComponent<SkinnedMeshRenderer>().material;
                }
            }
        }
    }
}