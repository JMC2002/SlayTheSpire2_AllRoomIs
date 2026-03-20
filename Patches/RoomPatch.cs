using AllRoomIs.Core;
using HarmonyLib;
using MegaCrit.Sts2.Core.Map;

namespace AllRoomIs.Patches // 记得换成你的命名空间
{
    // 指定类名和私有方法的名字字符串
    [HarmonyPatch(typeof(StandardActMap), "AssignPointTypes")]
    public static class MapAllQuestionMarksPatch
    {
        // 因为是实例方法，所以用 __instance 获取当前的 StandardActMap 对象
        public static void Postfix(StandardActMap __instance)
        {
            if (__instance == null) return;

            int modifiedCount = 0;

            // 遍历当前地图的所有节点
            foreach (MapPoint point in __instance.GetAllMapPoints())
            {
                // 保留 Boss 节点、第二 Boss 节点（如果有）以及初始节点
                if (point.PointType != MapPointType.Boss &&
                    point != __instance.BossMapPoint &&
                    point != __instance.SecondBossMapPoint &&
                    point != __instance.StartingMapPoint) // 通常起点也不要改成问号
                {
                    // 强制修改类型为 问号
                    point.PointType = MapPointType.Unknown;
                    modifiedCount++;
                }
            }

            // 打印日志，确认 Hook 成功执行
            ModLogger.Info($"[AllRoomIs] 核心 Hook 触发：成功将 {modifiedCount} 个非Boss节点转化为了问号 (Unknown)！");
        }
    }
}