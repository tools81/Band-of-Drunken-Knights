using Presenter.Customization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View.Customization
{
    [RequireComponent(typeof(PlayerCustomizationPresenter))]
    public class PlayerCustomizationView : MonoBehaviour
    {
        private PlayerCustomizationPresenter presenter;
        
        private void Awake() 
        {
            PlayerPrefs.DeleteAll();
            presenter = GetComponent<PlayerCustomizationPresenter>();
            SetupCarousels();
        }

        private void Update()
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                float verticalInput = Input.GetAxis("Vertical");

                // Check if up arrow key or joystick up is pressed
                if (verticalInput > 0)
                {
                    SelectNextUIElement(true);
                }
                // Check if down arrow key or joystick down is pressed
                else if (verticalInput < 0)
                {
                    SelectNextUIElement(false);
                }
            }
        }

        private void SetupCarousels()
        {
            if (!PlayerPrefs.HasKey(CustomizeOptionsEnum.Gender.ToString()))
            { PlayerPrefs.SetInt(CustomizeOptionsEnum.Gender.ToString(), 0); }

            if (!PlayerPrefs.HasKey(CustomizeOptionsEnum.Eyes.ToString()))
            { PlayerPrefs.SetInt(CustomizeOptionsEnum.Eyes.ToString(), 0); }

            if (!PlayerPrefs.HasKey(CustomizeOptionsEnum.Skin.ToString()))
            { PlayerPrefs.SetInt(CustomizeOptionsEnum.Skin.ToString(), 0); }

            if (!PlayerPrefs.HasKey(CustomizeOptionsEnum.Head.ToString()))
            { PlayerPrefs.SetInt(CustomizeOptionsEnum.Head.ToString(), 0); }

            if (!PlayerPrefs.HasKey(CustomizeOptionsEnum.HairColor.ToString()))
            { PlayerPrefs.SetInt(CustomizeOptionsEnum.HairColor.ToString(), 0); }

            if (!PlayerPrefs.HasKey(CustomizeOptionsEnum.Hair.ToString()))
            { PlayerPrefs.SetInt(CustomizeOptionsEnum.Hair.ToString(), 0); }

            if (!PlayerPrefs.HasKey(CustomizeOptionsEnum.Eyebrows.ToString()))
            { PlayerPrefs.SetInt(CustomizeOptionsEnum.Eyebrows.ToString(), 0); }

            if (!PlayerPrefs.HasKey(CustomizeOptionsEnum.FacialHair.ToString()))
            { PlayerPrefs.SetInt(CustomizeOptionsEnum.FacialHair.ToString(), 0); }

            if (!PlayerPrefs.HasKey(CustomizeOptionsEnum.Ears.ToString()))
            { PlayerPrefs.SetInt(CustomizeOptionsEnum.Ears.ToString(), 0); }

            if (!PlayerPrefs.HasKey(CustomizeOptionsEnum.GearMain.ToString()))
            { PlayerPrefs.SetInt(CustomizeOptionsEnum.GearMain.ToString(), 0); }

            if (!PlayerPrefs.HasKey(CustomizeOptionsEnum.GearAlt.ToString()))
            { PlayerPrefs.SetInt(CustomizeOptionsEnum.GearAlt.ToString(), 0); }

            if (!PlayerPrefs.HasKey(CustomizeOptionsEnum.MetalMain.ToString()))
            { PlayerPrefs.SetInt(CustomizeOptionsEnum.MetalMain.ToString(), 0); }

            if (!PlayerPrefs.HasKey(CustomizeOptionsEnum.MetalAlt.ToString()))
            { PlayerPrefs.SetInt(CustomizeOptionsEnum.MetalAlt.ToString(), 0); }

            if (!PlayerPrefs.HasKey(CustomizeOptionsEnum.LeatherMain.ToString()))
            { PlayerPrefs.SetInt(CustomizeOptionsEnum.LeatherMain.ToString(), 0); }

            if (!PlayerPrefs.HasKey(CustomizeOptionsEnum.LeatherAlt.ToString()))
            { PlayerPrefs.SetInt(CustomizeOptionsEnum.LeatherAlt.ToString(), 0); }
        }

        private void SelectNextUIElement(bool isUp)
        {
            // Get the currently selected UI element
            GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
            var select = currentSelected.GetComponent<Selectable>();
            var down = currentSelected.GetComponent<Selectable>().FindSelectableOnDown();

            // Find the next UI element based on the navigation system
            GameObject nextSelected = isUp ? currentSelected.GetComponent<Selectable>().FindSelectableOnUp().gameObject :
                                             currentSelected.GetComponent<Selectable>().FindSelectableOnDown().gameObject;

            // Change the selected UI element
            if (nextSelected != null)
            {
                EventSystem.current.SetSelectedGameObject(nextSelected);
            }
        }

        public int GetValues(CustomizeOptionsEnum option)
        {
            return PlayerPrefs.GetInt(option.ToString());
        }

        public void SetValues(CustomizeOptionsEnum option, int value)
        {
            PlayerPrefs.SetInt(option.ToString(), value);

            presenter.UpdateCharacterModelByPreference(option, value);
        }        
    }
}