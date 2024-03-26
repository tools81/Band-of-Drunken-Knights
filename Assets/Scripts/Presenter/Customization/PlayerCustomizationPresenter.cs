using System;
using System.Collections.Generic;
using System.Linq;
using Model.Customization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using View;


namespace Presenter.Customization
{
    public class PlayerCustomizationPresenter : MonoBehaviour
    {      
        public PlayerCustomizationModel _model;
        [SerializeField] public CarouselUIElement _crslGender;
        [SerializeField] public CarouselUIElement _crslEyes;
        [SerializeField] public CarouselUIElement _crslSkin;
        [SerializeField] public CarouselUIElement _crslHead;
        [SerializeField] public CarouselUIElement _crslHairColor;
        [SerializeField] public CarouselUIElement _crslHair;
        [SerializeField] public CarouselUIElement _crslEyebrows;
        [SerializeField] public CarouselUIElement _crslFacialHair;
        [SerializeField] public CarouselUIElement _crslEars;
        [SerializeField] public CarouselUIElement _crslGearMain;
        [SerializeField] public CarouselUIElement _crslGearAlt;
        [SerializeField] public CarouselUIElement _crslMetalMain;
        [SerializeField] public CarouselUIElement _crslMetalAlt;
        [SerializeField] public CarouselUIElement _crslLeatherMain;
        [SerializeField] public CarouselUIElement _crslLeatherAlt;
        [SerializeField] private TMP_FontAsset _fontAsset;
        [SerializeField] private Material _material;

        // list of enabed objects on character
        [HideInInspector]
        public List<GameObject> _enabledObjects = new List<GameObject>();
        [HideInInspector]
        public IEnumerable<Gender> _genders;
        [HideInInspector]
        public Gender _currentGenderSelection = Gender.Male;

        private void Start()
        {
            _model = new PlayerCustomizationModel();
            _genders = Enum.GetValues(typeof(Gender)).Cast<Gender>();
            _model.mat = _material;

            // rebuild all lists
            ModelPopulation.BuildLists(_model, gameObject);

            // disable any enabled objects before clear
            if (_enabledObjects.Count != 0)
            {
                foreach (GameObject g in _enabledObjects)
                {
                    g.SetActive(false);
                }
            }

            // clear enabled objects list
            _enabledObjects.Clear();

            // set default male character
            ActivateItem(_model.male.headAllElements[0]);
            ActivateItem(_model.male.eyebrow[0]);
            ActivateItem(_model.male.facialHair[0]);
            ActivateItem(_model.male.torso[0]);
            ActivateItem(_model.male.arm_Upper_Right[0]);
            ActivateItem(_model.male.arm_Upper_Left[0]);
            ActivateItem(_model.male.arm_Lower_Right[0]);
            ActivateItem(_model.male.arm_Lower_Left[0]);
            ActivateItem(_model.male.hand_Right[0]);
            ActivateItem(_model.male.hand_Left[0]);
            ActivateItem(_model.male.hips[0]);
            ActivateItem(_model.male.leg_Right[0]);
            ActivateItem(_model.male.leg_Left[0]);
            PopulateCarousels();
        }

