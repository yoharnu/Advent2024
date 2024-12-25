using System.Text;

namespace Day09;

public class DiskMap
{
    private readonly string _diskmap;
    private List<int> _compressed;
    private List<int> _decompressed;
    public DiskMap(string diskmap)
    {
        _diskmap = diskmap;
        _compressed = new List<int>(_diskmap.Length);
        foreach (char c in _diskmap)
        {
            _compressed.Add(int.Parse(c.ToString()));
        }
        _decompressed = new List<int>(_diskmap.Length);
        Decompress();
    }

    public long Checksum()
    {
        long sum = 0;
        for (int i = 0, count = _decompressed.Count; i < count; i++)
        {
            int value = _decompressed[i];
            if (value != -1)
            {
                sum += i * value;
            }
        }
        return sum;
    }

    private void Decompress()
    {
        int id = 0;
        for (int i = 0; i < _diskmap.Length; i++)
        {
            int count = int.Parse(_diskmap[i].ToString()); // Convert char to string, then parse to int
            if (i % 2 == 0) // file size
            {
                _decompressed.AddRange(Enumerable.Repeat(id, count));
                id++;
            }
            else
            {
                _decompressed.AddRange(Enumerable.Repeat(-1, count));
            }
        }
    }

    public void Display()
    {
        var sb = new StringBuilder(_decompressed.Count);
        foreach (var x in _decompressed)
        {
            sb.Append(x == -1 ? '.' : x.ToString());
        }
        Console.WriteLine(sb.ToString());
    }

    public void MoveFiles()
    {
        int fileCount = _decompressed.Count(x => x != -1);
        int lastIndex = _decompressed.Count - 1;

        for (int i = 0; i < fileCount; i++)
        {
            if (_decompressed[i] == -1)
            {
                while (_decompressed[lastIndex] == -1)
                {
                    lastIndex--;
                }
                _decompressed[i] = _decompressed[lastIndex];
                _decompressed[lastIndex] = -1;
                lastIndex--;
            }
        }
    }

    public void Defrag()
    {
        for (var lastFile = LastFileIndex(); lastFile >= 0; lastFile = LastFileIndex(lastFile - 1))
        {
            var moveTo = FindSpace(FileSize(lastFile));
            if (moveTo != -1 && moveTo < lastFile)
            {
                MoveFile(lastFile, moveTo);
            }
        }
    }

    protected void MoveFile(int fileIndex, int moveTo)
    {
        if (_decompressed[fileIndex] == -1) return;
        var fileId = _decompressed[fileIndex];
        var endIndex = EndOfFile(fileIndex);
        var fileSize = endIndex - fileIndex + 1;

        if (moveTo + fileSize > _decompressed.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(moveTo), "Move destination is out of bounds.");
        }

        for (int i = 0; i < fileSize; i++)
        {
            if (_decompressed[moveTo + i] != -1)
            {
                throw new InvalidOperationException("Destination space is not empty.");
            }

            _decompressed[moveTo + i] = fileId;
            _decompressed[fileIndex + i] = -1;
        }
    }

    protected int LastFileIndex(int startingIndex = -1)
    {
        if (startingIndex == -1)
        {
            startingIndex = _decompressed.Count - 1;
        }
        var value = _decompressed[startingIndex];
        for (int i = startingIndex; i >= 0; i--)
        {
            if (_decompressed[i] != value)
            {
                if (value == -1)
                {
                    value = _decompressed[i];
                }
                else
                {
                    return i + 1;
                }
            }
        }
        return -1;
    }

    protected int FindSpace(int size)
    {
        for (int i = 0; i < _decompressed.Count; i++)
        {
            if (_decompressed[i] == -1)
            {
                int j = i;
                for (; j < _decompressed.Count; j++)
                {
                    if (_decompressed[j] != -1)
                    {
                        break;
                    }
                }
                if (j - i >= size)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    protected int FileSize(int fileIndex)
    {
        return EndOfFile(fileIndex) - fileIndex + 1;
    }

    protected int EndOfFile(int fileIndex)
    {
        var fileId = _decompressed[fileIndex];
        int i = fileIndex + 1;
        while (i < _decompressed.Count && _decompressed[i] == fileId)
        {
            i++;
        }
        return i - 1;
    }
}
