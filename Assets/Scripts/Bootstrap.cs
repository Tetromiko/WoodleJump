using System.Threading.Tasks;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    protected async void Awake()
    {
        await Initialize();
    }

    protected virtual async Task Initialize()
    {
        await Task.CompletedTask;
    }
}