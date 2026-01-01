using UnityEngine;
using Mirror;
using Akequ.Base;

namespace GuardEscapePlugin.Rooms
{
    public class Exit : Room
    {
        private static readonly string[] MtfClasses =
        {
            "MTFCadet",
            "MTFCommander",
            "MTFLieutenant"
        };

        public override void OnEnter(Collider other)
        {
            if (!NetworkServer.active)
                return;

            Player player = other.GetComponent<Player>();
            if (player == null)
                return;

            // 仅 Guard 触发
            if (player.ClassName != "Guard")
                return;

            // 随机选择 MTF 职业
            string targetClass = MtfClasses[Random.Range(0, MtfClasses.Length)];

            // 清空原物品
            player.Inventory.Clear();

            // 设置职业（自动刷新 / 出生点 / 默认物品）
            player.SetClass(targetClass);
        }
    }
}
