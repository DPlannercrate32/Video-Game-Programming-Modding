// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include <iostream>
using namespace std;

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    float* p;

    p = (float*)0x01A43210;
    *p = 1.00857f;
    p = (float*)0x01A431F4;
    *p = 1.00857f;
    p = (float*)0x01A4321C;
    *p = 2.0f;

    p = (float*)0x01A43220;
    *p = 3.0f;
    p = (float*)0x01A43214;
    *p = 0.1f;
    p = (float*)0x01A43218;
    *p = 0.14f;

    return TRUE;
}


