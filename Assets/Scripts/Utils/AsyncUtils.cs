using System.Threading;

namespace IdleTycoon.Utils
{
    public static class AsyncUtils
    {
        public static void CancelAndCreateCts(ref CancellationTokenSource cancellationTokenSource)
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }

            cancellationTokenSource = new CancellationTokenSource();
        }

        public static void CancelAndCreateCts(ref CancellationTokenSource cancellationTokenSource, CancellationToken linkedToken)
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }

            cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(linkedToken);
        }
    }
}