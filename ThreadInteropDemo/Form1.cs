using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ThreadInteropDemo
{
    // Delegate signature for the native callback.
    // Must match the typedef in C++: void(__stdcall*)(int, bool, bool)
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    delegate void WorkerCallback(int count, bool running, bool aborted);

    static class NativeMethods
    {
        // Managed definitions of the native DLL exports.
        // Note: CallingConvention must match the C++ side (stdcall).
        [DllImport("NativeThreadLib.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr CreateWorker(WorkerCallback cb);

        [DllImport("NativeThreadLib.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void StartWorker(IntPtr worker);

        [DllImport("NativeThreadLib.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void StopWorker(IntPtr worker);

        [DllImport("NativeThreadLib.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void AbortWorker(IntPtr worker);

        [DllImport("NativeThreadLib.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void CleanupWorker(IntPtr worker);
    }

    public partial class Form1 : Form
    {
        private IntPtr workerPtr = IntPtr.Zero;
        private WorkerCallback callback;   // must hold reference to avoid GC

        public Form1()
        {
            InitializeComponent();
            this.Icon = new Icon("MyIcon.ico");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create worker and set callback
            callback = new WorkerCallback(OnWorkerCallback);
            workerPtr = NativeMethods.CreateWorker(callback);

            statusValueLabel.Text = "Not Running";
            countValueLabel.Text = "-1";

            stopButton.Enabled = false;
            abortButton.Enabled = false;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            stopButton.Enabled = true;
            abortButton.Enabled = true;

            NativeMethods.StartWorker(workerPtr);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            NativeMethods.StopWorker(workerPtr);
        }

        private void abortButton_Click(object sender, EventArgs e)
        {
            NativeMethods.AbortWorker(workerPtr);
        }

        // Called from the native thread.
        // We use BeginInvoke to marshal updates back to the UI thread safely.
        private void OnWorkerCallback(int count, bool running, bool aborted)
        {
            this.BeginInvoke(new Action(() =>
            {
                countValueLabel.Text = count.ToString();
                statusValueLabel.Text = running ? "Running" : "Not Running";

                if (!running)
                {
                    MessageBox.Show(
                        aborted ? "Thread Aborted" : "Thread Ended Normally",
                        "Thread Status",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    startButton.Enabled = true;
                    stopButton.Enabled = false;
                    abortButton.Enabled = false;
                }
            }));
        }

        // Ensure thread cleanup when the form closes
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (workerPtr != IntPtr.Zero)
            {
                NativeMethods.AbortWorker(workerPtr);
                NativeMethods.CleanupWorker(workerPtr);
                workerPtr = IntPtr.Zero;
            }

            base.OnFormClosing(e);
        }
    }
}