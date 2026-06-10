Thermometer thermometer = new Thermometer();

thermometer.TemperatureChanged += (sender, e) => 
{
    if (e.NewTemperature > 35)
    {
        Console.WriteLine($"[ALARM]: Bahaya! Suhu terlalu panas: {e.NewTemperature}°C!");
    }
};
thermometer.TemperatureChanged += (sender, e) =>
{
    if (e.NewTemperature > 100)
    {
        Console.WriteLine("DUARRR!");
    }
};

// Simulasi perubahan suhu
thermometer.ChangeTemperature(28); // Aman, tidak membunyikan alarm
thermometer.ChangeTemperature(40); // Memicu alarm!
thermometer.ChangeTemperature(105); 


// 1. DATA EVENT(Wajib akhiran EventArgs)
public class TemperatureChangedEventArgs : EventArgs
{
    public int NewTemperature { get; }
    public TemperatureChangedEventArgs(int temp) => NewTemperature = temp;
}

// 2. PUBLISHER
public class Thermometer
{
    // Deklarasi Event Standar .NET
    public event EventHandler<TemperatureChangedEventArgs> TemperatureChanged;

    // Pelatuk Event (Wajib awalan "On", bertipe protected virtual)
    protected virtual void OnTemperatureChanged(TemperatureChangedEventArgs e)
    {
        TemperatureChanged?.Invoke(this, e); // Aman dari error null & multi-thread
    }

    public void ChangeTemperature(int currentTemp)
    {
        Console.WriteLine($"[Termometer]: Suhu bergeser ke {currentTemp}°C");
        
        // Picu pelatuk dan kirim datanya
        OnTemperatureChanged(new TemperatureChangedEventArgs(currentTemp));
    }
}


