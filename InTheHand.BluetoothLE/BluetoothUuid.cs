﻿//-----------------------------------------------------------------------
// <copyright file="BluetoothUuid.cs" company="In The Hand Ltd">
//   Copyright (c) 2020 In The Hand Ltd, All rights reserved.
//   This source code is licensed under the MIT License - see License.txt
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace InTheHand.Bluetooth
{
    public static class BluetoothUuid
    {
        private static readonly Guid BluetoothBase = new Guid(0x00000000, 0x0000, 0x1000, 0x80, 0x00, 0x00, 0x80, 0x5F, 0x9B, 0x34, 0xFB);

        /// <summary>
        /// Returns the bluetooth device UUID from a short ID.
        /// </summary>
        /// <param name="shortId">The short ID.</param>
        /// <returns>Returns the UUID.</returns>
        public static Guid FromShortId(uint shortId)
        {
            byte[] guidBytes = BluetoothBase.ToByteArray();
            BitConverter.GetBytes(shortId).CopyTo(guidBytes, 0);
            return new Guid(guidBytes);
        }

        /// <summary>
        /// Attempts to get the short Bluetooth ID from a UUID.
        /// </summary>
        /// <param name="uuid">The UUID.</param>
        /// <returns>Returns the short ID.</returns>
        public static uint? TryGetShortId(Guid uuid)
        {
            var bytes = uuid.ToByteArray();
            var baseBytes = BluetoothBase.ToByteArray();
            bool match = true;
            for (int i = 4; i < 16; i++)
            {
                if (bytes[i] != baseBytes[i])
                {
                    match = false;
                    break;
                }
            }

            return match ? BitConverter.ToUInt32(bytes, 0) : (uint?)null;
        }

        public static Guid GetService(string name)
        {
            if (Guid.TryParse(name, out Guid uuid))
            {
                return uuid;
            }
            else if (uint.TryParse(name, out uint alias))
            {
                return FromShortId(alias);
            }
            else
            {
                return GenericAttributeProfile.GattServiceUuids.FromBluetoothUti(name);
            }
        }

        public static Guid GetCharacteristic(string name)
        {
            if (Guid.TryParse(name, out Guid uuid))
            {
                return uuid;
            }
            else if (uint.TryParse(name, out uint alias))
            {
                return FromShortId(alias);
            }
            else
            {
                return GenericAttributeProfile.GattCharacteristicUuids.FromBluetoothUti(name);
            }
        }

        public static Guid GetDescriptor(string name)
        {
            if (Guid.TryParse(name, out Guid uuid))
            {
                return uuid;
            }
            else if (uint.TryParse(name, out uint alias))
            {
                return FromShortId(alias);
            }
            else
            {
                return GenericAttributeProfile.GattDescriptorUuids.FromBluetoothUti(name);
            }
        }
    }
}
