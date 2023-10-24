using UnityEditor;

namespace Editor
{
    [InitializeOnLoad]
    public static class RefreshAssetsOnPlay
    {
        /// <summary>
        ///     Subscribe to the playmode state change event
        /// </summary>
        static RefreshAssetsOnPlay()
        {
            EditorApplication.playModeStateChanged += PlayRefresh;
        }

        /// <summary>
        ///     Refresh assets when playmode is entered
        /// </summary>
        private static void PlayRefresh(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode) AssetDatabase.Refresh();
        }
    }
}