using PSPCoursach.Networking.Host;
using System.Numerics;

namespace PSPCoursach.Filtration
{
    public class ServerBlur : LocalBlur
    {
        public int ProcessedRowsNumber { get; protected set; }
        public List<StoredRow> StoredRows { get; protected set; } = new List<StoredRow>();

        public void AddRow(StoredRow newRow){
            lock (StoredRows) { 
                ProcessedRowsNumber++;
                StoredRows.Add(newRow);
            }
        }

        public int Id { get; }
        public ServerBlur(int id, ServerComponent network, int kernelSize) : base(kernelSize)
        {
            Id = id;
            server = network;
        }

        public virtual void SolveImagePart(byte[,,] imagePart, int width, int localHeight, int colorDepth, int localRowNumber, int globalRowNumber)
        {
            int offset = kernelSize / 2;
            byte[,] processedRow = base.ProcessOneRow(imagePart, localRowNumber, localHeight, width, colorDepth);
            AddRow(new StoredRow(globalRowNumber, processedRow));
        }

        protected ServerComponent server;
    }
}
