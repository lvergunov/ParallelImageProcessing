using PSPCoursach.Networking;
using PSPCoursach.Networking.Host;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.AccessControl;
namespace PSPCoursach.Filtration
{
    public class ParallelBlur : LocalBlur
    {
        public static int LastProcessedRow
        {
            get => _lastProcessedRow;
            set
            {
                if (value > _lastProcessedRow)
                {
                    _lastProcessedRow = value;
                }
            }
        }
        public ParallelBlur(ServerComponent network, int kernelSize) : base(kernelSize)
        {
            server = network;
            var thisServerBlur = new ServerBlur(0, network, kernelSize);
            var serverCounter = new NetworkCounter(0, thisServerBlur);
            _allAvailableCounters.Add(0, serverCounter);
            connectedClients.Enqueue(serverCounter);

            foreach (var client in network.ActiveNodes) {
                var clientBlur = new ConnectedClientBlur(client.Id, network, kernelSize);
                var clientCounter = new NetworkCounter(client.Id, clientBlur);
                _allAvailableCounters.Add(client.Id, clientCounter);
                connectedClients.Enqueue(clientCounter);
            }

            network.onRowReceivement += HandleRowReceiving;
            network.onConnectClient += AddAvailableCounter;
            network.onDisconnectClient += RemoveAvailableCounter;
        }

        public override Image<byte> ProcessFullImage(Image<byte> image)
        {
            thisImgWidth = image.Width;
            thisImgDepth = image.ColorFormat;
            for (int row = 0; row < image.Height; row++)
            {
                int offset = kernelSize / 2;
                int localRow = offset;
                int startRow = row - offset;
                if (startRow < 0)
                {
                    startRow = 0;
                    localRow = row;
                }
                int finalRow = row + offset;
                if (finalRow >= image.Height)
                {
                    finalRow = image.Height - 1;
                }
                byte[,,] stripe = image.GetStripe(startRow, finalRow);
                NetworkCounter counter = connectedClients.Dequeue();
                counter.SendDataToProcess(stripe, localRow, row, finalRow - startRow + 1, image.Width, image.ColorFormat);
                connectedClients.Enqueue(counter);
            }
            int procRowsNumber = 0;
            do {
                procRowsNumber = _allAvailableCounters.Sum(c=> c.Value.ServerBlur.ProcessedRowsNumber);
            } while (procRowsNumber < image.Height);

            List<StoredRow> allRows = new List<StoredRow>();
            foreach (var counter in _allAvailableCounters) {
                allRows.AddRange(counter.Value.ServerBlur.StoredRows);
            }
            allRows.Sort();
            byte[,,] newBitmap = new byte[image.Height, image.Width, image.ColorFormat];
            foreach (var row in allRows) {
                for (int col = 0; col < image.Width; col++) {
                    for (int dep = 0; dep < image.ColorFormat; dep++) {
                        newBitmap[row.rowNumber, col, dep] = row.row[col, dep];
                    }
                }
            }

            return new Image<byte>(newBitmap, image.Width, image.Height, image.ColorFormat);
        }

        public void AddAvailableCounter(int fromClientId, string ipAddress)
        {
            _allAvailableCounters.Add(fromClientId, new NetworkCounter(fromClientId, new ServerBlur(fromClientId, server, kernelSize)));
        }

        public void RemoveAvailableCounter(int fromClientId)
        {
            _allAvailableCounters.Remove(fromClientId);
        }

        public void HandleRowReceiving(int userID, int rowNumber, byte[] line)
        {
            byte[,] twoDim = Image<byte>.GetTwoDimRow(line, thisImgWidth, thisImgDepth);
            _allAvailableCounters[userID].ServerBlur.AddRow(new StoredRow(rowNumber, twoDim));
        }

        protected int thisImgWidth = 0;
        protected int thisImgDepth = 0;
        protected ServerComponent server;
        private Queue<NetworkCounter> connectedClients = new Queue<NetworkCounter>();
        private Dictionary<int, NetworkCounter> _allAvailableCounters = new Dictionary<int, NetworkCounter>();
        private NetworkCounter _hostCounter;
        private static int _lastProcessedRow = 0;
    }
}
