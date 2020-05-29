using System;
using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Exceptions;

namespace Codenation.Challenge
{
    public class SoccerTeamsManager : IManageSoccerTeams
    {
        private List<Team> team;
        private List<Player> player;
        public SoccerTeamsManager()
        {
            team = new List<Team>();
            player = new List<Player>();
        }

        public void AddTeam(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {
            if (team.Any(x => x.Id == id))
                throw new UniqueIdentifierException();

            team.Add(new Team()
            {
                Id = id,
                Name = name,
                CreateDate = createDate,
                MainShirtColor = mainShirtColor,
                SecondaryShirtColor = secondaryShirtColor
            });
        }

        public void AddPlayer(long id, long teamId, string name, DateTime birthDate, int skillLevel, decimal salary)
        {
            if (player.Any(x => x.Id == id))
                throw new UniqueIdentifierException();

            player.Add(new Player()
            {
                Id = id,
                TeamId = teamId,
                Name = name,
                BirthDate = birthDate,
                SkillLevel = skillLevel,
                Salary = salary
            });
        }

        public void SetCaptain(long playerId)
        {
            if (!player.Any(plr => plr.Id == playerId))
                throw new PlayerNotFoundException();

            var p = player.Find(plr => playerId == plr.Id);
            var cap = team.Find(tm => tm.Id == p.TeamId);
            cap.Captain = p;
        }

        public long GetTeamCaptain(long teamId)
        {
            var cap = team.Find(tm => tm.Id == teamId);
            if (cap == null)
                throw new TeamNotFoundException();

            var result = cap.Captain != null ?
                cap.Captain.Id :
                throw new CaptainNotFoundException();
            return result;
        }

        public string GetPlayerName(long playerId)
        {
            var p = player.Find(plr => playerId == plr.Id);
            if (p == null)
                throw new PlayerNotFoundException();

            return p.Name;
        }

        public string GetTeamName(long teamId)
        {
            var t = team.Find(tm => tm.Id == teamId);
            if (t == null)
                throw new TeamNotFoundException();

            return t.Name;
        }

        public List<long> GetTeamPlayers(long teamId)
        {
            if (!team.Any(t => t.Id == teamId))
                throw new TeamNotFoundException();

            var p = player.FindAll(plr => plr.TeamId == teamId);
            var result = p.Select(x => x.Id)
                .OrderBy(x => x)
                .ToList();
            return result;
        }

        public long GetBestTeamPlayer(long teamId)
        {
            if (!team.Any(tm => tm.Id == teamId))
                throw new TeamNotFoundException();

            var result = player.OrderByDescending(sk => sk.SkillLevel)
            .ThenBy(x => x.Id)
            .FirstOrDefault()
            .Id;
            return result;
        }

        public long GetOlderTeamPlayer(long teamId)
        {
            if (!team.Any(tm => tm.Id == teamId))
                throw new TeamNotFoundException();

            var result = player.OrderBy(ag => ag.BirthDate).FirstOrDefault().Id;
            return result;
        }

        public List<long> GetTeams()
        {
            return team.Select(x => x.Id)
                .OrderBy(y => y)
                .ToList();
        }

        public long GetHigherSalaryPlayer(long teamId)
        {
            if (!team.Any(tm => tm.Id == teamId))
                throw new TeamNotFoundException();

            var result = player.OrderByDescending(sl => sl.Salary)
                .ThenBy(x => x.Id)
                .FirstOrDefault()
                .Id;
            return result;
        }

        public decimal GetPlayerSalary(long playerId)
        {
            var p = player.Find(plr => playerId == plr.Id);
            if (p == null)
                throw new PlayerNotFoundException();

            return p.Salary;
        }

        public List<long> GetTopPlayers(int top)
        {
            return player.OrderByDescending(sk => sk.SkillLevel)
                .ThenBy(x => x.Id)
                .Take(top)
                .Select(x => x.Id)
                .ToList();
        }

        public string GetVisitorShirtColor(long teamId, long visitorTeamId)
        {
            var home = team.Find(x => x.Id == teamId);
            var visitor = team.Find(x => x.Id == visitorTeamId);
            if (home == null || visitor == null)
                throw new TeamNotFoundException();

            var result = (home.MainShirtColor == visitor.MainShirtColor) ?
                visitor.SecondaryShirtColor :
                visitor.MainShirtColor;

            return result;

        }

    }
}
