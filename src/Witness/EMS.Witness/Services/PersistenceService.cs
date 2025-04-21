using EMS.Judge.Application.Common.Services;
using EMS.Witness.Rpc;

namespace EMS.Witness.Services;

public class PersistenceService : IPersistenceService
{
    private readonly WitnessState _state;
    private readonly IJsonSerializationService _jsonSerializer;
	private readonly IWitnessLogger _witnessLogger;
	private readonly string _path;

    public PersistenceService(WitnessState state, IJsonSerializationService jsonSerializer, IWitnessLogger witnessLogger)
    {
        _state = state;
        _jsonSerializer = jsonSerializer;
		_witnessLogger = witnessLogger;
		_path = Path.Combine(FileSystem.Current.AppDataDirectory, "e.witness");
    }

    public async Task RestoreIfAny(int eventId, string name)
    {
        try
        {
			if (!File.Exists(_path))
			{
				return;
			}
			var contents = await File.ReadAllTextAsync(_path);
			var state = _jsonSerializer.Deserialize<WitnessState>(contents);
            // Reuse the state across event connections to be able to more easily rectify mistakes
            // like wrong event selected on Judge program until a better solution is figured out
            if (state.EventId != eventId)
            {
                state.EventId = eventId;
                state.EventName = name;
            }
            _state.Set(state);
		}
        catch (Exception ex)
        {
            await _witnessLogger.Log("RestoreState", ex);
        }
    }

    public async Task Store()
    {
        try
        {
			var serialized = this._jsonSerializer.Serialize(this._state);
			await File.WriteAllTextAsync(this._path, serialized);
		}
        catch (Exception ex)
        {
            await _witnessLogger.
                Log("StoreState", ex);
        }
    }
}

public interface IPersistenceService
{
    public Task Store();
    public Task RestoreIfAny(int eventId, string name);
}
