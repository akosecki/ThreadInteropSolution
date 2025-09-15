#include "ThreadWorker.h"
#include <chrono>

struct Worker {
    std::thread th;
    std::atomic<bool> running{ false };
    std::atomic<bool> abort{ false };
    std::atomic<int> count{ 0 };
    CallbackFn callback;
};

void runThread(Worker* w) {
    w->running = true;
    w->count = 0;

    bool stopRequested = false;

    while (w->running && !w->abort) {
        // Sleep in slices for responsive abort (<200ms)
        for (int i = 0; i < 10; i++) {
            std::this_thread::sleep_for(std::chrono::milliseconds(100));
            if (w->abort) break;
            if (!w->running) { stopRequested = true; break; }
        }

        if (w->abort) break;

        if (stopRequested) {
            w->count++;
            w->callback(w->count, true, false);
            break;
        }

        // Regular update
        w->count++;
        w->callback(w->count, true, false);
    }

    // Final callback
    w->callback(w->count, false, w->abort);
    w->running = false;
}

extern "C" {
    void* __stdcall CreateWorker(CallbackFn cb) {
        Worker* w = new Worker();
        w->callback = cb;
        return w;
    }

    void __stdcall StartWorker(void* worker) {
        Worker* w = (Worker*)worker;
        if (!w->running) {
            w->abort = false;
            w->th = std::thread(runThread, w);
        }
    }

    void __stdcall StopWorker(void* worker) {
        Worker* w = (Worker*)worker;
        w->running = false;
        if (w->th.joinable()) w->th.join();
    }

    void __stdcall AbortWorker(void* worker) {
        Worker* w = (Worker*)worker;
        w->abort = true;
        w->running = false;
        if (w->th.joinable()) w->th.join();
    }

    void __stdcall CleanupWorker(void* worker) {
        Worker* w = (Worker*)worker;
        delete w;
    }
}