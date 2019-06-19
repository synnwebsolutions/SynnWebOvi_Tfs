using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace XmusicCore
{
    public static class MusicManipulator
    {
        public static void TrimMp3(MusicItem sourceMp3, TimeSpan? begin, TimeSpan? end)
        {
            var outputPath = sourceMp3.FullFileName.Replace(Path.GetFileNameWithoutExtension(sourceMp3.FullFileName),$"{Path.GetFileNameWithoutExtension(sourceMp3.FullFileName)}_trimed");
            using (var reader = new Mp3FileReader(sourceMp3.FullFileName))
            using (var writer = File.Create(outputPath))
            {
                Mp3Frame frame;
                while ((frame = reader.ReadNextFrame()) != null)
                    if (reader.CurrentTime >= begin || !begin.HasValue)
                    {
                        if (reader.CurrentTime <= end || !end.HasValue)
                            writer.Write(frame.RawData, 0, frame.RawData.Length);
                        else break;
                    }
            }
            sourceMp3.FullFileName = outputPath;
        }
    }
}