        private void PopulateCarousels()
        {
            List<string> carouselTextOptions = new List<string>();
            List<Color> carouselColorOptions = new List<Color>();

            foreach (Gender gender in _genders)
            {
                carouselTextOptions.Add(gender.ToString());
            }
            PopulateDropdown(carouselTextOptions, _crslGender);

            carouselTextOptions.Clear();

            foreach (Color color in _model.eyes)
            {
                carouselColorOptions.Add(color);
            }
            PopulateDropdown(carouselColorOptions, _crslEyes);

            carouselColorOptions.Clear();

            foreach (Color color in _model.skinColor)
            {
                carouselColorOptions.Add(color);
            }
            PopulateDropdown(carouselColorOptions, _crslSkin);

            carouselColorOptions.Clear();

            PopulateGenderDependentCarousels();

            foreach (Color color in _model.hairColor)
            {
                carouselColorOptions.Add(color);
            }
            PopulateDropdown(carouselColorOptions, _crslHairColor);

            carouselColorOptions.Clear();

            foreach (GameObject hair in _model.allGender.all_Hair)
            {
                carouselTextOptions.Add(StringUtil.GetSubstringAfterLast(hair.name, '_'));
            }
            PopulateDropdown(carouselTextOptions, _crslHair);

            carouselTextOptions.Clear();

            foreach (GameObject ear in _model.allGender.elf_Ear)
            {
                carouselTextOptions.Add(StringUtil.GetSubstringAfterLast(ear.name, '_'));
            }
            PopulateDropdown(carouselTextOptions, _crslEars);

            carouselTextOptions.Clear();

            foreach (Color color in _model.gearPrimary)
            {
                carouselColorOptions.Add(color);
            }
            PopulateDropdown(carouselColorOptions, _crslGearMain);

            carouselColorOptions.Clear();

            foreach (Color color in _model.gearSecondary)
            {
                carouselColorOptions.Add(color);
            }
            PopulateDropdown(carouselColorOptions, _crslGearAlt);

            carouselColorOptions.Clear();

            foreach (Color color in _model.metalPrimary)
            {
                carouselColorOptions.Add(color);
            }
            PopulateDropdown(carouselColorOptions, _crslMetalMain);

            carouselColorOptions.Clear();

            foreach (Color color in _model.metalSecondary)
            {
                carouselColorOptions.Add(color);
            }
            PopulateDropdown(carouselColorOptions, _crslMetalAlt);

            carouselColorOptions.Clear();

            foreach (Color color in _model.leatherPrimary)
            {
                carouselColorOptions.Add(color);
            }
            PopulateDropdown(carouselColorOptions, _crslLeatherMain);

            carouselColorOptions.Clear();

            foreach (Color color in _model.leatherSecondary)
            {
                carouselColorOptions.Add(color);
            }
            PopulateDropdown(carouselColorOptions, _crslLeatherAlt);

            carouselColorOptions.Clear();
        }

        private void PopulateGenderDependentCarousels()
        {
            List<string> carouselTextOptions = new List<string>();

            if (_currentGenderSelection == Gender.Male)
            {
                foreach (GameObject head in _model.male.headAllElements)
                {
                    carouselTextOptions.Add(StringUtil.GetSubstringAfterLast(head.name, '_'));
                }
                PopulateDropdown(carouselTextOptions, _crslHead);

                carouselTextOptions.Clear();

                foreach (GameObject eyebrow in _model.male.eyebrow)
                {
                    carouselTextOptions.Add(StringUtil.GetSubstringAfterLast(eyebrow.name, '_'));
                }
                PopulateDropdown(carouselTextOptions, _crslEyebrows);

                carouselTextOptions.Clear();

                foreach (GameObject facialHair in _model.male.facialHair)
                {
                    carouselTextOptions.Add(StringUtil.GetSubstringAfterLast(facialHair.name, '_'));
                }
                PopulateDropdown(carouselTextOptions, _crslFacialHair);
            }
            else
            {
                foreach (GameObject head in _model.female.headAllElements)
                {
                    carouselTextOptions.Add(StringUtil.GetSubstringAfterLast(head.name, '_'));
                }
                PopulateDropdown(carouselTextOptions, _crslHead);

                carouselTextOptions.Clear();

                foreach (GameObject eyebrow in _model.female.eyebrow)
                {
                    carouselTextOptions.Add(StringUtil.GetSubstringAfterLast(eyebrow.name, '_'));
                }
                PopulateDropdown(carouselTextOptions, _crslEyebrows);
            }
        }

        // void Randomize()
        // {
        //     // initialize settings
        //     Gender gender = Gender.Male;
        //     Race race = Race.Human;
        //     SkinColor skinColor = SkinColor.White;
        //     Elements elements = Elements.Yes;
        //     HeadCovering headCovering = HeadCovering.HeadCoverings_Base_Hair;
        //     FacialHair facialHair = FacialHair.Yes;

        //     // disable any enabled objects before clear
        //     if (enabledObjects.Count != 0)
        //     {
        //         foreach (GameObject g in enabledObjects)
        //         {
        //             g.SetActive(false);
        //         }
        //     }

        //     // clear enabled objects list (all objects now disabled)
        //     enabledObjects.Clear();

        //     // roll for gender
        //     if (!GetPercent(50))
        //         gender = Gender.Female;

        //     // roll for human (70% chance, 30% chance for elf)
        //     if (!GetPercent(70))
        //         race = Race.Elf;

        //     // roll for facial elements (beard, eyebrows)
        //     if (!GetPercent(50))
        //         elements = Elements.No;

