using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OscCore;
using BlobHandles;
using System.IO.Ports;
using System.Threading;
using System.Linq;
public class HapticsBHaptics : MonoBehaviour
{
    [Header("Variables")]
    SerialPort puerto = new SerialPort("COM5", 9600);
    public OscReceiver vrcReceiver;
    public OscSender sender;

    [Header("Front 6 Array")]
    public bool F1;
    public bool F2;
    public bool F3;
    public bool F4;
    public bool F5;
    public bool F6;

    [Header("Back 6 Array")]
    public bool B1;
    public bool B2;
    public bool B3;
    public bool B4;
    public bool B5;
    public bool B6;

    void Awake()
    {
        #region Front Array Read Set

        //Just a whole shit ton of telling VRCOSC "Hey, listen for this!"

        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Front_3", F11Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Front_4", F12Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Front_7", F13Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Front_8", F14Read);

        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Front_1", F21Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Front_2", F22Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Front_5", F23Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Front_6", F24Read);

        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Front_11", F31Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Front_12", F32Read);

        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Front_9", F41Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Front_10", F42Read);

        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Front_15", F51Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Front_15", F52Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Front_19", F53Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Front_20", F54Read);

        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Front_13", F61Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Front_13", F62Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Front_17", F63Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Front_18", F64Read);
        #endregion

        #region Rear Array Read Set

        //Just a whole shit ton of telling VRCOSC "Hey, listen for this!"

        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Back_3", B11Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Back_4", B12Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Back_7", B13Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Back_8", B14Read);

        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Back_1", B21Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Back_2", B22Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Back_5", B23Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Back_6", B24Read);

        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Back_11", B31Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Back_12", B32Read);

        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Back_9", B41Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Back_10", B42Read);

        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Back_15", B51Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Back_15", B52Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Back_19", B53Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Back_20", B54Read);

        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Back_13", B61Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Back_13", B62Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Back_17", B63Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/bHapticsOSC_Vest_Back_18", B64Read);
        #endregion

        //Init-ing the OSC Debug menu in VRC, mainly just to make sure OSC is connected and okay.

        sender.Client.Send("/vest/F1", F1);
        sender.Client.Send("/vest/F2", F2);
        sender.Client.Send("/vest/F3", F3);
        sender.Client.Send("/vest/F4", F4);
        sender.Client.Send("/vest/F5", F5);
        sender.Client.Send("/vest/F6", F6);

        sender.Client.Send("/vest/B1", B1);
        sender.Client.Send("/vest/B2", B2);
        sender.Client.Send("/vest/B3", B3);
        sender.Client.Send("/vest/B4", B4);
        sender.Client.Send("/vest/B5", B5);
        sender.Client.Send("/vest/B6", B6);
    }

    public void Open()
    {
        //Open Serial
        puerto.Open();
    }

    public void Close()
    {
        //Close Serial
        puerto.Close();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (puerto.IsOpen)
        {
            int[] ActiveNow = new int[12]; //Create Array to be sent as Data

            //Check the state of each bool, if true, set int to 1. 
            ActiveNow[0] = Map(F1);
            ActiveNow[1] = Map(F2);
            ActiveNow[2] = Map(F3);
            ActiveNow[3] = Map(F4);
            ActiveNow[4] = Map(F5);
            ActiveNow[5] = Map(F6);
            ActiveNow[6] = Map(B1);
            ActiveNow[7] = Map(B2);
            ActiveNow[8] = Map(B3);
            ActiveNow[9] = Map(B4);
            ActiveNow[10] = Map(B5);
            ActiveNow[11] = Map(B6);

            //Convert int to bytes because serial does bytes
            byte[] sendbytes = ActiveNow.Select(x => (byte)x).ToArray();
            SendSerial(sendbytes);
        }
    }

    int Map(bool State)
    {
        if (State)
            return 1;

        return 0;
    }

    void SendSerial(byte[] sendbytes)
    {
        puerto.Write(sendbytes, 0, sendbytes.Length);

        Debug.Log($"{sendbytes[0]}, {sendbytes[1]}, {sendbytes[2]}, {sendbytes[3]}, {sendbytes[4]}, {sendbytes[5]}, {sendbytes[6]}, {sendbytes[7]}, {sendbytes[8]}, {sendbytes[9]}, {sendbytes[10]}, {sendbytes[11]}");
    }

    private void OnDisable()
    {
        Close();
    }

    #region Front Array
    void F11Read(OscMessageValues values)
    {
        F1 = ReadOSCBool(values);
        sender.Client.Send("/vest/F1", F1);
    }

    void F12Read(OscMessageValues values)
    {
        F1 = ReadOSCBool(values);
        sender.Client.Send("/vest/F1", F1);
    }

    void F13Read(OscMessageValues values)
    {
        F1 = ReadOSCBool(values);
        sender.Client.Send("/vest/F1", F1);
    }

    void F14Read(OscMessageValues values)
    {
        F1 = ReadOSCBool(values);
        sender.Client.Send("/vest/F1", F1);
    }

    void F21Read(OscMessageValues values)
    {
        F2 = ReadOSCBool(values);
        sender.Client.Send("/vest/F2", F2);
    }

    void F22Read(OscMessageValues values)
    {
        F2 = ReadOSCBool(values);
        sender.Client.Send("/vest/F2", F2);
    }

    void F23Read(OscMessageValues values)
    {
        F2 = ReadOSCBool(values);
        sender.Client.Send("/vest/F2", F2);
    }

    void F24Read(OscMessageValues values)
    {
        F2 = ReadOSCBool(values);
        sender.Client.Send("/vest/F2", F2);
    }

    void F31Read(OscMessageValues values)
    {
        F3 = ReadOSCBool(values);
        sender.Client.Send("/vest/F3", F3);
    }

    void F32Read(OscMessageValues values)
    {
        F3 = ReadOSCBool(values);
        sender.Client.Send("/vest/F3", F3);
    }

    void F41Read(OscMessageValues values)
    {
        F4 = ReadOSCBool(values);
        sender.Client.Send("/vest/F4", F4);
    }

    void F42Read(OscMessageValues values)
    {
        F4 = ReadOSCBool(values);
        sender.Client.Send("/vest/F4", F4);
    }


    void F51Read(OscMessageValues values)
    {
        F5 = ReadOSCBool(values);
        sender.Client.Send("/vest/F5", F5);
    }

    void F52Read(OscMessageValues values)
    {
        F5 = ReadOSCBool(values);
        sender.Client.Send("/vest/F5", F5);
    }

    void F53Read(OscMessageValues values)
    {
        F5 = ReadOSCBool(values);
        sender.Client.Send("/vest/F5", F5);
    }

    void F54Read(OscMessageValues values)
    {
        F5 = ReadOSCBool(values);
        sender.Client.Send("/vest/F5", F5);
    }

    void F61Read(OscMessageValues values)
    {
        F6 = ReadOSCBool(values);
        sender.Client.Send("/vest/F6", F6);
    }

    void F62Read(OscMessageValues values)
    {
        F6 = ReadOSCBool(values);
        sender.Client.Send("/vest/F6", F6);
    }

    void F63Read(OscMessageValues values)
    {
        F6 = ReadOSCBool(values);
        sender.Client.Send("/vest/F6", F6);
    }

    void F64Read(OscMessageValues values)
    {
        F6 = ReadOSCBool(values);
        sender.Client.Send("/vest/F6", F6);
    }
    #endregion

    #region Rear Array
    void B11Read(OscMessageValues values)
    {
        B1 = ReadOSCBool(values);
        sender.Client.Send("/vest/B1", B1);
    }

    void B12Read(OscMessageValues values)
    {
        B1 = ReadOSCBool(values);
        sender.Client.Send("/vest/B1", B1);
    }

    void B13Read(OscMessageValues values)
    {
        B1 = ReadOSCBool(values);
        sender.Client.Send("/vest/B1", B1);
    }

    void B14Read(OscMessageValues values)
    {
        B1 = ReadOSCBool(values);
        sender.Client.Send("/vest/B1", B1);
    }

    void B21Read(OscMessageValues values)
    {
        B2 = ReadOSCBool(values);
        sender.Client.Send("/vest/B2", B2);
    }

    void B22Read(OscMessageValues values)
    {
        B2 = ReadOSCBool(values);
        sender.Client.Send("/vest/B2", B2);
    }

    void B23Read(OscMessageValues values)
    {
        B2 = ReadOSCBool(values);
        sender.Client.Send("/vest/B2", B2);
    }

    void B24Read(OscMessageValues values)
    {
        B2 = ReadOSCBool(values);
        sender.Client.Send("/vest/B2", B2);
    }

    void B31Read(OscMessageValues values)
    {
        B3 = ReadOSCBool(values);
        sender.Client.Send("/vest/B3", B3);
    }

    void B32Read(OscMessageValues values)
    {
        B3 = ReadOSCBool(values);
        sender.Client.Send("/vest/B3", B3);
    }

    void B41Read(OscMessageValues values)
    {
        B4 = ReadOSCBool(values);
        sender.Client.Send("/vest/B4", B4);
    }

    void B42Read(OscMessageValues values)
    {
        B4 = ReadOSCBool(values);
        sender.Client.Send("/vest/B4", B4);
    }


    void B51Read(OscMessageValues values)
    {
        B5 = ReadOSCBool(values);
        sender.Client.Send("/vest/B5", B5);
    }

    void B52Read(OscMessageValues values)
    {
        B5 = ReadOSCBool(values);
        sender.Client.Send("/vest/B5", B5);
    }

    void B53Read(OscMessageValues values)
    {
        B5 = ReadOSCBool(values);
        sender.Client.Send("/vest/B5", B5);
    }

    void B54Read(OscMessageValues values)
    {
        B5 = ReadOSCBool(values);
        sender.Client.Send("/vest/B5", B5);
    }

    void B61Read(OscMessageValues values)
    {
        B6 = ReadOSCBool(values);
        sender.Client.Send("/vest/B6", B6);
    }

    void B62Read(OscMessageValues values)
    {
        B6 = ReadOSCBool(values);
        sender.Client.Send("/vest/B6", B6);
    }

    void B63Read(OscMessageValues values)
    {
        B6 = ReadOSCBool(values);
        sender.Client.Send("/vest/B6", B6);
    }

    void B64Read(OscMessageValues values)
    {
        B6 = ReadOSCBool(values);
        sender.Client.Send("/vest/B6", B6);
    }
    #endregion

    bool ReadOSCBool(OscMessageValues v)
    {
        return v.ReadBooleanElement(0);
    }
}
