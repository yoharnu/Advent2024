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
        for (int i = 0; i < _decompressed.Count; i++)
        {
            if (_decompressed[i] == -1) continue;

            sum += i * _decompressed[i];
        }
        return sum;
    }

    private void Decompress()
    {
        int id = 0;
        for (int i = 0; i < _diskmap.Length; i++)
        {
            if (i % 2 == 0) // file size
            {
                for (int j = 0; j < int.Parse(_diskmap[i].ToString()); j++)
                {
                    _decompressed.Add(id);
                }
                id++;
            }
            else
            {
                for (int j = 0; j < int.Parse(_diskmap[i].ToString()); j++)
                {
                    _decompressed.Add(-1);
                }
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
}
