using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    public enum OptionsType { Text, Color }
    public class CarouselUIElement : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        [Header("Carousel Members")]
        [SerializeField, Tooltip("Array containing gameobjects used for options.")] private List<GameObject> _optionsObjects = new List<GameObject>();

        [SerializeField, Tooltip("Button that increments index by 1.")] private GameObject _nextButton;

        [SerializeField, Tooltip("Button that decrements index by 1.")] private GameObject _prevButton;

        [Header("Settings")]
        [SerializeField, Tooltip("Time to deactivate inbetween refires.")] private float _resetDuration = 0.1f;
        [SerializeField, Tooltip("Stipulate whether the options are text or color.")]private OptionsType _optionsType;
        [SerializeField, Tooltip("If true, when the index reaches either limit the next/previous buttons are hidden.")] private bool _doesNotCycleBack = false;

        private int _currentIndex = 0;

        public int CurrentIndex
        {
            get { return _currentIndex; }
            set { _currentIndex = value; }
        }

        public delegate void InputDetected();
        public event InputDetected InputEvent = delegate { };

        private bool _isProcessing = false; //HERE TO DELAY REFIRES
        private WaitForSeconds _resetDelay; //WORKS WITH DELAY COROUTINE

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject)
            {
                float horizontalInput = Input.GetAxis("Horizontal");

                if (horizontalInput > 0)
                {
                    PressNext();
                }
                else if (horizontalInput < 0)
                {
                    PressPrevious();
                }
            }
        }

        private void Initialize()
        {            
            _resetDelay = new WaitForSeconds(_resetDuration);

            if (_optionsObjects.Count > 0) //Only update on init if populated
            {
                UpdateUI();
            }           
        }

        private void UpdateUI()
        {
            foreach (GameObject option in _optionsObjects)
            {
                option.SetActive(false); //IF ONE OF THE OPTIONS IS NULL IT WILL CREATE AN ERROR HERE
            }

            _optionsObjects[_currentIndex].SetActive(true);

            if (_optionsType == OptionsType.Color)
            {
                ColorUtility.TryParseHtmlString("#" + _optionsObjects[_currentIndex].ToString().Substring(0, 6), out Color color);
                transform.GetComponent<UnityEngine.UI.Image>().color = color;
            }

            if (_doesNotCycleBack && _currentIndex == _optionsObjects.Count - 1)
            {
                _nextButton.SetActive(false);
            }
            else
            {
                _nextButton.SetActive(true);
            }

            if (_doesNotCycleBack && _currentIndex == 0)
            {
                _prevButton.SetActive(false);
            }
            else
            {
                _prevButton.SetActive(true);
            }

        }

        /// <summary>
        /// Prevents further refires until duration ends.
        /// </summary>
        /// <returns></returns>
        private IEnumerator LockoutDelay()
        {
            _isProcessing = true; //PREVENTS BUTTON MASHING

            yield return _resetDelay;

            _isProcessing = false;

            yield break;
        }

        //METHOD ACCESSED BY NEXT BUTTON
        public void PressNext()
        {
            if (_isProcessing)
            {
                return;
            }

            if (_doesNotCycleBack && _currentIndex == _optionsObjects.Count - 1)
            {
                return;
            }

            StartCoroutine(LockoutDelay());

            if (_currentIndex < (_optionsObjects.Count - 1))
            {
                _currentIndex += 1;

                UpdateUI();
            }
            else
            {
                _currentIndex = 0;

                UpdateUI();
            }

            InputEvent?.Invoke();
        }

        //METHOD ACCESSED BY PREVIOUS BUTTON
        public void PressPrevious()
        {
            if (_isProcessing)
            {
                return;
            }

            if (_doesNotCycleBack && _currentIndex == 0)
            {
                return;
            }

            StartCoroutine(LockoutDelay());

            if (_currentIndex > 0)
            {
                _currentIndex -= 1;

                UpdateUI();
            }
            else
            {
                _currentIndex = (_optionsObjects.Count - 1);

                UpdateUI();
            }

            InputEvent?.Invoke();
        }

        /// <summary>
        /// Used by an associated processor to update the index of this carousel.
        /// </summary>
        /// <param name="input"></param>
        public void UpdateIndex(int input)
        {
            _currentIndex = input;
            UpdateUI();
        }

        public void ClearOptions()
        {
            _optionsObjects.Clear();
        }

        public void AddOptions(List<GameObject> optionObjects)
        {
            _optionsObjects.AddRange(optionObjects);
            UpdateUI();
        }

        public void OnSelect(BaseEventData eventData)
        {
            transform.Find("Button_right").GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
            transform.Find("Button_left").GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            transform.Find("Button_right").GetComponent<UnityEngine.UI.Image>().color = Color.white;
            transform.Find("Button_left").GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }
    }
}
