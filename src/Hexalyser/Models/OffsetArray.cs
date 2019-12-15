namespace Hexalyser.Models
{
    public class OffsetArray<T>
    {
        protected T[] Source;
        protected int Offset;

        public int Length { get; protected set; }

        public T this[int index]
        {
            get => Source[index + Offset];
        }

        public OffsetArray(T[] source, int offset, int length)
        {
            Source = source;
            Offset = offset;
            Length = length;
        }
    }
}
