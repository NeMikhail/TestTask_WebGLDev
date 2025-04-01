using System;
using UnityEngine;

namespace GameCoreModule
{
    public class UIEventBus
    {
        private Action<PointerCheckEventCallback> _onPointerCheck;
        public Action<string, Sprite, int> _onShowCollectPopup;
        
        public Action<PointerCheckEventCallback> OnPointerCheck
        { get => _onPointerCheck; set => _onPointerCheck = value; }
        public Action<string, Sprite, int> OnShowCollectPopup
        { get => _onShowCollectPopup; set => _onShowCollectPopup = value; }
    }
}