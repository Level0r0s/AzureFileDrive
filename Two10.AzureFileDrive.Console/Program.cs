﻿#region Copyright (c) 2014 Two10 degrees
//
// (C) Copyright 2014 Two10 degrees
//      All rights reserved.
//
// This software is provided "as is" without warranty of any kind,
// express or implied, including but not limited to warranties as to
// quality and fitness for a particular purpose. Active Web Solutions Ltd
// does not support the Software, nor does it warrant that the Software
// will meet your requirements or that the operation of the Software will
// be uninterrupted or error free or that any defects will be
// corrected. Nothing in this statement is intended to limit or exclude
// any liability for personal injury or death caused by the negligence of
// Active Web Solutions Ltd, its employees, contractors or agents.
//
#endregion

namespace Two10.AzureFileDrive.Console
{
    using Dokan;
    using System.Configuration;
    using System.Diagnostics;

    class Program
    {
        static void Main(string[] args)
        {
            DokanOptions opt = new DokanOptions();
            opt.MountPoint = ConfigurationManager.AppSettings["DriveLetter"];
            opt.DebugMode = true;
            opt.UseStdErr = true;
            opt.VolumeLabel = "AZURE";

            // in case the volume has already been mounted
            DokanNet.DokanUnmount(opt.MountPoint[0]);

            int status = DokanNet.DokanMain(opt, new AzureOperations(ConfigurationManager.AppSettings["AzureConnectionString"], "test"));
            switch (status)
            {
                case DokanNet.DOKAN_DRIVE_LETTER_ERROR:
                    Trace.WriteLine("Drive letter error");
                    break;
                case DokanNet.DOKAN_DRIVER_INSTALL_ERROR:
                    Trace.WriteLine("Driver install error");
                    break;
                case DokanNet.DOKAN_MOUNT_ERROR:
                    Trace.WriteLine("Mount error");
                    break;
                case DokanNet.DOKAN_START_ERROR:
                    Trace.WriteLine("Start error");
                    break;
                case DokanNet.DOKAN_ERROR:
                    Trace.WriteLine("Unknown error");
                    break;
                case DokanNet.DOKAN_SUCCESS:
                    Trace.WriteLine("Success");
                    break;
                default:
                    Trace.WriteLine(string.Format("Unknown status: %d", status));
                    break;

            }
        }
    }
}
