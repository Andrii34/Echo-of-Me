using UnityEngine;

public interface ITouchInputService
{
    bool TryGetTouch(out Touch touch);
}