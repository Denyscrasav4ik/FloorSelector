using BepInEx;
using BepInEx.Bootstrap;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

[BepInPlugin("denyscrasav4ik.basicallyukrainian.floorselector", "Floor Selector", "1.0.0")]
[BepInDependency("denyscrasav4ik.basicallyukrainian.chalkboardbg", BepInDependency.DependencyFlags.SoftDependency)]
public class FloorSelector : BaseUnityPlugin
{
    private bool chalkboardLoaded;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        chalkboardLoaded = Chainloader.PluginInfos.ContainsKey("denyscrasav4ik.basicallyukrainian.chalkboardbg");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MainMenu")
            return;

        EnableTargetObjects();
    }

    private void EnableTargetObjects()
    {
        string[] targets = { "MainNew_2", "MainNew_3", "MainNew_4", "MainNew_5" };

        var allTransforms = Resources.FindObjectsOfTypeAll<Transform>();

        foreach (var t in allTransforms.Where(t => targets.Contains(t.name)))
        {
            Transform current = t;

            while (current.parent != null)
            {
                if (!current.gameObject.activeSelf)
                {
                    current.gameObject.SetActive(true);
                }

                current = current.parent;

                if (current.parent == null)
                    break;
            }

            if (chalkboardLoaded)
            {
                t.localPosition += new Vector3(50f, 0f, 0f);
            }
        }
    }
}
