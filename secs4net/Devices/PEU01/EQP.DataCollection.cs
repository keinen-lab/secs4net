﻿using System;
using Cim.Eap.Tx;
using Secs4Net;
namespace Cim.Eap {
    partial class Driver {
        void EQP_DataCollection(SecsMessage msg) {
            Item dataList = msg.SecsItem.Items[2].Items[0].Items[1];
            string pjId = dataList.Items[0].GetString();
            string carrier_slot_port = (string)dataList.Items[1];
            int i = carrier_slot_port.IndexOf('.');
            byte slotNo = Convert.ToByte(carrier_slot_port.Substring(i + 1, 2));
            string carrierId = carrier_slot_port.Substring(0, i); 
            var dc = new DataCollectionReport(GetProcessJob(carrierId,slotNo));
            try {
                dc.AddWaferData("SNW0", slotNo, slotNo);
                dc.AddWaferData("PTW0", slotNo, dataList.Items[2]);
                dc.AddWaferData("TXW0", slotNo, (uint)dataList.Items[3] / 100);
                dc.AddWaferData("TIW0", slotNo, (uint)dataList.Items[4] / 100);
                dc.AddWaferData("TAW0", slotNo, (uint)dataList.Items[5] / 100);
            } finally {
                EAP.Report(dc);
            }
        }
    }
}