using System.Reflection;
using UnityEditor;

public class Templet_DOTS
{
    [MenuItem("DOTS/Systems/Unmanaged System",priority =1)]
    public static void CreateUnmanagedSystem()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile($"Assets/Editor/DOTSTemplet/UnmanagedSystem.txt", "UnmanagedSystem.cs");
    }
    [MenuItem("DOTS/Systems/Managed System", priority = 2)]
    public static void CreateManagedSystem()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile($"Assets/Editor/DOTSTemplet/ManagedSystem.txt", "ManagedSystem.cs");
    }
    [MenuItem("DOTS/Components/Authoring Component", priority =3)]
    public static void CreateAuthoringComponent()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile($"Assets/Editor/DOTSTemplet/AuthoringComponent.txt", "AuthoringComponent.cs");
    }
    [MenuItem("DOTS/Components/I Component Data", priority =4)]
    public static void CreateIComponentData()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile($"Assets/Editor/DOTSTemplet/IComponentData.txt", "IComponentData.cs");
    }
}
