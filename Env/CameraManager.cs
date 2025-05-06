using DG.Tweening;
using RPG.EventSystem;
using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace RPG
{
    public enum PanDirection
    {
        UP, DOWN, LEFT, RIGHT
    }
    public class CameraManager : MonoBehaviour
    {
        [field : SerializeField] public CinemachineCamera CurrentCamera { get; private set; }
        [SerializeField] private int _activeCamPriority = 15, backCamPriority = 10;
        [SerializeField] private GameEventChannelSO _cameraChannel;

        private Vector2 _originTrackPosition;
        private CinemachinePositionComposer _positionComposer;

        private Dictionary<PanDirection, Vector2> _panDictionary = new Dictionary<PanDirection, Vector2>();
        private Tween _panTween;

        private void Awake()
        {
            _positionComposer = GetComponent<CinemachinePositionComposer>();
            _panDictionary = new Dictionary<PanDirection, Vector2>()
            {
                { PanDirection.UP, Vector2.up},
                { PanDirection.DOWN, Vector2.down},
                { PanDirection.LEFT, Vector2.left},
                { PanDirection.RIGHT, Vector2.right}
            };

            _cameraChannel.AddListner<PanEvent>(HandleCameraPanning);
            _cameraChannel.AddListner<SwapCameraEvnet>(HandleSwapCamera);
            ChangeCamera(CurrentCamera);
        }

        private void OnDestroy()
        {
            _cameraChannel.RemoveListner<PanEvent>(HandleCameraPanning);
            _cameraChannel.RemoveListner<SwapCameraEvnet>(HandleSwapCamera);
            if (_panTween != null && _panTween.IsActive())
            {
                _panTween.Kill();
            }
        }

        private void HandleSwapCamera(SwapCameraEvnet evt)
        {
            if(CurrentCamera == evt.leftCamera && evt.direction.x > 0)
            {
                ChangeCamera(evt.rightCamera);
            }
            else if(CurrentCamera == evt.rightCamera && evt.direction.x < 0)
            {
                ChangeCamera(evt.leftCamera);
            }
        }

        public void ChangeCamera(CinemachineCamera activeCamera)
        {
            CurrentCamera.Priority.Value = backCamPriority;
            Transform followTarget = CurrentCamera.Follow;
            CurrentCamera = activeCamera;
            CurrentCamera.Priority = _activeCamPriority;
            CurrentCamera.Follow = followTarget;

            _positionComposer = CurrentCamera.GetComponent<CinemachinePositionComposer>();
            _originTrackPosition = _positionComposer.TargetOffset;
        }

        private void HandleCameraPanning(PanEvent evt)
        {
            Vector3 endPos = evt.isRewindToStart ? _originTrackPosition : _panDictionary[evt.direction] * evt.distance + _originTrackPosition;

            if(_panTween != null && _panTween.IsActive())
            {
                _panTween.Kill();
            }

            _panTween = DOTween.To(() => _positionComposer.TargetOffset, value => _positionComposer.TargetOffset = value, endPos, evt.penTime);
        }

    }
}
