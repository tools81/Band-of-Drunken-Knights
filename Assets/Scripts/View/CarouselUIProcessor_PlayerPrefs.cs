using UnityEngine;
using View.Customization;

namespace View
{
    public class CarouselUIProcessor_PlayerPrefs : CarouselUI_Processor_Base
    {
        [SerializeField, Tooltip("Tracker gameobject, necessary for updating and tracking.")] private PlayerCustomizationView _charCustomizationUIInstance;
        [SerializeField, Tooltip("Playerpref setting to set by this controller.")] private CustomizeOptionsEnum _customizeOptionsEnum;

        protected override void UpdateCarouselUI()
        {
            //STORES VALUES ON THIS INSTANCE OF THE SCRIPT, FIRED ONENABLE & WHENEVER CAROUSEL UPDATES
            _storedSettingsIndex = _charCustomizationUIInstance.GetValues(_customizeOptionsEnum);

            base.UpdateCarouselUI(); //NEEDED BECAUSE THIS ALLOWS UPDATING OF THE ASSOCIATED CAROUSEL ELEMENT INDEX
        }

        protected override void DetermineOutput(int input)
        {
            //THIS OUTPUTS THE NEW VALUE FROM THE CAROUSEL
            _charCustomizationUIInstance.SetValues(_customizeOptionsEnum, input);
            //Debug.Log($"Input for {this.gameObject.name} is {input}");

            //BASE VERSION NOT REFERENCED HERE AS IT ONLY CONTAINS A PRINT
        }
    }
}