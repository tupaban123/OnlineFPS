using Mirror;
using UnityEngine;

namespace OnlineFPS.GunBase
{
    public class LaserView : GunView
    {
        [Header("Component")]
        [SerializeField] private LineRenderer _laserLineRenderer;

        [Header("Colors")]
        [SerializeField] private Color playerColor;
        [SerializeField] private Color enemyColor;

        protected override void Update()
        {
            if (_isFireing)
                CreateLaser();
            else
                _laserLineRenderer.positionCount = 0;

            base.Update();
        }

        [Command]
        public override void CmdFire()
        {
            base.CmdFire();
        }

        private void CreateLaser()
        {
            var laserPos = _laserSpawnpoint.position;
            var laserLookDirection = _laserSpawnpoint.forward;

            Vector3 hitPoint = laserPos + laserLookDirection * 1000;

            if (Physics.Raycast(laserPos, laserLookDirection, out RaycastHit hit))
                hitPoint = hit.point;

            var colorToSet = isLocalPlayer ? playerColor : enemyColor;

            _laserLineRenderer.startColor = colorToSet;
            _laserLineRenderer.endColor = colorToSet;
            _laserLineRenderer.material.color = colorToSet;

            _laserLineRenderer.positionCount = 2;

            _laserLineRenderer.SetPosition(0, _laserSpawnpoint.position);
            _laserLineRenderer.SetPosition(1, hitPoint);
        }
    }
}