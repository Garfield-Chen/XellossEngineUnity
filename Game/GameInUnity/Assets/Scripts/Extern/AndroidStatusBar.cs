﻿using System;







/**
class AndroidStatusBar
{



















    /**

    // Enums
    public enum States
    {
        Unknown,
        Visible,
        VisibleOverContent,
        TranslucentOverContent,
        Hidden,
    }

    // Constants
    private const uint DEFAULT_BACKGROUND_COLOR = 0xff000000;














































#if UNITY_ANDROID
#endif
    // Properties
    private static States _statusBarState;
    //        private static States _navigationBarState;

    private static uint _statusBarColor = DEFAULT_BACKGROUND_COLOR;
    //        private static uint _navigationBarColor = DEFAULT_BACKGROUND_COLOR;

    private static bool _isStatusBarTranslucent; // Just so we know whether its translucent when hidden or not
                                                 //        private static bool _isNavigationBarTranslucent;

    private static bool _dimmed;
    // ================================================================================================================
    // INTERNAL INTERFACE ---------------------------------------------------------------------------------------------

    static AndroidStatusBar()
    {
        applyUIStates();
        applyUIColors();
    }

    private static void applyUIStates()
    {































































































































#if UNITY_ANDROID && !UNITY_EDITOR
#endif

    private static void applyUIColors()
    {





#if UNITY_ANDROID && !UNITY_EDITOR
#endif












































































































































#if UNITY_ANDROID
#endif
    // ================================================================================================================
    // ACCESSOR INTERFACE ---------------------------------------------------------------------------------------------
    /*
    public static States statusBarState
    {
        get { return _statusBarState; }
        set
        {
            if (_statusBarState != value)
            {
                _statusBarState = value;
                applyUIStates();
            }
        }
    }

    public static bool dimmed
    {
        get { return _dimmed; }
        set
        {
            if (_dimmed != value)
            {
                _dimmed = value;
                applyUIStates();
            }
        }
    }

    public static uint statusBarColor
    {
        get { return _statusBarColor; }
        set
        {
            if (_statusBarColor != value)
            {
                _statusBarColor = value;
                applyUIColors();
                applyUIStates();
            }
        }
    }






















    /*
}