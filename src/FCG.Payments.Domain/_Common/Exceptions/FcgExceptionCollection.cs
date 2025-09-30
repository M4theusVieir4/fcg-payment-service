using System.Collections;

namespace FCG.Payments.Domain._Common.Exceptions;
public class FcgExceptionCollection : FcgException, IEnumerable<FcgException>
{
    public IReadOnlyCollection<FcgException> Exceptions { get; }

    public FcgExceptionCollection(IReadOnlyCollection<FcgException> exceptions)
        : base("One or more errors ocurred.")
    {
        Exceptions = exceptions;
    }

    public IEnumerator<FcgException> GetEnumerator()
        => Exceptions.GetEnumerator();    

    IEnumerator IEnumerable.GetEnumerator()
        => Exceptions.GetEnumerator();

}
