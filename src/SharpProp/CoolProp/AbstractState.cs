// ReSharper disable BuiltInTypeReferenceStyle

namespace SharpProp;

[ExcludeFromCodeCoverage]
public class AbstractState : IDisposable
{
    private bool _disposed;
    private HandleRef _handle;

    private AbstractState(IntPtr pointer) => _handle = new HandleRef(this, pointer);

    public void Dispose()
    {
        InternalDispose();
        GC.SuppressFinalize(this);
    }

    ~AbstractState() => InternalDispose();

    private void InternalDispose()
    {
        CoolPropLock.Invoke(() =>
        {
            if (_disposed || _handle.Handle == IntPtr.Zero)
            {
                return;
            }

            AbstractStatePInvoke.Delete(_handle);
            _handle = new HandleRef(null, IntPtr.Zero);
            _disposed = true;
        });
    }

    public static AbstractState Factory(string backend, string fluidNames)
    {
        var pointer = CoolPropLock.Invoke(() => AbstractStatePInvoke.Factory(backend, fluidNames));
        return new AbstractState(pointer);
    }

    public void SetMassFractions(DoubleVector massFractions) =>
        CoolPropLock.Invoke(() =>
            AbstractStatePInvoke.SetMassFractions(_handle, massFractions.Handle)
        );

    public void SetMoleFractions(DoubleVector moleFractions) =>
        CoolPropLock.Invoke(() =>
            AbstractStatePInvoke.SetMoleFractions(_handle, moleFractions.Handle)
        );

    public void SetVolumeFractions(DoubleVector volumeFractions) =>
        CoolPropLock.Invoke(() =>
            AbstractStatePInvoke.SetVolumeFractions(_handle, volumeFractions.Handle)
        );

    public static InputPairs? GetInputPair(string inputPairName)
    {
        try
        {
            return CoolPropLock.Invoke(() =>
                (InputPairs)AbstractStatePInvoke.GetInputPairIndex(inputPairName)
            );
        }
        catch
        {
            return null;
        }
    }

    public void Update(InputPairs inputPair, double firstInput, double secondInput) =>
        CoolPropLock.Invoke(() =>
            AbstractStatePInvoke.Update(_handle, (int)inputPair, firstInput, secondInput)
        );

    public void Clear() => CoolPropLock.Invoke(() => AbstractStatePInvoke.Clear(_handle));

    public void SpecifyPhase(Phases phase) =>
        CoolPropLock.Invoke(() => AbstractStatePInvoke.SpecifyPhase(_handle, (int)phase));

    public void UnspecifyPhase() =>
        CoolPropLock.Invoke(() => AbstractStatePInvoke.UnspecifyPhase(_handle));

    public double KeyedOutput(Parameters key) =>
        CoolPropLock.Invoke(() => AbstractStatePInvoke.KeyedOutput(_handle, (int)key));
}
