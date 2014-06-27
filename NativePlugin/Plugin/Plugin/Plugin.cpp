// Plugin.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

#include "stdafx.h"
#include "Plugin.h"


// これは、エクスポートされた関数の例です。
extern "C" PLUGIN_API int PluginAdd( int a, int b )
{
	return a + b;
}