        //     // select head covering 33% chance for each
        //     int headCoveringRoll = Random.Range(0, 100);
        //     // HeadCoverings_Base_Hair
        //     if (headCoveringRoll <= 33)
        //         headCovering = HeadCovering.HeadCoverings_Base_Hair;
        //     // HeadCoverings_No_FacialHair
        //     if (headCoveringRoll > 33 && headCoveringRoll < 66)
        //         headCovering = HeadCovering.HeadCoverings_No_FacialHair;
        //     // HeadCoverings_No_Hair
        //     if (headCoveringRoll >= 66)
        //         headCovering = HeadCovering.HeadCoverings_No_Hair;

        //     // select skin color if human, otherwise set skin color to elf
        //     switch (race)
        //     {
        //         case Race.Human:
        //             // select human skin 33% chance for each
        //             int colorRoll = Random.Range(0, 100);
        //             // select white skin
        //             if (colorRoll <= 33)
        //                 skinColor = SkinColor.White;
        //             // select brown skin
        //             if (colorRoll > 33 && colorRoll < 66)
        //                 skinColor = SkinColor.Brown;
        //             // select black skin
        //             if (colorRoll >= 66)
        //                 skinColor = SkinColor.Black;
        //             break;
        //         case Race.Elf:
        //             // select elf skin
        //             skinColor = SkinColor.Elf;
        //             break;
        //     }

        //     //roll for gender
        //     switch (gender)
        //     {
        //         case Gender.Male:
        //             // roll for facial hair if male
        //             if (!GetPercent(50))
        //                 facialHair = FacialHair.No;

        //             // initialize randomization
        //             RandomizeByVariable(male, gender, elements, race, facialHair, skinColor, headCovering);
        //             break;

        //         case Gender.Female:

        //             // no facial hair if female
        //             facialHair = FacialHair.No;

        //             // initialize randomization
        //             RandomizeByVariable(female, gender, elements, race, facialHair, skinColor, headCovering);
        //             break;
        //     }
        // }

        // void RandomizeByVariable(CharacterObjectGroups cog, Gender gender, Elements elements, Race race, FacialHair facialHair, SkinColor skinColor, HeadCovering headCovering)
        // {
        //     // if facial elements are enabled
        //     switch (elements)
        //     {
        //         case Elements.Yes:
        //             //select head with all elements
        //             if (cog.headAllElements.Count != 0)
        //                 ActivateItem(cog.headAllElements[Random.Range(0, cog.headAllElements.Count)]);

        //             //select eyebrows
        //             if (cog.eyebrow.Count != 0)
        //                 ActivateItem(cog.eyebrow[Random.Range(0, cog.eyebrow.Count)]);

        //             //select facial hair (conditional)
        //             if (cog.facialHair.Count != 0 && facialHair == FacialHair.Yes && gender == Gender.Male && headCovering != HeadCovering.HeadCoverings_No_FacialHair)
        //                 ActivateItem(cog.facialHair[Random.Range(0, cog.facialHair.Count)]);

        //             // select hair attachment
        //             switch (headCovering)
        //             {
        //                 case HeadCovering.HeadCoverings_Base_Hair:
        //                     // set hair attachment to index 1
        //                     if (allGender.all_Hair.Count != 0)
        //                         ActivateItem(allGender.all_Hair[1]);
        //                     if (allGender.headCoverings_Base_Hair.Count != 0)
        //                         ActivateItem(allGender.headCoverings_Base_Hair[Random.Range(0, allGender.headCoverings_Base_Hair.Count)]);
        //                     break;
        //                 case HeadCovering.HeadCoverings_No_FacialHair:
        //                     // no facial hair attachment
        //                     if (allGender.all_Hair.Count != 0)
        //                         ActivateItem(allGender.all_Hair[Random.Range(0, allGender.all_Hair.Count)]);
        //                     if (allGender.headCoverings_No_FacialHair.Count != 0)
        //                         ActivateItem(allGender.headCoverings_No_FacialHair[Random.Range(0, allGender.headCoverings_No_FacialHair.Count)]);
        //                     break;
        //                 case HeadCovering.HeadCoverings_No_Hair:
        //                     // select hair attachment
        //                     if (allGender.headCoverings_No_Hair.Count != 0)
        //                         ActivateItem(allGender.all_Hair[Random.Range(0, allGender.all_Hair.Count)]);
        //                     // if not human
        //                     if (race != Race.Human)
        //                     {
        //                         // select elf ear attachment
        //                         if (allGender.elf_Ear.Count != 0)
        //                             ActivateItem(allGender.elf_Ear[Random.Range(0, allGender.elf_Ear.Count)]);
        //                     }
        //                     break;
        //             }
        //             break;

