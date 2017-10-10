using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Crimscent.SequentialGuid
{
    public static class SequentialGuid
    {
        private static readonly DateTime UnixEpochStart = new DateTime(1970, 1, 1);

        private static readonly byte[] MachineId;

        private static readonly byte[] ProcessId;

        private static int _increment;

        static SequentialGuid()
        {
            _increment = new Random().Next();

            using (var algorithm = MD5.Create())
            {
                MachineId = algorithm.ComputeHash(Encoding.UTF8.GetBytes(Environment.MachineName));
            }

            ProcessId = BitConverter.GetBytes(Process.GetCurrentProcess().Id);
        }

        public static Guid Next()
        {
            var increment = Interlocked.Increment(ref _increment);

            var timestamp = (int)DateTime.UtcNow.Subtract(UnixEpochStart).TotalSeconds;

            var guidBytes = new byte[16];

            guidBytes[10] = (byte)(timestamp >> 24);
            guidBytes[11] = (byte)(timestamp >> 16);
            guidBytes[12] = (byte)(timestamp >> 8);
            guidBytes[13] = (byte)timestamp;
            guidBytes[8] = MachineId[0];
            guidBytes[9] = MachineId[1];
            guidBytes[6] = MachineId[2];
            guidBytes[7] = MachineId[3];
            guidBytes[4] = ProcessId[0];
            guidBytes[5] = ProcessId[1];
            guidBytes[0] = (byte)(increment >> 24);
            guidBytes[1] = (byte)(increment >> 16);
            guidBytes[2] = (byte)(increment >> 8);
            guidBytes[3] = (byte)increment;

            return new Guid(guidBytes);
        }
    }
}