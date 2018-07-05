﻿using NadekoBot.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace NadekoBot.Services.Database.Repositories.Impl
{
    public class WarningsRepository : Repository<Warning>, IWarningsRepository
    {
        public WarningsRepository(DbContext context) : base(context)
        {
        }

        public Warning[] For(ulong guildId, ulong userId)
        {
            var query = _set.Where(x => x.GuildId == guildId && x.UserId == userId)
                .OrderByDescending(x => x.DateAdded);

            return query.ToArray();
        }

        public async Task ForgiveAll(ulong guildId, ulong userId, string mod)
        {
            await _set.Where(x => x.GuildId == guildId && x.UserId == userId)
                .ForEachAsync(x =>
                {
                    if (x.Forgiven != true)
                    {
                        x.Forgiven = true;
                        x.ForgivenBy = mod;
                    }
                })
                .ConfigureAwait(false);
        }
        public bool Forgive(ulong guildId, ulong userId, string mod, int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            var warn = _set.Where(x => x.GuildId == guildId && x.UserId == userId)
                .OrderByDescending(x => x.DateAdded)
                .Skip(index)
                .FirstOrDefault();

            if (warn == null || warn.Forgiven)
                return false;

            warn.Forgiven = true;
            warn.ForgivenBy = mod;
            return true;
        }
        public Warning[] GetForGuild(ulong id)
        {
            return _set.Where(x => x.GuildId == id).ToArray();
        }
    }
}
