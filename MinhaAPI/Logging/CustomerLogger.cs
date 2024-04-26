
namespace MinhaAPI.Logging;

public class CustomerLogger : ILogger
{
    readonly string loggername;
    readonly CustomLoggerProviderConfiguration loggerConfig;

    public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
    {
        loggername = name;
        loggerConfig = config;
    }

    public IDisposable? BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == loggerConfig.LogLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string messagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";
        EscreverTextoNoArquivo(messagem);
    }

    private void EscreverTextoNoArquivo(string mensagem)
    {
        string caminhoArquivoLog = @"C:\Users\Guilherme Batista\Documents\logger\guilherme.txt";

        using (StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog, true))
        {
            try
            {
                streamWriter.WriteLine(mensagem);
                streamWriter.Close();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }

}
