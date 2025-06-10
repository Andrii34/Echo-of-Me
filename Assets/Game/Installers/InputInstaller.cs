using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
#if UNITY_EDITOR
    bool isTouchDevice = false;
#else
    bool isTouchDevice = SystemInfo.deviceType == DeviceType.Handheld;
#endif
    public override void InstallBindings()
    {
        if (isTouchDevice)
        {
            Container.BindInterfacesTo<PlayerMobileInput>().AsSingle();
        }
        else
        {
            Container.BindInterfacesTo<PlayerDesktopInput>().AsSingle();
        }
    }
}



