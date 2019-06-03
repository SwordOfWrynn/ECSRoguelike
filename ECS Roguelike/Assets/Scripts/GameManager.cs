using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

public static class GameManager
{
    static bool m_LocalPlayersTurn = true;

    public static bool run;

    public static void SetPlayerTurn(bool isPlayerTurn)
    {
        m_LocalPlayersTurn = isPlayerTurn;
    }

    static void WorldControl()
    {
    }

}
