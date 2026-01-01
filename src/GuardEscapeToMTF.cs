using UnityEngine;
using Akequ.Base;

public class GuardEscapeToMTF : MonoBehaviour
{
    private void Start()
    {
        HookManager.Add(gameObject, "OnRoomEnter", OnRoomEnter);
    }

    // args[0] = Player
    // args[1] = NetRoom
    public void OnRoomEnter(object[] args)
    {
        if (args.Length < 2) return;

        Player player = args[0] as Player;
        NetRoom room = args[1] as NetRoom;

        if (player == null || room == null) return;

        // 只处理 Guard
        if (player.playerClass.GetName() != "Guard")
            return;

        // 只处理 Exit（不是 ExitB）
        if (room.roomName != "Exits")
            return;

        EscapeAndConvert(player);
    }

    private void EscapeAndConvert(Player player)
    {
        // 清空物品
        player.DropAllItems();

        string[] mtfClasses = new string[]
        {
            "MTFCadet",
            "MTFLieutenant",
            "MTFCommander"
        };

        string targetClass = mtfClasses[Random.Range(0, mtfClasses.Length)];

        // SetClass 会：
        // - 自动传送到对应出生点
        // - 自动给予该职业初始物品（由服务器配置）
        player.SetClass(targetClass);

        CustomLogger.Log(
            $"[GuardEscapeToMTF] {player.accountName} escaped and became {targetClass}"
        );
    }
}
