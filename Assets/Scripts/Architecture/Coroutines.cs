using System.Collections;
using UnityEngine;

namespace Architecture.Routine
{
    //от этого класса нельзя наследоваться
    public sealed class Coroutines : MonoBehaviour
    {
        private static Coroutines instance
        {
            get
            {
                if(m_instance == null)
                {
                    var go = new GameObject("[COROUTINE MANAGER]");
                    m_instance = go.AddComponent<Coroutines>();
                    DontDestroyOnLoad(go);
                }
                return m_instance;
            }
        }

        private static Coroutines m_instance;

        public static Coroutine StartRoutine(IEnumerator enumerator)
        {
            return instance.StartCoroutine(enumerator);
        }

        public static void StopRoutine(Coroutine routine)
        {
            instance.StopCoroutine(routine);
        }
    }
}
