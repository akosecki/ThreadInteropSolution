#pragma once
#include <thread>
#include <atomic>

// Callback signature (stdcall matches managed delegate in C#)
typedef void(__stdcall* CallbackFn)(int count, bool running, bool aborted);

extern "C" {
    __declspec(dllexport) void* __stdcall CreateWorker(CallbackFn cb);
    __declspec(dllexport) void __stdcall StartWorker(void* worker);
    __declspec(dllexport) void __stdcall StopWorker(void* worker);
    __declspec(dllexport) void __stdcall AbortWorker(void* worker);
    __declspec(dllexport) void __stdcall CleanupWorker(void* worker);
}