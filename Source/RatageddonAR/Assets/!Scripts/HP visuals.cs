using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class HpVisuals : MonoBehaviour
    {
        [SerializeField] private Image _hpBar;
        private Kitchen _kitchen;
        private Castle _castle;
        private Pan _pan;
        private int _hp;
        private float _progress;
        private void Start()
        {
            TryGetComponent<Kitchen>(out var kitchen);
            TryGetComponent<Castle>(out var castle);
            TryGetComponent<Pan>(out var pan);
            
            if (kitchen)
            {
                _kitchen = kitchen;

            }
            else if (castle)
            {
                _castle = castle;
            }
            else if (pan)
            {
                _pan = pan;
            }
        }

        private void Update()
        {
            if (_kitchen)
            {
                _hp = _kitchen._hp;
                _hpBar.fillAmount = _hp / 100f;
            }
            else if (_castle)
            {
                _hp = _castle._hp;
                _hpBar.fillAmount = _hp / 100f;
            }
            else if (_pan)
            {
                _progress = _pan._progress;
                _hpBar.fillAmount = _progress / 3f;
            }
        }
        
    }
}