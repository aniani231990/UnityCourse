using System.Diagnostics;

public class TestGeneric
{
    public void Swap<T>(ref T item1, ref T item2) {
        T temp = item1;
        item1 = item2;
        item2 = temp;
    }
}