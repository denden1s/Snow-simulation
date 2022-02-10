using Snow_simulation.Model;
using Snow_simulation.Model.Drift;
using Newtonsoft.Json;

namespace SnowInBrowser.Models;

public class Converter
{
    private List<SnowFlake> _flakes;
    private SnowDrift _drift;
    private string _flakesPath = "Flakes.json";
    private string _flakesInJson = "";
    public List<SnowFlake> Flakes
    {
        get { return _flakes; }
    }

    public Converter()
    {
        Deserialize();
    }
    public Converter( List<SnowFlake> flakes, SnowDrift drift)
    {
        _flakes = flakes;
        _drift = drift;
    }
    private void AddInfoToFile(string str, bool addOrCreate, string path)
    {
        using(StreamWriter sf = new StreamWriter(path, addOrCreate, System.Text.Encoding.Default))
        {
            sf.WriteLine(str);
        }
    }
    private void Deserialize()
    {
        string json = System.IO.File.ReadAllText(_flakesPath);
        _flakes = JsonConvert.DeserializeObject<List<SnowFlake>>(json);
    }
    public void Serialize()
    {
        while (true)
        {
            _flakesInJson = JsonConvert.SerializeObject(_flakes);
            AddInfoToFile(_flakesInJson,false, _flakesPath);
            Thread.Sleep(1000);
        }
    }

}