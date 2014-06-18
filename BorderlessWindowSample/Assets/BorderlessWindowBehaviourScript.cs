using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class BorderlessWindowBehaviourScript : MonoBehaviour
{

    public string WindowName = "";

    public int X = 0;
    public int Y = 0;
    public int Width = 640;
    public int Height = 480;

	// Use this for initialization
	void Start () {
        BorderlessWindow();
	}

    private void BorderlessWindow()
    {
        // 対象のウィンドウを探す
        IntPtr hWnd = FindWindow( null, WindowName );
        if ( hWnd==IntPtr.Zero ) {
            return;
        }

        // 現在のウィンドウスタイルを取得する
        var value = GetWindowLong( hWnd, GWL_STYLE );

        // 不要なものを外す
        value &= ~(WS_BORDER|WS_DLGFRAME|WS_THICKFRAME);

        // ウィンドウスタイルを更新する
        SetWindowLong( hWnd, GWL_STYLE, value );

        // ウィンドウを移動する
        SetWindowPos( hWnd, 0, X, Y, Width, Height, SWP_SHOWWINDOW );
    }

    #region Win32
    [DllImport( "user32.dll", SetLastError=true )]
    static extern int GetWindowLong( IntPtr hWnd, int nIndex );

    [DllImport( "user32.dll" )]
    static extern int SetWindowLong( IntPtr hWnd, int nIndex, int dwNewLong );

    [DllImport( "user32.dll" )]
    static extern IntPtr FindWindow( string lpClassName, string lpWindowName );

    [DllImport("user32.dll")]
    static extern bool SetWindowPos( IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags );

    const int WS_BORDER     = 0x00800000;
    const int WS_DLGFRAME   = 0x00400000;
    const int WS_THICKFRAME = 0x00040000;

    const int GWL_STYLE = -16;

    const uint SWP_SHOWWINDOW = 0x0040;
    #endregion
}
