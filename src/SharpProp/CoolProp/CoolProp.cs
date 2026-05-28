// ReSharper disable UnusedMember.Global

namespace SharpProp;

[ExcludeFromCodeCoverage]
public static class CoolProp
{
    public static double PropsSI(
        string outputKey,
        string firstInputKey,
        double firstInputValue,
        string secondInputKey,
        double secondInputValue,
        string fluidName
    )
    {
        return CoolPropLock.Invoke(() =>
            CoolPropPInvoke.PropsSI(
                outputKey,
                firstInputKey,
                firstInputValue,
                secondInputKey,
                secondInputValue,
                fluidName
            )
        );
    }

    public static double HAPropsSI(
        string outputKey,
        string firstInputKey,
        double firstInputValue,
        string secondInputKey,
        double secondInputValue,
        string thirdInputKey,
        double thirdInputValue
    )
    {
        return CoolPropLock.Invoke(() =>
            CoolPropPInvoke.HAPropsSI(
                outputKey,
                firstInputKey,
                firstInputValue,
                secondInputKey,
                secondInputValue,
                thirdInputKey,
                thirdInputValue
            )
        );
    }

    public static string GetGlobalParamString(string paramName) =>
        CoolPropLock.Invoke(() => CoolPropPInvoke.GetGlobalParamString(paramName));

    public static string GetFluidParamString(string fluidName, string paramName) =>
        CoolPropLock.Invoke(() => CoolPropPInvoke.GetFluidParamString(fluidName, paramName));
}
