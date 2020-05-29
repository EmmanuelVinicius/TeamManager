# Backend para gerenciar times de futebol

Você é responsável por construir o backend de um novo gerenciador de times de futebol. Após fecharem o escopo do projeto, você e sua equipe definiram a `interface` que o software deve implementar. A interface é a seguinte:

``` csharp

    public interface IManageSoccerTeams 
	{
		void AddTeam(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor);

		void AddPlayer(long id, long teamId, string name, DateTime birthDate, int skillLevel, decimal salary);

		void SetCaptain(long playerId);

		long GetTeamCaptain(long teamId);

		string GetPlayerName(long playerId);

		string GetTeamName(long teamId);

		long GetHigherSalaryPlayer(long teamId);

		decimal GetPlayerSalary(long playerId);

		List<long> GetTeamPlayers(long teamId);

		long GetBestTeamPlayer(long teamId);

		long GetOlderTeamPlayer(long teamId);

		List<long> GetTeams();

		List<long> GetTopPlayers(int top);

		string GetVisitorShirtColor(long teamId, long visitorTeamId);
	}
```

Os dados devem ficar armazenados na memória.

## Requisitos de Sistema

- Parâmetros com `*` são obrigatórios. Um parâmetro obrigatório significa que ele deve ser informado na chamada do método. 
- Os parâmetros, com exceção de identificadores, são sempre íntegros e farão sentido.