        //         case Elements.No:
        //             //select head with no elements
        //             if (cog.headNoElements.Count != 0)
        //                 ActivateItem(cog.headNoElements[Random.Range(0, cog.headNoElements.Count)]);
        //             break;
        //     }

        //     // start randomization of the random characters colors
        //     RandomizeColors(skinColor);
        // }

        // void RandomizeColors(SkinColor skinColor)
        // {
        //     // set skin and hair colors based on skin color roll
        //     switch (skinColor)
        //     {
        //         case SkinColor.White:
        //             // randomize and set white skin, hair, stubble, and scar color
        //             RandomizeAndSetHairSkinColors("White", whiteSkin, whiteHair, whiteStubble, whiteScar);
        //             break;

        //         case SkinColor.Brown:
        //             // randomize and set brown skin, hair, stubble, and scar color
        //             RandomizeAndSetHairSkinColors("Brown", brownSkin, brownHair, brownStubble, brownScar);
        //             break;

        //         case SkinColor.Black:
        //             // randomize and black elf skin, hair, stubble, and scar color
        //             RandomizeAndSetHairSkinColors("Black", blackSkin, blackHair, blackStubble, blackScar);
        //             break;

        //         case SkinColor.Elf:
        //             // randomize and set elf skin, hair, stubble, and scar color
        //             RandomizeAndSetHairSkinColors("Elf", elfSkin, elfHair, elfStubble, elfScar);
        //             break;
        //     }

        //     // randomize and set primary color
        //     if (gearPrimary.Length != 0)
        //         mat.SetColor("_Color_Primary", gearPrimary[Random.Range(0, gearPrimary.Length)]);
        //     else
        //         Debug.Log("No Primary Colors Specified In The Inspector");

        //     // randomize and set secondary color
        //     if (gearSecondary.Length != 0)
        //         mat.SetColor("_Color_Secondary", gearSecondary[Random.Range(0, gearSecondary.Length)]);
        //     else
        //         Debug.Log("No Secondary Colors Specified In The Inspector");

        //     // randomize and set primary metal color
        //     if (metalPrimary.Length != 0)
        //         mat.SetColor("_Color_Metal_Primary", metalPrimary[Random.Range(0, metalPrimary.Length)]);
        //     else
        //         Debug.Log("No Primary Metal Colors Specified In The Inspector");

        //     // randomize and set secondary metal color
        //     if (metalSecondary.Length != 0)
        //         mat.SetColor("_Color_Metal_Secondary", metalSecondary[Random.Range(0, metalSecondary.Length)]);
        //     else
        //         Debug.Log("No Secondary Metal Colors Specified In The Inspector");

        //     // randomize and set primary leather color
        //     if (leatherPrimary.Length != 0)
        //         mat.SetColor("_Color_Leather_Primary", leatherPrimary[Random.Range(0, leatherPrimary.Length)]);
        //     else
        //         Debug.Log("No Primary Leather Colors Specified In The Inspector");

        //     // randomize and set secondary leather color
        //     if (leatherSecondary.Length != 0)
        //         mat.SetColor("_Color_Leather_Secondary", leatherSecondary[Random.Range(0, leatherSecondary.Length)]);
        //     else
        //         Debug.Log("No Secondary Leather Colors Specified In The Inspector");

        //     // randomize and set body art color
        //     if (bodyArt.Length != 0)
        //         mat.SetColor("_Color_BodyArt", bodyArt[Random.Range(0, bodyArt.Length)]);
        //     else
        //         Debug.Log("No Body Art Colors Specified In The Inspector");

        //     // randomize and set body art amount
        //     mat.SetFloat("_BodyArt_Amount", Random.Range(0.0f, 1.0f));
        // }

        // void RandomizeAndSetHairSkinColors(string info, Color[] skin, Color[] hair, Color stubble, Color scar)
        // {
        //     // randomize and set elf skin color
        //     if (skin.Length != 0)
        //     {
        //         mat.SetColor("_Color_Skin", skin[Random.Range(0, skin.Length)]);
        //     }
        //     else
        //     {
        //         Debug.Log("No " + info + " Skin Colors Specified In The Inspector");
        //     }

