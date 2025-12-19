using GameControllers.Controllers.Properties;
using MusicSystem;
using UnityEngine;

namespace GameControllers.Controllers
{
    public class ClickDetector : IHaveUpdate
    {
        private readonly Camera _camera = Camera.main;
        private readonly int _clickableLayer = LayerMask.NameToLayer("ClickableObject");
        private readonly ParticleSystem _clickParticle;
        private readonly DestroyAbility _destroyAbility;
        private readonly MusicSwitcher _musicSwitcher;

        public ClickDetector(ParticleSystem clickParticle, DestroyAbility destroyAbility, MusicSwitcher musicSwitcher)
        {
            _clickParticle = clickParticle;
            _destroyAbility = destroyAbility;
            _musicSwitcher = musicSwitcher;
        }

        public void UpdateSystem()
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit, Mathf.Infinity, 1 << _clickableLayer) &&
                    hit.collider != null &&
                    CheckInteractType(hit))
                {
                    PlayParticle(hit.point);
                    _musicSwitcher.PlayClickGameObjectSound();
                }
            }
        }

        private bool CheckInteractType(RaycastHit hit)
        {
            if (_destroyAbility != null &&
                _destroyAbility.TryUseAbility() && 
                hit.collider.TryGetComponent(out ICanDisappearWithClick disappearingObject))
            {
                if (disappearingObject.TryReact())
                    return true;
                
                disappearingObject.DisappearWithClick();
                _destroyAbility.UseAbility();
                return true;
            }
            
            if (hit.collider.TryGetComponent(out IClickableObject clickableObject))
                return clickableObject.TryReact();
            
            return false;
        }

        private void PlayParticle(Vector3 point)
        {
            _clickParticle.transform.position = point;
            
            if (_clickParticle.isPlaying)
                _clickParticle.Clear();
            
            _clickParticle.Play();
        }
    }
}
