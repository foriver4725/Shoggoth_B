using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ex
{
    // 向き
    public enum DIR
    {
        DOWN,
        LEFT,
        UP,
        RIGHT
    }

    public static class To
    {
        // 向きを単位ベクトルに変換する。
        public static Vector3 ToVector3(this DIR dir)
        {
            Vector3 moveDir = dir switch
            {
                DIR.DOWN => Vector3.down,
                DIR.LEFT => Vector3.left,
                DIR.UP => Vector3.up,
                DIR.RIGHT => Vector3.right,
                _ => Vector3.zero
            };

            return moveDir;
        }

        // 単位ベクトルを向きに変換する。
        public static DIR ToDir(this Vector2 vec)
        {
            // 向きを判定する際に使う2つのベクトル。
            // 与えられた単位ベクトルとこれらとの内積の正負の組み合わせによって、向いている方向を判断する。
            Vector2 baseVec1 = new(-1, -1);
            Vector2 baseVec2 = new(1, -1);

            float dot1 = Vector2.Dot(baseVec1, vec);
            float dot2 = Vector2.Dot(baseVec2, vec);

            DIR dir = (dot1 >= 0, dot2 >= 0) switch
            {
                (true, true) => DIR.DOWN,
                (true, false) => DIR.LEFT,
                (false, true) => DIR.RIGHT,
                (false, false) => DIR.UP,
            };

            return dir;
        }

        // Vector3をVector2Intに変換する。zは0になる。
        public static Vector2Int ToVec2I(this Vector3 vec)
        {
            int x = (int)vec.x;
            int y = (int)vec.y;
            return new(x, y);
        }

        // Vector2IntをVector3に変換する。zを指定できる。
        public static Vector3 ToVec3(this Vector2Int vec, float z = 0)
        {
            return new(vec.x, vec.y, z);
        }
    }

    namespace AStar
    {
        public class PriorityQueue<TElement, TPriority>
        {
            private List<KeyValuePair<TElement, TPriority>> elements = new List<KeyValuePair<TElement, TPriority>>();
            private IComparer<TPriority> comparer;

            public PriorityQueue() : this(null) { }

            public PriorityQueue(IComparer<TPriority> comparer)
            {
                this.comparer = comparer ?? Comparer<TPriority>.Default;
            }

            public int Count => elements.Count;

            public void Enqueue(TElement element, TPriority priority)
            {
                elements.Add(new KeyValuePair<TElement, TPriority>(element, priority));
                elements.Sort((x, y) => comparer.Compare(x.Value, y.Value));
            }

            public TElement Dequeue()
            {
                var bestElement = elements[0];
                elements.RemoveAt(0);
                return bestElement.Key;
            }

            public bool Contains(TElement element)
            {
                return elements.Exists(e => EqualityComparer<TElement>.Default.Equals(e.Key, element));
            }
        }

        public class Pathfinding
        {
            public static List<Vector2Int> FindPath(Vector2Int start, Vector2Int goal, HashSet<Vector2Int> validPositions)
            {
                // A*アルゴリズムのためのオープンリストとクローズドリストを作成する。
                HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();
                PriorityQueue<Vector2Int, float> openSet = new PriorityQueue<Vector2Int, float>();
                openSet.Enqueue(start, 0);

                // 各ノードの親ノードとコストを追跡する辞書を作成する。
                Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
                Dictionary<Vector2Int, float> gScore = new Dictionary<Vector2Int, float>
        {
            { start, 0 }
        };
                Dictionary<Vector2Int, float> fScore = new Dictionary<Vector2Int, float>
        {
            { start, Heuristic(start, goal) }
        };

                while (openSet.Count > 0)
                {
                    Vector2Int current = openSet.Dequeue();

                    if (current == goal)
                    {
                        return ReconstructPath(cameFrom, current);
                    }

                    closedSet.Add(current);

                    foreach (Vector2Int neighbor in GetNeighbors(current))
                    {
                        if (!validPositions.Contains(neighbor) || closedSet.Contains(neighbor))
                        {
                            continue;
                        }

                        float tentativeGScore = gScore[current] + 1; // すべての移動コストは1と仮定する。

                        if (!gScore.ContainsKey(neighbor))
                        {
                            gScore[neighbor] = float.MaxValue;
                        }

                        if (tentativeGScore < gScore[neighbor])
                        {
                            cameFrom[neighbor] = current;
                            gScore[neighbor] = tentativeGScore;
                            fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, goal);

                            if (!openSet.Contains(neighbor))
                            {
                                openSet.Enqueue(neighbor, fScore[neighbor]);
                            }
                        }
                    }
                }

                // 経路が見つからなかった場合、空のリストを返す。
                return new List<Vector2Int>();
            }

            private static List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current)
            {
                List<Vector2Int> totalPath = new List<Vector2Int> { current };
                while (cameFrom.ContainsKey(current))
                {
                    current = cameFrom[current];
                    totalPath.Add(current);
                }
                totalPath.Reverse();
                return totalPath;
            }

            private static float Heuristic(Vector2Int a, Vector2Int b)
            {
                return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
            }

            private static List<Vector2Int> GetNeighbors(Vector2Int current)
            {
                List<Vector2Int> neighbors = new List<Vector2Int>
        {
            new Vector2Int(current.x + 1, current.y),
            new Vector2Int(current.x - 1, current.y),
            new Vector2Int(current.x, current.y + 1),
            new Vector2Int(current.x, current.y - 1)
        };
                return neighbors;
            }
        }
    }

    public static class PlaySound
    {
        // 与えられたAudioSourceを用いて、BGM/SEを再生する
        public static void Raise(this AudioSource source, AudioClip clip, bool sType, float volume = 1, float pitch = 1)
        {
            source.volume = volume;
            source.pitch = pitch;

            if (sType == SType.BGM)
            {
                source.clip = clip;
                source.outputAudioMixerGroup = SO_Sound.Entity.AMGroupBGM;
                source.playOnAwake = false;
                source.loop = true;
                source.Play();
            }
            else
            {
                source.outputAudioMixerGroup = SO_Sound.Entity.AMGroupSE;
                source.playOnAwake = false;
                source.loop = false;
                source.PlayOneShot(clip);
            }
        }

        // 与えられたAudioSourceを用いて、BGM/SEを再生する
        public static void Raise(this AudioSource source, AudioClip clip, bool sType, bool isLoop, float volume = 1, float pitch = 1)
        {
            source.loop = isLoop;
            source.volume = volume;
            source.pitch = pitch;

            if (sType == SType.BGM)
            {
                source.clip = clip;
                source.outputAudioMixerGroup = SO_Sound.Entity.AMGroupBGM;
                source.playOnAwake = false;
                source.Play();
            }
            else
            {
                source.outputAudioMixerGroup = SO_Sound.Entity.AMGroupSE;
                source.playOnAwake = false;
                source.PlayOneShot(clip);
            }
        }
    }

    // サウンドの種類(BGM or SE)
    public static class SType
    {
        public static bool BGM = true;
        public static bool SE = false;
    }
}