        //     // randomize and set elf hair color
        //     if (hair.Length != 0)
        //     {
        //         mat.SetColor("_Color_Hair", hair[Random.Range(0, hair.Length)]);
        //     }
        //     else
        //     {
        //         Debug.Log("No " + info + " Hair Colors Specified In The Inspector");
        //     }

        //     // set stubble color
        //     mat.SetColor("_Color_Stubble", stubble);

        //     // set scar color
        //     mat.SetColor("_Color_Scar", scar);
        // }

        // // method for rolling percentages (returns true/false)
        // bool GetPercent(int pct)
        // {
        //     bool p = false;
        //     int roll = Random.Range(0, 100);
        //     if (roll <= pct)
        //     {
        //         p = true;
        //     }
        //     return p;
        // }

        // enable game object and add it to the enabled objects list
        void ActivateItem(GameObject go)
        {
            // enable item
            go.SetActive(true);

            // add item to the enabled items list
            _enabledObjects.Add(go);
        }   

        void DeactivateItems(List<GameObject> objects)
        {
            foreach (GameObject go in objects)
            {
                go.SetActive(false);
            }
        }            

        private void PopulateDropdown(List<string> list, CarouselUIElement element)
        {
            element.ClearOptions();

            var options = element.transform.Find("Options");

            for (int i = options.childCount - 1; i >= 0; i--)
            {
                Destroy(options.GetChild(i).gameObject);
            }

            List<GameObject> optionObjects = new List<GameObject>();

            foreach (string textOption in list)
            {
                GameObject textMeshProObject = new GameObject(textOption);
                textMeshProObject.SetActive(false);

                TextMeshProUGUI textMeshProComponent = textMeshProObject.AddComponent<TextMeshProUGUI>();
                textMeshProComponent.text = textOption;

                textMeshProComponent.alignment = TextAlignmentOptions.Center;
                textMeshProComponent.font = _fontAsset;
                //textMeshProComponent.outlineWidth = 0.083f;
                textMeshProComponent.outlineColor = Color.black;
                textMeshProComponent.enableWordWrapping = false;

                textMeshProComponent.transform.SetParent(options);
                textMeshProComponent.transform.localPosition = new Vector3(0f, 0f, 0f);
                textMeshProComponent.transform.localRotation = Quaternion.identity;
                textMeshProComponent.transform.localScale = Vector3.one;

                optionObjects.Add(textMeshProObject);
            }

            element.AddOptions(optionObjects);
        }

        private void PopulateDropdown(List<Color> list, CarouselUIElement element)
        {
            element.ClearOptions();

            List<GameObject> optionObjects = new List<GameObject>();

            foreach (Color colorOption in list)
            {
                var colorString = ColorUtility.ToHtmlStringRGB(colorOption);

                GameObject textMeshProObject = new GameObject(colorString);
                textMeshProObject.SetActive(false);

                TextMeshProUGUI textMeshProComponent = textMeshProObject.AddComponent<TextMeshProUGUI>();
                textMeshProComponent.text = colorString;

                textMeshProComponent.alignment = TextAlignmentOptions.Center;
                textMeshProComponent.enabled = false;

                textMeshProComponent.transform.SetParent(element.transform.Find("Options"));
                textMeshProComponent.transform.localPosition = new Vector3(0f, 0f, 0f);
                textMeshProComponent.transform.localRotation = Quaternion.identity;
                textMeshProComponent.transform.localScale = Vector3.one;

                optionObjects.Add(textMeshProObject);
            }

            element.AddOptions(optionObjects);
        }

        private void ActivateDeactivateOnGender()
        {
            if (_currentGenderSelection == Gender.Female)
            {
                DeactivateItems(_model.male.headAllElements);
                ActivateItem(_model.female.headAllElements[0]);
                DeactivateItems(_model.male.eyebrow);
                ActivateItem(_model.female.eyebrow[0]);
                DeactivateItems(_model.male.facialHair);

                foreach(Button button in _crslFacialHair.GetComponentsInChildren<Button>())
                {
                    button.enabled = false;
                    button.transform.GetComponent<Image>().color = Color.gray;
                }
                _crslFacialHair.transform.GetComponent<Image>().color = Color.gray;
            }
            else
            {
                DeactivateItems(_model.female.headAllElements);
                ActivateItem(_model.male.headAllElements[0]);
                DeactivateItems(_model.female.eyebrow);
                ActivateItem(_model.male.eyebrow[0]);
                ActivateItem(_model.male.facialHair[0]);

                foreach (Button button in _crslFacialHair.GetComponentsInChildren<Button>())
                {
                    button.enabled = true;
                    button.transform.GetComponent<Image>().color = Color.white;
                }
                _crslFacialHair.transform.GetComponent<Image>().color = Color.white;
            }
        }

