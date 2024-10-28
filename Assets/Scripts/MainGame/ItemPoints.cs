using Ex;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace MainGame
{
    [Serializable]
    public sealed class ItemPoints
    {
        [SerializeField, Tooltip("B1Fの1部屋目")]
        private Transform[] itemPoints11;

        [SerializeField, Tooltip("B1Fの2部屋目")]
        private Transform[] itemPoints12;

        [SerializeField, Tooltip("B1Fの3部屋目")]
        private Transform[] itemPoints13;

        [SerializeField, Tooltip("B1Fの4部屋目")]
        private Transform[] itemPoints14;

        [SerializeField, Tooltip("B2Fの1部屋目")]
        private Transform[] itemPoints21;

        [SerializeField, Tooltip("B2Fの2部屋目")]
        private Transform[] itemPoints22;

        [SerializeField, Tooltip("B2Fの3部屋目")]
        private Transform[] itemPoints23;

        [SerializeField, Tooltip("B2Fの4部屋目")]
        private Transform[] itemPoints24;

        /// <summary>
        /// 処理コスト高め
        /// </summary>
        /// <param name="floor">B1Fなら1、B2Fなら2</param>
        /// <param name="num">いくつ取得するか</param>
        public ReadOnlyCollection<Vector3> GetRandomPosition(byte floor, byte num)
        {
            if (floor is not (1 or 2)) return null;

            List<Vector3> posList = new();
            foreach (Transform e in floor == 1 ? itemPoints11 : itemPoints21) posList.Add(e.position);
            foreach (Transform e in floor == 1 ? itemPoints12 : itemPoints22) posList.Add(e.position);
            foreach (Transform e in floor == 1 ? itemPoints13 : itemPoints23) posList.Add(e.position);
            foreach (Transform e in floor == 1 ? itemPoints14 : itemPoints24) posList.Add(e.position);

            if (!(0 < num && num < posList.Count)) return null;

            Vector3[] returnPosArray = new Vector3[num];
            for (int i = 0; i < num; i++)
            {
                int index = UnityEngine.Random.Range(0, posList.Count);
                returnPosArray[i] = posList[index];
                posList.RemoveAt(index);
            }

            return Array.AsReadOnly(returnPosArray);
        }
    }

    public static class Interact
    {
        public static bool IsInteractableAgainst(this PlayerMove playerMove, Vector3 targetPosition)
        {
            Vector2 p = playerMove.transform.position.ToVec2I();
            Vector2 t = targetPosition.ToVec2I();
            DIR d = playerMove.LookingDir;

            return d switch
            {
                DIR.UP => p.x == t.x && p.y == t.y - 1,
                DIR.DOWN => p.x == t.x && p.y == t.y + 1,
                DIR.LEFT => p.y == t.y && p.x == t.x + 1,
                DIR.RIGHT => p.y == t.y && p.x == t.x - 1,
                _ => false
            };
        }
    }
}
