﻿using System;

namespace Hexalyser.Models
{
    public class DesignModelService : IModelService
    {
        public void GetModel(Action<Model, Exception> callback)
        {
            // Build a model to use in blend/xaml designer
            Model model = new Model();
            Document d = new Document("C:\\Temp\\File1.bin", new byte[]
            {
                0x42, 0x53, 0x4a, 0x42, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0c, 0x00, 0x00, 0x00,
                0x50, 0x44, 0x42, 0x20, 0x76, 0x31, 0x2e, 0x30, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00,
                0x7c, 0x00, 0x00, 0x00, 0x8c, 0x00, 0x00, 0x00, 0x23, 0x50, 0x64, 0x62, 0x00, 0x00, 0x00, 0x00,
                0x08, 0x01, 0x00, 0x00, 0x40, 0x19, 0x00, 0x00, 0x23, 0x7e, 0x00, 0x00, 0x48, 0x1a, 0x00, 0x00,
                0x70, 0x01, 0x00, 0x00, 0x23, 0x53, 0x74, 0x72, 0x69, 0x6e, 0x67, 0x73, 0x00, 0x00, 0x00, 0x00,
                0xb8, 0x1b, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x23, 0x55, 0x53, 0x00, 0xbc, 0x1b, 0x00, 0x00,
                0x50, 0x00, 0x00, 0x00, 0x23, 0x47, 0x55, 0x49, 0x44, 0x00, 0x00, 0x00, 0x0c, 0x1c, 0x00, 0x00,
                0xec, 0x28, 0x00, 0x00, 0x23, 0x42, 0x6c, 0x6f, 0x62, 0x00, 0x00, 0x00, 0xec, 0x83, 0x3f, 0xa5,
                0x6c, 0xcc, 0xbb, 0x42, 0x94, 0x6d, 0xdc, 0x01, 0x5e, 0x2c, 0xfb, 0x61, 0x28, 0x1e, 0x7c, 0xc2,
                0x56, 0x00, 0x00, 0x06, 0x57, 0x1f, 0xb6, 0x1f, 0x09, 0x1f, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                0x72, 0x00, 0x00, 0x00, 0x25, 0x00, 0x00, 0x00, 0x43, 0x00, 0x00, 0x00, 0xe1, 0x00, 0x00, 0x00,
                0xe1, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0xb3, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00,
                0x92, 0x00, 0x00, 0x00, 0x2a, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00,
                0x0e, 0x00, 0x00, 0x00, 0x2b, 0x00, 0x00, 0x00, 0x51, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00,
                0x01, 0x00, 0x00, 0x00, 0x15, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                0x0f, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                0x15, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x01,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xaf, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x84, 0x00,
                0x25, 0x00, 0x00, 0x00, 0xe1, 0x00, 0x00, 0x00, 0xe2, 0x00, 0x00, 0x00, 0x7a, 0x00, 0x00, 0x00,
                0x54, 0x00, 0x00, 0x00, 0x59, 0x00, 0x00, 0x00, 0x6c, 0x00, 0x01, 0x00, 0x7a, 0x00, 0x02, 0x00,
                0x3e, 0x02, 0x01, 0x00, 0x52, 0x02, 0x02, 0x00, 0xec, 0x03, 0x01, 0x00, 0x00, 0x04, 0x02, 0x00,
                0xe4, 0x04, 0x01, 0x00, 0xf8, 0x04, 0x02, 0x00, 0x83, 0x05, 0x01, 0x00, 0x97, 0x05, 0x02, 0x00,
                0x62, 0x06, 0x01, 0x00, 0x76, 0x06, 0x02, 0x00, 0xe4, 0x06, 0x01, 0x00, 0xf8, 0x06, 0x02, 0x00,
                0x95, 0x07, 0x01, 0x00, 0xa9, 0x07, 0x02, 0x00, 0x76, 0x0d, 0x01, 0x00, 0x8a, 0x0d, 0x02, 0x00,
                0x53, 0x10, 0x01, 0x00, 0x62, 0x10, 0x02, 0x00, 0x8c, 0x10, 0x04, 0x00, 0x98, 0x10, 0x02, 0x00,
                0x1e, 0x11, 0x01, 0x00, 0x2c, 0x11, 0x02, 0x00, 0xe9, 0x11, 0x01, 0x00, 0xfa, 0x11, 0x02, 0x00,
                0x2d, 0x12, 0x04, 0x00, 0x3b, 0x12, 0x02, 0x00, 0x9b, 0x12, 0x01, 0x00, 0xa9, 0x12, 0x02, 0x00,
                0x6d, 0x13, 0x01, 0x00, 0x7e, 0x13, 0x02, 0x00, 0xaf, 0x13, 0x04, 0x00, 0xbd, 0x13, 0x02, 0x00,
                0x20, 0x14, 0x01, 0x00, 0x2e, 0x14, 0x02, 0x00, 0x85, 0x16, 0x01, 0x00, 0x93, 0x16, 0x02, 0x00,
                0xac, 0x18, 0x01, 0x00, 0xba, 0x18, 0x02, 0x00, 0x41, 0x19, 0x01, 0x00, 0x51, 0x19, 0x02, 0x00,
                0x37, 0x1a, 0x01, 0x00, 0x47, 0x1a, 0x02, 0x00, 0x9f, 0x1a, 0x01, 0x00, 0xaf, 0x1a, 0x02, 0x00,
                0x98, 0x1c, 0x01, 0x00, 0xa8, 0x1c, 0x02, 0x00, 0x59, 0x1d, 0x01, 0x00, 0x67, 0x1d, 0x02, 0x00,
                0xd1, 0x1d, 0x01, 0x00, 0xdf, 0x1d, 0x02, 0x00, 0x19, 0x20, 0x01, 0x00, 0x27, 0x20, 0x02, 0x00,
                0xa2, 0x20, 0x01, 0x00, 0xb0, 0x20, 0x02, 0x00, 0x04, 0x21, 0x01, 0x00, 0x14, 0x21, 0x02, 0x00,
                0x75, 0x21, 0x01, 0x00, 0x85, 0x21, 0x02, 0x00, 0xf2, 0x22, 0x01, 0x00, 0x02, 0x23, 0x02, 0x00,
                0x21, 0x24, 0x01, 0x00, 0x31, 0x24, 0x02, 0x00, 0x4b, 0x25, 0x01, 0x00, 0x5b, 0x25, 0x02, 0x00,
                0xa2, 0x26, 0x01, 0x00, 0xb0, 0x26, 0x02, 0x00, 0xff, 0x26, 0x01, 0x00, 0x0d, 0x27, 0x02, 0x00,
                0x71, 0x27, 0x01, 0x00, 0x7f, 0x27, 0x02, 0x00, 0xf1, 0x27, 0x01, 0x00, 0xff, 0x27, 0x02, 0x00,
                0x01, 0x00, 0x9b, 0x00, 0x01, 0x00, 0xb1, 0x00, 0x01, 0x00, 0xc4, 0x00, 0x01, 0x00, 0xd7, 0x00,
                0x01, 0x00, 0xe9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x73, 0x02, 0x02, 0x00, 0x84, 0x02,
                0x02, 0x00, 0x95, 0x02, 0x02, 0x00, 0xa6, 0x02, 0x02, 0x00, 0xb7, 0x02, 0x02, 0x00, 0xc8, 0x02,
                0x02, 0x00, 0xd9, 0x02, 0x02, 0x00, 0x06, 0x03, 0x02, 0x00, 0x57, 0x03, 0x02, 0x00, 0x7f, 0x03,
                0x02, 0x00, 0xa7, 0x03, 0x03, 0x00, 0x21, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x19, 0x05, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x2a, 0x05, 0x00, 0x00, 0x00, 0x00, 0x05, 0x00, 0xb8, 0x05,
                0x05, 0x00, 0xc9, 0x05, 0x05, 0x00, 0xda, 0x05, 0x05, 0x00, 0x02, 0x06, 0x05, 0x00, 0x37, 0x06,
                0x06, 0x00, 0x97, 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0xa8, 0x06,
                0x00, 0x00, 0x00, 0x00, 0x07, 0x00, 0x19, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x07, 0x00, 0x19, 0x07, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0xca, 0x07, 0x08, 0x00, 0xdb, 0x07,
                0x08, 0x00, 0xec, 0x07, 0x08, 0x00, 0x19, 0x08, 0x08, 0x00, 0x2a, 0x08, 0x08, 0x00, 0x3b, 0x08,
                0x08, 0x00, 0x63, 0x08, 0x08, 0x00, 0x74, 0x08, 0x08, 0x00, 0x85, 0x08, 0x08, 0x00, 0xad, 0x08,
                0x08, 0x00, 0xbe, 0x08, 0x08, 0x00, 0xcf, 0x08, 0x08, 0x00, 0xf7, 0x08, 0x08, 0x00, 0x08, 0x09,
                0x08, 0x00, 0x19, 0x09, 0x08, 0x00, 0x41, 0x09, 0x08, 0x00, 0x53, 0x09, 0x08, 0x00, 0x65, 0x09,
                0x08, 0x00, 0x8e, 0x09, 0x08, 0x00, 0xa0, 0x09, 0x08, 0x00, 0xb2, 0x09, 0x08, 0x00, 0xdb, 0x09,
                0x08, 0x00, 0xed, 0x09, 0x08, 0x00, 0xff, 0x09, 0x08, 0x00, 0x28, 0x0a, 0x08, 0x00, 0x6a, 0x0a,
                0x08, 0x00, 0xa7, 0x0a, 0x08, 0x00, 0xe9, 0x0a, 0x08, 0x00, 0x26, 0x0b, 0x08, 0x00, 0x7b, 0x0b,
                0x08, 0x00, 0xcb, 0x0b, 0x08, 0x00, 0x16, 0x0c, 0x08, 0x00, 0x66, 0x0c, 0x08, 0x00, 0xc5, 0x0c,
                0x08, 0x00, 0x24, 0x0d, 0x09, 0x00, 0xab, 0x0d, 0x09, 0x00, 0xbc, 0x0d, 0x09, 0x00, 0xcd, 0x0d,
                0x09, 0x00, 0xde, 0x0d, 0x09, 0x00, 0xef, 0x0d, 0x09, 0x00, 0x17, 0x0e, 0x09, 0x00, 0x63, 0x0e,
                0x00, 0x00, 0xad, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0c, 0x00, 0x4d, 0x11,
                0x00, 0x00, 0x50, 0x12, 0x00, 0x00, 0x00, 0x00, 0x0f, 0x00, 0xca, 0x12, 0x00, 0x00, 0xd2, 0x13,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x12, 0x00, 0x4f, 0x14, 0x12, 0x00, 0x56, 0x14,
                0x12, 0x00, 0x21, 0x15, 0x12, 0x00, 0x28, 0x15, 0x12, 0x00, 0x2f, 0x15, 0x12, 0x00, 0x36, 0x15,
                0x12, 0x00, 0x3d, 0x15, 0x12, 0x00, 0x44, 0x15, 0x12, 0x00, 0x4b, 0x15, 0x12, 0x00, 0x52, 0x15,
                0x12, 0x00, 0x6e, 0x15, 0x12, 0x00, 0x99, 0x15, 0x12, 0x00, 0x1b, 0x16, 0x12, 0x00, 0x2c, 0x16,
                0x13, 0x00, 0xb4, 0x16, 0x13, 0x00, 0xd6, 0x16, 0x13, 0x00, 0xdd, 0x16, 0x13, 0x00, 0xe4, 0x16,
                0x13, 0x00, 0xeb, 0x16, 0x13, 0x00, 0xf2, 0x16, 0x13, 0x00, 0xf9, 0x16, 0x13, 0x00, 0x00, 0x17,
                0x13, 0x00, 0x17, 0x17, 0x13, 0x00, 0x7d, 0x17, 0x13, 0x00, 0xa8, 0x17, 0x13, 0x00, 0xd1, 0x17,
                0x13, 0x00, 0x41, 0x18, 0x14, 0x00, 0xdb, 0x18, 0x14, 0x00, 0x17, 0x19, 0x15, 0x00, 0x72, 0x19,
                0x15, 0x00, 0x79, 0x19, 0x15, 0x00, 0x84, 0x19, 0x15, 0x00, 0x9f, 0x19, 0x16, 0x00, 0x72, 0x19,
                0x16, 0x00, 0x79, 0x19, 0x16, 0x00, 0x84, 0x19, 0x16, 0x00, 0x9f, 0x19, 0x17, 0x00, 0xd0, 0x1a,
                0x17, 0x00, 0xf0, 0x1a, 0x17, 0x00, 0x33, 0x1b, 0x17, 0x00, 0xe0, 0x1b, 0x18, 0x00, 0xc9, 0x1c
            });
            model.Documents.Add(d);
            model.Documents.Add(new Document("C:\\Temp\\File2.bin"));
            callback(model, null);
        }
    }
}
