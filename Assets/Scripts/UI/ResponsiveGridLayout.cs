using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class ResponsiveGridLayout : MonoBehaviour
{
    private RectTransform rectTransform;
    private GridLayoutGroup gridLayoutGroup;
    private Vector2 lastScreenSize;
    private float initialSpacingToCellSizeRatio;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        lastScreenSize = new Vector2(Screen.width, Screen.height);

        // Store the initial ratio of spacing to cell size
        initialSpacingToCellSizeRatio = gridLayoutGroup.spacing.x / gridLayoutGroup.cellSize.x;
    }

    void Start()
    {
        UpdateLayout();
    }

    void UpdateLayout()
    {
        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        int constraintCount = gridLayoutGroup.constraintCount;

        // The total width or height is the sum of the cell sizes and spacings
        // totalSize = cellSize * constraintCount + spacing * (constraintCount - 1)
        // Solve for cellSize to get:
        // cellSize = (totalSize - spacing * (constraintCount - 1)) / constraintCount

        // Calculate the spacing based on the original ratio and a guessed cell size
        float guessedCellSize = Mathf.Min(parentWidth, parentHeight) / constraintCount;
        float spacing = guessedCellSize * initialSpacingToCellSizeRatio;

        // Now solve for the actual cell size using the formula above
        float cellSizeWidth = (parentWidth - spacing * (constraintCount - 1)) / constraintCount;
        float cellSizeHeight = (parentHeight - spacing * (constraintCount - 1)) / constraintCount;

        // Take the minimum of the two to keep the cells square
        float targetCellSize = Mathf.Min(cellSizeWidth, cellSizeHeight);

        // Recalculate the spacing based on the actual cell size
        float targetSpacing = targetCellSize * initialSpacingToCellSizeRatio;

        // Update the GridLayoutGroup properties
        gridLayoutGroup.cellSize = new Vector2(targetCellSize, targetCellSize);
        gridLayoutGroup.spacing = new Vector2(targetSpacing, targetSpacing);
    }

    void Update()
    {
        Vector2 currentScreenSize = new Vector2(Screen.width, Screen.height);
        if (lastScreenSize != currentScreenSize)
        {
            UpdateLayout();
            lastScreenSize = currentScreenSize;
        }
    }
}
