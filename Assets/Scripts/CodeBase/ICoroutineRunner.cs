using System.Collections;

using UnityEngine;

namespace OnlineFPS.CodeBase
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator enumerator);
    }
}