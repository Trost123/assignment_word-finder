using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class ResponsiveGridLayout : MonoBehaviour
    {
        private GridLayoutGroup _gridLayoutGroup;
        private float _initialSpacingToCellSizeRatio;
        private Vector2 _lastScreenSize;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            _lastScreenSize = new Vector2(Screen.width, Screen.height);

            // Store the initial ratio of spacing to cell size
            _initialSpacingToCellSizeRatio = _gridLayoutGroup.spacing.x / _gridLayoutGroup.cellSize.x;
        }

        private void Start()
        {
            UpdateLayout();
        }

        private void Update()
        {
            var currentScreenSize = new Vector2(Screen.width, Screen.height);
            if (_lastScreenSize != currentScreenSize)
            {
                UpdateLayout();
                _lastScreenSize = currentScreenSize;
            }
        }

        private void UpdateLayout()
        {
            var parentWidth = _rectTransform.rect.width;
            var parentHeight = _rectTransform.rect.height;

            var constraintCount = _gridLayoutGroup.constraintCount;

            // The total width or height is the sum of the cell sizes and spacings
            // totalSize = cellSize * constraintCount + spacing * (constraintCount - 1)
            // Solve for cellSize to get:
            // cellSize = (totalSize - spacing * (constraintCount - 1)) / constraintCount

            // Calculate the spacing based on the original ratio and a guessed cell size
            var guessedCellSize = Mathf.Min(parentWidth, parentHeight) / constraintCount;
            var spacing = guessedCellSize * _initialSpacingToCellSizeRatio;

            // Now solve for the actual cell size using the formula above
            var cellSizeWidth = (parentWidth - spacing * (constraintCount - 1)) / constraintCount;
            var cellSizeHeight = (parentHeight - spacing * (constraintCount - 1)) / constraintCount;

            // Take the minimum of the two to keep the cells square
            var targetCellSize = Mathf.Min(cellSizeWidth, cellSizeHeight);

            // Recalculate the spacing based on the actual cell size
            var targetSpacing = targetCellSize * _initialSpacingToCellSizeRatio;

            // Update the GridLayoutGroup properties
            _gridLayoutGroup.cellSize = new Vector2(targetCellSize, targetCellSize);
            _gridLayoutGroup.spacing = new Vector2(targetSpacing, targetSpacing);
        }
    }
}