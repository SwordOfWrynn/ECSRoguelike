using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    static bool m_LocalPlayersTurn = true;

    public static void SetPlayerTurn(bool isPlayerTurn)
    {
        m_LocalPlayersTurn = isPlayerTurn;
    }

}