        public void UpdateCharacterModelByPreference(CustomizeOptionsEnum option, int value)
        {
            switch (option)
            {
                case CustomizeOptionsEnum.Gender:
                {
                    _currentGenderSelection = _genders.ElementAt(value);

                    ActivateDeactivateOnGender();

                    PopulateGenderDependentCarousels();
                    break;
                }
                case CustomizeOptionsEnum.Eyes:
                {
                    _model.mat.SetColor("_Color_Eyes", _model.eyes[value]);
                    break;
                }
                case CustomizeOptionsEnum.Skin:
                {
                    var color = _model.skinColor[value];

                    _model.mat.SetColor("_Color_Skin", color);

                    var darkerShade = new Color(color.r * (1 - _model.colorDarkeningFactor), color.g * (1 - _model.colorDarkeningFactor), color.b * (1 - _model.colorDarkeningFactor), color.a);

                    _model.mat.SetColor("_Color_Stubble", darkerShade);
                    _model.mat.SetColor("_Color_Scar", darkerShade);
                    break;
                }
                case CustomizeOptionsEnum.Head:
                {
                    if (_currentGenderSelection == Gender.Male)
                    {
                        DeactivateItems(_model.male.headAllElements);
                        ActivateItem(_model.male.headAllElements[value]);
                    }
                    else
                    {
                        DeactivateItems(_model.female.headAllElements);
                        ActivateItem(_model.female.headAllElements[value]);
                    }
                    break;
                }
                case CustomizeOptionsEnum.HairColor:
                {
                    _model.mat.SetColor("_Color_Hair", _model.hairColor[value]);
                    break;
                }
                case CustomizeOptionsEnum.Hair:
                {
                    DeactivateItems(_model.allGender.all_Hair);
                    ActivateItem(_model.allGender.all_Hair[value]);
                    
                    break;
                }
                case CustomizeOptionsEnum.Eyebrows:
                {
                    if (_currentGenderSelection == Gender.Male)
                    {
                        DeactivateItems(_model.male.eyebrow);
                        ActivateItem(_model.male.eyebrow[value]);
                    }
                    else
                    {
                        DeactivateItems(_model.female.eyebrow);
                        ActivateItem(_model.female.eyebrow[value]);
                    }
                    break;
                }
                case CustomizeOptionsEnum.FacialHair:
                {
                    DeactivateItems(_model.male.facialHair);
                    ActivateItem(_model.male.facialHair[value]);
                    break;
                }
                case CustomizeOptionsEnum.Ears:
                {
                    DeactivateItems(_model.allGender.elf_Ear);
                    ActivateItem(_model.allGender.elf_Ear[value]);
                    break;
                }
                case CustomizeOptionsEnum.GearMain:
                {
                    _model.mat.SetColor("_Color_Primary", _model.gearPrimary[value]);
                    break;
                }
                case CustomizeOptionsEnum.GearAlt:
                {
                    _model.mat.SetColor("_Color_Secondary", _model.gearSecondary[value]);
                    break;
                }
                case CustomizeOptionsEnum.MetalMain:
                {
                    _model.mat.SetColor("_Color_Metal_Primary", _model.metalPrimary[value]);
                    break;
                }
                case CustomizeOptionsEnum.MetalAlt:
                {
                    _model.mat.SetColor("_Color_Metal_Secondary", _model.metalSecondary[value]);
                    break;
                }
                case CustomizeOptionsEnum.LeatherMain:
                {
                    _model.mat.SetColor("_Color_Leather_Primary", _model.leatherPrimary[value]);
                    break;
                }
                case CustomizeOptionsEnum.LeatherAlt:
                {
                    _model.mat.SetColor("_Color_Leather_Secondary", _model.leatherSecondary[value]);
                    break;
                }
            }
        }        
    }
}
