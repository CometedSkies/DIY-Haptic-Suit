using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OscCore;
using BlobHandles;
using System.IO.Ports;
using System.Threading;
using System.Linq;

public class Haptics : MonoBehaviour
{
    [Header("Variables")]
    SerialPort puerto = new SerialPort("COM4", 9600);
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

        vrcReceiver.Server.TryAddMethod("/avatar/parameters/F1", F1Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/F2", F2Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/F3", F3Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/F4", F4Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/F5", F5Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/F6", F6Read);
        #endregion

        #region Rear Array Read Set

        //Just a whole shit ton of telling VRCOSC "Hey, listen for this!"

        vrcReceiver.Server.TryAddMethod("/avatar/parameters/B1", B1Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/B2", B2Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/B3", B3Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/B4", B4Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/B5", B5Read);
        vrcReceiver.Server.TryAddMethod("/avatar/parameters/B6", B6Read);
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
    void F1Read(OscMessageValues values)
    {
        F1 = ReadOSCBool(values);
        sender.Client.Send("/vest/F1", F1);
    }

    void F2Read(OscMessageValues values)
    {
        F2 = ReadOSCBool(values);
        sender.Client.Send("/vest/F2", F2);
    }

    void F3Read(OscMessageValues values)
    {
        F3 = ReadOSCBool(values);
        sender.Client.Send("/vest/F3", F3);
    }

    void F4Read(OscMessageValues values)
    {
        F4 = ReadOSCBool(values);
        sender.Client.Send("/vest/F4", F4);
    }

    void F5Read(OscMessageValues values)
    {
        F5 = ReadOSCBool(values);
        sender.Client.Send("/vest/F5", F5);
    }

    void F6Read(OscMessageValues values)
    {
        F6 = ReadOSCBool(values);
        sender.Client.Send("/vest/F6", F6);
    }
    #endregion

    #region Rear Array
    void B1Read(OscMessageValues values)
    {
        B1 = ReadOSCBool(values);
        sender.Client.Send("/vest/B1", B1);
    }

    void B2Read(OscMessageValues values)
    {
        B2 = ReadOSCBool(values);
        sender.Client.Send("/vest/B2", B2);
    }
    void B3Read(OscMessageValues values)
    {
        B3 = ReadOSCBool(values);
        sender.Client.Send("/vest/B3", B3);
    }

    void B4Read(OscMessageValues values)
    {
        B4 = ReadOSCBool(values);
        sender.Client.Send("/vest/B4", B4);
    }

    void B5Read(OscMessageValues values)
    {
        B5 = ReadOSCBool(values);
        sender.Client.Send("/vest/B5", B5);
    }

    void B6Read(OscMessageValues values)
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