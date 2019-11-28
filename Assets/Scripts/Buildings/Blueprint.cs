using Entities;
using UnityEngine;

namespace Buildings
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Blueprint : MonoBehaviour
    {
        [SerializeField] private Color blueprintColor;
        [SerializeField] private Color blockedColor;
        [SerializeField] private float towerOffset;

        private SpriteRenderer _renderer;
        private LayerMask _blockedLayers;
        private LayerMask _groundLayer;
        private Collider2D _collider;
        private Player _player;
        private Transform _transform;

        public bool IsBuildable { get; private set; }

        private void Awake()
        {
            _transform = transform;
            _collider = GetComponent<Collider2D>();
            _renderer = GetComponent<SpriteRenderer>();
            _player = _transform.parent.GetComponent<Player>();
        }

        private void Start() => _renderer.color = blueprintColor;

        private void Update()
        {
            IsBuildable = !_collider.IsTouchingLayers(_blockedLayers);
            _renderer.color = IsBuildable ? blueprintColor : blockedColor;
            _transform.position =
                new Vector3(_transform.parent.position.x + towerOffset * (_player.IsFacingLeft ? -1 : 1),
                    GetGroundHeight() ?? _transform.position.y, 0);
        }

        public void Setup(LayerMask blockedLayers, LayerMask groundLayer)
        {
            _blockedLayers = blockedLayers;
            _groundLayer = groundLayer;
        }

        public void Build(Building towerPrefab)
        {
            Vector3 position = transform.position;
            float? y = GetGroundHeight();
            if (y == null)
                return;
            Instantiate(towerPrefab, new Vector3(position.x, (float) y, position.z), Quaternion.identity);
        }

        private float? GetGroundHeight()
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(_transform.position.x, _transform.parent.position.y),
                Vector2.down, 100, _groundLayer);
            if (!hit) return null;
            return hit.point.y;
        }
    }
}