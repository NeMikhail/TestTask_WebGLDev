using System;
using GameCoreModule;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class ButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private TMP_Text _buttonText;
        [SerializeField] private bool _hasSound;
        private AudioEventBus _audioEventBus;
    
        public Button Button => _button;
        public Image ButtonImage => _buttonImage;
        public TMP_Text ButtonText => _buttonText;

        
        [Inject]
        public void Construct(AudioEventBus audioEventBus)
        {
            _audioEventBus = audioEventBus;
        }

        private void Start()
        {
            _button.onClick.AddListener(OnButtonClick);
        }
        
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        public void OnButtonClick()
        {
            if (_hasSound)
            {
                _audioEventBus.OnPlaySound?.Invoke(SoundID.SoundClickUI);
            }
        }
    }
}
