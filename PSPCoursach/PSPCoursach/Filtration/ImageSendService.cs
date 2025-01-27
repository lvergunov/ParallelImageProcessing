
namespace PSPCoursach.Filtration
{
    public record ImageSendService(int localRowNumber, int globalRowNumber, int localHeight, byte[] partOfImage)
    {
    }

    public record StoredRow(int rowNumber, byte[,] row) : IComparable<StoredRow>
    {
        public int CompareTo(StoredRow? obj)
        {
            if(obj is null) throw new ArgumentNullException(nameof(obj));
            else return rowNumber.CompareTo(obj.rowNumber);
        }
    }
}
