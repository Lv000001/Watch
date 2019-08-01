       /// <summary>
        /// 解压
        /// </summary>
        /// <param name="fileName"></param>
        private void DoUnZip(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            string folder = fileInfo.DirectoryName + "/" + fileInfo.Name.Replace(fileInfo.Extension, "") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "/";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            ZipHelper zipHelper = new ZipHelper();
            try
            {
                zipHelper.UnZip(fileName, folder);
                string resFile = folder + "com.huawei.watchface";
                string destFolder = folder + "watchface/res/";
                if (!Directory.Exists(destFolder))
                {
                    Directory.CreateDirectory(destFolder);
                }
                UnZipImage(resFile, destFolder);
                MessageBox.Show("解压成功");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        /// <summary>
        /// 解压图片
        /// </summary>
        /// <param name="resFile"></param>
        /// <param name="destFolder"></param>
        private void UnZipImage(string resFile, string destFolder)
        {
            using (FileStream file = new FileStream(resFile, FileMode.Open))
            {
                byte[] temp = new byte[4];
                int byteCount = file.Read(temp, 0, 4);
                int width = 0, height = 0, i = 0, index = 1;
                byte[] rawValues = new byte[4];
                bool isStart = false;
                while (byteCount > 0)
                {
                    if (isStart)
                    {
                        if (temp[0] == 0x45 && temp[1] == 0x23 && temp[2] == 0x88 && temp[3] == 0x88)
                        {
                            if (i > 0)
                            {
                                ByteToBitmap(rawValues, width, height).Save(destFolder + string.Format("A100_{0:D3}.png", index++), ImageFormat.Png);
                            }
                            file.Read(temp, 0, 4);
                            width = BitConverter.ToInt16(temp, 0);
                            height = BitConverter.ToInt16(temp, 2);
                            rawValues = new byte[width * height * 4];
                            i = 0;
                        }
                        else if (temp[0] == 0x89 && temp[1] == 0x67 && temp[2] == 0x45 && temp[3] == 0x23)
                        {
                            byte[] copyTemp = new byte[4];
                            byteCount = file.Read(copyTemp, 0, 4);
                            byteCount = file.Read(temp, 0, 4);
                            int count = BitConverter.ToInt32(temp, 0);
                            for (int j = 0; j < count; j++)
                            {
                                Buffer.BlockCopy(copyTemp, 0, rawValues, i, 4);
                                i += 4;
                            }
                        }
                        else
                        {
                            Buffer.BlockCopy(temp, 0, rawValues, i, 4);
                            i += 4;
                        }
                        byteCount = file.Read(temp, 0, 4);
                    }
                    else
                    {
                        if (temp[0] == 0x45 && temp[1] == 0x23 && temp[2] == 0x88 && temp[3] == 0x88)
                        {
                            isStart = true;
                            file.Read(temp, 0, 4);
                            width = BitConverter.ToInt16(temp, 0);
                            height = BitConverter.ToInt16(temp, 2);
                            rawValues = new byte[width * height * 4];
                            i = 0;
                            byteCount = file.Read(temp, 0, 4);
                        }
                        else
                        {
                            file.Seek(-3, SeekOrigin.Current);
                            byteCount = file.Read(temp, 0, 4);
                        }                       
                    }
                }

                // 最后一个图片
                if (isStart && i > 0)
                {
                    ByteToBitmap(rawValues, width, height).Save(destFolder + string.Format("A100_{0:D3}.png", index++), ImageFormat.Png);
                }
            }
        }
        
        
          /// <summary>
        /// 将一个字节数组转换为位图
        /// </summary>
        /// <param name="rawValues"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Bitmap ByteToBitmap(byte[] rawValues, int width, int height)
        {
            // 申请目标位图的变量，并将其内存区域锁定
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height),
               ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            IntPtr iptr = bmpData.Scan0;  // 获取bmpData的内存起始位置
            System.Runtime.InteropServices.Marshal.Copy(rawValues, 0, iptr, rawValues.Length);
            bmp.UnlockBits(bmpData);  // 解锁内存区域
            return bmp;
        }
