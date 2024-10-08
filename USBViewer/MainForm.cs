﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Globalization;

namespace USBViewer
{
    public partial class MainForm : Form
    {
        // Registry keys to scan
        private string[] _keyNames = {
                                        "Enum\\USB",
                                        "Enum\\USBSTOR",
                                        "Control\\DeviceClasses\\{53f56307-b6bf-11d0-94f2-00a0c91efb8b}",
                                        //"Control\\DeviceClasses\\{a5dcbf10-6530-11d2-901f-00c04fb951ed}",
                                    };

        private List<string> _usbKeyNames = null;
        private Dictionary<int, string> _vendorIds = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitVendorId();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Application.DoEvents();
        }

        private void tsbDetect_Click(object sender, EventArgs e)
        {
            Detect();
        }

        private void tsbQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OOPS(string errMsg)
        {
            MessageBox.Show(errMsg, "USB Storage device usage trace detection", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Initialize a list of device IDs - vendor names.
        /// </summary>
        private void InitVendorId()
        {
            string[] spliter = { "\n" };
            _vendorIds = new Dictionary<int,string>();
            foreach (string line in Properties.Resources.VendorIDs.Split(spliter, StringSplitOptions.RemoveEmptyEntries))
            {
                _vendorIds.Add(int.Parse(line.Substring(0, 4), NumberStyles.HexNumber), line.Substring(5));
            }
        }

        /// <summary>
        /// Find the vendor name by the ID value.
        /// </summary>
        /// <param name="vendorId"></param>
        /// <returns></returns>
        private string FindVendorName(string vendorId)
        {
            int vendorIdInt = int.Parse(vendorId, NumberStyles.HexNumber);
            foreach (KeyValuePair<int, string> keyValue in _vendorIds)
            {
                if (keyValue.Key == vendorIdInt)
                    return keyValue.Value;
            }

            return string.Empty;
        }

        /// <summary>
        /// Enumerate the following key values:
        /// HKEY_LOCAL_MACHINE\SYSTEM\ControlSetxxx\Enum\USB
        /// HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSetxxx\Enum\USB
        /// </summary>
        /// <param name="usbstorKey"></param>
        private void EnumerateUsb(RegistryKey usbstorKey)
        {
            Regex regx = new Regex("^\\w{8}&\\w{8}$");
            foreach (string subUsbstorKey in usbstorKey.GetSubKeyNames())
            {
                if (!regx.IsMatch(subUsbstorKey.ToUpper())) continue;

                RegistryKey tempUsbKey = usbstorKey.OpenSubKey(subUsbstorKey);
                if (null == tempUsbKey) continue;
                if (tempUsbKey.SubKeyCount == 0) continue;

                RegistryKey subTempUsbKey = tempUsbKey.OpenSubKey(tempUsbKey.GetSubKeyNames()[0]);
                if (null == subTempUsbKey) continue;
                object valueService = subTempUsbKey.GetValue("Service");
                if (null != valueService)
                {
                    //if ( !string.Equals(valueService.ToString().ToUpper(), "USBSTOR") &&
                    //    !string.Equals(valueService.ToString().ToUpper(), "WUDFRD") &&
                    //    !string.Equals(valueService.ToString().ToUpper(), "VBOXUSB"))
                    //    continue;

                    if (!_usbKeyNames.Contains(subUsbstorKey))
                    {
                        _usbKeyNames.Add(subUsbstorKey);
                        ListViewItem item = this.lvwList.Items.Add(string.Empty, 0);
                        item.SubItems.Add(subUsbstorKey);
                        item.Tag = subUsbstorKey;

                        object valueFriendlyName = subTempUsbKey.GetValue("FriendlyName");
                        if (null == valueFriendlyName)
                        {
                            string vendorId = subUsbstorKey.Substring(4, 4);
                            item.SubItems.Add(FindVendorName(vendorId));
                        }
                        else
                            item.SubItems.Add(valueFriendlyName.ToString());
                    }
                }
                subTempUsbKey.Close();
                tempUsbKey.Close();
            }
        }

        /// <summary>
        /// Enumerate the following key values:
        /// HKEY_LOCAL_MACHINE\SYSTEM\ControlSetxxx\Enum\USBSTOR
        /// HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSetxxx\Enum\USBSTOR
        /// HKEY_LOCAL_MACHINE\SYSTEM\ControlSetxxx\Control\DeviceClasses\{53f56307-b6bf-11d0-94f2-00a0c91efb8b}
        /// HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSetxxx\Control\DeviceClasses\{53f56307-b6bf-11d0-94f2-00a0c91efb8b}
        /// </summary>
        /// <param name="usbstorKey"></param>
        private void EnumerateUsbstorDeviceClasses(RegistryKey usbstorKey)
        {
            foreach (string subUsbstorKey in usbstorKey.GetSubKeyNames())
            {
                if (!_usbKeyNames.Contains(subUsbstorKey))
                {
                    _usbKeyNames.Add(subUsbstorKey);
                    ListViewItem item = this.lvwList.Items.Add(string.Empty, 0);
                    item.SubItems.Add(subUsbstorKey);
                    item.Tag = subUsbstorKey;

                    RegistryKey usbFoundKey = usbstorKey.OpenSubKey(subUsbstorKey);
                    if (null == usbFoundKey)
                        continue;

                    if (usbFoundKey.SubKeyCount > 0)
                    {
                        RegistryKey subUsbFoundKey = usbFoundKey.OpenSubKey(usbFoundKey.GetSubKeyNames()[0]);
                        if (null == subUsbFoundKey)
                            continue;

                        object value = subUsbFoundKey.GetValue("FriendlyName");
                        if (null == value)
                            item.SubItems.Add(string.Empty);
                        else
                            item.SubItems.Add(value.ToString());
                        subUsbFoundKey.Close();
                    }
                    usbFoundKey.Close();
                }
            }
        }

        /// <summary>
        /// Detect traces of USB storage device usage.
        /// </summary>
        private void Detect()
        {
            this.lvwList.Items.Clear();

            _usbKeyNames = new List<string>();

            RegistryKey hklm = Registry.LocalMachine;
            RegistryKey systemKey = hklm.OpenSubKey("SYSTEM");

            if (null == systemKey)
            {
                OOPS("Detection failed. Unable to open system registry key HKEY_LOCAL_MACHINE\\SYSTEM 。");
                return;
            }

            foreach (string sysKey in systemKey.GetSubKeyNames())
            {
                if (!sysKey.ToUpper().StartsWith("CONTROLSET") &&
                    !sysKey.ToUpper().StartsWith("CURRENTCONTROLSET"))
                    continue;

                RegistryKey subSystemKey = systemKey.OpenSubKey(sysKey);
                if (null == subSystemKey)
                    continue;

                foreach (string key in _keyNames)
                {
                    RegistryKey usbstorKey = subSystemKey.OpenSubKey(key);
                    if (null == usbstorKey)
                        continue;

                    if (string.Equals(key.ToUpper(), "ENUM\\USB"))
                    {
                        EnumerateUsb(usbstorKey);
                    }
                    else
                    {
                        EnumerateUsbstorDeviceClasses(usbstorKey);
                    }
                    usbstorKey.Close();
                }
                subSystemKey.Close();
            }
            systemKey.Close();
        }
       
    }
}
